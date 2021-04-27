namespace DistributedPasswordGuessing.PasswordGuessing.Tests
{
    #region

    using System.Globalization;

    using DistributedPasswordGuessing.Interconnection;
    using DistributedPasswordGuessing.PasswordGuessing;
    using DistributedPasswordGuessing.PasswordGuessing.Exceptions;

    using NUnit.Framework;

    #endregion

    /// <summary>
    /// Тестирование класса подбора паролей.
    /// </summary>
    [TestFixture]
    public class SearchEngineSolutionsTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Тестирование инициализации.
        /// </summary>
        [Test]
        public void SearchEngineSolutionsInitializatedCorrectly()
        {
            TaskFormat task = new TaskFormat();
            task.Convolutions.Add(Cryptography.Encryption("zzzzzz"));

            SearchEngineSolutions searchEngineSolutions = new SearchEngineSolutions(task);
            AnswerFormat answerFormat = searchEngineSolutions.FindSolution();
            Assert.AreEqual(answerFormat.Solution.Length, 0);

            task.NumberOfWordsThatNeedToBeIterated = 100;
            task.Convolutions.Add(
                Cryptography.Encryption(Alphabet.GetSymbol(99).ToString(CultureInfo.InvariantCulture)));
            searchEngineSolutions = new SearchEngineSolutions(task);
            answerFormat = searchEngineSolutions.FindSolution();
            Assert.AreEqual(answerFormat.Solution.Length, 1);

            Assert.Throws<InitializationOfSearchEngineSolutionsWasFailedException>(
                () => searchEngineSolutions = new SearchEngineSolutions(null));
        }

        #endregion
    }
}