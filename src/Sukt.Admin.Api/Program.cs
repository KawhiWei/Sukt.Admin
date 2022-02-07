using Serilog;
using Serilog.Events;
using Sukt.Admin.Api;

var builder = WebApplication.CreateBuilder(args);




//����SeriLog����
builder.Host.UseSerilog((webHost, logconfiguration) =>
{
    //�õ������ļ�
    var serilog = webHost.Configuration.GetSection("Serilog");
    //��С����
    var minimumLevel = serilog["MinimumLevel:Default"];
    //��־�¼�����
    var logEventLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), minimumLevel);

    logconfiguration.ReadFrom.Configuration(webHost.Configuration.GetSection("Serilog")).Enrich.FromLogContext().WriteTo.Console(logEventLevel);

    logconfiguration.WriteTo.Map(le => MapData(le), (key, log) => log.Async(o => o.File(Path.Combine("logs", @$"{key.time:yyyy-MM-dd}\{key.level.ToString().ToLower()}.txt"), logEventLevel)));

    (DateTime time, LogEventLevel level) MapData(LogEvent logEvent)
    {

        return (new DateTime(logEvent.Timestamp.Year, logEvent.Timestamp.Month, logEvent.Timestamp.Day, logEvent.Timestamp.Hour, logEvent.Timestamp.Minute, logEvent.Timestamp.Second), logEvent.Level);
    }

})
    .ConfigureLogging((hostingContext, builder) =>
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(LogLevel.Information);
        builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        builder.AddConsole();
        builder.AddDebug();
    });





// Add services to the container.

builder.Services.AddApplication<SuktAppWebModule>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.InitializeApplication();

app.Run();