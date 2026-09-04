using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forge.Api.Features.Auth.Domain;
using Forge.Api.Features.Shared.Application.Common.ApplicationResult;
using Forge.Api.Features.Shared.Application.Core.CustomMediator;
using Microsoft.AspNetCore.Identity;

namespace Forge.Api.Features.Auth.Application.Command.RegisterCommand
{
    public class RegisterCommandRequestHandler : IRequestHandler<RegisterCommandRequest, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;


        public RegisterCommandRequestHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public async Task<Result> HandleAsync(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email
            };


            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return Result.Failure(
                    result.Errors.Select(x => x.Description).FirstOrDefault() ?? "Error"
                );
            }

            await _signInManager.SignInAsync(user, true);

            return Result.Success();
        }
    }
}