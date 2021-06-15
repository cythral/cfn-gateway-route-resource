using Amazon.AppMesh;

using Lambdajection.Core;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cythral.CloudFormation.Resources
{
    /// <summary>
    /// Startup class to configure services for <see cref="Handler" />.
    /// </summary>
    public class Startup : ILambdaStartup
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">Configuration to use.</param>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Configure services for <see cref="Handler" />.
        /// </summary>
        /// <param name="services">Services to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.UseAwsService<IAmazonAppMesh>();
        }
    }
}
