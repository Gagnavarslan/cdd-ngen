using CoreData.Common.Extensions;
using System;

namespace CoreData.Desktop.Server.Http.Auth
{
    // looks similar to System.Net.Authorization
    public class AccessToken
    {
        public static AccessToken Empty() => new AccessToken();

        private AccessToken() { }

        public string Value { get; private set; }

        public DateTime? ExpirationUtc { get; private set; }

        public bool Valid =>
            !Value.IsNullOrEmpty() && (ExpirationUtc == null || ExpirationUtc >= DateTime.UtcNow);

        public void Set(string value, DateTime? expirationUtc)
        {
            Value = value;
            ExpirationUtc = expirationUtc;
        }

        public void Clear() => Set(null, null);
    }
}
