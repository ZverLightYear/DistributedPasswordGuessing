namespace DistributedPasswordGuessing.PasswordGuessing.Tests
{
    using System;

    using DistributedPasswordGuessing.Interconnection;

    using NUnit.Framework;

    /// <summary>
    /// Тестирование менеджера клиента.
    /// </summary>
    [TestFixture]
    public class ClientManagerTests
    {
        /// <summary>
        /// Менеджер клиента.
        /// </summary>
        private ClientManager clientManager;

        /// <summary>
        /// Начальная настройка тестриования.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            clientManager = new ClientManager(".");
        }

        /// <summary>
        /// Тестирование подключения.
        /// </summary>
        [Test]
        public void ConnectionTest()
        {
            Assert.IsTrue(clientManager.IsConnected);
            clientManager = new ClientManager(string.Empty);
            Assert.IsTrue(clientManager.IsConnected);
        }

        /// <summary>
        /// Тестирование потока перебора.
        /// </summary>
        [Test]
        public void BrutingTest()
        {
            Router router = new Router();
            
            const string PathTaskQueue = DefaultNetworkSettings.ServerName + DefaultNetworkSettings.TaskQueuePath;
            const string PathClientAnswerQueue = DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ClientAnswerQueuePath;
            const string PathServiceQueue = DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ServiceQueuePath;
            const string PathConfirmationQueue = DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ConfirmationQueuePath;

            router.QueueCreate(PathTaskQueue, PathClientAnswerQueue, PathServiceQueue, PathConfirmationQueue);

            router.QueueConnect(
                "Formatname:DIRECT=OS:" + PathTaskQueue,
                "Formatname:DIRECT=OS:" + PathClientAnswerQueue,
                "Formatname:DIRECT=OS:" + PathServiceQueue,
                "Formatname:DIRECT=OS:" + PathConfirmationQueue);

            router.MaxTimeForAnswer = new TimeSpan(0,0,0,10);

            TaskFormat task = new TaskFormat("123", 100);
            task.AddConvolution("123");

            router.SendTask(task);

            clientManager.ConnectToServer(".");

            clientManager.Brut(1);
        }

        /// <summary>
        /// Тестирование работоспособности потока.
        /// </summary>
        [Test]
        public void GuessingTest()
        {
            Router router = new Router();

            const string PathTaskQueue = DefaultNetworkSettings.ServerName + DefaultNetworkSettings.TaskQueuePath;
            const string PathClientAnswerQueue = DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ClientAnswerQueuePath;
            const string PathServiceQueue = DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ServiceQueuePath;
            const string PathConfirmationQueue = DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ConfirmationQueuePath;

            router.QueueCreate(PathTaskQueue, PathClientAnswerQueue, PathServiceQueue, PathConfirmationQueue);

            router.QueueConnect(
                "Formatname:DIRECT=OS:" + PathTaskQueue,
                "Formatname:DIRECT=OS:" + PathClientAnswerQueue,
                "Formatname:DIRECT=OS:" + PathServiceQueue,
                "Formatname:DIRECT=OS:" + PathConfirmationQueue);

            router.MaxTimeForAnswer = new TimeSpan(0, 0, 0, 10);

            TaskFormat task;

            task = new TaskFormat("П", 100);
            task.AddConvolution("d48dc410c5c1d5d899a08b2f13274a57");
            router.SendTask(task);

            task = new TaskFormat("q", 100);
            task.AddConvolution("d48dc410c5c1d5d899a08b2f13274a57");
            router.SendTask(task);
            
            task = new TaskFormat("1", 100);
            task.AddConvolution("d48dc410c5c1d5d899a08b2f13274a57");
            router.SendTask(task);
            
            task = new TaskFormat("ads", 100);
            task.AddConvolution("d48dc410c5c1d5d899a08b2f13274a57");
            router.SendTask(task);

            clientManager.ConnectToServer(".");

            clientManager.StartGuessing();
        }

        /// <summary>
        /// Тестирование работоспособности потока при неустановленном подключении.
        /// </summary>
        [Test]
        public void GuessingWrongTest()
        {
            clientManager = new ClientManager();
            clientManager.StartGuessing();
        }
    }
}
