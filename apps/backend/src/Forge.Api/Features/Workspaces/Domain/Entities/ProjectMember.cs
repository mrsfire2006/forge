using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Echo.Api.Shared.Domain;

namespace Forge.Api.Features.Workspaces.Domain.Entities
{
    public class ProjectMember : Entity
    {
        public Guid WorkspaceMemberId { get; private set; }
        public Guid ProjectId { get; private set; }

        public DateTime JoinedAt { get; private set; }

        private ProjectMember() : base(Guid.Empty)
        {

        }
        private ProjectMember(
    Guid id,
    Guid workspaceMemberId,
    Guid projectId) : base(id)
        {
            WorkspaceMemberId = workspaceMemberId;
            ProjectId = projectId;
            JoinedAt = DateTime.UtcNow;
        }

        public static ProjectMember Create(
            Guid workspaceMemberId,
            Guid projectId)
        {
            if (workspaceMemberId == Guid.Empty)
                throw new ArgumentException(
                    "WorkspaceMemberId is required.",
                    nameof(workspaceMemberId));

            if (projectId == Guid.Empty)
                throw new ArgumentException(
                    "ProjectId is required.",
                    nameof(projectId));

            return new ProjectMember(
                Guid.NewGuid(),
                workspaceMemberId,
                projectId);
        }
    }
}