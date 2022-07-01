using MeasuringDevice;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_for_Module_10
{

    [TestClass()]
    public class MeasureDataDeviceTest
    {


        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        internal virtual MeasureDataDevice CreateMeasureDataDevice()
        {
            MeasureDataDevice target = new MeasureMassDevice(Units.Imperial, "TestLog.txt");
            return target;
        }

        [TestMethod()]
        public void DisposeTest()
        {
            MeasureDataDevice target = CreateMeasureDataDevice();
            target.Dispose();
            Assert.Inconclusive("Метод, который не возвращает значение, не может быть проверен.");
        }


        [TestMethod()]
        public void GetLoggingFileTest()
        {
            MeasureDataDevice target = CreateMeasureDataDevice();
            string expected = "TestLog.txt";
            string actual;
            actual = target.GetLoggingFile();
            Assert.AreEqual(expected, actual);
            target.Dispose();
        }

        [TestMethod()]
        public void GetRawDataTest()
        {
            MeasureDataDevice target = CreateMeasureDataDevice();
            target.StartCollecting();
            int[] actual;
            actual = target.GetRawData();
            Assert.IsNotNull(actual);
            target.StopCollecting();
            target.Dispose();
        }

        [TestMethod()]
        public void ImperialValueTest()
        {
            MeasureDataDevice target = CreateMeasureDataDevice();
            target.StartCollecting();
            Decimal actual;
            actual = target.ImperialValue();
            Assert.IsNotNull(actual);
            target.StopCollecting();
            target.Dispose();
        }

        [TestMethod()]
        public void MetricValueTest()
        {
            MeasureDataDevice target = CreateMeasureDataDevice();
            target.StartCollecting();   
            Decimal actual;
            actual = target.MetricValue();
            Assert.IsNotNull(actual);
            target.StopCollecting();
            target.Dispose();
        }

        [TestMethod()]
        public void StartCollectingTest()
        {
            MeasureDataDevice target = CreateMeasureDataDevice(); 
            target.StartCollecting();
            target.StopCollecting();
            target.Dispose();
            Assert.Inconclusive("Метод, который не возвращает значение, не может быть проверен.");
        }

        [TestMethod()]
        public void StopCollectingTest()
        {
            MeasureDataDevice target = CreateMeasureDataDevice();
            target.StartCollecting();
            target.StopCollecting();
            target.Dispose();
            Assert.Inconclusive("Метод, который не возвращает значение, не может быть проверен.");
        }

        [TestMethod()]
        public void DataCapturedTest()
        {
            MeasureDataDevice target = CreateMeasureDataDevice();
            target.StartCollecting();
            int[] actual;
            actual = target.DataCaptured;
            Assert.IsNotNull(actual);
            target.Dispose();
        }

        [TestMethod()]
        public void LoggingFileNameTest()
        {
            MeasureDataDevice target = CreateMeasureDataDevice();
            string expected = "NewTestLogFile.txt";
            string actual;
            target.LoggingFileName = expected;
            actual = target.LoggingFileName;
            Assert.AreEqual(expected, actual);
            target.Dispose(); 
        }

        [TestMethod()]
        public void MostRecentMeasureTest()
        {
            MeasureDataDevice target = CreateMeasureDataDevice();
            target.StartCollecting();
            int actual;
            actual = target.MostRecentMeasure;
            Assert.IsNotNull(actual);
            target.StopCollecting();
            target.Dispose();
        }
>
        [TestMethod()]
        public void UnitsToUseTest()
        {
            MeasureDataDevice target = CreateMeasureDataDevice();
            Units expected = Units.Imperial;
            Units actual;
            actual = target.UnitsToUse;
            Assert.AreEqual(expected, actual);
            target.Dispose();
        }
    }
}
