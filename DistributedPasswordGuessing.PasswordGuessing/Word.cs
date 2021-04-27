namespace DistributedPasswordGuessing.PasswordGuessing
{
    using DistributedPasswordGuessing.PasswordGuessing.Exceptions;

    /// <summary>
    /// Слово, полученое с использованием статического алфавита <see cref="Alphabet" />
    /// </summary>
    public class Word
    {
        #region Constants

        /// <summary>
        /// Длинна слова по умолчанию.
        /// </summary>
        private const int DefaultWordLength = 6;

        #endregion

        #region Fields

        /// <summary>
        /// Массив, в ячейках которого находится индекс символа из статического алфавита <see cref="Alphabet" />
        /// </summary>
        private int[] word;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Word" />.
        /// </summary>
        public Word()
        {
            this.word = new int[DefaultWordLength];
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Word"/>.
        /// </summary>
        /// <param name="wordLength">
        /// Длинна слова.
        /// </param>
        public Word(byte wordLength)
        {
            if (wordLength <= 0)
            {
                throw new WordLengthChangeIsIgnoredException();
            }

            this.word = new int[wordLength];
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Получает длинну слова.
        /// </summary>
        /// <value>
        /// Длинна слова.
        /// </value>
        public byte WordLength
        {
            get
            {
                return (byte)this.word.Length;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Возвращает строковое значение слова.
        /// </summary>
        /// <returns>
        /// Слово <see cref="string" />.
        /// </returns>
        public string GetCurrentWord()
        {
            string res = string.Empty;
            foreach (var numberOfSymbol in this.word)
            {
                res += Alphabet.GetSymbol(numberOfSymbol);
            }

            return res;
        }

        /// <summary>
        /// Возвращает строковое значение слова, полученного переходом от текущего слова, к следующему в статическом алфавите
        /// </summary>
        /// <returns>
        /// Строковое значение слова, полученного переходом от текущего слова, к следующему в статическом алфавите.
        /// </returns>
        public string GetNextWord()
        {
            this.GoToNextWord();
            return this.GetCurrentWord();
        }

        /// <summary>
        /// Переходит от текущего слова, к следующему в статическом алфавите <see cref="Alphabet" />.
        /// </summary>
        public void GoToNextWord()
        {
            this.GoToWordAfterCurrent(1);
        }

        /// <summary>
        /// Переходит к слову, находящемся на расстоянии от текущего слова в статическом алфавите Alphabet
        /// </summary>
        /// <param name="step">
        /// Шаг перехода.
        /// </param>
        public void GoToWordAfterCurrent(long step)
        {
            int i = this.WordLength - 1;
            while (step != 0)
            {
                if (i >= 0)
                {
                    int buf = this.word[i];
                    this.word[i] = (int)((buf + step) % Alphabet.Length);
                    step = (buf + step) / Alphabet.Length;
                    i--;
                }
                else
                {
                    int[] tempWord = new int[this.WordLength + 1];
                    for (int j = this.WordLength; j > 0; j--)
                    {
                        tempWord[j] = this.word[j - 1];
                    }

                    tempWord[0] += (int)((step - 1) % Alphabet.Length);
                    step = (step - 1) / Alphabet.Length;
                    this.word = tempWord;
                }
            }
        }

        /// <summary>
        /// Устанавливает текущее слово.
        /// </summary>
        /// <param name="newWord">
        /// Значение нового слова.
        /// </param>
        public void SetWord(string newWord)
        {
            if (string.IsNullOrEmpty(newWord))
            {
                throw new SetNewWordWasFailedException();
            }

            this.word = new int[newWord.Length];

            var i = 0;
            foreach (var symbol in newWord)
            {
                this.word[i] = Alphabet.IndexOf(symbol);
                i++;
            }
        }

        /// <summary>
        /// Возвращает строковое значение текущего слова.
        /// </summary>
        /// <returns>
        /// Строковое значение текущего слова.
        /// </returns>
        public override string ToString()
        {
            return this.GetCurrentWord();
        }

        #endregion
    }
}