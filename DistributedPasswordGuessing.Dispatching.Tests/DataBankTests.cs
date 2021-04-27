namespace DistributedPasswordGuessing.Dispatching.Tests
{
    using DistributedPasswordGuessing.Dispatching;
    using DistributedPasswordGuessing.Interconnection;

    using NUnit.Framework;

    /// <summary>
    /// Тесты для проверки работы контейнера данных.
    /// </summary>
    [TestFixture]
    public class DataBankTests
    {
        /// <summary>
        /// Проверка на добавление задания в контейнер данных.
        /// </summary>
        [Test]
        public void CorrectTaskAdding()
        {
            DataBank dataBank = new DataBank();
            TaskFormat task = new TaskFormat();
            dataBank.AddTask(task);

            Assert.AreEqual(1, dataBank.TaskCount);
            Assert.AreEqual(0, dataBank.ProgressedTaskCount);

            dataBank.AddTaskToProgressedList(task);

            Assert.AreEqual(1, dataBank.TaskCount);
            Assert.AreEqual(1, dataBank.ProgressedTaskCount);
        }

        /// <summary>
        /// Проверка на удаление задания из контейнера данных.
        /// </summary>
        [Test]
        public void CorrectTaskRemoving()
        {
            DataBank dataBank = new DataBank();
            TaskFormat task = new TaskFormat();
            dataBank.AddTask(task);
            dataBank.AddTaskToProgressedList(task);

            Assert.AreEqual(1, dataBank.TaskCount);
            Assert.AreEqual(1, dataBank.ProgressedTaskCount);

            dataBank.RemoveTask(task);

            Assert.AreEqual(0, dataBank.TaskCount);
            Assert.AreEqual(1, dataBank.ProgressedTaskCount);

            dataBank.RemoveTaskFromProgressedList(task);

            Assert.AreEqual(0, dataBank.TaskCount);
            Assert.AreEqual(0, dataBank.ProgressedTaskCount);
        }

        /// <summary>
        /// Проверка на добавление свертки в контейнер данных.
        /// </summary>
        [Test]
        public void CorrectConvolutionsAdding()
        {
            DataBank dataBank = new DataBank();

            Assert.AreEqual(0, dataBank.ConvolutionsCount);
            dataBank.Convolutions.Add("123");
            Assert.AreEqual(1, dataBank.ConvolutionsCount);
        }
    }
}
