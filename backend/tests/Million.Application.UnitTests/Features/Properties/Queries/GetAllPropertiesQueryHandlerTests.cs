using FluentAssertions;
using Million.Application.Features.Properties.Queries.GetAllProperties;
using Million.Application.Shared.Contracts.Infrastructure.Persistence.Repositories;
using Million.Domain.Entities;
using Moq;

namespace Million.Application.UnitTests.Features.Properties.Queries;

public class GetAllPropertiesQueryHandlerTests
{
    private readonly Mock<IPropertyRepository> _repositoryMock;
    private readonly GetAllPropertiesQueryHandler _handler;

    public GetAllPropertiesQueryHandlerTests()
    {
        _repositoryMock = new Mock<IPropertyRepository>();
        _handler = new GetAllPropertiesQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsPagedResponse_WhenPropertiesExist()
    {
        // Arrange
        var properties = new List<Property>
        {
            new()
            {
                Id = "1",
                Name = "Apartamento Norte",
                Address = "Cra 45 #10-22",
                Price = 250000000,
                Year = 2021,
                CodeInternal = "APT1",
                IdOwner = "OWNER001",
                Images = new List<PropertyImage>
                {
                    new() { File = "img1.jpg", Enabled = true, IsMain = true }
                }
            }
        };

        _repositoryMock
            .Setup(r => r.GetAllPaginatedAsync(1, 10))
            .ReturnsAsync((properties, properties.Count));

        var query = new GetAllPropertiesQuery(PageIndex: 1, PageSize: 10);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.TotalRecords.Should().Be(1);
        result.Data.First().ImageUrl.Should().Be("img1.jpg");
    }

    [Fact]
    public async Task Handle_ReturnsEmptyResponse_WhenNoPropertiesExist()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.GetAllPaginatedAsync(1, 10))
            .ReturnsAsync((new List<Property>(), 0));

        var query = new GetAllPropertiesQuery(PageIndex: 1, PageSize: 10);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().BeEmpty();
        result.TotalRecords.Should().Be(0);
    }
}
