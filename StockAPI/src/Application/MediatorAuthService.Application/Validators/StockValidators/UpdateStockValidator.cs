using FluentValidation;
using StockAPI.Application.Cqrs.Commands.StockCommands;

namespace StockAPI.Application.Validators.StockValidators;

public class UpdateStockValidator : AbstractValidator<UpdateStockCommand>
{
    public UpdateStockValidator()
    {
        RuleFor(x => x.VariantCode)
            .MaximumLength(5)
            .NotEmpty()
            .WithMessage("VariantCode boş geçilemez 5 karakterden büyük olamaz");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Quantity boş geçilemez");
    }
}