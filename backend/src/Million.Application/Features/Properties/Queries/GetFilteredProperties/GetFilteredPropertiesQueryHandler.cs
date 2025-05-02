using MediatR;
using Million.Application.Shared.Models;
using Million.Application.Shared.Contracts.Infrastructure.Persistence.Repositories;
using Million.Application.Features.Properties.Dtos;

namespace Million.Application.Features.Properties.Queries.GetFilteredProperties;

public class GetFilteredPropertiesQueryHandler
    : IRequestHandler<GetFilteredPropertiesQuery, PagedResponse<PropertyDto>>
{
    private readonly IPropertyRepository _repository;

    public GetFilteredPropertiesQueryHandler(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResponse<PropertyDto>> Handle(GetFilteredPropertiesQuery request, CancellationToken cancellationToken)
    {
        var (properties, totalCount) = await _repository.GetFilteredPaginatedAsync(
            request.Name,
            request.Address,
            request.MinPrice,
            request.MaxPrice,
            request.PageIndex,
            request.PageSize
        );

        var data = properties.Select(p => new PropertyDto
        {
            Id = p.Id,
            IdOwner = p.IdOwner,
            Name = p.Name,
            Address = p.Address,
            Price = p.Price,
            ImageUrl = p.Images.FirstOrDefault(i => i.IsMain && i.Enabled)?.File
        });

        return new PagedResponse<PropertyDto>(data, request.PageIndex, request.PageSize, totalCount);
    }
}
