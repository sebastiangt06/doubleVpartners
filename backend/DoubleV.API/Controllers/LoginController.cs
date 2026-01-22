using DoubleV.Application.ExceptionManager;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DoubleV.Application.Command.Login;
using DoubleV.Domain.DTO.Login;

namespace DoubleV.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [TypeFilter(typeof(ExceptionManager))]
    public class LoginController : ControllerBase
    {
        #region fields
        private readonly IMediator _mediator;

        #endregion
        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionManager))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ExceptionManager))]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            return await _mediator.Send( new LoginCommand
            {
                LoginRequest = loginRequest
            });
        }
    }
}