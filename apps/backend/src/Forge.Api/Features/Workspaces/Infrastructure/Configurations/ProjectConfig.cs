using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forge.Api.Features.Workspaces.Domain.Aggregates;
using Forge.Api.Features.Workspaces.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forge.Api.Features.Workspaces.Infrastructure.Configurations
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<Workspace>()
            .WithMany()
            .HasForeignKey(x => x.WorkspaceId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<WorkspaceMember>()
            .WithMany()
            .HasForeignKey(x => x.CreatedByWorkspaceMemberId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}