namespace DistributedPasswordGuessing.Dispatching.Tests
{
    using DistributedPasswordGuessing.Dispatching.Exceptions;

    using NUnit.Framework;

    /// <summary>
    /// Тестирование менеджера заданий
    /// </summary>
    [TestFixture]
    public class TaskManagerTests
    {
        /// <summary>
        /// Рабочий менеджер заданий.
        /// </summary>
        private TaskManager manager;

        /// <summary>
        /// Первоначальные настройки для теста
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.manager = new TaskManager();
        }

        /// <summary>
        /// Тестирование загрузки сверток
        /// </summary>
        [Test]
        public void LoadingConvolutionsTest()
        {
            this.manager.LoadConvolutions("..\\..\\..\\Server\\bin\\Debug\\default.conv");
        }

        /// <summary>
        /// Тестирование загрузки сверток
        /// </summary>
        [Test]
        public void WrongLoadingConvolutionsTest()
        {
            Assert.Throws<FileWithConvolutionsNotFound>(() => this.manager.LoadConvolutions("asdasda"));
        }

        /// <summary>
        /// Тестирование создания очередей менеджером заданий.
        /// </summary>
        [Test]
        public void QueueCreatingTest()
        {
            this.manager.QueueCreate();
        }

        /// <summary>
        /// Тестирование подключения к очередям менеджера заданий.
        /// </summary>
        [Test]
        public void QueueConnectingTest()
        {
            this.manager.QueueConnect();
        }

/*      [Test]
        public void FullTest()
        {
            TaskManager taskManager = new TaskManager();

            taskManager.QueueCreate();
            taskManager.QueueConnect();

            const string PathOfConvolutions = "..\\..\\..\\Server\\bin\\Debug\\default.conv";

            taskManager.LoadConvolutions(PathOfConvolutions);

            taskManager.StartDispathing();
        }*/
    }
}
