namespace SomeProject.Specs
{
    using FluentAssertions;
    using Xunit;

    public class SomethingSpecs
    {
        [Fact]
        public void Something_Should_Be_Alwasys_Something()
        {
            this.Should().Be(this);
        }
    }
}
