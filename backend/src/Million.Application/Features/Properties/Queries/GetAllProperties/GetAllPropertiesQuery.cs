using MediatR;
using Million.Application.Features.Properties.Dtos;
using Million.Application.Shared.Models;

namespace Million.Application.Features.Properties.Queries.GetAllProperties;

public record GetAllPropertiesQuery(int PageIndex = 1, int PageSize = 10)
    : IRequest<PagedResponse<PropertyDto>>;
