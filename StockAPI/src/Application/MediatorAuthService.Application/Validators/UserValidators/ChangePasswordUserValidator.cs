using FluentValidation;
using StockAPI.Application.Cqrs.Commands.UserCommands;

namespace StockAPI.Application.Validators.UserValidators;

public class ChangePasswordUserValidator : AbstractValidator<ChangePasswordUserCommand>
{
	public ChangePasswordUserValidator()
	{
		RuleFor(x => x.OldPassword)
			.NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty();
    }
}