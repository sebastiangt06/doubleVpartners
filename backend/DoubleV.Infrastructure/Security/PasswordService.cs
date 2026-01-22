using DoubleV.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DoubleV.Infrastructure.Security
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string Hash(string plain) => _hasher.HashPassword(new object(), plain);

        public bool Verify(string plain, string hash) =>
            _hasher.VerifyHashedPassword(new object(), hash, plain) == PasswordVerificationResult.Success;
    }
}
