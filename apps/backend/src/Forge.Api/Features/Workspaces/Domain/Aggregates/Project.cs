using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Echo.Api.Shared.Domain;
using Forge.Api.Features.Workspaces.Domain.Enum;

namespace Forge.Api.Features.Workspaces.Domain.Aggregates
{
    public class Project : AggregateRoot
    {
        public Guid WorkspaceId { get; private set; }
        public Guid CreatedByWorkspaceMemberId { get; private set; }

        public string Name { get; private set; } = default!;
        public string? Description { get; private set; }

        public ProjectStatus Status { get; private set; }

        public string Color { get; private set; } = default!;

        public DateTime? StartDate { get; private set; }
        public DateTime? DueDate { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        private Project() : base(Guid.Empty)
        {
        }

        private Project(
               Guid id,
               Guid workspaceId,
               Guid createdBy,
               string name,
               string? description,
               string color,
               DateTime? startDate,
               DateTime? dueDate
           ) : base(id)
        {
            WorkspaceId = workspaceId;
            CreatedByWorkspaceMemberId = createdBy;
            Name = name;
            Description = description;
            Color = color;

            StartDate = startDate;
            DueDate = dueDate;

            Status = ProjectStatus.Planning;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public static Project Create(
            Guid workspaceId,
            Guid createdBy,
            string name,
            string? description = null,
            string color = "#000000",
            DateTime? startDate = null,
            DateTime? dueDate = null)
        {

            return new Project(
                Guid.NewGuid(),
                workspaceId,
                createdBy,
                name,
                description,
                color,
                startDate,
                dueDate);
        }
    }
}