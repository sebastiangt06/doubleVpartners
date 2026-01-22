using DoubleV.Domain.DTO.Login;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace DoubleV.Application.Command.Login
{
    public class LoginCommand : IRequest<IActionResult>
    {
        public LoginRequest LoginRequest {get; set;} = null!;
    }
}