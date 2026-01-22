using DoubleV.Domain.DTO.Login;
using DoubleV.Domain.Entities;
using DoubleV.Domain.Entities.Users;
using DoubleV.Domain.Services.Interfaces.Persons;
using DoubleV.Domain.Services.Interfaces.Users;
using DoubleV.Infrastructure.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DoubleV.Infrastructure.Repositories.Persons
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _dbContext;

        public UsersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetByUserNameAsync(LoginRequest request)
        {
            return await _dbContext.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.UserName == request.Username);
        }
    }
}