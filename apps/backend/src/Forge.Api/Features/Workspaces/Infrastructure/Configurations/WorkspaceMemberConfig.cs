using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forge.Api.Features.Auth.Domain;
using Forge.Api.Features.Workspaces.Domain.Aggregates;
using Forge.Api.Features.Workspaces.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forge.Api.Features.Workspaces.Infrastructure.Configurations
{
    public class WorkspaceMemberConfig : IEntityTypeConfiguration<WorkspaceMember>
    {
        public void Configure(EntityTypeBuilder<WorkspaceMember> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new {x.WorkspaceId, x.UserId}).IsUnique();

            
            builder.HasOne<Workspace>()
            .WithMany()
            .HasForeignKey(x => x.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}