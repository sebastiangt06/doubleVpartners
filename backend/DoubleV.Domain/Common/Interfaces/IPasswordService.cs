namespace DoubleV.Application.Common.Interfaces
{
    public interface IPasswordService
    {
        string Hash(string plain);
        bool Verify(string plain, string hash);
    }
}
