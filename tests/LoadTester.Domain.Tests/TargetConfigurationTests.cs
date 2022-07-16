using FluentAssertions;

namespace LoadTester.Domain.Tests;

public class TargetConfigurationTests
{
    private readonly Uri validUri = new("https://foo.bar");
    private const int ValidRequestsPerSecond = 10;
    private readonly TimeSpan validTestDuration = TimeSpan.FromSeconds(20);

    [Fact]
    public void Create_ValidParameters_DoesNotThrow()
    {
        // Arrange
        Action getSut = () => TargetConfiguration.Create(this.validUri, ValidRequestsPerSecond, this.validTestDuration);

        // Assert
        getSut.Should().NotThrow();
    }

    [Fact]
    public void Create_InvalidTestDuration_ThrowsArgumentException()
    {
        // Arrange
        var invalidTestDuration = TimeSpan.FromMilliseconds(50);

        Action getSut = () => TargetConfiguration.Create(this.validUri, ValidRequestsPerSecond, invalidTestDuration);
        
        // Assert
        getSut.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_InvalidUri_ThrowsArgumentException()
    {
        // Arrange
        Uri invalidUri = null!;

        Action getSut = () => TargetConfiguration.Create(invalidUri, ValidRequestsPerSecond, this.validTestDuration);

        // Assert
        getSut.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_InvalidRequestsPerSecond_ThrowsArgumentException()
    {
        // Arrange
        const int invalidRequestsPerSecond = 0;

        Action getSut = () => TargetConfiguration.Create(this.validUri, invalidRequestsPerSecond, this.validTestDuration);

        // Assert
        getSut.Should().Throw<ArgumentException>();
    }
}