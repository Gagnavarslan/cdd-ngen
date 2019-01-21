using System.IO;

namespace CoreData.Common.Cryptography.Streams
{
    /// <summary>Stream segment of a read|write operations.</summary>
    public class StreamBlock
    {
        public StreamBlock(Stream stream, SeekOrigin seekOrigin, long offset,
            byte[] buffer, int bytesToProcess)
        {
            Stream = stream;
            SeekOrigin = seekOrigin;
            Offset = offset;
            Buffer = buffer;
            BytesToProcess = bytesToProcess;
        }

        public Stream Stream { get; }
        public SeekOrigin SeekOrigin { get; }
        public long Offset { get; }
        public byte[] Buffer { get; }
        public int BytesToProcess { get; }

        public int BytesProcessed { get; set; }
    }
}
