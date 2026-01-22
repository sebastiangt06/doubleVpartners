using DoubleV.Domain.Entities;

namespace DoubleV.Domain.Services.Interfaces.Persons
{
    public interface IPersonsRepository
    {
        Task<List<Person>> GetAllAsync();
        Task<bool> EmailExistsAsync(string email);
        Task<bool> IdentificationExistsAsync(string type, string number);
        Task<Person> CreateAsync(Person person);
    }
}