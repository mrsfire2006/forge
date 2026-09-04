using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forge.Api.Features.Auth.Domain;
using Forge.Api.Features.Shared.Application.Common.ApplicationResult;
using Forge.Api.Features.Shared.Application.Core.CustomMediator;
using Microsoft.AspNetCore.Identity;

namespace Forge.Api.Features.Auth.Application.Command.LoginCommand
{
    public class LoginCommandRequestHandler : IRequestHandler<LoginCommandRequest, Result>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginCommandRequestHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<Result> HandleAsync(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return Result.Failure("Invalid email or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: false);

            if (!result.Succeeded)
                return Result.Failure("Invalid email or password.");

            await _signInManager.SignInAsync(
                user,
                isPersistent: true);

            return Result.Success();
        }
    }
}