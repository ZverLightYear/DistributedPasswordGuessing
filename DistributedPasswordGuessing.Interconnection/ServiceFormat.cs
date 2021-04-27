namespace DistributedPasswordGuessing.Interconnection
{
    #region

    using System;

    #endregion

    /// <summary>
    /// Формат информации о клиенте. 
    /// </summary>
    public class ServiceFormat
    {
        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ServiceFormat" />.
        /// </summary>
        public ServiceFormat()
        {
            this.MachineName = Environment.MachineName;
            this.CountOfCore = Environment.ProcessorCount;
            this.Power = 0;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Получает или задает количество ядер клиента.
        /// </summary>
        /// <value>
        /// Количество ядер клиента.
        /// </value>
        public int CountOfCore { get; set; }

        /// <summary>
        /// Получает или задает имя машины клиента.
        /// </summary>
        /// <value>
        /// Имя машины клиента.
        /// </value>
        public string MachineName { get; set; }

        /// <summary>
        /// Получает или задает производительную мощность клиента.
        /// </summary>
        /// <value>
        /// Производительная мощность клиента.
        /// </value>
        public long Power { get; set; }

        #endregion
    }
}