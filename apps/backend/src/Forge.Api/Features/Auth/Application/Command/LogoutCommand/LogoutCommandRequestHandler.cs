using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forge.Api.Features.Auth.Domain;
using Forge.Api.Features.Shared.Application.Common.ApplicationResult;
using Forge.Api.Features.Shared.Application.Core.CustomMediator;
using Microsoft.AspNetCore.Identity;

namespace Forge.Api.Features.Auth.Application.Command.LogoutCommand
{
    public class LogoutCommandRequestHandler : IRequestHandler<LogoutCommandRequest, Result>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;


        public LogoutCommandRequestHandler(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<Result> HandleAsync(LogoutCommandRequest request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            return Result.Success();
        }
    }
}