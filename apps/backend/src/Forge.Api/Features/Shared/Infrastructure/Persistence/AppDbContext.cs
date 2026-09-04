
using Echo.Api.Shared.Domain;
using Forge.Api.Features.Auth.Domain;
using Forge.Api.Features.Workspaces.Domain.Aggregates;
using Forge.Api.Features.Workspaces.Domain.Entities;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Forge.Api.Features.Shared.Infrastructure.Persistence
{

    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, IDataProtectionKeyContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
                {
                    builder.Entity(entityType.ClrType)
                        .Property<Guid>("Id")
                        .ValueGeneratedNever();
                }
            }
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
        .ToTable("AspNetUsers", "auth");

            builder.Entity<IdentityRole<Guid>>()
                .ToTable("AspNetRoles", "auth");

            builder.Entity<IdentityUserClaim<Guid>>()
                .ToTable("AspNetUserClaims", "auth");

            builder.Entity<IdentityUserLogin<Guid>>()
                .ToTable("AspNetUserLogins", "auth");

            builder.Entity<IdentityUserToken<Guid>>()
                .ToTable("AspNetUserTokens", "auth");

            builder.Entity<IdentityRoleClaim<Guid>>()
                .ToTable("AspNetRoleClaims", "auth");

            builder.Entity<IdentityUserRole<Guid>>()
                .ToTable("AspNetUserRoles", "auth");
            builder.Entity<DataProtectionKey>()
.ToTable("DataProtectionKeys", "auth");
            builder.ApplyConfigurationsFromAssembly(typeof(FeatureMarker).Assembly);
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<WorkspaceMember> WorkspaceMembers { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
    }
}