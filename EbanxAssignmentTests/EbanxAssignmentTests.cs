﻿using System.Net;
using System.Text;
using System.Text.Json;
using Application;
using FluentAssertions;

namespace EbanxAssignmentTests;

public class EbanxAssignmentTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory webApplication;

    public EbanxAssignmentTests(CustomWebApplicationFactory customWebApplication)
    {
        webApplication = customWebApplication;
    }
    
    [Fact, TestPriority(1)]
    public async Task ResetState_ReturnsOk()
    {
        //Arrange
        var httpClient = webApplication.CreateClient();
        
        //Act
        var response = await httpClient.PostAsync($"http://localhost:5192/reset", null);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact, TestPriority(2)]
    public async Task GetBalance_NonExistingAccount_ReturnsNotFound()
    {
        //Arrange
        var httpClient = webApplication.CreateClient();
        
        //Act
        var response = await httpClient.GetAsync($"http://localhost:5192/balance?account_id=1234");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound); 
        (await response.Content.ReadAsStringAsync()).Should().Be("0");
    }

    [Fact, TestPriority(3)]
    public async Task CreateAccount_WithInitialBalance_ReturnsCreated()
    {
        //Arrange
        var httpClient = webApplication.CreateClient();
        var payload = new { type = "deposit", destination = "100", amount = 10 };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        
        //Act
        var response = await httpClient.PostAsync($"http://localhost:5192/event", content);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<DTOBankAccountResponse>(
            responseContent, 
            new JsonSerializerOptions 
                {
                    PropertyNameCaseInsensitive = true
                });
        Assert.Equal("100", result.Destination.Id);
        Assert.Equal(10, result.Destination.Balance);
    }

    [Fact, TestPriority(4)]
    public async Task Deposit_ExistingAccount_ReturnsUpdatedBalance()
    {
        //Arrange
        var httpClient = webApplication.CreateClient();
        var payload = new { type = "deposit", destination = "100", amount = 10 };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        
        //Act
        var response = await httpClient.PostAsync($"http://localhost:5192/event", content);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<DTOBankAccountResponse>(
            responseContent, 
            new JsonSerializerOptions 
            {
                PropertyNameCaseInsensitive = true
            });
        Assert.Equal("100", result.Destination.Id);
        Assert.Equal(20, result.Destination.Balance);
    }

    [Fact, TestPriority(5)]
    public async Task GetBalance_ExistingAccount_ReturnsBalance()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        
        //Act
        var response = await httpClient.GetAsync($"http://localhost:5192/balance?account_id=100");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Should().Be(20);
    }

    [Fact, TestPriority(6)]
    public async Task Withdraw_NonExistingAccount_ReturnsNotFound()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        var payload = new { type = "withdraw", origin = "200", amount = 10 };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        
        //Act
        var response = await httpClient.PostAsync($"http://localhost:5192/event", content);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Content.Should().Be(0);
    }

    [Fact, TestPriority(7)]
    public async Task Withdraw_ExistingAccount_ReturnsUpdatedBalance()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        var payload = new { type = "withdraw", origin = "100", amount = 5 };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        
        //Act
        var response = await httpClient.PostAsync($"http://localhost:5192/event", content);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<dynamic>(responseContent);
        Assert.Equal("100", (string)result.origin.id);
        Assert.Equal(15, (int)result.origin.balance);
    }

    [Fact, TestPriority(8)]
    public async Task Transfer_ExistingAccount_ReturnsUpdatedBalances()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        var payload = new { type = "transfer", origin = "100", amount = 15, destination = "300" };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        
        //Act
        var response = await httpClient.PostAsync($"http://localhost:5192/event", content);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<dynamic>(responseContent);
        Assert.Equal("100", (string)result.origin.id);
        Assert.Equal(0, (int)result.origin.balance);
        Assert.Equal("300", (string)result.destination.id);
        Assert.Equal(15, (int)result.destination.balance);
    }

    [Fact, TestPriority(9)]
    public async Task Transfer_NonExistingAccount_ReturnsNotFound()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        var payload = new { type = "transfer", origin = "200", amount = 15, destination = "300" };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        
        //Act
        var response = await httpClient.PostAsync($"http://localhost:5192/event", content);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Content.Should().Be(0);
    }
}