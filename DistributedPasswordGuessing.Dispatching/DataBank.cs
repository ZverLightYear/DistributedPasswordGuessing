namespace DistributedPasswordGuessing.Dispatching
{
    #region

    using System;
    using System.Collections.Generic;

    using DistributedPasswordGuessing.Interconnection;

    #endregion

    /// <summary>
    /// Контейнер информации.
    /// </summary>
    public class DataBank
    {
        #region Fields

        /// <summary>
        /// Список обрабатываемых заданий.
        /// </summary>
        public readonly Dictionary<TaskFormat, DateTime> ProgressedTaskList = new Dictionary<TaskFormat, DateTime>();

        /// <summary>
        /// Список текущих заданий.
        /// </summary>
        public readonly List<TaskFormat> TaskList = new List<TaskFormat>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DataBank"/>.
        /// </summary>
        public DataBank()
        {
            this.Convolutions = new List<string>();
        }

        /// <summary>
        /// Получает или задает свертки для которых нужно найти решение.
        /// </summary>
        /// <value>
        /// Свертки для которых нужно найти решение.
        /// </value>
        public List<string> Convolutions { get; set; }

        /// <summary>
        /// Получает количество сверток для которых нужно найти решение.
        /// </summary>
        /// <value>
        /// Количество сверток для которых нужно найти решение.
        /// </value>
        public int ConvolutionsCount
        {
            get
            {
                return this.Convolutions.Count;
            }
        }

        /// <summary>
        /// Получает количество заданий, которые находятся на обработке у клиентов.
        /// </summary>
        /// <value>
        /// Количество заданий, которые находятся на обработке у клиентов.
        /// </value>
        public int ProgressedTaskCount
        {
            get
            {
                return this.ProgressedTaskList.Count;
            }
        }

        /// <summary>
        /// Получает количество заданий, находящихся в очереди на обработку.
        /// </summary>
        /// <value>
        /// Количество заданий, находящихся в очереди на обработку.
        /// </value>
        public int TaskCount
        {
            get
            {
                return this.TaskList.Count;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Метод добавления задания в список текущих заданий.
        /// </summary>
        /// <param name="taskFormat">
        /// Задание для добавления.
        /// </param>
        public void AddTask(TaskFormat taskFormat)
        {
            this.TaskList.Add(taskFormat);
        }

        /// <summary>
        /// Метод добавления задания в список обрабатываемых заданий.
        /// </summary>
        /// <param name="taskFormat">
        /// Задание для добавления.
        /// </param>
        public void AddTaskToProgressedList(TaskFormat taskFormat)
        {
            this.ProgressedTaskList.Add(taskFormat, DateTime.Now);
        }

        /// <summary>
        /// Метод удаления задания из списка текущих заданий.
        /// </summary>
        /// <param name="taskFormat">
        /// Задание для удаления.
        /// </param>
        public void RemoveTask(TaskFormat taskFormat)
        {
            this.TaskList.Remove(taskFormat);
        }

        /// <summary>
        /// Метод удаления задания из списка обрабатываемых заданий.
        /// </summary>
        /// <param name="taskFormat">
        /// Задание для удаления.
        /// </param>
        public void RemoveTaskFromProgressedList(TaskFormat taskFormat)
        {
            this.ProgressedTaskList.Remove(taskFormat);
        }

        #endregion
    }
}