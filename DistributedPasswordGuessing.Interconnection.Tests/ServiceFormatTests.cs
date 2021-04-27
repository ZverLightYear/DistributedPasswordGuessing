namespace DistributedPasswordGuessing.Interconnection.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Тест информации о пользователе.
    /// </summary>
    [TestFixture]
    public class ServiceFormatTests
    {
        /// <summary>
        /// Тест проверки корректности информации о пользователе.
        /// </summary>
        [Test]
        public void ServiceFormatCreatingTests()
        {
            ServiceFormat serviceFormat = new ServiceFormat();
            Assert.NotNull(serviceFormat.CountOfCore);
            Assert.NotNull(serviceFormat.MachineName);
            Assert.NotNull(serviceFormat.Power);
        }
    }
}
