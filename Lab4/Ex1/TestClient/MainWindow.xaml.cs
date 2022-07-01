using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.textBlock1.Text = "Инициирующая последовательность испытаний: " + DateTime.Now.ToLongTimeString();
            SwitchDevices.Switch sd = new SwitchDevices.Switch();

            // Step 1 - disconnect from the Power Generator
            try
            {
                if (sd.DisconnectPowerGenerator() == SwitchDevices.SuccessFailureResult.Fail)
                {
                    this.textBlock1.Text += "\nStep 1: Не удалось отключить систему выработки электроэнергии";
                }
                else
                {
                    this.textBlock1.Text += "\nStep 1: Успешно отключенная система выработки электроэнергии";
                }
            }
            catch (SwitchDevices.PowerGeneratorCommsException ex)
            {

                this.textBlock1.Text += "\n*** Исключение в step 1: " + ex.Message;
            }

            // Step 2 - Verify the status of the Primary Coolant System
            try
            {
                switch (sd.VerifyPrimaryCoolantSystem())
                {
                    case SwitchDevices.CoolantSystemStatus.OK:
                        this.textBlock1.Text += "\nStep 2: Первичная система охлаждения в порядке";
                        break;
                    case SwitchDevices.CoolantSystemStatus.Check:
                        this.textBlock1.Text += "\nStep 2: Система охлаждения первого контура требует ручной проверки";
                        break;
                    case SwitchDevices.CoolantSystemStatus.Fail:
                        this.textBlock1.Text += "\nStep 2: Сообщается о проблеме с системой охлаждения первого контура";
                        break;
                }
            }
            catch (SwitchDevices.CoolantPressureReadException ex)
            {
                this.textBlock1.Text += "\n*** Исключение в step 2: " + ex.Message;
            }
            catch (SwitchDevices.CoolantTemperatureReadException ex)
            {
                this.textBlock1.Text += "\n*** Исключение в step 2: " + ex.Message;
            }

            // Step 3 - Verify the status of the Backup Coolant System
            try
            {
                switch (sd.VerifyBackupCoolantSystem())
                {
                    case SwitchDevices.CoolantSystemStatus.OK:
                        this.textBlock1.Text += "\nStep 3: Резервная система охлаждения в порядке";
                        break;
                    case SwitchDevices.CoolantSystemStatus.Check:
                        this.textBlock1.Text += "\nStep 3: Резервная система охлаждения требует ручной проверки";
                        break;
                    case SwitchDevices.CoolantSystemStatus.Fail:
                        this.textBlock1.Text += "\nStep 3: Резервная система с системой охлаждения первого контура";
                        break;
                }
            }
            catch (SwitchDevices.CoolantPressureReadException ex)
            {
                this.textBlock1.Text += "\n*** Исключение в step 3: " + ex.Message;
            }
            catch (SwitchDevices.CoolantTemperatureReadException ex)
            {
                this.textBlock1.Text += "\n*** Исключение в step 3: " + ex.Message;
            }

            // Step 4 - Record the core temperature prior to shutting down the reactor
            try
            {
                this.textBlock1.Text += "\nStep 4: Температура активной зоны перед выключением: " + sd.GetCoreTemperature();
            }
            catch (SwitchDevices.CoreTemperatureReadException ex)
            {
                this.textBlock1.Text += "\n*** Исключение в step 4: " + ex.Message;
            }

            // Step 5 - Insert the control rods into the reactor
            try
            {
                if (sd.InsertRodCluster() == SwitchDevices.SuccessFailureResult.Success)
                {
                    this.textBlock1.Text += "\nStep 5: Управляющие стержни успешно вставлены";
                }
                else
                {
                    this.textBlock1.Text += "\nStep 5: Установка управляющего стержня не удалась";
                }
            }
            catch (SwitchDevices.RodClusterReleaseException ex)
            {
                this.textBlock1.Text += "\n*** Исключение в step 5: " + ex.Message;
            }

            // Step 6 - Record the core temperature after shutting down the reactor
            try
            {
                this.textBlock1.Text += "\nStep 6: Температура активной зоны после выключения: " + sd.GetCoreTemperature();
            }
            catch (SwitchDevices.CoreTemperatureReadException ex )
            {
                this.textBlock1.Text += "\n*** Исключение в step 6: " + ex.Message;
            }

            // Step 7 - Record the core radiation levels after shutting down the reactor
            try
            {
                this.textBlock1.Text += "\nStep 7: Уровень радиации в активной зоне после отключения: " + sd.GetRadiationLevel();
            }
            catch (SwitchDevices.CoreRadiationLevelReadException ex)
            {
                this.textBlock1.Text += "\n*** Исключение в step 7: " + ex.Message;
            }

            // Step 8 - Broadcast "Shutdown Complete" message
            try
            {
                sd.SignalShutdownComplete();
                this.textBlock1.Text += "\nStep 8: Сообщение о завершении широковещательной передачи";
            }
            catch (SwitchDevices.SignallingException ex)
            {
                this.textBlock1.Text += "\n*** Исключение в step 8: " + ex.Message;
            }
 
            this.textBlock1.Text += "\nПоследовательность испытаний завершена: " + DateTime.Now.ToLongTimeString();
        }
    }
}
