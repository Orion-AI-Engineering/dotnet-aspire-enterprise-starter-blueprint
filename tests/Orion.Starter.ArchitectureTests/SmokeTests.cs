using FluentAssertions;
using Xunit;

namespace Orion.Starter.ArchitectureTests;

public sealed class SmokeTests
{
    [Fact]
    public void Architecture_placeholder_test_should_pass()
    {
        true.Should().BeTrue();
    }
}
