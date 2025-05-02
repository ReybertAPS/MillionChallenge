using MediatR;
using Million.Application.Features.Properties.Dtos;

namespace Million.Application.Features.Properties.Queries.GetPropertyById;

public record GetPropertyByIdQuery(string Id) : IRequest<PropertyDto>;
