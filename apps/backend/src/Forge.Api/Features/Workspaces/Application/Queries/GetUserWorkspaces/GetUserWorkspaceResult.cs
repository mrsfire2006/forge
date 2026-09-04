using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forge.Api.Features.Workspaces.Domain.Enum;

namespace Forge.Api.Features.Workspaces.Application.Queries.GetUserWorkspaces
{
    public record WorkspaceMemberPreview(
    Guid UserId,
    string Username
);
    public record GetUserWorkspaceResult(Guid Id, string Name, string Slug, string Visibility, string? Description, int TotalMembers, int TotalProjects, DateTime UpdateAt, IReadOnlyList<WorkspaceMemberPreview> MembersPreviews);
}