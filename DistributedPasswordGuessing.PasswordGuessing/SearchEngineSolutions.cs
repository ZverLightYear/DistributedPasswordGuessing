namespace DistributedPasswordGuessing.PasswordGuessing
{
    #region

    using System;

    using DistributedPasswordGuessing.Interconnection;
    using DistributedPasswordGuessing.PasswordGuessing.Exceptions;

    #endregion

    /// <summary>
    /// Метод для перебора значений.
    /// </summary>
    public class SearchEngineSolutions
    {
        #region Fields

        /// <summary>
        /// Выполняемое задание.
        /// </summary>
        private readonly TaskFormat currentTask;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SearchEngineSolutions"/>.
        /// </summary>
        /// <param name="task">
        /// Задание для которого ищется решение.
        /// </param>
        public SearchEngineSolutions(TaskFormat task)
        {
            if (task == null)
            {
                throw new InitializationOfSearchEngineSolutionsWasFailedException();
            }

            this.currentTask = task;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Поиск решения.
        /// </summary>
        /// <returns>
        /// Формат ответа клиента.
        /// </returns>
        public AnswerFormat FindSolution()
        {
            TaskFormat workTask = this.currentTask;

            Word workWord = new Word();
            workWord.SetWord(this.currentTask.StartWord);

            AnswerFormat answer = new AnswerFormat();

            while (workTask.NumberOfWordsThatNeedToBeIterated > 0)
            {
                foreach (string convolution in workTask.Convolutions)
                {
                    if (Cryptography.Encryption(workWord.ToString()) == convolution)
                    {
                        ConvolutionSolution convolutionSolution = new ConvolutionSolution(
                            workWord.ToString(), convolution);
                        answer.AddSolution(convolutionSolution);
                        answer.Task = this.currentTask;
                        Console.WriteLine("Найдено решение:" + convolutionSolution);
                    }
                }

                workWord.GoToNextWord();
                workTask.NumberOfWordsThatNeedToBeIterated--;
            }

            return answer;
        }

        #endregion
    }
}