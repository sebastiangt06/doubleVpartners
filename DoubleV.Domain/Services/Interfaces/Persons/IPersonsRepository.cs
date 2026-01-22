using DoubleV.Domain.Entities;

namespace DoubleV.Domain.Services.Interfaces.Persons
{
    public interface IPersonsRepository
    {
        Task<List<Person>> GetAllAsync();
    }
}