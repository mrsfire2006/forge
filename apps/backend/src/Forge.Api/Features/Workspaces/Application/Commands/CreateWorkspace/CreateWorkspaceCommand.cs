using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Forge.Api.Features.Shared.Application.Common.ApplicationResult;
using Forge.Api.Features.Shared.Application.Core.CustomMediator;
using Forge.Api.Features.Workspaces.Domain.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Forge.Api.Features.Workspaces.Application.Commands.CreateWorkspace
{
    public record CreateWorkspaceCommand(string Name, string? Description, WorkspaceVisibility Visibility) : IRequest<Result<CreateWorkspaceResult>>
    {
        [JsonIgnore]
        [BindNever]
        public Guid UserId { get; set; }
    };
}