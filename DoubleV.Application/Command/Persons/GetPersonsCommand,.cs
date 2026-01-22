
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace DoubleV.Application.Command.Persons
{
    public class GetPersonsCommand : IRequest<IActionResult>
    {
    }
}