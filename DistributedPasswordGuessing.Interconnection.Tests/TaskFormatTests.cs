namespace DistributedPasswordGuessing.Interconnection.Tests
{
    #region

    using DistributedPasswordGuessing.Interconnection;

    using NUnit.Framework;

    #endregion

    /// <summary>
    /// Тесты формата задания клиентам.
    /// </summary>
    [TestFixture]
    public class TaskFormatTests
    {
        /// <summary>
        /// Локальное задание.
        /// </summary>
        private TaskFormat task;

        /// <summary>
        /// Настройка тестов.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.task = new TaskFormat("qweqwe", 100);
        }

        /// <summary>
        /// Тесты работы сверток.
        /// </summary>
        [Test]
        public void ConvolutionsTest()
        {
            this.task.AddConvolution("asdasd");
            Assert.AreEqual(this.task.Convolutions.Count, 1);
            Assert.AreEqual("qweqwe : asdasd", this.task.ToString());

            this.task.RemoveConvolution("111111");
            Assert.AreEqual(this.task.Convolutions.Count, 1);
            Assert.AreEqual("qweqwe : asdasd", this.task.ToString());

            this.task.RemoveConvolution("asdasd");
            Assert.AreEqual(this.task.Convolutions.Count, 0);
            Assert.AreEqual("qweqwe", this.task.ToString());
        }
    }
}