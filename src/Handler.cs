using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Amazon.AppMesh;
using Amazon.AppMesh.Model;

using Lambdajection.Attributes;
using Lambdajection.CustomResource;

using Microsoft.Extensions.Logging;

#pragma warning disable IDE0060

namespace Cythral.CloudFormation.Resources
{
    /// <summary>
    /// Custom resource provider for app mesh gateway routes.
    /// </summary>
    [CustomResourceProvider(typeof(Startup))]
    public partial class Handler
    {
        private readonly IAmazonAppMesh appMesh;
        private readonly ILogger<Handler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler" /> class.
        /// </summary>
        /// <param name="appMesh">Client to use for interacting with app mesh.</param>
        /// <param name="logger">Logger used to log info to some destination(s).</param>
        public Handler(
            IAmazonAppMesh appMesh,
            ILogger<Handler> logger
        )
        {
            this.appMesh = appMesh;
            this.logger = logger;
        }

        /// <summary>
        /// Creates a new app mesh gateway route.
        /// </summary>
        /// <param name="request">Gateway Route Request Properties.</param>
        /// <param name="cancellationToken">Token used to cancel the operation.</param>
        /// <returns>The gateway route output data.</returns>
        public async Task<GatewayRouteData> Create(CustomResourceRequest<GatewayRouteProperties> request, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Received request: " + JsonSerializer.Serialize(request));
            cancellationToken.ThrowIfCancellationRequested();

            var props = request.ResourceProperties!;
            var mappedRequest = new CreateGatewayRouteRequest
            {
                ClientToken = props.ClientToken,
                GatewayRouteName = props.GatewayRouteName,
                MeshName = props.MeshName,
                MeshOwner = props.MeshOwner,
                Spec = props.Spec,
                Tags = props.Tags ?? new List<TagRef>(),
                VirtualGatewayName = props.VirtualGatewayName,
            };

            logger.LogInformation("Sending appmesh:CreateGatewayRoute request: " + JsonSerializer.Serialize(mappedRequest));
            var response = await appMesh.CreateGatewayRouteAsync(mappedRequest, cancellationToken);
            logger.LogInformation("Received appmesh:CreateGatewayRoute response: " + JsonSerializer.Serialize(response));

            return new GatewayRouteData();
        }

        /// <summary>
        /// Updates an existing app mesh gateway route.
        /// </summary>
        /// <param name="request">Gateway Route Request Properties.</param>
        /// <param name="cancellationToken">Token used to cancel the operation.</param>
        /// <returns>The gateway route output data.</returns>
        public async Task<GatewayRouteData> Update(CustomResourceRequest<GatewayRouteProperties> request, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Received request: " + JsonSerializer.Serialize(request));
            cancellationToken.ThrowIfCancellationRequested();

            var props = request.ResourceProperties!;
            var mappedRequest = new UpdateGatewayRouteRequest
            {
                ClientToken = props.ClientToken,
                GatewayRouteName = props.GatewayRouteName,
                MeshName = props.MeshName,
                MeshOwner = props.MeshOwner,
                Spec = props.Spec,
                VirtualGatewayName = props.VirtualGatewayName,
            };

            logger.LogInformation("Sending appmesh:UpdateGatewayRoute request: " + JsonSerializer.Serialize(mappedRequest));
            var response = await appMesh.UpdateGatewayRouteAsync(mappedRequest, cancellationToken);
            logger.LogInformation("Received appmesh:UpdateGatewayRoute response: " + JsonSerializer.Serialize(response));

            return new GatewayRouteData { Id = request.PhysicalResourceId };
        }

        /// <summary>
        /// Deletes an app mesh gateway route.
        /// </summary>
        /// <param name="request">Gateway Route Request Properties.</param>
        /// <param name="cancellationToken">Token used to cancel the operation.</param>
        /// <returns>The gateway route output data.</returns>
        public async Task<GatewayRouteData> Delete(CustomResourceRequest<GatewayRouteProperties> request, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Received request: " + JsonSerializer.Serialize(request));
            cancellationToken.ThrowIfCancellationRequested();

            var props = request.ResourceProperties!;
            var mappedRequest = new DeleteGatewayRouteRequest
            {
                GatewayRouteName = props.GatewayRouteName,
                MeshName = props.MeshName,
                MeshOwner = props.MeshOwner,
                VirtualGatewayName = props.VirtualGatewayName,
            };

            logger.LogInformation("Sending appmesh:DeleteGatewayRoute request: " + JsonSerializer.Serialize(mappedRequest));
            var response = await appMesh.DeleteGatewayRouteAsync(mappedRequest, cancellationToken);
            logger.LogInformation("Received appmesh:DeleteGatewayRoute response: " + JsonSerializer.Serialize(response));

            return new GatewayRouteData { Id = request.PhysicalResourceId };
        }
    }
}
