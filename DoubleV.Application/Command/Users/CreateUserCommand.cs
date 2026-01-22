using DoubleV.Domain.DTO.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DoubleV.Application.Command.Users
{
    public class CreateUserCommand : IRequest<IActionResult>
    {
        public CreateUserRequest CreateUserRequest { get; set; } = default!;
    }
}
