using FluentAssertions;
using Xunit;

namespace Orion.Starter.Api.Tests;

public sealed class SmokeTests
{
    [Fact]
    public void Api_placeholder_test_should_pass()
    {
        true.Should().BeTrue();
    }
}
