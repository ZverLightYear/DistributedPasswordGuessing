namespace DistributedPasswordGuessing.Interconnection.Tests
{
    #region

    using System;
    using System.Messaging;

    using DistributedPasswordGuessing.Interconnection;
    using DistributedPasswordGuessing.Interconnection.Exceptions;
    using DistributedPasswordGuessing.PasswordGuessing;

    using NUnit.Framework;

    #endregion

    /// <summary>
    /// Тест роутера.
    /// </summary>
    [TestFixture]
    public class RouterTests
    {
        /// <summary>
        /// Путь к очереди заданий.
        /// </summary>
        public const string PathTaskQueue = DefaultNetworkSettings.ServerName + DefaultNetworkSettings.TaskQueuePath;

        /// <summary>
        /// Путь к очереди ответов клиента.
        /// </summary>
        public const string PathClientAnswerQueue = DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ClientAnswerQueuePath;

        /// <summary>
        /// Путь к очереди информации о клиенте.
        /// </summary>
        public const string PathServiceQueue = DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ServiceQueuePath;

        /// <summary>
        /// Путь к очереди подтверждений.
        /// </summary>
        public const string PathConfirmationQueue = DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ConfirmationQueuePath;

        /// <summary>
        /// Рабочий роутер.
        /// </summary>
        private Router router = new Router();

        #region Public Methods and Operators

        /// <summary>
        /// Начальная настройка.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            router = new Router();

            router.QueueCreate(PathTaskQueue, PathClientAnswerQueue, PathServiceQueue, PathConfirmationQueue);

            router.QueueConnect(
                "Formatname:DIRECT=OS:" + PathTaskQueue,
                "Formatname:DIRECT=OS:" + PathClientAnswerQueue,
                "Formatname:DIRECT=OS:" + PathServiceQueue,
                "Formatname:DIRECT=OS:" + PathConfirmationQueue);
        }

        /// <summary>
        /// Тест создания несуществующих очередей
        /// </summary>
        [Test]
        public void NotExistedQueueCreatingTest()
        {
            router = new Router();

            if (MessageQueue.Exists(PathTaskQueue)) MessageQueue.Delete(PathTaskQueue);
            if (MessageQueue.Exists(PathClientAnswerQueue)) MessageQueue.Delete(PathClientAnswerQueue);
            if (MessageQueue.Exists(PathServiceQueue)) MessageQueue.Delete(PathServiceQueue);
            if (MessageQueue.Exists(PathConfirmationQueue)) MessageQueue.Delete(PathConfirmationQueue);

            router.QueueCreate(PathTaskQueue, PathClientAnswerQueue, PathServiceQueue, PathConfirmationQueue);
        }

        /// <summary>
        /// Тест создания существующих очередей
        /// </summary>
        [Test]
        public void ExistedQueueCreatingTest()
        {
            router.QueueCreate(PathTaskQueue, PathClientAnswerQueue, PathServiceQueue, PathConfirmationQueue);
        }
        
        /// <summary>
        /// Тест подключения к локальному компьютеру
        /// </summary>
        [Test]
        public void WorkingTest()
        {
            router.ClearQueues();
            router.MaxTimeForAnswer = new TimeSpan(0,0,0,10);
            ServiceFormat sf = new ServiceFormat();
            router.SendServiceMessage(sf);

            TaskFormat t = new TaskFormat("zveq", 100);
            t.Convolutions.Add(Cryptography.Encryption("zver"));
            t.Convolutions.Add(Cryptography.Encryption("q"));
            t.Convolutions.Add(Cryptography.Encryption("qwerty"));
            t.Convolutions.Add(Cryptography.Encryption("zver31031993"));
            t.StartWord = "q";

            router.SendTask(t);

            TaskFormat task = router.GetTask();

            SearchEngineSolutions fabrica = new SearchEngineSolutions(task);
            AnswerFormat answer = fabrica.FindSolution();

            router.SendAnswer(answer);
        }

        /// <summary>
        /// Тест очистки очередей.
        /// </summary>
        [Test]
        public void ClearingQueueTest()
        {
            TaskFormat t = new TaskFormat("zveq", 100000);
            t.Convolutions.Add(Cryptography.Encryption("zver"));
            t.Convolutions.Add(Cryptography.Encryption("q"));
            t.Convolutions.Add(Cryptography.Encryption("qwerty"));
            t.Convolutions.Add(Cryptography.Encryption("zver31031993"));
            t.StartWord = "q";

            router.SendTask(t);
            router.ClearQueues();
        }

        /// <summary>
        /// Тест отправки ответа клиента.
        /// </summary>
        [Test]
        public void SendingAnswerTest()
        {
            AnswerFormat answer = new AnswerFormat();
            answer.Solution = new[] { new ConvolutionSolution("qwe", "123") };
            router.SendAnswer(answer);
        }

        /// <summary>
        /// Тест получения ответа клиента.
        /// </summary>
        [Test]
        public void GettingAnswerTest()
        {
            AnswerFormat answer = new AnswerFormat();
            answer.Solution = new[] { new ConvolutionSolution("qwe", "00e0a9dff334507bd9526906c01d3eee") };
            router.ClearQueues();
            router.SendAnswer(answer);

            AnswerFormat gettinganswer = router.GetAnswer();

            Assert.AreEqual(answer.Solution[0].Convolution, gettinganswer.Solution[0].Convolution);
            Assert.AreEqual(answer.Solution[0].Word, gettinganswer.Solution[0].Word);

            Assert.AreEqual(answer.Task, gettinganswer.Task);
        }

        /// <summary>
        /// Тест получения сервисной информации от клиента.
        /// </summary>
        [Test]
        public void GettingServiceTest()
        {
            ServiceFormat service = new ServiceFormat();
            router.ClearQueues();
            router.SendServiceMessage(service);

            ServiceFormat gettingService = router.GetServiceMessage();

            Assert.AreEqual(gettingService.CountOfCore, service.CountOfCore);
            Assert.AreEqual(gettingService.MachineName, service.MachineName);
            Assert.AreEqual(gettingService.Power, service.Power);
        }

        /// <summary>
        /// Потпытка получения сообщения из пустой очереди информации о клиентах
        /// </summary>
        [Test]
        public void GettingServiceWasExceptionTest()
        {
            router.ClearQueues();
            router.MaxTimeForAnswer = new TimeSpan(0,0,0,1);

            Assert.Throws<ServiceMessageWasNullException>(() => router.GetServiceMessage());
        }

        /// <summary>
        /// Потпытка получения сообщения из пустой очереди ответов клиента
        /// </summary>
        [Test]
        public void GettingAnswerWasExceptionTest()
        {
            router.ClearQueues();
            router.MaxTimeForAnswer = new TimeSpan(0, 0, 0, 1);

            Assert.Throws<AnswerMessageWasNullException>(() => router.GetAnswer());
        }

        /// <summary>
        /// Потпытка получения сообщения из пустой очереди заданий
        /// </summary>
        [Test]
        public void GettingTaskWasExceptionTest()
        {
            router.ClearQueues();
            router.MaxTimeForAnswer = new TimeSpan(0, 0, 0, 1);

            Assert.Throws<TaskMessageWasNullException>(() => router.GetTask());
        }

        /// <summary>
        /// Потпытка получения сообщения из пустой очереди заданий
        /// </summary>
        [Test]
        public void GettingConfirmationWasExceptionTest()
        {
            router.ClearQueues();
            router.MaxTimeForAnswer = new TimeSpan(0, 0, 0, 1);

            Assert.Throws<ConfirmMessageWasNullException>(() => router.TaskTaken());
        }

        /// <summary>
        /// Тестирование метода подтверждения взятия задания
        /// </summary>
        [Test]
        public void GettingConfirmationTest()
        {
            router.ClearQueues();
            router.MaxTimeForAnswer = new TimeSpan(0, 0, 0, 1);
            
            TaskFormat task = new TaskFormat("123", 1000);
            task.AddConvolution("123");
            router.SendTask(task);

            router.GetTask();

            var confirm = router.TaskTaken();

            Assert.AreEqual(confirm.NumberOfWordsThatNeedToBeIterated, task.NumberOfWordsThatNeedToBeIterated);
            Assert.AreEqual(confirm.StartWord, task.StartWord);
            for (int i = 0; i < confirm.Convolutions.Count; i++)
            {
                Assert.AreEqual(confirm.Convolutions[i], task.Convolutions[i]);
            }
        }

        #endregion
    }
}