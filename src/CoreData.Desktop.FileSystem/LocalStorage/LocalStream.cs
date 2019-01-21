using System;
using System.IO;
using System.Security.Cryptography;

namespace CoreData.Desktop.FileSystem.BackingDataStore
{
    //public class LocalStream<TStream> : Stream
    //    where TStream : Stream
    //    // https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream?view=netframework-4.7.2#remarks
    //    // todo: UnmanagedMemoryStream, MemoryStream, FileStream, X_CryptoStream - from fast to slow performance
    //    // https://docs.microsoft.com/en-us/dotnet/api/system.io.unmanagedmemorystream?view=netframework-4.7.2#examples
    //{
    //}

    // https://docs.microsoft.com/en-us/dotnet/api/system.web.hosting.virtualpathprovider?view=netframework-4.7.2
    public class LocalStream : Stream
    {
        public LocalStream()
        {
            // !!!: leaveOpen - possible to leave it open
            //var cStream = new CryptoStream(stream, transform, mode, leaveOpen: true);
        }
        public static byte[] EncryptDataWithPersistedKey(byte[] data, byte[] iv)
        {
            using(Aes aes = new AesCng("CDD_KEY",
                CngProvider.MicrosoftSoftwareKeyStorageProvider, CngKeyOpenOptions.UserKey))
            {
                aes.IV = iv;

                // Using the zero-argument overload is required to make use of the persisted key
                using(var encryptor = aes.CreateEncryptor())
                {
                    if(!encryptor.CanTransformMultipleBlocks)
                    {
                        throw new InvalidOperationException("This is a sample, this case wasn’t handled...");
                    }

                    return encryptor.TransformFinalBlock(data, 0, data.Length);
                }
            }
        }

        public override void Flush() => throw new System.NotImplementedException();
        public override long Seek(long offset, SeekOrigin origin) => throw new System.NotImplementedException();
        public override void SetLength(long value) => throw new System.NotImplementedException();
        public override int Read(byte[] buffer, int offset, int count) => throw new System.NotImplementedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new System.NotImplementedException();

        public override bool CanRead { get; }
        public override bool CanSeek { get; }
        public override bool CanWrite { get; }
        public override long Length { get; }
        public override long Position { get; set; }
    }
}
