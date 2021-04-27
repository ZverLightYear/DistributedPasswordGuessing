namespace DistributedPasswordGuessing.Interconnection.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Тесты решения для свертки.
    /// </summary>
    public class ConvolutionSolutionTests
    {
        /// <summary>
        /// Тесты на создание решения для свертки.
        /// </summary>
        [Test]
        public void ConvolutionCreateTests()
        {
            ConvolutionSolution convolution = new ConvolutionSolution("312","123");
            Assert.AreEqual("312", convolution.Word);
            Assert.AreEqual("123", convolution.Convolution);
            Assert.AreEqual("312 - 123", convolution.ToString());
        }
    }
}
