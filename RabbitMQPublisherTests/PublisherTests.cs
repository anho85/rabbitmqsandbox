using FluentAssertions;
using Xunit;

namespace RabbitMQPublisherTests
{
    public class PublisherTests
    {
        [Fact]
        public void Constructor_Empty_FactoryNotInitialized()
        {
            var sut = new RabbitMQPublisher.Implementation.RabbitMQPublisher();

            //sut._factory.Should().BeNull();

        }
    }
}
