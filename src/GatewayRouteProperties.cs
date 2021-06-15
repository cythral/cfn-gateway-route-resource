using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Amazon.AppMesh.Model;

using Lambdajection.Attributes;

namespace Cythral.CloudFormation.Resources
{
    /// <summary>
    /// Properties to use for a Gateway Route.
    /// </summary>
    public class GatewayRouteProperties
    {
        /// <summary>
        /// Gets or sets the client token.
        /// </summary>
        public string? ClientToken { get; set; }

        /// <summary>
        /// Gets or sets the gateway route name.
        /// </summary>
        [Required]
        [UpdateRequiresReplacement]
        public string GatewayRouteName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the mesh name.
        /// </summary>
        [Required]
        [UpdateRequiresReplacement]
        public string MeshName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the mesh owner.
        /// </summary>
        [Required]
        [UpdateRequiresReplacement]
        public string MeshOwner { get; set; } = null!;

        /// <summary>
        /// Gets or sets the gateway route spec.
        /// </summary>
        [Required]
        public GatewayRouteSpec Spec { get; set; } = null!;

        /// <summary>
        /// Gets or sets the gateway route tags.
        /// </summary>
        public List<TagRef>? Tags { get; set; } = null!;

        /// <summary>
        /// Gets or sets the virtual gateway name.
        /// </summary>
        [Required]
        [UpdateRequiresReplacement]
        public string VirtualGatewayName { get; set; } = null!;
    }
}
