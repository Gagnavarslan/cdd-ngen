using System.Security.Cryptography;
using System.Text;

namespace CoreData.Common.Cryptography
{
    public class StringProtector : IDataProtector<string>
    {
        private readonly byte[] _entropy;
        private readonly Encoding _encoding;

        internal StringProtector(byte[] entropy, Encoding encoding = null)
        {
            _entropy = entropy;
            _encoding = encoding ?? Encoding.UTF8;
        }

        public byte[] Encrypt(string value)
        {
            if(string.IsNullOrEmpty(value))
                return null;

            var data = _encoding.GetBytes(value);
            return ProtectedData.Protect(data, _entropy, DataProtectionScope.CurrentUser);
        }

        public string Decrypt(byte[] value)
        {
            if(value == null)
                return null;

            var data = ProtectedData.Unprotect(value, _entropy, DataProtectionScope.CurrentUser);
            return _encoding.GetString(data);
        }
    }
}
