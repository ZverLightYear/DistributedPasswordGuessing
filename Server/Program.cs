namespace Server
{
    #region

    using System;

    using DistributedPasswordGuessing.Dispatching;

    #endregion

    /// <summary>
    /// Серверная часть.
    /// </summary>
    public class Program
    {
        #region Public Methods and Operators

        /// <summary>
        /// Запускаемый метод
        /// </summary>
        public static void Main()
        {
            TaskManager taskManager = new TaskManager();

            taskManager.QueueCreate();
            taskManager.QueueConnect();

            Console.Write("Введите путь к файлу, содержащему свертки [..\\default.conv]:");
            string pathOfConvolutions = Console.ReadLine();

            if (string.IsNullOrEmpty(pathOfConvolutions))
            {
                pathOfConvolutions = DefaultDispatchingSettings.ConvolutionsPath;
            }

            taskManager.LoadConvolutions(pathOfConvolutions);

            taskManager.StartDispathing();
        }

        #endregion
    }
}