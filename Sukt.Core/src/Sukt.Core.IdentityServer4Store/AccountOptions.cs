using System;

namespace Sukt.Core.IdentityServer4Store
{
    public class AccountOptions
    {
        public static bool AllowLocalLogin = true;
        public static bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        public static bool ShowLogoutPrompt = false;
        public static bool AutomaticRedirectAfterSignOut = true;//是否自动回调到退出登录站点

        public static string InvalidCredentialsErrorMessage = "账户名或密码错误!";
    }
}
