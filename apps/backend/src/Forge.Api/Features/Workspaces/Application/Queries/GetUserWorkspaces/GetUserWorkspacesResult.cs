using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forge.Api.Features.Workspaces.Application.Queries.GetUserWorkspaces
{
    public record GetUserWorkspacesResult(
        int Count,
        int TotalMembers,
        IReadOnlyList<GetUserWorkspaceResult> Data
    );


}