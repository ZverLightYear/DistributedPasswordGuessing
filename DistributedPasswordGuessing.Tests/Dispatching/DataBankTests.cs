namespace DistributedPasswordGuessing.Tests.Dispatching
{
    using DistributedPasswordGuessing.Dispatching;
    using DistributedPasswordGuessing.Interconnection;

    using NUnit.Framework;

    /// <summary>
    /// The data bank tests.
    /// </summary>
    [TestFixture]
    public class DataBankTests
    {
        /// <summary>
        /// The correct.
        /// </summary>
        [Test]
        public void Correct()
        {
            DataBank dataBank = new DataBank();
            dataBank.AddTask(new TaskFormat());
        }
    }
}
