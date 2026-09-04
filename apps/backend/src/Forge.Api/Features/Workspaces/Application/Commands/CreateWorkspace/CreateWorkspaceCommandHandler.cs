using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Forge.Api.Features.Shared.Application.Common.ApplicationResult;
using Forge.Api.Features.Shared.Application.Core.CustomMediator;
using Forge.Api.Features.Shared.Infrastructure.Persistence;
using Forge.Api.Features.Workspaces.Domain.Aggregates;
using Forge.Api.Features.Workspaces.Domain.Entities;
using Forge.Api.Features.Workspaces.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Forge.Api.Features.Workspaces.Application.Commands.CreateWorkspace
{
    public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, Result<CreateWorkspaceResult>>
    {
        private readonly AppDbContext _context;
        public CreateWorkspaceCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<CreateWorkspaceResult>> HandleAsync(CreateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var slug = await GenerateUniqueSlugAsync(
                request.Name,
                cancellationToken);

            var workspace = Workspace.Create(request.Name, slug, request.Description, request.Visibility);

            _context.Workspaces.Add(workspace);
            var member = WorkspaceMember.Create(workspace.Id, request.UserId, WorkspaceRole.Owner);
            _context.WorkspaceMembers.Add(member);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<CreateWorkspaceResult>.Success(new CreateWorkspaceResult(workspace.Id, workspace.Name, workspace.Slug));
        }
        private async Task<string> GenerateUniqueSlugAsync(
    string name,
    CancellationToken cancellationToken)
        {
            var baseSlug = Generate(name);

            var candidates = Enumerable.Range(0, 5)
                .Select(i => i == 0
                    ? baseSlug
                    : $"{baseSlug}-{RandomNumberGenerator.GetInt32(1000, 100000)}")
                .ToArray();

            var existingSlugs = await _context.Workspaces
                .Where(x => candidates.Contains(x.Slug))
                .Select(x => x.Slug)
                .ToListAsync(cancellationToken);

            var existing = existingSlugs.ToHashSet();

            var slug = candidates.FirstOrDefault(
                x => !existing.Contains(x));

            if (slug is not null)
                return slug;

            return $"{baseSlug}-{Guid.NewGuid():N}";
        }
        private string Generate(string value)
        {
            var slug = new StringBuilder();
            var previousWasSeparator = false;

            foreach (var c in value.Trim().ToLowerInvariant())
            {
                if (char.IsLetterOrDigit(c))
                {
                    slug.Append(c);
                    previousWasSeparator = false;
                }
                else if (!previousWasSeparator)
                {
                    slug.Append('-');
                    previousWasSeparator = true;
                }
            }

            return slug.ToString().Trim('-');
        }
    }
}