namespace DistributedPasswordGuessing.Tests.PasswordGuessing
{
    #region

    using System.Diagnostics.CodeAnalysis;

    using DistributedPasswordGuessing.PasswordGuessing;
    using DistributedPasswordGuessing.PasswordGuessing.Exceptions;

    using NUnit.Framework;

    #endregion

    /// <summary>
    ///     The word tests.
    /// </summary>
    [TestFixture]
    public class WordTests
    {
        #region Fields

        /// <summary>
        ///     The word.
        /// </summary>
        private Word word = new Word();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The test2.
        /// </summary>
        [Test]
        public void WordLengthOfWordsChangesCorrectly()
        {
            this.word = new Word();
            Assert.Throws<WordLengthChangeIsIgnoredException>(() => this.word = new Word(0));
            this.word = new Word(1);
            this.word = new Word(10);
            this.word = new Word(12);
            this.word = new Word(255);
        }

        /// <summary>
        ///     The test.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1407:ArithmeticExpressionsMustDeclarePrecedence", 
            Justification = "Reviewed. Suppression is OK here.")]
        [Test]
        public void WordsAreFormedFromAlphabetCorrectly()
        {
            this.word = new Word(6);
            Assert.AreEqual(this.word.ToString(), "aaaaaa");
            Assert.AreEqual(this.word.GetNextWord(), "aaaaab");
            this.word.GoToNextWord();
            Assert.AreEqual(this.word.ToString(), "aaaaac");
            Assert.AreEqual(this.word.GetNextWord(), "aaaaad");

            this.word = new Word(1);
            Assert.AreEqual(this.word.ToString(), "a");
            Assert.AreEqual(this.word.GetNextWord(), "b");

            this.word.SetWord("tttt");
            Assert.AreEqual(this.word.ToString(), "tttt");
            Assert.AreEqual(this.word.GetNextWord(), "tttu");

            Assert.Throws<SetNewWordWasFailedException>(() => this.word.SetWord(null));

            this.word.SetWord("aa");
            this.word.GoToWordAfterCurrent(100000001);
            Assert.AreEqual(this.word.GetCurrentWord(), "W7ТС");

            Word secondWord = new Word();
            secondWord.SetWord("a");
            this.word.SetWord("a");

            this.word.GoToWordAfterCurrent(1000000);
            for (int i = 1; i <= 1000000; i++)
            {
                secondWord.GoToNextWord();
            }

            Assert.AreEqual(this.word.ToString(), secondWord.ToString());

            this.word.SetWord("9");
            this.word.GoToWordAfterCurrent(1 + Alphabet.Length * Alphabet.Length);
            Assert.AreEqual(this.word.ToString(), "aaa");
        }

        #endregion
    }
}