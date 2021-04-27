namespace DistributedPasswordGuessing.Tests.Interconnection
{
    #region

    using DistributedPasswordGuessing.Interconnection;

    using NUnit.Framework;

    #endregion

    /// <summary>
    ///     The task tests.
    /// </summary>
    [TestFixture]
    public class TaskFormatTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The test 1.
        /// </summary>
        [Test]
        public void Test1()
        {
            TaskFormat task = new TaskFormat("qweqwe", 100);

            task.AddConvolution("asdasd");
            Assert.AreEqual(task.Convolutions.Count, 1);

            task.RemoveConvolution("111111");
            Assert.AreEqual(task.Convolutions.Count, 1);

            task.RemoveConvolution("asdasd");
            Assert.AreEqual(task.Convolutions.Count, 0);
        }

        #endregion
    }
}