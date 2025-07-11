﻿using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.Web.Controllers;

[Collection("Sequential")]
public class OrderIndexOnGet : IClassFixture<TestApplication>
{
    public OrderIndexOnGet(TestApplication factory)
    {
        Client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    public HttpClient Client { get; }

    [Fact]
    public async Task ReturnsRedirectGivenAnonymousUser()
    {
        var response = await Client.GetAsync("/order/my-orders", TestContext.Current.CancellationToken);
        var redirectLocation = response!.Headers.Location!.OriginalString;

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Contains("/Account/Login", redirectLocation);
    }
}
