using Xunit;
using Moq;
using FluentAssertions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using server.Services.Implementations;
using server.Repositories.Interfaces;
using server.Models;
using server.DTOs;

namespace SERVER.Tests.Services
{
    public class GiftServiceTests
    {
        private readonly Mock<IGiftRepository> _giftRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<GiftService>> _loggerMock;
        private readonly GiftService _giftService;

        public GiftServiceTests()
        {
            _giftRepoMock = new Mock<IGiftRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<GiftService>>();

            _giftService = new GiftService(
                _giftRepoMock.Object,
                _loggerMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task GetGiftByIdAsync_WhenGiftExists_ReturnsGiftResponseDto()
        {
            // Arrange
            var giftId = 1;

            var giftModel = new GiftModel
            {
                Id = giftId,
                Description = "Test Gift",
                Price = 100
            };

            var giftDto = new GiftResponseDto
            {
                Id = giftId,
                Description = "Test Gift",
                Price = 100
            };

            _giftRepoMock
                .Setup(r => r.GetGiftByIdAsync(giftId))
                .ReturnsAsync(giftModel);

            _mapperMock
                .Setup(m => m.Map<GiftResponseDto>(giftModel))
                .Returns(giftDto);

            // Act
            var result = await _giftService.GetGiftByIdAsync(giftId);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(giftId);
            result.Description.Should().Be("Test Gift");
            result.Price.Should().Be(100);
        }
    

    [Fact]
public async Task DeleteGiftAsync_WhenGiftHasPurchases_ShouldThrowInvalidOperationException()
{
    // Arrange
    var giftId = 5;

    _giftRepoMock
        .Setup(r => r.HasPurchasesAsync(giftId))
        .ReturnsAsync(true);

    // Act
    Func<Task> act = async () => await _giftService.DeleteGiftAsync(giftId);

    // Assert
    await act.Should()
        .ThrowAsync<InvalidOperationException>()
        .WithMessage("*associated purchases*");
}

}
}