using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forge.Api.Features.Workspaces.Application.Commands.CreateWorkspace
{
    public record CreateWorkspaceResult(
        Guid Id,
        string Name,
        string Slug
    );
}