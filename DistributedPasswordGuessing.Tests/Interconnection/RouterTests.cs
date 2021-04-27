namespace DistributedPasswordGuessing.Tests.Interconnection
{
    #region

    using NUnit.Framework;

    #endregion

    /// <summary>
    ///     The router tests.
    /// </summary>
    [TestFixture]
    public class RouterTests
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The test 1.
        /// </summary>
        [Test]
        public void Test1()
        {
            /*            Router router = new Router();
            string serverName = ".";

            router.QueueCreate(
               serverName + "\\Private$\\TaskQueue",
               serverName + "\\Private$\\ClientAnswerQueue",
               serverName + "\\Private$\\ServiceQueue",
               serverName + "\\Private$\\ConfirmationQueue");

            router.QueueConnect(
                "Formatname:DIRECT=OS:" + serverName + "\\Private$\\TaskQueue",
                "Formatname:DIRECT=OS:" + serverName + "\\Private$\\ClientAnswerQueue",
                "Formatname:DIRECT=OS:" + serverName + "\\Private$\\ServiceQueue",
                "Formatname:DIRECT=OS:" + serverName + "\\Private$\\ConfirmationQueue");


            ServiceFormat sf = new ServiceFormat();
            router.SendServiceMessage(sf);

            TaskFormat t = new TaskFormat("zveq", 100000);
            t.Convolutions.Add(Cryptography.Encryption("zver"));
            t.Convolutions.Add(Cryptography.Encryption("q"));
            t.Convolutions.Add(Cryptography.Encryption("qwerty"));
            t.Convolutions.Add(Cryptography.Encryption("zver31031993"));
            t.StartWord = "q";

            router.SendTask(t);

            TaskFormat task = router.GetTask();

            SearchEngineSolutions fabrica = new SearchEngineSolutions(task);
            AnswerFormat answer = fabrica.FindSolution();

            router.SendAnswer(answer);*/
        }

        #endregion
    }
}