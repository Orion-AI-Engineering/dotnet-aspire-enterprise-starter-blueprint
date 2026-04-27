using FluentAssertions;
using Xunit;

namespace Orion.Starter.Worker.Tests;

public sealed class SmokeTests
{
    [Fact]
    public void Worker_placeholder_test_should_pass()
    {
        true.Should().BeTrue();
    }
}
