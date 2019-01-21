using System;

namespace CoreData.Common.HostEnvironment
{
    public static class AppRegistration
    {
        static AppRegistration()
        {
            AppContext.TryGetSwitch("Switch.CoreData.SkipAppRegistration", out var skip);
            Skip = skip;
            Approved = Skip || Register();
        }

        public static bool Skip;
        public static bool Approved;

        private static bool Register()
        {
            return false;
        }
    }
}
