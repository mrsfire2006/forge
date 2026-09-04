using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forge.Api.Features.Shared.Application.Common.ApplicationResult;

namespace Forge.Api.Features.Shared.Application.Core.CustomMediator
{
    public interface IRequest<out TResponse>;
    public interface IRequest : IRequest<Result>;
}