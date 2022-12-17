using Utilities.Interfaces;

namespace Utilities
{
    public class TokenGenerator : ITokenGenerator
    {
        public Task<string> GenerateTokenAsync(int expiresHours)
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());

            return Task.FromResult(token);
        }

        public Task<bool> VerifyAsync(string token)
        {
            byte[] data = Convert.FromBase64String(token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));

            return Task.FromResult(when < DateTime.UtcNow);
        }
    }
}
