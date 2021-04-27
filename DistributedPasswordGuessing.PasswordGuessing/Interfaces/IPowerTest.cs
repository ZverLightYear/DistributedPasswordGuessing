namespace DistributedPasswordGuessing.PasswordGuessing.Interfaces
{
    /// <summary>
    /// Интерфейс тестирования производителности
    /// </summary>
    public interface IPowerTest
    {
        #region Public Properties

        /// <summary>
        /// Получает количество паролей для тестирования производительности.
        /// </summary>
        /// <value>
        /// Количество паролей для тестирования производительности.
        /// </value>
        long PasswordCount { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Медот для запуска тестирования производительности.
        /// </summary>
        /// <returns>
        /// Количество операций в секунду.
        /// </returns>
        long Run();

        #endregion
    }
}