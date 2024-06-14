using Xunit;
using Moq;
using LoyaltySystemAPI.Controllers;
using LoyaltySystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace LoyaltySystemAPI.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly UsersController _controller;
        private readonly Mock<LoyaltyContext> _contextMock;
        private readonly Mock<IValidator<EarnPointsRequest>> _validatorMock;

        public UsersControllerTests()
        {
            var options = new DbContextOptionsBuilder<LoyaltyContext>()
                .UseInMemoryDatabase(databaseName: "LoyaltyTestDb")
                .Options;
            var context = new LoyaltyContext(options);
            _contextMock = new Mock<LoyaltyContext>(options);
            _validatorMock = new Mock<IValidator<EarnPointsRequest>>();
            _controller = new UsersController(context, _validatorMock.Object);
        }

        [Fact]
        public async Task EarnPoints_UserExists_AddsPoints()
        {
            // Arrange
            var user = new User { Id = 1, Points = 10 };
            await _controller.Context.Users.AddAsync(user);
            await _controller.Context.SaveChangesAsync();

            var request = new EarnPointsRequest { Points = 5 };
            _validatorMock.Setup(v => v.ValidateAsync(request, default))
                          .ReturnsAsync(new ValidationResult());

            // Act
            var result = await _controller.EarnPoints(1, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(15, returnedUser.Points);
        }

        [Fact]
        public async Task EarnPoints_UserDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var request = new EarnPointsRequest { Points = 5 };
            _validatorMock.Setup(v => v.ValidateAsync(request, default))
                          .ReturnsAsync(new ValidationResult());

            // Act
            var result = await _controller.EarnPoints(999, request);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EarnPoints_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var request = new EarnPointsRequest { Points = -5 };
            var validationResult = new ValidationResult(new[] {
                new ValidationFailure("Points", "Points must be greater than zero.")
            });

            _validatorMock.Setup(v => v.ValidateAsync(request, default))
                          .ReturnsAsync(validationResult);

            // Act
            var result = await _controller.EarnPoints(1, request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var error = Assert.IsType<ValidationResult>(badRequestResult.Value);
            Assert.Equal("Points must be greater than zero.", error.Errors[0].ErrorMessage);
        }
    }
}
