using System.ComponentModel.DataAnnotations.Schema;

namespace DoubleV.Domain.Entities
{   
    [Table("Personas", Schema ="dbo")]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Identification { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string IdentificationType { get; set; } = default!;
        public DateTime CreatedAt { get; set; }

        // DB CALCULATED (it doesnt get set on the app)
        public string FullIdentification { get; private set; } = default!;
        public string FullName { get; private set; } = default!;
    }


}
