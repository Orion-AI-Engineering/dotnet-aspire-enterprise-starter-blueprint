using FluentAssertions;
using Xunit;

namespace Orion.Starter.Web.Tests;

public sealed class SmokeTests
{
    [Fact]
    public void Web_placeholder_test_should_pass()
    {
        true.Should().BeTrue();
    }
}
