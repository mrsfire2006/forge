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
    public class WorkspaceConfig : IEntityTypeConfiguration<Workspace>
    {
        public void Configure(EntityTypeBuilder<Workspace> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Slug)
     .IsUnique();
            builder.Property(x => x.CreatedAt);



            

        }
    }
}