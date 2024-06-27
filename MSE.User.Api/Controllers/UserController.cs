using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MSE.User.Application.Commands.UserAccounts;
using MSE.User.Application.Commands.Users;
using MSE.User.Application.Queries.Users;
using Route = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MSE.User.Api.Controllers
{
    [Route("api/v1/users")]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// POST api/v1/users: Add new user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// POST api/v1/users: Add new user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("user_account")]
        public async Task<IActionResult> CreateUserAccountAsync([FromBody] CreateUserAccountCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// GET api/v1/users/{id}: Get user by id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(response);
        }
    }
}
