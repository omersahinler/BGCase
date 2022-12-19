using FluentValidation;
using StockAPI.Application.Cqrs.Commands.AuthCommands;

namespace StockAPI.Application.Validators.AuthValidators;

public class CreateTokenByRefreshTokenValidator : AbstractValidator<CreateTokenByRefreshTokenCommand>
{
	public CreateTokenByRefreshTokenValidator()
	{
		RuleFor(x => x.RefreshToken)
			.NotEmpty();
	}
}