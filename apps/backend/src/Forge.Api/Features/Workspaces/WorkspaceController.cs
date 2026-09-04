using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forge.Api.Features.Shared.Application.Common.ApplicationResult;
using Forge.Api.Features.Shared.Application.Core.CustomMediator;
using Forge.Api.Features.Workspaces.Application.Commands.CreateWorkspace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forge.Api.Features.Workspaces
{
    [Route("workspace")]
    public class WorkspaceController : ApiControllerBase
    {
        private readonly Mediator _mediator;
        public WorkspaceController(Mediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("new")]
        [Authorize]
        [ProducesResponseType(typeof(Result<CreateWorkspaceResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateWorkspace(
       [FromBody] CreateWorkspaceCommand command,
       CancellationToken cancellationToken)
        {
            if (EnsureAuthenticatedUser(out Guid userId) is IActionResult)
            {
                return HandleResult(Result.Failure("Unauthorized", StatusCodes.Status401Unauthorized));

            }
            command.UserId = userId;
            var result = await _mediator.SendAsync<CreateWorkspaceCommand, Result<CreateWorkspaceResult>>(command, cancellationToken);

            return HandleResult(result);
        }

    }
}