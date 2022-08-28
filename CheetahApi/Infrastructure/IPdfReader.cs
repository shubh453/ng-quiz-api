namespace CheetahApi.Infrastructure
{
    public interface IPdfReader
    {
        Task<string> ExtractText(byte[] file);
    }
}
