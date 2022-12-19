using FluentValidation;
using StockAPI.Application.Cqrs.Queries.AuthQueries;

namespace StockAPI.Application.Validators.AuthValidators;

public class CreateTokenValidator : AbstractValidator<CreateTokenQuery>
{
	public CreateTokenValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty();

		RuleFor(x => x.Password)
			.NotEmpty();
	}
}