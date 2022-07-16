using FluentValidation;

namespace LoadTester.Domain.Validation;

internal class TargetConfigurationValidator : AbstractValidator<TargetConfiguration>
{
    internal TargetConfigurationValidator()
    {
        RuleFor(config => config.TestDuration).GreaterThan(TimeSpan.FromSeconds(1));
        RuleFor(config => config.RequestPerSecond).GreaterThanOrEqualTo(1);
        RuleFor(config => config.TargetUri).NotNull();
    }
}