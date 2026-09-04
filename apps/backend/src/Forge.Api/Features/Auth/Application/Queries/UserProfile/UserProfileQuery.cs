
using Forge.Api.Features.Shared.Application.Common.ApplicationResult;
using Forge.Api.Features.Shared.Application.Core.CustomMediator;

namespace Forge.Api.Features.Auth.Application.Queries.UserProfile
{
    public record UserProfileQuery(Guid UserId) : IRequest<Result<UserProfileResult>>;

}