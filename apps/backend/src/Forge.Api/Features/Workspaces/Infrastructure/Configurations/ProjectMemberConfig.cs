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
    public class ProjectMemberConfig : IEntityTypeConfiguration<ProjectMember>
    {
        public void Configure(EntityTypeBuilder<ProjectMember> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new {x.ProjectId, x.WorkspaceMemberId}).IsUnique();


            builder.HasOne<WorkspaceMember>()
            .WithMany()
            .HasForeignKey(x => x.WorkspaceMemberId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Project>()
            .WithMany()
            .HasForeignKey(x  => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}