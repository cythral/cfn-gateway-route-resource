using System.Threading;
using System.Threading.Tasks;

using Amazon.AppMesh;
using Amazon.AppMesh.Model;

using AutoFixture.NUnit3;

using FluentAssertions;

using Lambdajection.CustomResource;

using NSubstitute;

using NUnit.Framework;

using static NSubstitute.Arg;

namespace Cythral.CloudFormation.Resources
{
    public class HandlerTests
    {
        [TestFixture]
        public class CreateTests
        {
            [Test, Auto]
            public async Task ShouldCallAppMeshCreate(
                CustomResourceRequest<GatewayRouteProperties> request,
                [Frozen] IAmazonAppMesh appMesh,
                [Target] Handler handler,
                CancellationToken cancellationToken
            )
            {
                await handler.Create(request, cancellationToken);

                await appMesh.Received().CreateGatewayRouteAsync(Any<CreateGatewayRouteRequest>(), Is(cancellationToken));
                var appMeshRequest = TestUtils.GetArg<CreateGatewayRouteRequest>(appMesh, nameof(appMesh.CreateGatewayRouteAsync), 0);
                var props = request.ResourceProperties!;

                appMeshRequest.ClientToken.Should().Be(props.ClientToken);
                appMeshRequest.GatewayRouteName.Should().Be(props.GatewayRouteName);
                appMeshRequest.MeshName.Should().Be(props.MeshName);
                appMeshRequest.MeshOwner.Should().Be(props.MeshOwner);
                appMeshRequest.Spec.Should().Be(props.Spec);
                appMeshRequest.Tags.Should().BeEquivalentTo(props.Tags);
                appMeshRequest.VirtualGatewayName.Should().Be(props.VirtualGatewayName);
            }
        }

        [TestFixture]
        public class UpdateTests
        {
            [Test, Auto]
            public async Task ShouldCallAppMeshUpdate(
                CustomResourceRequest<GatewayRouteProperties> request,
                [Frozen] IAmazonAppMesh appMesh,
                [Target] Handler handler,
                CancellationToken cancellationToken
            )
            {
                await handler.Update(request, cancellationToken);

                await appMesh.Received().UpdateGatewayRouteAsync(Any<UpdateGatewayRouteRequest>(), Is(cancellationToken));
                var appMeshRequest = TestUtils.GetArg<UpdateGatewayRouteRequest>(appMesh, nameof(appMesh.UpdateGatewayRouteAsync), 0);
                var props = request.ResourceProperties!;

                appMeshRequest.ClientToken.Should().Be(props.ClientToken);
                appMeshRequest.GatewayRouteName.Should().Be(props.GatewayRouteName);
                appMeshRequest.MeshName.Should().Be(props.MeshName);
                appMeshRequest.MeshOwner.Should().Be(props.MeshOwner);
                appMeshRequest.Spec.Should().Be(props.Spec);
                appMeshRequest.VirtualGatewayName.Should().Be(props.VirtualGatewayName);
            }
        }

        [TestFixture]
        public class DeleteTests
        {
            [Test, Auto]
            public async Task ShouldCallAppMeshDelete(
                CustomResourceRequest<GatewayRouteProperties> request,
                [Frozen] IAmazonAppMesh appMesh,
                [Target] Handler handler,
                CancellationToken cancellationToken
            )
            {
                await handler.Delete(request, cancellationToken);

                await appMesh.Received().DeleteGatewayRouteAsync(Any<DeleteGatewayRouteRequest>(), Is(cancellationToken));
                var appMeshRequest = TestUtils.GetArg<DeleteGatewayRouteRequest>(appMesh, nameof(appMesh.DeleteGatewayRouteAsync), 0);
                var props = request.ResourceProperties!;

                appMeshRequest.GatewayRouteName.Should().Be(props.GatewayRouteName);
                appMeshRequest.MeshName.Should().Be(props.MeshName);
                appMeshRequest.MeshOwner.Should().Be(props.MeshOwner);
                appMeshRequest.VirtualGatewayName.Should().Be(props.VirtualGatewayName);
            }
        }
    }
}
