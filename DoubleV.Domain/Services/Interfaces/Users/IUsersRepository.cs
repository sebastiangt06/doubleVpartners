using DoubleV.Domain.DTO.Login;
using DoubleV.Domain.Entities.Users;

namespace DoubleV.Domain.Services.Interfaces.Users
{
    public interface IUsersRepository
    {
        Task<User?> GetByUserNameAsync(LoginRequest request);
    }
}