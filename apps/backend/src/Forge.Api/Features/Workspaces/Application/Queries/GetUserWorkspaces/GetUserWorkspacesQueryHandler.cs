using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forge.Api.Features.Shared.Application.Common.ApplicationResult;
using Forge.Api.Features.Shared.Application.Core.CustomMediator;
using Forge.Api.Features.Shared.Infrastructure.Persistence;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace Forge.Api.Features.Workspaces.Application.Queries.GetUserWorkspaces
{
    public class GetUserWorkspacesQueryHandler : IRequestHandler<GetUserWorkspacesQuery, Result<GetUserWorkspacesResult>>
    {

        private readonly AppDbContext _context;
        public GetUserWorkspacesQueryHandler(AppDbContext context)
        {
            _context = context;

        }

        public async Task<Result<GetUserWorkspacesResult>> HandleAsync(GetUserWorkspacesQuery request, CancellationToken cancellationToken)
        {
            var Workspaces = await (from member in _context.WorkspaceMembers.AsNoTracking()
                                    join workspace in _context.Workspaces.AsNoTracking()
                                    on member.WorkspaceId equals workspace.Id

                                    where member.UserId == request.UserId
                                    orderby workspace.UpdatedAt
                                    let totalMembers = _context.WorkspaceMembers
                                        .Count(x => x.WorkspaceId == workspace.Id)
                                    let membersPreviews = _context.WorkspaceMembers.AsNoTracking().Where(x => x.WorkspaceId == workspace.Id)
                                    .Join(_context.Users.AsNoTracking(), member => member.UserId, user => user.Id, (member, user) => new { id = member.UserId, username = user.UserName, joinedAt = member.JoinedAt }).Where(x => x.username != null).OrderBy(x => x.joinedAt).Take(10).Select(x => new WorkspaceMemberPreview(x.id, x.username!)).ToList()

                                    select new GetUserWorkspaceResult(
                                           Id: workspace.Id,
                                           Name: workspace.Name,
                                           Slug: workspace.Slug,
                                           Visibility: workspace.Visibility.ToString(),
                                           workspace.Description,
                                           TotalMembers: totalMembers,
                                           0,
                                           workspace.UpdatedAt,
                                           membersPreviews

                                    )).ToListAsync();
            var TotalWorkspaceMember = Workspaces.Sum(x => x.TotalMembers);
            var TotalWorkspace = Workspaces.Count();

            return Result<GetUserWorkspacesResult>.Success(new GetUserWorkspacesResult(TotalWorkspace, TotalWorkspaceMember, Workspaces));

        }
    }
}