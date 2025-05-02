using FluentAssertions;
using Million.Application.Features.Properties.Queries.GetPropertyById;
using Million.Application.Shared.Contracts.Infrastructure.Persistence.Repositories;
using Million.Domain.Entities;
using Moq;

namespace Million.Application.UnitTests.Features.Properties.Queries;

public class GetPropertyByIdQueryHandlerTests
{
    private readonly Mock<IPropertyRepository> _repositoryMock;
    private readonly GetPropertyByIdQueryHandler _handler;

    public GetPropertyByIdQueryHandlerTests()
    {
        _repositoryMock = new Mock<IPropertyRepository>();
        _handler = new GetPropertyByIdQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsProperty_WhenIdIsValid()
    {
        // Arrange
        var property = new Property
        {
            Id = "PROP001",
            Name = "Apartamento Norte",
            Address = "Cra 45 #10-22",
            Price = 250000000,
            CodeInternal = "APT1",
            Year = 2021,
            IdOwner = "OWNER001",
            Images = new List<PropertyImage>
            {
                new() { File = "apt1.jpg", Enabled = true, IsMain = true }
            }
        };

        _repositoryMock
            .Setup(r => r.GetByIdAsync("PROP001"))
            .ReturnsAsync(property);

        var query = new GetPropertyByIdQuery("PROP001");

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Apartamento Norte");
        result.ImageUrl.Should().Be("apt1.jpg");
    }

    [Fact]
    public async Task Handle_ThrowsKeyNotFoundException_WhenPropertyNotFound()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.GetByIdAsync("INVALID"))
            .ReturnsAsync((Property?)null);

        var query = new GetPropertyByIdQuery("INVALID");

        // Act
        var act = async () => await _handler.Handle(query, default);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("Property with Id 'INVALID' was not found.");
    }
}
