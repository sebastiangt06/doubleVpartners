using DoubleV.Application.Command.Persons;
using DoubleV.Application.Feature;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoubleV.Domain.Services.Interfaces.Persons;

namespace DoubleV.Application.Command.Login
{

    public class GetPersonsCommandHandler : IRequestHandler<GetPersonsCommand, IActionResult>
    {
        private readonly IPersonsRepository _personsRepository;

        public GetPersonsCommandHandler(IPersonsRepository personsRepository)
        {
            _personsRepository = personsRepository;
        }
        public async Task<IActionResult> Handle(GetPersonsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _personsRepository.GetAllAsync();

                return ResponseApiService.Response(StatusCodes.Status200OK, result);
            }
            catch(Exception ex)
            {
                return ResponseApiService.Response(StatusCodes.Status400BadRequest, ex.Message);    
            }
        }
    }
}