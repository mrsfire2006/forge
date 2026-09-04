
using Forge.Api.Features.Auth.Application.Command.LoginCommand;
using Forge.Api.Features.Auth.Application.Command.LogoutCommand;
using Forge.Api.Features.Auth.Application.Command.RegisterCommand;
using Forge.Api.Features.Auth.Application.Queries.UserProfile;
using Forge.Api.Features.Shared.Application.Common.ApplicationResult;
using Forge.Api.Features.Shared.Application.Core.CustomMediator;
using Forge.Api.Features.Workspaces.Application.Queries.GetUserWorkspaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forge.Api.Features.Auth
{


    [Route("auth")]
    public class AuthController : ApiControllerBase
    {
        private readonly Mediator _mediator;
        public AuthController(Mediator mediator)
        {
            _mediator = mediator;

        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterCommandRequest request, CancellationToken cancellationToken)
        {

            var result = await _mediator.SendAsync<RegisterCommandRequest, Result>(request, cancellationToken);
            return HandleResult(result);
        }
        [HttpPost("login")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest request, CancellationToken cancellationToken)
        {

            var result = await _mediator.SendAsync<LoginCommandRequest, Result>(request, cancellationToken);
            return HandleResult(result);
        }
        [HttpPost("logout")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {

            var request = new LogoutCommandRequest();
            var result = await _mediator.SendAsync<LogoutCommandRequest, Result>(request, cancellationToken);
            return HandleResult(result);
        }

        [HttpGet("me")]
        [ProducesResponseType(typeof(Result<UserProfileResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UserProfile(CancellationToken cancellationToken)
        {
            if (EnsureAuthenticatedUser(out Guid userId) is IActionResult)
            {
                return HandleResult(Result.Failure("Unauthorized", StatusCodes.Status401Unauthorized));

            }
            var request = new UserProfileQuery(userId);

            var result = await _mediator.SendAsync<UserProfileQuery, Result<UserProfileResult>>(request, cancellationToken);
            return HandleResult(result);
        }

        [ProducesResponseType(typeof(Result<GetUserWorkspacesResult>), StatusCodes.Status200OK)]
        [HttpGet("workspaces")]
        [Authorize]
        public async Task<IActionResult> GetWorkspaces(
CancellationToken cancellationToken)
        {
            if (EnsureAuthenticatedUser(out Guid userId) is IActionResult)
            {
                return HandleResult(Result.Failure("Unauthorized", StatusCodes.Status401Unauthorized));

            }
            var query = new GetUserWorkspacesQuery(userId);

            var result = await _mediator.SendAsync<GetUserWorkspacesQuery, Result<GetUserWorkspacesResult>>(query, cancellationToken);

            return HandleResult(result);
        }


    }
}