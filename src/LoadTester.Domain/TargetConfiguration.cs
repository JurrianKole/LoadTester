using LoadTester.Domain.Validation;

namespace LoadTester.Domain;

public class TargetConfiguration
{
    private TargetConfiguration(Uri targetUri, int requestPerSecond, TimeSpan testDuration)
    {
        this.TargetUri = targetUri;
        this.RequestPerSecond = requestPerSecond;
        this.TestDuration = testDuration;

        var validator = new TargetConfigurationValidator();

        var validationResult = validator.Validate(this);

        if (!validationResult.IsValid)
        {
            throw new ArgumentException(validationResult.ToString());
        }
    }

    public Uri TargetUri { get; }

    public int RequestPerSecond { get; }

    public TimeSpan TestDuration { get;}

    public static TargetConfiguration Create(Uri targetUri, int requestsPerSecond, TimeSpan testDuration)
    {
        return new TargetConfiguration(targetUri, requestsPerSecond, testDuration);
    }
}
