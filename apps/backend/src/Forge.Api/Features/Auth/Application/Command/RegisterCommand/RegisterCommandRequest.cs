using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forge.Api.Features.Shared.Application.Core.CustomMediator;

namespace Forge.Api.Features.Auth.Application.Command.RegisterCommand
{
    public record RegisterCommandRequest(string Username, string Email, string Password) : IRequest;
}