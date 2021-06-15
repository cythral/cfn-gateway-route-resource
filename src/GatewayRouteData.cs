using System;

using Lambdajection.CustomResource;

namespace Cythral.CloudFormation.Resources
{
    /// <inheritdoc />
    public class GatewayRouteData : ICustomResourceOutputData
    {
        /// <inheritdoc />
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
