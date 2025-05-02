using MediatR;
using Million.Application.Features.Properties.Dtos;
using Million.Application.Shared.Contracts.Infrastructure.Persistence.Repositories;

namespace Million.Application.Features.Properties.Queries.GetPropertyById;

public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, PropertyDto>
{
    private readonly IPropertyRepository _repository;

    public GetPropertyByIdQueryHandler(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<PropertyDto> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        var property = await _repository.GetByIdAsync(request.Id);

        if (property == null)
            throw new KeyNotFoundException($"Property with Id '{request.Id}' was not found.");

        return new PropertyDto
        {
            Id = property.Id,
            IdOwner = property.IdOwner,
            Name = property.Name,
            Address = property.Address,
            Price = property.Price,
            CodeInternal = property.CodeInternal,
            Year = property.Year,
            ImageUrl = property.Images.FirstOrDefault(i => i.IsMain && i.Enabled)?.File,
            Images = property.Images
                .Where(i => i.Enabled)
                .Select(i => new PropertyImageDto
                {
                    File = i.File,
                    IsMain = i.IsMain
                }).ToList()
        };
    }
}
