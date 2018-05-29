using FluentAssertions;
using OSRabbitMQPublisher.Implementation;
using System;
using Xunit;

namespace RabbitMQPublisherTests
{
    public class PublisherTests
    {
        private static readonly string rabbitMQUrl = "localhost:5672";
        [Fact]
        public void Constructor_Empty_FactoryNotInitialized()
        {
            var sut = new RabbitMQPublisher();

            sut._factory.Should().BeNull("because of empty constructor");
        }

        [Fact]
        public void Constructor_WithValue_ShouldInitFactory()
        {
            var sut = new RabbitMQPublisher(new Uri(rabbitMQUrl));

            sut._factory.Should().NotBeNull("because factory was initialised");
            sut._rabbitMQUrl.Should().Be(new Uri(rabbitMQUrl), "because url was passed");
        }

        [Fact]
        public void Connect_WithoutSetUrl_ShouldThrowException()
        {
            //Arrange
            var sut = new RabbitMQPublisher();
            //Act
            Action act = () => sut.Connect();
            //Assert
            act.Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void Connect_WithSetUrl_ShouldConnect()
        {
            //Arrange
            var sut = new RabbitMQPublisher(new Uri(rabbitMQUrl));
            //Act
            sut.Connect();
            //Assert
            sut._connection.Should().NotBeNull();
        }

        [Fact]
        public void Connect_WithAlreadyExistingConnectin_ShouldCreateConnection()
        {
            //Arrange
            var sut = new RabbitMQPublisher(new Uri(rabbitMQUrl));
            sut.Connect();
            //Act
            sut.Connect();
            //Assert
            sut._connection.Should().NotBeNull();
        }

        [Fact] 
        public void Publish_StringMessage_ShouldSendWithoutException()
        {
            //Arrange
            var message = "Hello from Publish Test";
            var sut = new RabbitMQPublisher(new Uri(rabbitMQUrl));
            sut.Connect();
            //Act
            Action act = () => sut.Publish(message,"TestChannel");
            //Assert
            act.Should().NotThrow<Exception>();
        }
    }
}
