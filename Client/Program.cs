namespace Client
{
    #region

    using System;

    using DistributedPasswordGuessing.PasswordGuessing;

    #endregion

    /// <summary>
    /// Клиентская часть.
    /// </summary>
    public class Program
    {
        #region Public Methods and Operators

        /// <summary>
        /// Запускаемый метод.
        /// </summary>
        public static void Main()
        {
            Console.Write("Введите имя удаленной машины:");
            string serverName = Console.ReadLine();

            ClientManager clientManager = new ClientManager(serverName);

            clientManager.StartGuessing();
        }

        #endregion
    }
}