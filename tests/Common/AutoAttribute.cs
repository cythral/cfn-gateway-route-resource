using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.NUnit3;

using RichardSzalay.MockHttp;

#pragma warning disable EF1001

internal class AutoAttribute : AutoDataAttribute
{
    public AutoAttribute()
        : base(Create)
    {
    }

    public static IFixture Create()
    {
        var fixture = new Fixture();
        fixture.Inject(new CancellationToken(false));

        var mockHttpHandler = new MockHttpMessageHandler();
        var httpClient = new HttpClient(mockHttpHandler) { BaseAddress = new("http://localhost/") };
        fixture.Inject(mockHttpHandler);
        fixture.Inject(httpClient);

        fixture.Customize(new AutoNSubstituteCustomization { ConfigureMembers = true });
        fixture.Customizations.Add(new OptionsRelay());
        fixture.Customizations.Add(new TypeOmitter<IDictionary<string, JsonElement>>());
        fixture.Customizations.Add(new TypeOmitter<JsonConverter>());
        fixture.Customizations.Add(new TypeOmitter<MemoryStream>());
        fixture.Customizations.Insert(-1, new TargetRelay());
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        return fixture;
    }
}
