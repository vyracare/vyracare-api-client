using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Vyracare.Api.Client.Common.Tenancy;

namespace Vyracare.Api.Client.Tests.Common.Tenancy;

public sealed class TenantContextMiddlewareTests
{
    [Fact]
    public async Task Rejects_authenticated_request_without_tenant_claims()
    {
        var nextCalled = false;
        var middleware = new TenantContextMiddleware(_ => { nextCalled = true; return Task.CompletedTask; });
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        context.User = new ClaimsPrincipal(new ClaimsIdentity([new Claim("sub", "user-a")], "test"));

        await middleware.InvokeAsync(context);

        Assert.Equal(StatusCodes.Status403Forbidden, context.Response.StatusCode);
        Assert.False(nextCalled);
    }

    [Fact]
    public async Task Allows_authenticated_request_with_complete_tenant_context()
    {
        var nextCalled = false;
        var middleware = new TenantContextMiddleware(_ => { nextCalled = true; return Task.CompletedTask; });
        var context = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([
                new Claim("sub", "user-a"),
                new Claim("tenant_id", "tenant-a"),
                new Claim("membership_id", "membership-a"),
                new Claim("tenant_role", "Owner")
            ], "test"))
        };

        await middleware.InvokeAsync(context);

        Assert.True(nextCalled);
    }
}
