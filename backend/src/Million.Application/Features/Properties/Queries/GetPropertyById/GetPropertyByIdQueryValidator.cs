using FluentValidation;
using MongoDB.Bson;

namespace Million.Application.Features.Properties.Queries.GetPropertyById;

public class GetPropertyByIdQueryValidator : AbstractValidator<GetPropertyByIdQuery>
{
    public GetPropertyByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id no puede estar vacío.")
            .Must(BeValidObjectId).WithMessage("El Id no tiene un formato válido de ObjectId.");
    }

    private bool BeValidObjectId(string id)
    {
        return ObjectId.TryParse(id, out _);
    }
}
