using DoubleV.Application.Command.Login;
using DoubleV.Application.Common.Interfaces;
using DoubleV.Application.Feature;
using DoubleV.Domain.Services.Interfaces.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoubleV.Application.Handlers.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, IActionResult>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordService _passwordService;

        public LoginCommandHandler(IUsersRepository usersRepository, IPasswordService passwordService)
        {
            _usersRepository = usersRepository;
            _passwordService = passwordService;
        }

        public async Task<IActionResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var user = await _usersRepository.GetByUserNameAsync(request.LoginRequest);
                if (user is null)
                    return ResponseApiService.Response(StatusCodes.Status401Unauthorized, "Credenciales inválidas");

                var ok = _passwordService.Verify(request.LoginRequest.Password, user.PassHash);
                if (!ok)
                    return ResponseApiService.Response(StatusCodes.Status401Unauthorized, "Credenciales inválidas");

                // Si no vas a usar JWT aún, devuelve OK simple
                return ResponseApiService.Response(StatusCodes.Status200OK, new
                {
                    user.Id,
                    user.UserName,
                    user.PersonId
                });
            }
            catch (Exception ex)
            {
                return ResponseApiService.Response(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
