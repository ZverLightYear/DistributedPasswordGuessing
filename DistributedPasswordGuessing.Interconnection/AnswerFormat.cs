namespace DistributedPasswordGuessing.Interconnection
{
    /// <summary>
    /// Формат ответа клиентов.
    /// </summary>
    public class AnswerFormat
    {
        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AnswerFormat" />.
        /// </summary>
        public AnswerFormat()
        {
            this.Task = null;
            this.Solution = new ConvolutionSolution[0];
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Получает или задает найденые решения для заданого задания.
        /// </summary>
        /// <value>
        /// Найденые решения для заданого задания.
        /// </value>
        public ConvolutionSolution[] Solution { get; set; }

        /// <summary>
        /// Получает или задает задание для которого найдены решения <see cref="Solution" />.
        /// </summary>
        /// <value>
        /// Задание для которого найдены решения <see cref="Solution" />.
        /// </value>
        public TaskFormat Task { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Метод для добавления найденого решения.
        /// </summary>
        /// <param name="newConvolutionSolution">
        /// Найденное решение.
        /// </param>
        public void AddSolution(ConvolutionSolution newConvolutionSolution)
        {
            ConvolutionSolution[] convolutionSolutionTemp = new ConvolutionSolution[this.Solution.Length + 1];

            int i = 0;
            foreach (ConvolutionSolution solution in this.Solution)
            {
                convolutionSolutionTemp[i] = solution;
                i++;
            }

            convolutionSolutionTemp[i] = newConvolutionSolution;
            this.Solution = convolutionSolutionTemp;
        }

        #endregion
    }
}