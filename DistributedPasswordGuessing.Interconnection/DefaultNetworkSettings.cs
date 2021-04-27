namespace DistributedPasswordGuessing.Interconnection
{
    /// <summary>
    /// Настройки по умолчанию для сетевого соединения.
    /// </summary>
    public static class DefaultNetworkSettings
    {
        #region Constants

        /// <summary>
        /// Путь к очереди ответов клиента.
        /// </summary>
        public const string ClientAnswerQueuePath = "\\Private$\\ZverevClientAnswerQueue";

        /// <summary>
        /// Путь к очереди подтверждений клиента.
        /// </summary>
        public const string ConfirmationQueuePath = "\\Private$\\ZverevConfirmationQueue";

        /// <summary>
        /// Путь к серверу.
        /// </summary>
        public const string ServerName = ".";

        /// <summary>
        /// Путь к очереди подключений клиента.
        /// </summary>
        public const string ServiceQueuePath = "\\Private$\\ZverevServiceQueue";

        /// <summary>
        /// Путь к очереди заданий.
        /// </summary>
        public const string TaskQueuePath = "\\Private$\\ZverevTaskQueue";

        #endregion
    }
}