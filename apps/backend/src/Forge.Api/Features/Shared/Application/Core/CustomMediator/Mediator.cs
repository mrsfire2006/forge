using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forge.Api.Features.Shared.Application.Core.CustomMediator
{
    public class Mediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;


        public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
                where TRequest : IRequest<TResponse>
        {
            var handler = _serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
            // Func<Task<TResponse>> coreHandler = async () =>
            //     {
            //         return await handler.HandleAsync(request, cancellationToken);
            //     };
            // var builder = new RequestPipelineBuilder<TRequest, TResponse>();

            // if (configurePipeline != null)
            // {
            //     configurePipeline(builder);
            // }
            return await handler.HandleAsync(request, cancellationToken);
        }


    }
}