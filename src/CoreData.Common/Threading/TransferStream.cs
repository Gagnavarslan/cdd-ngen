using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace CoreData.Common.Threading
{
    //!!!: ???: concurrent read|write stream
    /// <summary>Writeable stream for using a separate thread in a producer/consumer scenario.</summary> 
    public sealed class TransferStream : AbstractStreamBase
    {
        /// <summary>The underlying stream to target.</summary> 
        private Stream _writeableStream;
        /// <summary>The collection of chunks to be written.</summary> 
        private BlockingCollection<byte[]> _chunks;
        /// <summary>The Task to use for background writing.</summary> 
        private Task _processingTask;

        /// <summary>Initializes a new instance of the TransferStream.</summary> 
        /// <param name="writeableStream">The underlying stream to which to write.</param> 
        public TransferStream(Stream writeableStream)
        {
            // Validate arguments 
            if (writeableStream == null) throw new ArgumentNullException("writeableStream");
            if (!writeableStream.CanWrite) throw new ArgumentException("Target stream is not writeable.");

            // Set up the producer/consumer relationship, including starting the consumer running 
            _writeableStream = writeableStream;
            _chunks = new BlockingCollection<byte[]>();
            _processingTask = Task.Factory.StartNew(() =>
            {
                // Write out all chunks to the underlying stream 
                foreach (var chunk in _chunks.GetConsumingEnumerable())
                    _writeableStream.Write(chunk, 0, chunk.Length);
            }, TaskCreationOptions.LongRunning);
        }

        /// <summary>Determines whether data can be written to the stream.</summary> 
        public override bool CanWrite { get { return true; } }

        /// <summary>Writes a sequence of bytes to the stream.</summary> 
        /// <param name="buffer">An array of bytes. Write copies count bytes from buffer to the stream.</param> 
        /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the stream.</param> 
        /// <param name="count">The number of bytes to be written to the current stream.</param> 
        public override void Write(byte[] buffer, int offset, int count)
        {
            // Validate all arguments 
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (offset < 0 || offset >= buffer.Length) throw new ArgumentOutOfRangeException("offset");
            if (count < 0 || offset + count > buffer.Length) throw new ArgumentOutOfRangeException("count");
            if (count == 0) return;

            // Store the data to the collection 
            var chunk = new byte[count];
            Buffer.BlockCopy(buffer, offset, chunk, 0, count);
            _chunks.Add(chunk);
        }

        /// <summary>Closes the stream and releases all resources associated with it.</summary> 
        public override void Close()
        {
            // Complete the collection and waits for the consumer to process all of the data 
            _chunks.CompleteAdding();
            try { _processingTask.Wait(); }
            finally { base.Close(); }
        }
    }

    /// <summary>Base stream class that implements all of Stream's abstract members.</summary> 
    public abstract class AbstractStreamBase : Stream
    {
        /// <summary>Determines whether data can be read from the stream.</summary> 
        public override bool CanRead { get { return false; } }
        /// <summary>Determines whether data can be written to the stream.</summary> 
        public override bool CanWrite { get { return false; } }
        /// <summary>Determines whether the stream can be seeked.</summary> 
        public override bool CanSeek { get { return false; } }
        /// <summary>Flushes the contents of the stream to the underlying storage.</summary> 
        public override void Flush() { }

        /// <summary>Gets the length of the stream.</summary> 
        public override long Length { get { throw new NotSupportedException(); } }

        /// <summary>Gets or sets the current position of the stream.</summary> 
        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        /// <summary> 
        /// Reads a sequence of bytes from the current 
        /// stream and advances the position within the stream by the number of bytes read. 
        /// </summary> 
        /// <param name="buffer"> 
        /// An array of bytes. When Read returns, the buffer contains the specified 
        /// byte array with the values between offset and (offset + count - 1) replaced 
        /// by the bytes read from the current source. 
        /// </param> 
        /// <param name="offset"> 
        /// The zero-based byte offset in buffer at which to begin storing the data read 
        /// from the current stream. 
        /// </param> 
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param> 
        /// <returns> 
        /// The total number of bytes read into the buffer. This can be less than the 
        /// number of bytes requested if that many bytes are not currently available, 
        /// or zero (0) if the end of the stream has been reached. 
        /// </returns> 
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        /// <summary>Sets the position within the current stream.</summary> 
        /// <param name="offset">A byte offset relative to the origin parameter.</param> 
        /// <param name="origin"> 
        /// A value of type System.IO.SeekOrigin indicating the reference point used 
        /// to obtain the new position. 
        /// </param> 
        /// <returns>The new position within the current stream.</returns> 
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>Sets the length of the current stream.</summary> 
        /// <param name="value">The desired length of the current stream in bytes.</param> 
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <summary>Writes a sequence of bytes to the stream.</summary> 
        /// <param name="buffer">An array of bytes. Write copies count bytes from buffer to the stream.</param> 
        /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the stream.</param> 
        /// <param name="count">The number of bytes to be written to the current stream.</param> 
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
