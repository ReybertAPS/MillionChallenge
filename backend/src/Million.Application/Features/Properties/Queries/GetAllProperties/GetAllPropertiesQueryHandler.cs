using MediatR;
using Million.Application.Features.Properties.Dtos;
using Million.Application.Shared.Contracts.Infrastructure.Persistence.Repositories;
using Million.Application.Shared.Models;

namespace Million.Application.Features.Properties.Queries.GetAllProperties;

public class GetAllPropertiesQueryHandler
    : IRequestHandler<GetAllPropertiesQuery, PagedResponse<PropertyDto>>
{
    private readonly IPropertyRepository _repository;

    public GetAllPropertiesQueryHandler(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResponse<PropertyDto>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
    {
        var (properties, totalCount) = await _repository
            .GetAllPaginatedAsync(request.PageIndex, request.PageSize);

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
