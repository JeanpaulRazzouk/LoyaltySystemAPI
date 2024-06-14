public class EarnPointsRequestValidator : AbstractValidator<EarnPointsRequest>
{
    public EarnPointsRequestValidator()
    {
        RuleFor(x => x.Points).GreaterThan(0);
    }
}

// In Program.cs, add FluentValidation
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EarnPointsRequestValidator>());
