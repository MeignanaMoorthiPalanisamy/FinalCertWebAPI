using FinalCertWebAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBench;

namespace FinalCert.Tests
{ /// <summary>
  /// Summary description for APIPeformanceTest
  /// </summary>
    //[TestClass]
    public class NBenchPeformanceTest
    {
        private Counter _counter;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
        }


        [TestMethod()]
        [PerfBenchmark(NumberOfIterations = 10, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterMeasurement("TestCounter")]
        public void GetParentTasksTest()
        {
            ParentTaskController controller = new ParentTaskController();
            _counter.Increment();
            var result = controller.GetParent_Task();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        [PerfBenchmark(NumberOfIterations = 10, RunMode = RunMode.Throughput,
           RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 500)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThan, 500)]
        [CounterMeasurement("TestCounter")]
        public void GetTaskControllerTest()
        {
            UserController controller = new UserController();
            _counter.Increment();
            var result = controller.GetUsers();
            Assert.IsNotNull(result);
        }


        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }
    }
}
