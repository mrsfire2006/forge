using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forge.Api.Features.Shared.Application.Common.ApplicationResult;
using Forge.Api.Features.Shared.Application.Core.CustomMediator;
using Forge.Api.Features.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Forge.Api.Features.Auth.Application.Queries.UserProfile
{
    public class UserProfileQueryHandler : IRequestHandler<UserProfileQuery, Result<UserProfileResult>>
    {
        private readonly AppDbContext _context;

        public UserProfileQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Result<UserProfileResult>> HandleAsync(UserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            return Result<UserProfileResult>.Success(new UserProfileResult(user.Id, user.UserName, user.Email));
        }
    }
}