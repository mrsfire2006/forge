using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Echo.Api.Shared.Domain;
using Forge.Api.Features.Workspaces.Domain.Enum;

namespace Forge.Api.Features.Workspaces.Domain.Entities
{
    public class WorkspaceMember : Entity
    {

        public Guid WorkspaceId { get; set; } = default!;
        public Guid UserId { get; set; } = default!;

        public WorkspaceRole Role { get; set; }

        public DateTime JoinedAt { get; set; }


        private WorkspaceMember() : base(Guid.Empty)
        {
        }

        private WorkspaceMember(Guid id, Guid workspaceId, Guid userId, WorkspaceRole role) : base(id)
        {
            Role = role;
            WorkspaceId = workspaceId;
            UserId = userId;
            JoinedAt = DateTime.UtcNow;
        }

        public static WorkspaceMember Create(Guid workspaceId, Guid userId, WorkspaceRole role = WorkspaceRole.Member)
        {
            return new WorkspaceMember(Guid.NewGuid(), workspaceId, userId, role);
        }

        public void UpdateRole(WorkspaceRole role)
        {
            if (Role == role)
            {
                return;
            }
            Role = role;
        }
    }
}