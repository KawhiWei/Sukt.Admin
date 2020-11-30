using System;

namespace Sukt.Core.IdentityServer4Store
{
    public class AccountOptions
    {
        public static bool AllowLocalLogin = true;
        public static bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        public static bool ShowLogoutPrompt = true;
        public static bool AutomaticRedirectAfterSignOut = true;

        public static string InvalidCredentialsErrorMessage = "账户名或密码错误!";
    }
}
