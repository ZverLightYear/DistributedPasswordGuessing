namespace DistributedPasswordGuessing.PasswordGuessing
{
    #region

    using System;
    using System.Threading;

    using DistributedPasswordGuessing.Interconnection;

    #endregion

    /// <summary>
    /// Менеджер клиента.
    /// </summary>
    public class ClientManager
    {
        #region Fields

        /// <summary>
        /// Клиентский маршрутизатор.
        /// </summary>
        private readonly Router clientRouter = new Router();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClientManager"/>.
        /// </summary>
        /// <param name="serverName">
        /// Имя сервера для подключения.
        /// </param>
        public ClientManager(string serverName)
            : this()
        {
            if (string.IsNullOrEmpty(serverName))
            {
                serverName = new ServiceFormat().MachineName;
                Console.WriteLine("Выбрано имя сервера по умолчанию: " + serverName);
            }

            this.ConnectToServer(serverName);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClientManager" />.
        /// </summary>
        public ClientManager()
        {
            this.ClientMachine = new ServiceFormat();
            PowerTest powerTest = new PowerTest(1000);
            this.ClientMachine.Power = powerTest.Run();
            this.IsConnected = false;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Получает или задает информацию о клиенте.
        /// </summary>
        /// <value>
        /// Информация о клиенте.
        /// </value>
        public ServiceFormat ClientMachine { get; set; }

        /// <summary>
        /// Получает или задает значение, показывающее, подключен ли клиент или нет.
        /// </summary>
        /// <value>
        /// Значение, показывающее, подключен ли клиент или нет.
        /// </value>
        public bool IsConnected { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Метод подбора пароля к свертке.
        /// </summary>
        /// <param name="threadName">
        /// Имя потока, в котором запускается метод.
        /// </param>
        public void Brut(object threadName)
        {
            while (true)
            {
                TaskFormat task;
                try
                {
                    task = this.clientRouter.GetTask();
                }
                catch
                {
                    Console.WriteLine("Thread " + threadName.ToString() + ": Перебор закончен. Заданий от сервера не поступает.");
                    break;
                }

                Console.WriteLine(threadName + ". Задание получено: " + task);

                SearchEngineSolutions searchEngineSolutions = new SearchEngineSolutions(task);
                AnswerFormat answer = searchEngineSolutions.FindSolution();

                Console.WriteLine(threadName + ".Задание отработано: " + task);
                this.clientRouter.SendAnswer(answer);
            }

            // ReSharper disable FunctionNeverReturns
        }

        // ReSharper restore FunctionNeverReturns

        /// <summary>
        /// Подключение к серверу.
        /// </summary>
        /// <param name="serverName">
        /// Имя сервера.
        /// </param>
        public void ConnectToServer(string serverName)
        {
            this.clientRouter.QueueConnect(
                "Formatname:DIRECT=OS:" + serverName + DefaultNetworkSettings.TaskQueuePath, 
                "Formatname:DIRECT=OS:" + serverName + DefaultNetworkSettings.ClientAnswerQueuePath, 
                "Formatname:DIRECT=OS:" + serverName + DefaultNetworkSettings.ServiceQueuePath, 
                "Formatname:DIRECT=OS:" + serverName + DefaultNetworkSettings.ConfirmationQueuePath);

            this.clientRouter.SendServiceMessage(this.ClientMachine);
            this.IsConnected = true;
        }

        /// <summary>
        /// Метод запуска диспечеризации.
        /// </summary>
        public void StartGuessing()
        {
            if (this.IsConnected == false)
            {
                return;
            }

            Thread[] processes = new Thread[this.ClientMachine.CountOfCore];

            for (int i = 0; i < processes.Length; i++)
            {
                processes[i] = new Thread(this.Brut);
                processes[i].Start(i + 1);
            }
        }

        #endregion
    }
}