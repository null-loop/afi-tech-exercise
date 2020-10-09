using System;
using FluentAssertions;
using Xunit;

namespace AFIExercise.Tests
{
    public class BaselineTests
    {
        /// <summary>
        /// Canary test to ensure that tests are being run.
        /// </summary>
        /// <remarks>Should always pass, presence of test means tests are being run correctly.</remarks>
        [Fact]
        public void Canary()
        {
            true.Should().BeTrue();
        }
    }
}
