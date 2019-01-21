using System;
using System.Diagnostics.Contracts;

namespace CoreData.Common.Cryptography.Streams
{
    ///// <summary></summary>
    ///// <remarks>simultaneous read-write a file in C#</remarks>
    ///// <seealso cref="https://stackoverflow.com/a/3817526"/>
    ///// <see cref="System.Threading.Tasks.ConcurrentExclusiveSchedulerPair"/>
    ///// <see cref="System.Threading.ReaderWriterLockSlim"/>
    //public abstract class StreamBlockAccessHanler
    //{
    //    private readonly IDataProtector<byte[]> _dataProtector;

    //    protected StreamBlockAccessHanler(IDataProtector<byte[]> dataProtector) =>
    //        _dataProtector = dataProtector;

    //    public StreamBlock Process(StreamBlock value)
    //    {
    //        Contract.Requires<ArgumentNullException>(value != null);

    //        return ProcessStreamBlock(value);
    //    }

    //    protected abstract StreamBlock ProcessStreamBlock(StreamBlock block);
    //}

    //public class StreamBlockReadHanler : StreamBlockAccessHanler
    //{
    //    public StreamBlockReadHanler(IDataProtector<StreamBlock> dataProtector)
    //    {

    //    }

    //    protected override StreamBlock ProcessStreamBlock(StreamBlock block)
    //    {
    //        stream.Seek(position, SeekOrigin.Begin);
    //        bytesRead = stream.Read(buffer, 0, bytesToRead);
    //        stream.Flush();
    //    }
    //}
    //public class StreamBlockWriteHanler : StreamBlockAccessHanler
    //{
    //    public StreamBlockWriteHanler(IDataProtector<StreamBlock> dataProtector)
    //    {

    //    }

    //    protected override StreamBlock ProcessStreamBlock(StreamBlock block)
    //    {
    //        var currentPosition = stream.Seek(position, SeekOrigin.Begin);
    //        stream.Write(buffer, 0, bytesToWrite);
    //        bytesWritten = (int)(stream.Position - currentPosition);
    //        stream.Flush();
    //    }
    //}

    ///// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=netframework-4.7.2#examples"/>
    //public class ProtectedStreamHandler
    //{
    //    private readonly IDataProtector<StreamBlock> _dataProtector;

    //    public ProtectedStreamHandler(IDataProtector<StreamBlock> dataProtector)
    //    {
    //        _dataProtector = dataProtector;
    //    }

    //    public StreamBlock Process(StreamBlock value)
    //    {
    //        Contract.Requires<ArgumentNullException>(value != null);
    //    }
    //}
}
