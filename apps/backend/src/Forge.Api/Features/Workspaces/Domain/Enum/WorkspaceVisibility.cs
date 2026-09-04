using System.Text.Json.Serialization;

namespace Forge.Api.Features.Workspaces.Domain.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WorkspaceVisibility
    {
        Public,
        Private
    }
}