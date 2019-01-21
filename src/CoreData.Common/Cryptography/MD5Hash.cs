using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CoreData.Common.Cryptography
{
    public static class MD5Hash
    {
        public static string GetHash(this Stream ctx)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(ctx);
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            }
        }

        public static string GetHash(IEnumerable<string> lines, Encoding encoding) //Encoding.UTF8
        {
            var text = string.Join(Environment.NewLine, lines);
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream, encoding))
                {
                    writer.Write(text);
                    writer.Flush();
                    stream.Position = 0;
                    return GetHash(stream);
                }
            }
        }
    }
}
