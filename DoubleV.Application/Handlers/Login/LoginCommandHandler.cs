using DoubleV.Application.Command.Login;
using DoubleV.Application.Common.Interfaces;
using DoubleV.Application.Feature;
using DoubleV.Domain.Services.Interfaces.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoubleV.Application.Handlers.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, IActionResult>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginCommandHandler(IUsersRepository usersRepository, IPasswordService passwordService,
            IJwtTokenService jwtTokenService)
        {
            _usersRepository = usersRepository;
            _passwordService = passwordService;
            _jwtTokenService = jwtTokenService;
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

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("personId", user.PersonId.ToString())
                    };

                var token = _jwtTokenService.CreateToken(claims);

                return ResponseApiService.Response(StatusCodes.Status200OK, new
                {
                    token,
                    expiresIn = 60,
                    user = new { user.Id, user.UserName, user.PersonId }
                });
            }
            catch (Exception ex)
            {
                return ResponseApiService.Response(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
