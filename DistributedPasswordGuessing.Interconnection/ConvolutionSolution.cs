namespace DistributedPasswordGuessing.Interconnection
{
    /// <summary>
    /// Решения для сверток.
    /// </summary>
    public class ConvolutionSolution
    {
        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConvolutionSolution"/>.
        /// </summary>
        /// <param name="word">
        /// Слово для свертки.
        /// </param>
        /// <param name="convolution">
        /// Свертка для которой найдено слово.
        /// </param>
        public ConvolutionSolution(string word, string convolution)
            : this()
        {
            this.Word = word;
            this.Convolution = convolution;
        }

        /// <summary>
        /// Предотвращает вызов конструктора по умолчанию для класса <see cref="ConvolutionSolution" />.
        /// </summary>
        private ConvolutionSolution()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Получает или задает свертку для которой найдено решение
        /// </summary>
        /// <value>
        /// Свертка для которой найдено решение.
        /// </value>
        public string Convolution { get; set; }

        /// <summary>
        /// Получает или задает слово, которое является решением для свертки <see cref="Convolution" />
        /// </summary>
        /// <value>
        /// Слово, которое является решением.
        /// </value>
        public string Word { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Преобразование решения в строку.
        /// </summary>
        /// <returns>
        /// Строковое представление решения.
        /// </returns>
        public override string ToString()
        {
            return this.Word + " - " + this.Convolution;
        }

        #endregion
    }
}