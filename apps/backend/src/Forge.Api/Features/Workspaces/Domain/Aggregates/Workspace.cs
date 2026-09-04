

using Echo.Api.Shared.Domain;
using Forge.Api.Features.Workspaces.Domain.Enum;

namespace Forge.Api.Features.Workspaces.Domain.Aggregates
{
    public sealed class Workspace : AggregateRoot
    {

        public string Name { get; private set; } = default!;

        public string Slug { get; private set; } = default!;

        public string? Description { get; private set; }

        public WorkspaceVisibility Visibility { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        private Workspace() : base(Guid.Empty) { }

        private Workspace(
            Guid id,
            string name,
            string slug,
            string? description = null,
            WorkspaceVisibility visibility = WorkspaceVisibility.Private) : base(id)
        {
            Id = id;
            Name = name;
            Slug = slug;
            Description = description;
            Visibility = visibility;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Workspace Create(string name,
            string slug,
            string? description = null,
            WorkspaceVisibility visibility = WorkspaceVisibility.Private)
        {
            return new Workspace(Guid.NewGuid(), name, slug, description, visibility);
        }

        public void Update(
            string name,
            string slug,
            string? description)
        {
            Name = name;
            Slug = slug;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeVisibility(WorkspaceVisibility visibility)
        {
            Visibility = visibility;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}