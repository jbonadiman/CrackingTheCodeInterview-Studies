using Structures;
using System;
using Xunit;

namespace CrackingTheCodeInterview.Tests
{
    public class SimpleNodeTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(99.0)]
        [InlineData("abc")]
        [InlineData(true)]
        [InlineData(new int[] {1, 2, 3})]
        public void NewNode<T>(T value)
        {
            var node = new SimpleNode<T>(value);
 
            Assert.Equal(value, node.Value);
        }
    }
}
