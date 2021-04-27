namespace DistributedPasswordGuessing.PasswordGuessing
{
    #region

    using System;
    using System.Globalization;

    using DistributedPasswordGuessing.Interconnection;
    using DistributedPasswordGuessing.PasswordGuessing.Interfaces;

    #endregion

    /// <summary>
    /// Тест производительности.
    /// </summary>
    public class PowerTest : IPowerTest
    {
        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PowerTest"/>.
        /// </summary>
        /// <param name="passwordCount">
        /// Количество паролей для подбора.
        /// </param>
        public PowerTest(long passwordCount)
        {
            if (passwordCount < 0) passwordCount = 0;
            this.PasswordCount = passwordCount;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Получает количество паролей для тестирования производительности.
        /// </summary>
        /// <value>
        /// Количество паролей для тестирования производительности.
        /// </value>
        public long PasswordCount { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Медот для запуска тестирования производительности.
        /// </summary>
        /// <returns>
        /// Количество операций в секунду.
        /// </returns>
        public long Run()
        {
            Console.WriteLine("Запущено тестирование производительности клиента.");
            Word word = new Word(5);
            TaskFormat tf = new TaskFormat(word.ToString(), this.PasswordCount);
            SearchEngineSolutions searchEngineSolutions = new SearchEngineSolutions(tf);

            DateTime startTime = DateTime.Now;
            searchEngineSolutions.FindSolution();
            DateTime finishTime = DateTime.Now;

            var result = (this.PasswordCount / ((finishTime - startTime).Milliseconds + 1)) * 100;
            Console.WriteLine("Тестирование производительности клиента окончено.");
            Console.WriteLine(
                "Производительность клиента: " + result.ToString(CultureInfo.InvariantCulture) + " оп/сек");
            return result;
        }

        #endregion
    }
}