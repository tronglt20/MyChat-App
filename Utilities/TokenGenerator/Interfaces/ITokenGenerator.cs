namespace Utilities.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> GenerateTokenAsync(int expiresHours);

        Task<bool> VerifyAsync(string token);
    }
}
