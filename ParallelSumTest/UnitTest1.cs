using ParallelCalculation;
namespace ParallelSumTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            //action
            ParallelCalc sumDiv = new ParallelCalc(1000L, 4);
            ParallelCalc sumNotDiv = new ParallelCalc(1000L, 7);

            //init
            long divResult = sumDiv.Sum();
            long notDivResult = sumNotDiv.Sum();

            //assert
            Assert.AreEqual(500500, divResult);
            Assert.AreEqual(500500, notDivResult);

        }
    }
}