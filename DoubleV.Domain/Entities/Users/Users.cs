using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace DoubleV.Domain.Entities.Users
{
    [Table("Usuarios", Schema ="dbo")]
    public class User
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string UserName { get; set; } = default!;
        public string PassHash { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public Person Person { get; set; } = default!;
    }

}