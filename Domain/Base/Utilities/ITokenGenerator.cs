namespace Domain.Base.Utilities
{
    public interface ITokenGenerator
    {
        Task<string> GenerateTokenAsync(int expiresHours);

        Task<bool> VerifyAsync(string token);
    }
}
