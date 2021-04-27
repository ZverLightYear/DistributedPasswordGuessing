namespace DistributedPasswordGuessing.Dispatching.Tests
{
    using System;

    using NUnit.Framework;

    /// <summary>
    /// Проверка настроек диспечеризации по умолчанию.
    /// </summary>
    [TestFixture]
    public class DefaultDispatchingSettingsTests
    {
        /// <summary>
        /// Проверка корректности максимального времени на одно задание.
        /// </summary>
        [Test]
        public void MaxTimeForTaskIsCorrect()
        {
            Assert.AreEqual(new TimeSpan(0, 0, 20, 0), DefaultDispatchingSettings.MaxTimeForTask);
        }
    }
}
