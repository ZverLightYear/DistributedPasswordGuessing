namespace DistributedPasswordGuessing.PasswordGuessing.Tests
{
    #region

    using DistributedPasswordGuessing.PasswordGuessing;
    using DistributedPasswordGuessing.PasswordGuessing.Exceptions;

    using NUnit.Framework;

    #endregion

    /// <summary>
    /// Тестирование алфавита
    /// </summary>
    [TestFixture]
    public class AlphabetTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Тестирование на возвращаемые символы.
        /// </summary>
        [Test]
        public void AllSybmolAreGettingCorrectly()
        {
            Assert.IsInstanceOf(typeof(char), Alphabet.GetSymbol(0));
            Assert.IsInstanceOf(typeof(char), Alphabet.GetSymbol(Alphabet.Length - 1));

            Assert.Throws<IndexInAlphabetOutOfRangeException>(() => Alphabet.GetSymbol(-1));
            Assert.Throws<IndexInAlphabetOutOfRangeException>(() => Alphabet.GetSymbol(Alphabet.Length));

            Assert.AreEqual(Alphabet.IndexOf('a'), 0);
            Assert.AreEqual(Alphabet.GetSymbol(1), 'b');

            Assert.Throws<SymbolNotFoundInAlphabetException>(() => Alphabet.IndexOf(']'));
        }

        #endregion
    }
}