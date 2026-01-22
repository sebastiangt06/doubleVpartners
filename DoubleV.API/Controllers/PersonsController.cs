using DoubleV.Application.ExceptionManager;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using DoubleV.Application.Command.Persons;

namespace DoubleV.API.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [TypeFilter(typeof(ExceptionManager))]
    public class PersonsController : ControllerBase
    {
        #region fields
        private readonly IMediator _mediator;

        #endregion
        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }  
        
        [AllowAnonymous]
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionManager))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ExceptionManager))]
        public async Task<IActionResult> GetPersons()
        {
            return await _mediator.Send( new GetPersonsCommand
            {
            });
        }
    }
}