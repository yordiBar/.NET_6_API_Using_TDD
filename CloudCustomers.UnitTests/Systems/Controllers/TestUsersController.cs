using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsCode200()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();

        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>()
            {
                new()
                {
                    Id = 1,
                    Name = "Jane",
                    Address = new Address()
                    {
                        Street = "Main Rd",
                        City = "Dublin",
                        Zipcode = "1234"
                    },
                    Email = "test@test.com"
                }
            });

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = (OkObjectResult)await sut.Get();

        // Assert
        result.StatusCode.Should().Be(200);

    }

    [Fact]
    public async Task Get_OnSuccess_InvokeUserServiceExactlyOnce()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();

        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = await sut.Get();

        // Assert
        mockUsersService.Verify(
            service => service.GetAllUsers(), 
            Times.Once());
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnListOfUsers()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();

        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>()
            {
                new()
                {
                    Id = 1,
                    Name = "Jane",
                    Address = new Address()
                    {
                        Street = "Main Rd",
                        City = "Dublin",
                        Zipcode = "1234"
                    },
                    Email = "test@test.com"
                }
            });

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = await sut.Get();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNoUsersFound_Returns404()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();

        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = await sut.Get();

        // Assert
        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);
    }
}