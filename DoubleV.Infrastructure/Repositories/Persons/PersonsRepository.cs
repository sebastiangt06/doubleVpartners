using DoubleV.Domain.Entities;
using DoubleV.Domain.Services.Interfaces.Persons;
using DoubleV.Infrastructure.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DoubleV.Infrastructure.Repositories.Persons
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly AppDbContext _dbContext;

        public PersonsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Person>> GetAllAsync()
        {
            // SP: dbo.sp_Personas_Creadas @FechaInicio, @FechaFin
            //In case you want to filter by date, you can set the parameters accordingly
            var pFrom = new SqlParameter("@FechaInicio", DBNull.Value);
            var pTo = new SqlParameter("@FechaFin", DBNull.Value);

            var result = await _dbContext.Set<Person>()
                .FromSqlRaw("EXEC dbo.sp_Personas_Creadas @FechaInicio, @FechaFin", pFrom, pTo)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }
    }
}