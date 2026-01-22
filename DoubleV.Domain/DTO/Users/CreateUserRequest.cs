namespace DoubleV.Domain.DTO.Users
{
    public class CreateUserRequest
    {
        //Person
        public string Name { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Identification { get; set; } = default!;
        public string IdentificationType { get; set; } = default!;
        public string Email { get; set; } = default!;
        // User
        public string UserName { get; set; } = default!;
        public string Pass { get; set; } = default!;
        public DateTime CreatedAt {get; set;}
    }
}