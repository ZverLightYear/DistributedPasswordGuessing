namespace DistributedPasswordGuessing.Interconnection
{
    #region

    using System;
    using System.Messaging;

    using DistributedPasswordGuessing.Interconnection.Exceptions;

    #endregion

    /// <summary>
    /// Класс маршрутизатор.
    /// </summary>
    public class Router
    {
        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Router" />.
        /// </summary>
        public Router()
        {
            this.ConfirmationQueue = null;
            this.ClientAnswerQueue = null;
            this.TaskQueue = null;
            this.ServiceQueue = null;
            this.MaxTimeForAnswer = new TimeSpan(0,0,0,20);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Получает или задает максимальное время для ожидания ответа.
        /// </summary>
        public TimeSpan MaxTimeForAnswer { get; set; }

        /// <summary>
        /// Получает или задает очередь ответов клиента.
        /// </summary>
        /// <value>
        /// Очередь ответов клиента.
        /// </value>
        private MessageQueue ClientAnswerQueue { get; set; }

        /// <summary>
        /// Получает или задает очередь подтверждения взятия задания.
        /// </summary>
        /// <value>
        /// Очередь подтверждения взятия задания.
        /// </value>
        private MessageQueue ConfirmationQueue { get; set; }

        /// <summary>
        /// Получает или задает очередь информации о клиентах.
        /// </summary>
        /// <value>
        /// Очередь информации о клиентах.
        /// </value>
        private MessageQueue ServiceQueue { get; set; }

        /// <summary>
        /// Получает или задает очередь заданий.
        /// </summary>
        /// <value>
        /// Очередь заданий.
        /// </value>
        private MessageQueue TaskQueue { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Метод очистки очередей.
        /// </summary>
        public void ClearQueues()
        {
            this.ClientAnswerQueue.Purge();
            this.TaskQueue.Purge();
            this.ServiceQueue.Purge();
            this.ConfirmationQueue.Purge();
            Console.WriteLine("Все очереди очищены");
        }

        /// <summary>
        /// Метод взятия ответов из очереди.
        /// </summary>
        /// <returns>
        /// Возвращает ответ.
        /// </returns>
        public AnswerFormat GetAnswer()
        {
            Message message;

            try
            {
                message = this.ClientAnswerQueue.Receive(this.MaxTimeForAnswer);
            }
            catch
            {
                throw new AnswerMessageWasNullException();
            }

            return message != null ? (AnswerFormat)message.Body : null;
        }

        /// <summary>
        /// Метод взятия информации о клиенте.
        /// </summary>
        /// <returns>
        /// Информация о клиенте.
        /// </returns>
        public ServiceFormat GetServiceMessage()
        {
            Message message;

            try
            {
                message = this.ServiceQueue.Receive(this.MaxTimeForAnswer);
            }
            catch
            {
                throw new ServiceMessageWasNullException();
            }

            return message != null ? (ServiceFormat)message.Body : null;
        }

        /// <summary>
        /// Метод взятия задания из очереди.
        /// </summary>
        /// <returns>
        /// Возвращает задание.
        /// </returns>
        public TaskFormat GetTask()
        {
            Message message;
            
            try
            {
                message = this.TaskQueue.Receive(this.MaxTimeForAnswer);
                ConfirmationQueue.Send(message);
            }
            catch
            {
                throw new TaskMessageWasNullException();
            }

            return message != null ? (TaskFormat)message.Body : null;
        }

        /// <summary>
        /// Метод подключения к очередям.
        /// </summary>
        /// <param name="pathTaskQueue">
        /// Путь к очереди заданий.
        /// </param>
        /// <param name="pathClientAnswerQueue">
        /// Путь к очереди ответов клиента.
        /// </param>
        /// <param name="pathServiceQueue">
        /// Путь к очереди информации о клиенте.
        /// </param>
        /// <param name="pathConfirmationQueue">
        /// Путь к очереди подтверждений клиента.
        /// </param>
        public void QueueConnect(
            string pathTaskQueue, string pathClientAnswerQueue, string pathServiceQueue, string pathConfirmationQueue)
        {
            this.TaskQueue = new MessageQueue(pathTaskQueue, false, true, QueueAccessMode.SendAndReceive);
            this.ClientAnswerQueue = new MessageQueue(pathClientAnswerQueue, false);
            this.ServiceQueue = new MessageQueue(pathServiceQueue, false);
            this.ConfirmationQueue = new MessageQueue(pathConfirmationQueue, false);

            this.TaskQueue.Formatter = new XmlMessageFormatter(new[] { typeof(TaskFormat) });
            this.ClientAnswerQueue.Formatter = new XmlMessageFormatter(new[] { typeof(AnswerFormat) });
            this.ServiceQueue.Formatter = new XmlMessageFormatter(new[] { typeof(ServiceFormat) });
            this.ConfirmationQueue.Formatter = new XmlMessageFormatter(new[] { typeof(TaskFormat) });
        }

        /// <summary>
        /// Метод создания очередей.
        /// </summary>
        /// <param name="pathTaskQueue">
        /// Путь к очереди заданий.
        /// </param>
        /// <param name="pathClientAnswerQueue">
        /// Путь к очереди ответов клиента.
        /// </param>
        /// <param name="pathServiceQueue">
        /// Путь к очереди информации о клиенте.
        /// </param>
        /// <param name="pathConfirmationQueue">
        /// Путь к очереди подтверждений клиента.
        /// </param>
        public void QueueCreate(
            string pathTaskQueue, string pathClientAnswerQueue, string pathServiceQueue, string pathConfirmationQueue)
        {
            if (!MessageQueue.Exists(pathTaskQueue))
            {
                this.TaskQueue = MessageQueue.Create(pathTaskQueue);
                Console.WriteLine("Создана очередь сообщений:" + pathTaskQueue);
            }

            if (!MessageQueue.Exists(pathClientAnswerQueue))
            {
                this.ClientAnswerQueue = MessageQueue.Create(pathClientAnswerQueue);
                Console.WriteLine("Создана очередь сообщений:" + pathClientAnswerQueue);
            }

            if (!MessageQueue.Exists(pathServiceQueue))
            {
                this.ServiceQueue = MessageQueue.Create(pathServiceQueue);
                Console.WriteLine("Создана очередь сообщений:" + pathServiceQueue);
            }

            if (!MessageQueue.Exists(pathConfirmationQueue))
            {
                MessageQueue.Create(pathConfirmationQueue);
                Console.WriteLine("Создана очередь сообщений:" + pathConfirmationQueue);
            }
        }

        /// <summary>
        /// Метод отправки в очередь ответов.
        /// </summary>
        /// <param name="answer">
        /// Ответ клиента.
        /// </param>
        public void SendAnswer(AnswerFormat answer)
        {
            Message message = new Message(answer);
            this.ClientAnswerQueue.Send(message);
        }

        /// <summary>
        /// Метод отправки в очередь информации о клиентах.
        /// </summary>
        /// <param name="service">
        /// Информация о клиенте.
        /// </param>
        public void SendServiceMessage(ServiceFormat service)
        {
            Message message = new Message(service);
            this.ServiceQueue.Send(message);
        }

        /// <summary>
        /// Метод отправки в очередь заданий.
        /// </summary>
        /// <param name="task">
        /// Задание для клиента.
        /// </param>
        public void SendTask(TaskFormat task)
        {
            Message message = new Message(task);
            this.TaskQueue.Send(message);
        }

        /// <summary>
        /// Метод проверки для взятых заданий.
        /// </summary>
        /// <returns>
        /// Заголовок взятого задания.
        /// </returns>
        public TaskFormat TaskTaken()
        {
            Message message;

            try
            {
                message = this.ConfirmationQueue.Receive(this.MaxTimeForAnswer);
            }
            catch
            {
                throw new ConfirmMessageWasNullException();
            }

            return (TaskFormat)(message != null ? message.Body : null);
        }

        #endregion
    }
}