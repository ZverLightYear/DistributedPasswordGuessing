namespace DistributedPasswordGuessing.Dispatching
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    using DistributedPasswordGuessing.Dispatching.Exceptions;
    using DistributedPasswordGuessing.Interconnection;
    using DistributedPasswordGuessing.PasswordGuessing;

    #endregion

    /// <summary>
    /// Менеджер заданий.
    /// </summary>
    public class TaskManager
    {
        #region Fields

        /// <summary>
        /// Cлово генерации заданий.
        /// </summary>
        private readonly Word workWord = new Word(1);

        /// <summary>
        /// Поток отслеживания ответов клиента.
        /// </summary>
        private readonly Thread answersListnerThread;

        /// <summary>
        /// Поток отслеживания подключенных клиентов
        /// </summary>
        private readonly Thread clientListnerThread;

        /// <summary>
        /// Поток отслеживания подтверждений взятия заданий.
        /// </summary>
        private readonly Thread confirmationListnerThread;

        /// <summary>
        /// Контейнер информации, необходимой для диспечеризации.
        /// </summary>
        private readonly DataBank dispatchingData = new DataBank();

        /// <summary>
        /// Сетевой роутер.
        /// </summary>
        private readonly Router taskManagerRouter = new Router();

        /// <summary>
        /// Поток для отслеживания заданий.
        /// </summary>
        private readonly Thread taskThread;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TaskManager" />.
        /// </summary>
        public TaskManager()
        {
            this.taskThread = new Thread(this.TaskChecker);
            this.clientListnerThread = new Thread(this.ClientListner);
            this.answersListnerThread = new Thread(this.AnswersListner);
            this.confirmationListnerThread = new Thread(this.ConfirmationListner);
        }

        #endregion

        #region Public Methods and Operators

        public string GetCurrentLocation()
        {
            return Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.LastIndexOf(@"\", StringComparison.Ordinal));
        }

        /// <summary>
        /// Метод для загрузки сверток.
        /// </summary>
        /// <param name="pathConvolutions">
        /// Путь к сверткам.
        /// </param>
        public void LoadConvolutions(string pathConvolutions)
        {
            string path = GetCurrentLocation() + "\\" + pathConvolutions;
            if (File.Exists(path))
            {
                this.dispatchingData.Convolutions = File.ReadAllLines(path).ToList();
            }
            else
            {
                throw new FileWithConvolutionsNotFound();
            }
        }

        /// <summary>
        /// Метод создания очередей.
        /// </summary>
        public void QueueCreate()
        {
            this.taskManagerRouter.QueueCreate(
                DefaultNetworkSettings.ServerName + DefaultNetworkSettings.TaskQueuePath, 
                DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ClientAnswerQueuePath, 
                DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ServiceQueuePath, 
                DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ConfirmationQueuePath);
        }

        /// <summary>
        /// Метод подключения к очередям.
        /// </summary>
        public void QueueConnect()
        {
            this.taskManagerRouter.QueueConnect(
                DefaultNetworkSettings.ServerName + DefaultNetworkSettings.TaskQueuePath,
                DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ClientAnswerQueuePath,
                DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ServiceQueuePath,
                DefaultNetworkSettings.ServerName + DefaultNetworkSettings.ConfirmationQueuePath);
        }

        /// <summary>
        /// Метод для запуска диспечеризации.
        /// </summary>
        public void StartDispathing()
        {
            Console.WriteLine("Сервер запущен:");
            Console.WriteLine("Сетевое имя сервера: " + new ServiceFormat().MachineName);
            Console.WriteLine();
            Console.WriteLine("Заданные свертки: ");

            foreach (string convolution in this.dispatchingData.Convolutions)
            {
                Console.WriteLine(convolution);
            }

            Console.WriteLine();

            // блок первоначальной генерации заданий
            while (this.dispatchingData.TaskCount < DefaultDispatchingSettings.MinCountTaskInQueue)
            {
                AddNewTasks();
            }

            this.clientListnerThread.Start();
            this.taskThread.Start();
            this.answersListnerThread.Start();
            this.confirmationListnerThread.Start();

            Console.WriteLine("Диспечеризация запущена.");
            Console.WriteLine();

            this.clientListnerThread.Join();
            this.taskThread.Join();
            this.answersListnerThread.Join();
            this.confirmationListnerThread.Join();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Прослушивание ответов клиента.
        /// </summary>
        private void AnswersListner()
        {
            while (true)
            {
                // блок обработки результатов клиента
                AnswerFormat af = this.taskManagerRouter.GetAnswer();

                foreach (ConvolutionSolution cs in af.Solution)
                {
                    Console.WriteLine("найдено решение : " + cs.Word + " : " + cs.Convolution);
                    this.dispatchingData.Convolutions.Remove(cs.Convolution);
                }

                if (af.Task != null) this.dispatchingData.RemoveTaskFromProgressedList(af.Task);
            }

            // ReSharper disable FunctionNeverReturns
        }

        // ReSharper restore FunctionNeverReturns

        /// <summary>
        /// Пруслушивание подключенных клиентов.
        /// </summary>
        private void ClientListner()
        {
            while (true)
            {
                // блок информации о клиентах
                ServiceFormat sf = this.taskManagerRouter.GetServiceMessage();

                Console.WriteLine("Клиент '" + sf.MachineName + "' был подключен.");
                Console.WriteLine(
                    "  -  Количество ядер процессора: " + sf.CountOfCore.ToString(CultureInfo.InvariantCulture));
                Console.WriteLine(
                    "  -  Количество операций в секунду: " + sf.Power.ToString(CultureInfo.InvariantCulture));
                Console.WriteLine();
            }

            // ReSharper disable FunctionNeverReturns
        }

        // ReSharper restore FunctionNeverReturns

        /// <summary>
        /// Прослушивание подтверждений взятия заданий.
        /// </summary>
        private void ConfirmationListner()
        {
            while (true)
            {
                // блок, проверяющий, какие задания были взяты
                var taskGetting = this.taskManagerRouter.TaskTaken();

                if (taskGetting == null)
                {
                    continue;
                }

                TaskFormat tf = this.dispatchingData.TaskList.Find(format => format.ToString() == taskGetting.ToString());
                this.dispatchingData.RemoveTask(tf);
                this.dispatchingData.AddTaskToProgressedList(tf);

                this.AddNewTasks();
            }

            // ReSharper disable FunctionNeverReturns
        }

        /// <summary>
        /// Добавить новое задание в очередь на исполнение.
        /// </summary>
        private void AddNewTasks()
        {
            while (this.dispatchingData.TaskCount < DefaultDispatchingSettings.MinCountTaskInQueue)
            {
                TaskFormat task = new TaskFormat(
                    this.workWord.ToString(),
                    DefaultDispatchingSettings.DefaultNumberOfWordsThatNeedToBeIterated)
                    {
                        Convolutions =
                            this
                                .dispatchingData
                                .Convolutions
                    };

                this.taskManagerRouter.SendTask(task);

                this.dispatchingData.AddTask(task);

                this.workWord.GoToWordAfterCurrent(
                    DefaultDispatchingSettings.DefaultNumberOfWordsThatNeedToBeIterated);
            }
        }

        // ReSharper restore FunctionNeverReturns

        /// <summary>
        /// Проверка выполнения заданий.
        /// </summary>
        private void TaskChecker()
        {
            while (true)
            {
                // блок проверки выполнения взятых заданий
                TaskFormat buf = null;
                
                if (this.dispatchingData.ProgressedTaskList.Count == 0) Thread.Sleep(DefaultDispatchingSettings.MaxTimeForTask);
                else
                {
                    var sleepTimeSpan = (DateTime.Now - this.dispatchingData.ProgressedTaskList.First().Value)
                                        + DefaultDispatchingSettings.MaxTimeForTask;
                    Thread.Sleep(sleepTimeSpan);
                }

                foreach (KeyValuePair<TaskFormat, DateTime> keyValuePair in this.dispatchingData.ProgressedTaskList)
                {
                    if (DateTime.Now - keyValuePair.Value >= DefaultDispatchingSettings.MaxTimeForTask)
                    {
                        this.taskManagerRouter.SendTask(keyValuePair.Key);

                        this.dispatchingData.AddTask(keyValuePair.Key);
                        buf = keyValuePair.Key;
                        break;
                    }
                }

                if (buf != null)
                {
                    this.dispatchingData.RemoveTaskFromProgressedList(buf);
                }
            }

            // ReSharper disable FunctionNeverReturns
        }

        #endregion

        // ReSharper restore FunctionNeverReturns
    }
}