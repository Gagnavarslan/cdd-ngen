namespace CoreData.Common.Cryptography
{
    public interface IDataProtector<TData>
    {
        byte[] Encrypt(TData value);

        TData Decrypt(byte[] value);
    }
}
