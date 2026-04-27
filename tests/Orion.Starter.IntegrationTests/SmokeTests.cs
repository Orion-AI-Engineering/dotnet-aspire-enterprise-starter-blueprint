using FluentAssertions;
using Xunit;

namespace Orion.Starter.IntegrationTests;

public sealed class SmokeTests
{
    [Fact]
    public void Integration_placeholder_test_should_pass()
    {
        true.Should().BeTrue();
    }
}
