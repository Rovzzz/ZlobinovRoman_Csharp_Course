using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using DeviceControl;

namespace Fabrikam.Devices.MeasuringDevices
{
    class LengthMeasuringDevice : IControllableDevice
    {
        Random random;

        public LengthMeasuringDevice()
        {
            random = new Random();
        }

        public void StartDevice()
        {
        }

        public void StopDevice()
        {
        }

        public int GetLatestMeasure()
        {
            Thread.Sleep(random.Next(6000));
            return random.Next(1000);
        }
    }
}
