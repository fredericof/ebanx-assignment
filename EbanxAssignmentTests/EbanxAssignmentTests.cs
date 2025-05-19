using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;

namespace EbanxAssignmentTests;

public class EbanxAssignmentTests
{
    [Fact]
    public async Task ResetState_ReturnsOk()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        
        //Act
        var response = await httpClient.PostAsync($"http://localhost:5192/reset", null);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GetBalance_NonExistingAccount_ReturnsNotFound()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        
        //Act
        var response = await httpClient.GetAsync($"http://localhost:5192/balance?account_id=1234");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Content.Should().Be(0);
    }

    [Fact]
    public async Task CreateAccount_WithInitialBalance_ReturnsCreated()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        var payload = new { type = "deposit", destination = "100", amount = 10 };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        
        //Act
        var response = await httpClient.PostAsync($"http://localhost:5192/event", content);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<dynamic>(responseContent);
        Assert.Equal("100", (string)result.destination.id);
        Assert.Equal(10, (int)result.destination.balance);
    }

    [Fact]
    public async Task Deposit_ExistingAccount_ReturnsUpdatedBalance()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        
        //Act
        
        //Assert
    }

    [Fact]
    public async Task GetBalance_ExistingAccount_ReturnsBalance()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        
        //Act
        
        //Assert
    }

    [Fact]
    public async Task Withdraw_NonExistingAccount_ReturnsNotFound()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        
        //Act
        
        //Assert
    }

    [Fact]
    public async Task Withdraw_ExistingAccount_ReturnsUpdatedBalance()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        
        //Act
        
        //Assert
    }

    [Fact]
    public async Task Transfer_ExistingAccount_ReturnsUpdatedBalances()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        
        //Act
        
        //Assert
    }

    [Fact]
    public async Task Transfer_NonExistingAccount_ReturnsNotFound()
    {
        //Arrange
        await using var webApplication = new CustomWebApplicationFactory();
        using var httpClient = webApplication.CreateClient();
        
        //Act
        
        //Assert
    }
}