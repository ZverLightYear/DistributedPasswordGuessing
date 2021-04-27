namespace DistributedPasswordGuessing.Interconnection
{
    #region

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Формат задания.
    /// </summary>
    public class TaskFormat
    {
        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TaskFormat"/>.
        /// </summary>
        /// <param name="startWord">
        /// Начальное слово для перебора.
        /// </param>
        /// <param name="numberOfWordsThatNeedToBeIterated">
        /// Количество слов, которое необходимо перебрать.
        /// </param>
        public TaskFormat(string startWord, long numberOfWordsThatNeedToBeIterated)
            : this()
        {
            this.Convolutions = new List<string>();

            this.StartWord = startWord;
            this.NumberOfWordsThatNeedToBeIterated = numberOfWordsThatNeedToBeIterated;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TaskFormat" />.
        /// </summary>
        public TaskFormat()
        {
            this.Convolutions = new List<string>();
            this.StartWord = "a";
            this.NumberOfWordsThatNeedToBeIterated = 0;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Получает или задает свертки для задания.
        /// </summary>
        /// <value>
        /// Свертки для задания.
        /// </value>
        public List<string> Convolutions { get; set; }

        /// <summary>
        /// Получает или задает количество слов, которое необходимо перебрать
        /// </summary>
        /// <value>
        /// Количество слов, которое необходимо перебрать.
        /// </value>
        public long NumberOfWordsThatNeedToBeIterated { get; set; }

        /// <summary>
        /// Получает или задает начальное слово.
        /// </summary>
        /// <value>
        /// Начальное слово.
        /// </value>
        public string StartWord { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Добавление свертки в задание.
        /// </summary>
        /// <param name="newConvolution">
        /// Новая свертка.
        /// </param>
        public void AddConvolution(string newConvolution)
        {
            this.Convolutions.Add(newConvolution);
        }

        /// <summary>
        /// Удаляет свертку из задания
        /// </summary>
        /// <param name="removingConvolution">
        /// Удаляемая свертка.
        /// </param>
        public void RemoveConvolution(string removingConvolution)
        {
            this.Convolutions.Remove(removingConvolution);
        }

        /// <summary>
        /// Возвращает текстовое представление задания.
        /// </summary>
        /// <returns>
        /// Текстовое представление задания.
        /// </returns>
        public override string ToString()
        {
            string res = this.StartWord;

            foreach (string convolution in this.Convolutions)
            {
                res += " : " + convolution;
            }

            return res;
        }

        #endregion
    }
}