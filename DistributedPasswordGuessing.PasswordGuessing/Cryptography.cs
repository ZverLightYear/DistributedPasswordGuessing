namespace DistributedPasswordGuessing.PasswordGuessing
{
    #region

    using System.Security.Cryptography;
    using System.Text;

    using DistributedPasswordGuessing.PasswordGuessing.Exceptions;

    #endregion

    /// <summary>
    /// Блок криптографии.
    /// </summary>
    public static class Cryptography
    {
        #region Public Methods and Operators

        /// <summary>
        /// Алгоритм шифрования.
        /// </summary>
        /// <param name="word">
        /// Слово, которое необходимо зашифровать.
        /// </param>
        /// <returns>
        /// Возвращает зашифрованное по md5 алгоритму слово.
        /// </returns>
        public static string Encryption(string word)
        {
            if (word != null)
            {
                // переводим исходное слово в байт-слово
                byte[] byteWord = Encoding.Unicode.GetBytes(word);

                // создаем объект для получения средст шифрования  
                var csp = new MD5CryptoServiceProvider();

                // вычисляем хеш-представление байт-слова в байтах  
                byte[] byteHashWord = csp.ComputeHash(byteWord);

                string hash = string.Empty;

                // формируем одну цельную строку из хэш-представления
                foreach (byte b in byteHashWord)
                {
                    hash += string.Format("{0:x2}", b);
                }

                // возвращаем ее в качестве результата
                return hash;
            }

            throw new IncorrectInputWordException();
        }

        #endregion
    }
}