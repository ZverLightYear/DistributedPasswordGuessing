namespace DistributedPasswordGuessing.Interconnection.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Тесты ответов клиента.
    /// </summary>
    [TestFixture]
    public class AnswerFormatTests
    {
        /// <summary>
        /// Тест добавления решения.
        /// </summary>
        [Test]
        public void AddingSolutionTest()
        {
            AnswerFormat answer = new AnswerFormat();
            ConvolutionSolution solution = new ConvolutionSolution("123", "321");
            Assert.AreEqual(0, answer.Solution.Length);
            answer.AddSolution(solution);
            Assert.AreEqual(1, answer.Solution.Length);
            solution = new ConvolutionSolution("1231", "3211");
            answer.AddSolution(solution);
            Assert.AreEqual(2, answer.Solution.Length);
        }

        /// <summary>
        /// Тест назначения задания.
        /// </summary>
        [Test]
        public void SetTaskTest()
        {
            AnswerFormat answer = new AnswerFormat();
            answer.Task = new TaskFormat("a", 100000);
        }
    }
}
