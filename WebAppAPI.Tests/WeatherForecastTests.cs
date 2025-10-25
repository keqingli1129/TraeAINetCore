using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace WebAppAPI.Tests;

public class WeatherForecastIntegrationTests
{
    private WebApplicationFactory<Program>? _factory;
    private HttpClient? _client;

    [SetUp]
    public void SetUp()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("http://localhost")
        });
    }

    [TearDown]
    public void TearDown()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }

    [Test]
    public async Task WeatherForecast_Returns_Five_Items_With_Expected_Properties()
    {
        Assert.That(_client, Is.Not.Null);
        var response = await _client!.GetAsync("/weatherforecast");
        response.EnsureSuccessStatusCode();

        await using var contentStream = await response.Content.ReadAsStreamAsync();
        using var jsonDoc = await JsonDocument.ParseAsync(contentStream);

        Assert.That(jsonDoc.RootElement.ValueKind, Is.EqualTo(JsonValueKind.Array));
        Assert.That(jsonDoc.RootElement.GetArrayLength(), Is.EqualTo(5));

        var first = jsonDoc.RootElement[0];
        Assert.That(first.TryGetProperty("date", out _), Is.True);
        Assert.That(first.TryGetProperty("temperatureC", out _), Is.True);
        Assert.That(first.TryGetProperty("temperatureF", out _), Is.True);
        Assert.That(first.TryGetProperty("summary", out _), Is.True);
    }
}