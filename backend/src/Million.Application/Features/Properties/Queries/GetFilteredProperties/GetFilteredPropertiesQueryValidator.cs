using FluentValidation;

namespace Million.Application.Features.Properties.Queries.GetFilteredProperties;

public class GetFilteredPropertiesQueryValidator : AbstractValidator<GetFilteredPropertiesQuery>
{
    public GetFilteredPropertiesQueryValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .When(x => !string.IsNullOrWhiteSpace(x.Name))
            .WithMessage("El nombre debe tener al menos 3 caracteres.");

        RuleFor(x => x.Address)
            .MinimumLength(3)
            .When(x => !string.IsNullOrWhiteSpace(x.Address))
            .WithMessage("La dirección debe tener al menos 3 caracteres.");

        RuleFor(x => x)
            .Must(x =>
                !x.MinPrice.HasValue || !x.MaxPrice.HasValue || x.MinPrice <= x.MaxPrice
            )
            .WithMessage("El precio mínimo no puede ser mayor al precio máximo.");
    }
}
