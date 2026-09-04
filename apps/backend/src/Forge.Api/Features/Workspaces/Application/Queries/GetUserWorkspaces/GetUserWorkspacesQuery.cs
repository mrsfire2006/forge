using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forge.Api.Features.Shared.Application.Common.ApplicationResult;
using Forge.Api.Features.Shared.Application.Core.CustomMediator;

namespace Forge.Api.Features.Workspaces.Application.Queries.GetUserWorkspaces
{
    public record GetUserWorkspacesQuery(Guid UserId) : IRequest<Result<GetUserWorkspacesResult>>;

}