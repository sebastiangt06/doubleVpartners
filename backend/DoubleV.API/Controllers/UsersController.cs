using DoubleV.Application.ExceptionManager;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DoubleV.Application.Command.Login;
using DoubleV.Domain.DTO.Login;
using DoubleV.Domain.DTO.Users;
using DoubleV.Application.Command.Users;
using Microsoft.AspNetCore.Authorization;

namespace DoubleV.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [TypeFilter(typeof(ExceptionManager))]
    public class UsersController : ControllerBase
    {
        #region fields
        private readonly IMediator _mediator;

        #endregion
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionManager))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ExceptionManager))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            return await _mediator.Send( new CreateUserCommand
            {
                CreateUserRequest = createUserRequest
            });
        }
    }
}