using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forge.Api.Features.Auth.Application.Queries.UserProfile
{
    public record UserProfileResult(Guid Id, string Username, string Email);

}