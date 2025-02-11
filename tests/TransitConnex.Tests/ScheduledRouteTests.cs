﻿using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using TransitConnex.API;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Query.Queries;
using TransitConnex.Tests.Infrastructure;
using TransitConnex.TestSeeds.SqlSeeds;
using Xunit.Abstractions;

namespace TransitConnex.Tests;

[Collection("NonParallelTests")]
public class ScheduledRouteTests(
    ITestOutputHelper testOutputHelper,
    ApiWebApplicationFactory<Program> fixture
) : APITestsBase(testOutputHelper, fixture)
{
    private const string Endpoint = "/api";

    [Fact]
    public async Task GET_Scheduled_Routes_OK()
    {
        // Arrange
        await PerformLogin(UserSeed.BasicLogin);
        var dbFirstSr = ScheduledRouteSeed.ScheduledRoutes.First();
        var searchTime = dbFirstSr.StartTime.AddMinutes(-5);
        var scheduledRouteQuery = new ScheduledRouteGetAllQuery(
            UserSeed.BasicUser.Id,
            LocationSeed.BrnoCityLocation.Id,
            LocationSeed.PrerovCityLocation.Id,
            searchTime);
        var queryParams = new Dictionary<string, string?>
        {
            { "startLocationId", scheduledRouteQuery.StartLocationId.ToString() },
            { "endLocationId", scheduledRouteQuery.EndLocationId.ToString() },
            { "startTime", scheduledRouteQuery.StartTime.ToString() },
        };
        string url = QueryHelpers.AddQueryString($"{Endpoint}/ScheduledRoute/GetScheduledRoutes", queryParams);

        // Act
        var response = await Client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var scheduledRoutes = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduledRouteDto>>();

        // Assert
        Assert.NotNull(scheduledRoutes);
        Assert.NotEmpty(scheduledRoutes);
        Assert.Equal(ScheduledRouteSeed.ScheduledRoutes.Count, scheduledRoutes.Count());

        var firstSr = scheduledRoutes.First();
        Assert.Equal(dbFirstSr.Id, firstSr.Id);
        Assert.True(searchTime < firstSr.StartTime);
    }

    [Fact]
    public async Task GET_Second_Scheduled_Route_OK()
    {
        // Arrange
        await PerformLogin(UserSeed.BasicLogin);
        var dbSecondSr = ScheduledRouteSeed.ScheduledRoutes.ElementAt(1);
        var searchTime = dbSecondSr.StartTime.AddMinutes(-5);
        var scheduledRouteQuery = new ScheduledRouteGetAllQuery(
            UserSeed.BasicUser.Id,
            LocationSeed.BrnoCityLocation.Id,
            LocationSeed.PrerovCityLocation.Id,
            searchTime);
        var queryParams = new Dictionary<string, string?>
        {
            { "startLocationId", scheduledRouteQuery.StartLocationId.ToString() },
            { "endLocationId", scheduledRouteQuery.EndLocationId.ToString() },
            { "startTime", scheduledRouteQuery.StartTime.ToString() },
        };
        string url = QueryHelpers.AddQueryString($"{Endpoint}/ScheduledRoute/GetScheduledRoutes", queryParams);

        // Act
        var response = await Client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var scheduledRoutes = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduledRouteDto>>();

        // Assert
        Assert.NotNull(scheduledRoutes);
        Assert.NotEmpty(scheduledRoutes);
        Assert.Single(scheduledRoutes);

        var firstSr = scheduledRoutes.First();
        Assert.Equal(dbSecondSr.Id, firstSr.Id);
        Assert.True(searchTime < firstSr.StartTime);
    }

    [Fact]
    public async Task GET_Scheduled_Routes_Should_be_Empty_OK()
    {
        // Arrange
        await PerformLogin(UserSeed.BasicLogin);
        var dbLastSr = ScheduledRouteSeed.ScheduledRoutes
            .OrderBy(x => x.StartTime)
            .Last();
        var searchTime = dbLastSr.StartTime.AddMinutes(5);
        var query = new ScheduledRouteGetAllQuery(
            UserSeed.BasicUser.Id,
            LocationSeed.BrnoCityLocation.Id,
            LocationSeed.PrerovCityLocation.Id,
            searchTime);
        var queryParams = new Dictionary<string, string?>
        {
            { "startLocationId", query.StartLocationId.ToString() },
            { "endLocationId", query.EndLocationId.ToString() },
            { "startTime", query.StartTime.ToString() },
        };
        string url = QueryHelpers.AddQueryString($"{Endpoint}/ScheduledRoute/GetScheduledRoutes", queryParams);

        // Act
        var response = await Client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var scheduledRoutes = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduledRouteDto>>();

        // Assert
        Assert.NotNull(scheduledRoutes);
        Assert.Empty(scheduledRoutes);
    }
}
