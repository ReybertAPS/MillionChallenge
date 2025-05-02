using MediatR;
using Million.Application.Features.Properties.Dtos;
using Million.Application.Shared.Models;

namespace Million.Application.Features.Properties.Queries.GetFilteredProperties;

public record GetFilteredPropertiesQuery(
    string? Name,
    string? Address,
    decimal? MinPrice,
    decimal? MaxPrice,
    int PageIndex = 1,
    int PageSize = 10
) : IRequest<PagedResponse<PropertyDto>>;
