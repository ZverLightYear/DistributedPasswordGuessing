namespace DistributedPasswordGuessing.Tests.PasswordGuessing
{
    #region

    using DistributedPasswordGuessing.PasswordGuessing;
    using DistributedPasswordGuessing.PasswordGuessing.Exceptions;

    using NUnit.Framework;

    #endregion

    /// <summary>
    ///     The cryptography section tests.
    /// </summary>
    [TestFixture]
    public class CryptographyTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The all variants of strings encrypt correctly.
        /// </summary>
        [Test]
        public void AllVariantsOfStringsEncryptCorrectly()
        {
            Assert.Throws<IncorrectInputWordException>(() => Cryptography.Encryption(null));
            Assert.That(Cryptography.Encryption(string.Empty) == "d41d8cd98f00b204e9800998ecf8427e");
            Assert.That(Cryptography.Encryption("Hello!") == "4b6dbcd1be945c127d6ddbf7d5092119");
            Cryptography.Encryption("This line will not throw exception!");
        }

        #endregion
    }
}