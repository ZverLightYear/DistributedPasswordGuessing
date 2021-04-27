namespace DistributedPasswordGuessing.PasswordGuessing
{
    #region

    using DistributedPasswordGuessing.PasswordGuessing.Exceptions;

    #endregion

    /// <summary>
    /// Используемый алфавит.
    /// </summary>
    public static class Alphabet
    {
        #region Constants

        /// <summary>
        /// Алфавит по умолчанию.
        /// </summary>
        private const string DefaultAlphabet =
            "abcdefghijklmnopqrstuvwxyz" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "абвгдеёжзиклмнопрстуфхцчшьщъыэюя"
            + "АБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЬЩЪЫЭЮЯ" + "0123456789";

        #endregion

        #region Public Properties

        /// <summary>
        /// Получает количество символов в алфавите.
        /// </summary>
        /// <value>
        /// Количество символов в алфавите.
        /// </value>
        public static int Length
        {
            get
            {
                return DefaultAlphabet.Length;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Возвращает символ из алфавита по заданному индексу.
        /// </summary>
        /// <param name="index">
        /// Индекс требуемого символа.
        /// </param>
        /// <returns>
        /// Символ из алфавита по заданному индексу.
        /// </returns>
        /// <exception cref="IndexInAlphabetOutOfRangeException">
        /// Ошибка индекса алфавита
        /// </exception>
        public static char GetSymbol(int index)
        {
            if ((index < 0) || (Length <= index))
            {
                throw new IndexInAlphabetOutOfRangeException();
            }

            return DefaultAlphabet[index];
        }

        /// <summary>
        /// Поиск ндекса символа.
        /// </summary>
        /// <param name="symbol">
        /// Символ для поиска индекса.
        /// </param>
        /// <returns>
        /// Индекс символа.
        /// </returns>
        public static int IndexOf(char symbol)
        {
            int index = DefaultAlphabet.IndexOf(symbol);
            if ((index < 0) || (Length <= index))
            {
                throw new SymbolNotFoundInAlphabetException();
            }

            return index;
        }

        #endregion
    }
}