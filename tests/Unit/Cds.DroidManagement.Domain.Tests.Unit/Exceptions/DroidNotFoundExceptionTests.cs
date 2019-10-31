﻿using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Cds.DroidManagement.Domain.DroidAggregate.Exceptions;
using FluentAssertions;
using Xunit;

namespace Cds.DroidManagement.Domain.Tests.Unit.Exceptions
{
    public class DroidNotFoundExceptionTests
    {
        private readonly string _defaultMessage = "Droid not found";

        [Fact]
        public void Constructor_Empty_ShouldSetDefaultMessage()
        {
            // Act
            var exception = new DroidNotFoundException();

            // Assert
            exception.Message.Should().Be(_defaultMessage);
        }
        
        [Fact]
        public void Constructor_WithMessage_ShouldSetMessage()
        {
            // Arrange
            var message = "Test Message";

            // Act
            var exception = new DroidNotFoundException(message);

            // Assert
            exception.Message.Should().Be(message);
        }
        
        [Fact]
        public void Constructor_WithMessageAndInnerException_ShouldSetBoth()
        {
            // Arrange
            var message = "Test Message";
            var innerException = new Exception("Inner Test Exception");

            // Act
            var exception = new DroidNotFoundException(message, innerException);

            // Assert
            exception.Message.Should().Be(message);
            exception.InnerException.Should().Be(innerException);
        }
        
        [Fact]
        public void Constructor_WithSerializationInfoAndStreamingContext_ShouldSetBoth()
        {
            // Arrange
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            var beforeException = new DroidNotFoundException();

            // Act
            formatter.Serialize(stream, beforeException);
            stream.Position = 0;
            var afterException = (DroidNotFoundException) formatter.Deserialize(stream);

            // Assert
            afterException.Message.Should().Be(_defaultMessage);
        }
    }
}
