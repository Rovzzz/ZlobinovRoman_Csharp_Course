using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using DeviceControl;
using System.IO;
using System.ComponentModel;

namespace MeasuringDevice
{
    public abstract class MeasureDataDevice : IEventEnabledMeasuringDevice, IDisposable
    {

        public abstract decimal MetricValue();


        public abstract decimal ImperialValue();
                
        private BackgroundWorker dataCollector;


        public void StartCollecting()
        {
            if (disposed == true) return;

            if (controller == null)
                controller = DeviceController.StartDevice(measurementType);


            if (loggingFileWriter == null)
            {
                if (!File.Exists(loggingFileName))
                {
                    loggingFileWriter = File.CreateText(loggingFileName);
                    loggingFileWriter.WriteLine("Log file status checked - Created");
                    loggingFileWriter.WriteLine("Collecting Started");
                }
                else
                {
                    loggingFileWriter = new StreamWriter(loggingFileName);
                    loggingFileWriter.WriteLine("Log file status checked - Opened");
                    loggingFileWriter.WriteLine("Collecting Started");
                }
            }
            else
            {
                loggingFileWriter.WriteLine("Log file status checked - Already open");
                loggingFileWriter.WriteLine("Collecting Started");
            }

            GetMeasurements();
       }

        private void GetMeasurements()
        {
            dataCollector = new BackgroundWorker();
            dataCollector.WorkerSupportsCancellation = true;
            dataCollector.WorkerReportsProgress = true;

            dataCollector.DoWork += new 
                DoWorkEventHandler(dataCollector_DoWork);

            dataCollector.ProgressChanged += new 
                ProgressChangedEventHandler(dataCollector_ProgressChanged);

            dataCollector.RunWorkerAsync();
        }

        void dataCollector_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnNewMeasurementTaken();
        }

        void dataCollector_DoWork(object sender, DoWorkEventArgs e)
        {
            dataCaptured = new int[10];

            int i = 0;

            while (!dataCollector.CancellationPending)
            {
                dataCaptured[i] = controller.TakeMeasurement();
                mostRecentMeasure = dataCaptured[i];

                if (disposed)
                {
                    break;
                }

                if (loggingFileWriter != null)
                {
                    loggingFileWriter.WriteLine
                        ("Measurement - {0}", mostRecentMeasure.ToString());
                }

                dataCollector.ReportProgress(0);

                i++;
                if (i > 9)
                {
                    i = 0;
                }
            }
        }
       

        public void StopCollecting()
        {
            if (disposed == true) return;

            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }

            if (loggingFileWriter != null)
            {
                loggingFileWriter.WriteLine("Collecting Stopped");
            }
                        
            if (dataCollector != null)
            {
                dataCollector.CancelAsync();
            }
        }

        public int[] GetRawData()
        {
            return dataCaptured;
        }

        protected Units unitsToUse;
        protected int[] dataCaptured;
        protected int mostRecentMeasure;
        protected DeviceController controller;
        protected DeviceType measurementType;

        protected string loggingFileName;
        private TextWriter loggingFileWriter;

        public string GetLoggingFile()
        {
            return loggingFileName;
        }

        private bool disposed = false;

        public void Dispose()
        {
            disposed = true;

            if (loggingFileWriter != null)
            {
                loggingFileWriter.WriteLine("Object Disposed");
                loggingFileWriter.Flush();
                loggingFileWriter.Close();
            }

            if (dataCollector != null)
            {
                dataCollector.Dispose();
            }
        }

        public Units UnitsToUse
        {
            get
            {
                return unitsToUse;
            }
        }

        public int[] DataCaptured
        {
            get
            {
                return dataCaptured;
            }
        }

        public int MostRecentMeasure
        {
            get
            {
                return mostRecentMeasure;
            }
        }

        public string LoggingFileName
        {
            get
            {
                return loggingFileName;
            }
            set
            {
                if (loggingFileWriter == null)
                {
                    loggingFileName = value;
                }
                else
                {
                    loggingFileWriter.WriteLine("Log File Changed");
                    loggingFileWriter.WriteLine("New Log File: {0}", value);
                    loggingFileWriter.Close();

                    loggingFileName = value;

                    if (!File.Exists(loggingFileName))
                    {
                        loggingFileWriter = File.CreateText(loggingFileName);
                        loggingFileWriter.WriteLine("Log file status checked - Created");
                        loggingFileWriter.WriteLine("Collecting Started");
                    }
                    else
                    {
                        loggingFileWriter = new StreamWriter(loggingFileName);
                        loggingFileWriter.WriteLine("Log file status checked - Opened");
                        loggingFileWriter.WriteLine("Collecting Started");
                    }
                    loggingFileWriter.WriteLine("Log File Changed Successfully");
                }
            }
        }
                
        public event EventHandler NewMeasurementTaken;

        protected virtual void OnNewMeasurementTaken()
        {
            if (NewMeasurementTaken != null)
            {
                NewMeasurementTaken(this, null);
            }
        }        
    }
}
