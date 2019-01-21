using System;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;

namespace CoreData.Common.Cryptography.Streams
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.symmetricalgorithm?view=netframework-4.7.2
    public class BlockProtector : IDataProtector<byte[]>
    {
        //public static class Cryptography
        //{
        //    public const int StreamBuffer = 32768;

        //    internal static readonly byte[] Salt = {
        //    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
        //    0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        //};

        //    public const int BlockSize = 32768;

        //    public const long LruCacheSizeMb = 1 * 1024 * 1024; //1 MB
        //    internal const int LruCacheSize = (int)(LruCacheSizeMb / BlockSize);

        //}

        //internal SegmentProtector(byte[] entropy)
        //{
        //    _entropy = entropy;
        //}

        public byte[] Encrypt(byte[] value)
        {
            Contract.Requires<ArgumentNullException>(value != null);
            ProtectedMemory.Protect(value, MemoryProtectionScope.SameLogon);
            return value;
        }

        public byte[] Decrypt(byte[] value)
        {
            Contract.Requires<ArgumentNullException>(value != null);
            ProtectedMemory.Unprotect(value, MemoryProtectionScope.SameLogon);
            return value;
        }
    }
}
