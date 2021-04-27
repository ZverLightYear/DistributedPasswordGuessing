namespace DistributedPasswordGuessing.PasswordGuessing.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Тестирование производительности системы.
    /// </summary>
    [TestFixture]
    public class PowerTestTests
    {
        /// <summary>
        /// Тестирование производительности для положительного большого числа.
        /// </summary>
        [Test]
        public void PowerTestForManyWords()
        {
            PowerTest powerTest = new PowerTest(100000);
            powerTest.Run();
        }

        /// <summary>
        /// Тестирование производительности на 0 слов, выдаст исключение.
        /// </summary>
        [Test]
        public void IncorrectPowerTestForZeroWords()
        {
            PowerTest powerTest = new PowerTest(0);
            powerTest.Run();
        }

        /// <summary>
        /// Тестирование производительности на отрицательное количество слов, выдаст исключение.
        /// </summary>
        [Test]
        public void IncorrectPowerTestForNegativeCountWords()
        {
            PowerTest powerTest = new PowerTest(-1);
            powerTest.Run();
        }
    }
}
