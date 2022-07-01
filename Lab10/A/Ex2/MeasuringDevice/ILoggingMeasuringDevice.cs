using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuringDevice
{
    interface ILoggingMeasuringDevice : IMeasuringDevice
    {
        string GetLoggingFile();
    }
}
