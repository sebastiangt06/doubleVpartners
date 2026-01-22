using DoubleV.Application.Command.Persons;
using DoubleV.Application.Feature;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoubleV.Domain.Services.Interfaces.Persons;
using DoubleV.Application.Command.Users;
using DoubleV.Application.Common.Interfaces;
using DoubleV.Domain.Services.Interfaces.Users;
using DoubleV.Domain.Entities;
using DoubleV.Domain.Entities.Users;
using System.Runtime.CompilerServices;
using DoubleV.Application.Interfaces;

namespace DoubleV.Application.Command.Login
{

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IActionResult>
    {
        private readonly IPersonsRepository _personsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordService _passwordService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(
            IPersonsRepository personsRepository,
                    IUsersRepository usersRepository,
            IPasswordService passwordService,
            IUnitOfWork unitOfWork)
        {
            _personsRepository = personsRepository;
            _usersRepository = usersRepository;
            _passwordService = passwordService;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var r = request.CreateUserRequest;

                // Validaciones de unicidad (como tu script tiene índices únicos, mejor validar antes)
                if (await _personsRepository.EmailExistsAsync(r.Email))
                    return ResponseApiService.Response(StatusCodes.Status409Conflict, "Email ya existe.");

                if (await _personsRepository.IdentificationExistsAsync(r.IdentificationType, r.Identification))
                    return ResponseApiService.Response(StatusCodes.Status409Conflict, "Identificación ya existe.");

                if (await _usersRepository.UserNameExistsAsync(r.UserName))
                    return ResponseApiService.Response(StatusCodes.Status409Conflict, "Usuario ya existe.");

                await _unitOfWork.BeginTransactionAsync();
                // Transacción: Person + User

                var person = new Person
                {
                    Name = r.Name,
                    LastName = r.LastName,
                    IdentificationType = r.IdentificationType,
                    Identification = r.Identification,
                    Email = r.Email
                };

                person = await _personsRepository.CreateAsync(person);

                var user = new User
                {
                    PersonId = person.Id,
                    UserName = r.UserName,
                    PassHash = _passwordService.Hash(r.Pass)
                };

                user = await _usersRepository.CreateAsync(user);


                await _unitOfWork.CommitTransactionAsync();

                return ResponseApiService.Response(StatusCodes.Status200OK, new
                {
                    PersonId = person.Id,
                    Email = person.Email,
                    UserId = user.Id,
                    UserName = user.UserName
                });
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ResponseApiService.Response(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}