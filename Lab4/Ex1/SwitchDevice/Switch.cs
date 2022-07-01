using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SwitchDevices
{
    public enum CoolantSystemStatus { OK, Check, Fail }
    public enum SuccessFailureResult { Success, Fail }
    public class Switch
    {
        private Random rand = new Random();

        public SuccessFailureResult DisconnectPowerGenerator()
        {
            SuccessFailureResult r = SuccessFailureResult.Fail;
            if (rand.Next(1, 10) > 2) r = SuccessFailureResult.Success;
            if (rand.Next(1, 20) > 18) throw new PowerGeneratorCommsException("Сбой в сети доступ к системе мониторинга электрогенератора");
            return r;
        }

        public CoolantSystemStatus VerifyPrimaryCoolantSystem()
        {
            CoolantSystemStatus c = CoolantSystemStatus.Fail;
            int r = rand.Next(1, 10);
            if (r > 5)
            {
                c = CoolantSystemStatus.OK;
            }
            else if (r > 2)
            {
                c = CoolantSystemStatus.Check;
            }
            if (rand.Next(1, 20) > 18) throw new CoolantTemperatureReadException("Не удалось определить температуру системы охлаждения первого контура");
            if (rand.Next(1, 20) > 18) throw new CoolantPressureReadException("Не удалось определить давление в системе охлаждения первого контура");
            return c;
        }

        public CoolantSystemStatus VerifyBackupCoolantSystem()
        {
            CoolantSystemStatus c = CoolantSystemStatus.Fail;
            int r = rand.Next(1, 10);
            if (r > 5)
            {
                c = CoolantSystemStatus.OK;
            }
            else if (r > 2)
            {
                c = CoolantSystemStatus.Check;
            }
            if (rand.Next(1, 20) > 19) throw new CoolantTemperatureReadException("Не удалось определить температуру резервной системы охлаждения");
            if (rand.Next(1, 20) > 19) throw new CoolantPressureReadException("Не удалось определить давление в резервной системе охлаждения");
            return c;
        }

        public double GetCoreTemperature()
        {
            if (rand.Next(1, 20) > 18) throw new CoreTemperatureReadException("Не удалось определить температуру системы активной зоны реактора");
            return rand.NextDouble() * 1000;
        }

        public SuccessFailureResult InsertRodCluster()
        {
            SuccessFailureResult r = SuccessFailureResult.Fail;
            if (rand.Next(1, 100) > 5) r = SuccessFailureResult.Success;
            if (rand.Next(1, 10) > 8) throw new RodClusterReleaseException("Неисправность датчика, не удается проверить расцепление штока");
            return r;
        }

        public double GetRadiationLevel()
        {
            if (rand.Next(1, 20) > 18) throw new CoreRadiationLevelReadException("Не удалось определить уровни радиации в активной зоне реактора");
            return rand.NextDouble() * 500;
        }

        public void SignalShutdownComplete()
        {
            if (rand.Next(1, 20) > 18) throw new SignallingException("Сбой в сети при подключении к широковещательным системам");
        }
    }

    public class PowerGeneratorCommsException : Exception
    {
        public PowerGeneratorCommsException(string message) : base(message) { }
    }

    public class CoolantSystemException : Exception
    {
        public CoolantSystemException(string message) : base(message) { }
    }

    public class CoolantTemperatureReadException : CoolantSystemException
    {
        public CoolantTemperatureReadException(string message) : base(message) { }
    }

    public class CoolantPressureReadException : CoolantSystemException
    {
        public CoolantPressureReadException(string message) : base(message) { }
    }

    public class CoreTemperatureReadException : Exception
    {
        public CoreTemperatureReadException(string message) : base(message) { }
    }

    public class CoreRadiationLevelReadException : Exception
    {
        public CoreRadiationLevelReadException(string message) : base(message) { }
    }

    public class RodClusterReleaseException : Exception
    {
        public RodClusterReleaseException(string message) : base(message) { }
    }

    public class SignallingException : Exception
    {
        public SignallingException(string message) : base(message) { }
    }
}
