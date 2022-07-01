using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DeviceControl;
using System.IO;

namespace MeasuringDevice
{
    public abstract class MeasureDataDevice : IMeasuringDeviceWithProperties, IDisposable
    {
        public abstract decimal MetricValue();

        public abstract decimal ImperialValue();

        public void StartCollecting()
        {
            controller = DeviceController.StartDevice(measurementType);

            if (loggingFileWriter == null)
            {
                if (!File.Exists(loggingFileName))
                {
                    loggingFileWriter = File.CreateText(loggingFileName);
                    loggingFileWriter.WriteLine("Статус файла журнала проверен - Создан");
                    loggingFileWriter.WriteLine("Сборка началась");
                }
                else
                {
                    loggingFileWriter = new StreamWriter(loggingFileName);
                    loggingFileWriter.WriteLine("Статус файла журнала проверен - Открыт");
                    loggingFileWriter.WriteLine("Сборка началась");
                }
            }
            else
            {
                loggingFileWriter.WriteLine("Статус файла журнала проверен - уже открыт");
                loggingFileWriter.WriteLine("Сборка началась началась");
            }
            GetMeasurements();
        }

        public void StopCollecting()
        {
            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }

            if (loggingFileWriter != null)
            {
                loggingFileWriter.WriteLine("Сборка остоновилась");
            }
        }

        public int[] GetRawData()
        {
            return dataCaptured;
        }

        private void GetMeasurements()
        {
            dataCaptured = new int[10];
            System.Threading.ThreadPool.QueueUserWorkItem((dummy) =>
            {
                int x = 0;
                Random timer = new Random();

                while (controller != null)
                {
                    System.Threading.Thread.Sleep(timer.Next(1000, 5000));
                    dataCaptured[x] = controller != null ? controller.TakeMeasurement() : dataCaptured[x];
                    mostRecentMeasure = dataCaptured[x];
                    
                    if (loggingFileWriter != null)
                    {
                        loggingFileWriter.WriteLine("Произведенные измерения: {0}", mostRecentMeasure.ToString());
                    }

                    x++;
                    if (x == 10)
                    {
                        x = 0;
                    }
                }
            });
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

        public void Dispose()
        {
            if (loggingFileWriter != null)
            {
                loggingFileWriter.WriteLine("Объект Удален");
                loggingFileWriter.Flush();
                loggingFileWriter.Close();
                loggingFileWriter = null;
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
                    loggingFileWriter.WriteLine("Файл журнала Изменен");
                    loggingFileWriter.WriteLine("Новый файл Журнала: {0}", value);
                    loggingFileWriter.Close();

                    loggingFileName = value;

                    if (!File.Exists(loggingFileName))
                    {
                        loggingFileWriter = File.CreateText(loggingFileName);
                        loggingFileWriter.WriteLine("Статус файла журнала проверен - Создан");
                        loggingFileWriter.WriteLine("Сборка началась");
                    }
                    else
                    {
                        loggingFileWriter = new StreamWriter(loggingFileName);
                        loggingFileWriter.WriteLine("Статус файла журнала проверен - Открыт");
                        loggingFileWriter.WriteLine("Сборка началась");
                    }
                    loggingFileWriter.WriteLine("Файл Журнала Успешно Изменен");
                }
            }
        }
    }
}
