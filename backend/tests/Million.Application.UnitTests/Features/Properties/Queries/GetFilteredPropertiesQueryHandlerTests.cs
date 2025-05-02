using FluentAssertions;
using Moq;
using Xunit;
using Million.Application.Features.Properties.Queries;
using Million.Application.Shared.Contracts.Infrastructure.Persistence.Repositories;
using Million.Domain.Entities;
using Million.Application.Features.Properties.Queries.GetFilteredProperties;

namespace Million.Application.UnitTests.Features.Properties.Queries;

public class GetFilteredPropertiesQueryHandlerTests
{
    private readonly Mock<IPropertyRepository> _repositoryMock;
    private readonly GetFilteredPropertiesQueryHandler _handler;

    public GetFilteredPropertiesQueryHandlerTests()
    {
        _repositoryMock = new Mock<IPropertyRepository>();
        _handler = new GetFilteredPropertiesQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsFilteredProperties_WhenMatchesExist()
    {
        // Arrange
        var filtered = new List<Property>
        {
            new()
            {
                Id = "2",
                Name = "Apartamento Sur",
                Address = "Av. Sur #30-15",
                Price = 210000000,
                Year = 2022,
                CodeInternal = "APT3",
                IdOwner = "OWNER003",
                Images = new List<PropertyImage>
                {
                    new() { File = "sur.jpg", Enabled = true, IsMain = true }
                }
            }
        };

        _repositoryMock
            .Setup(r => r.GetFilteredPaginatedAsync("sur", null, null, null, 1, 10))
            .ReturnsAsync((filtered, filtered.Count));

        var query = new GetFilteredPropertiesQuery
        (
            Name: "sur",
            Address: null,
            MinPrice: null,
            MaxPrice: null,
            PageIndex: 1,
            PageSize: 10
        );

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.Data.First().Name.Should().Be("Apartamento Sur");
        result.Data.First().ImageUrl.Should().Be("sur.jpg");
    }

    [Fact]
    public async Task Handle_ReturnsEmpty_WhenNoMatches()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.GetFilteredPaginatedAsync("x", "z", 0, 0, 1, 10))
            .ReturnsAsync((new List<Property>(), 0));

        var query = new GetFilteredPropertiesQuery
        (
            Name: "x",
            Address: "z",
            MinPrice: 0,
            MaxPrice: 0,
            PageIndex: 1,
            PageSize: 10
        );

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEmpty();
        result.TotalRecords.Should().Be(0);
    }
}
