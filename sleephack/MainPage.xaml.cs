using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Facebook.Client;
using Microsoft.Devices.Sensors;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using SleepHack.DataStorage;
using SleepHack.DataStructures;
using SleepHack.Helpers;

namespace SleepHack
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const int SleepCycles = 5000;//50;
        private const int SleepBigCycles = 250; // 2;
        private static string _flag;
        private readonly Accelerometer _accelSensor;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            _accelSensor = new Accelerometer
                {
                    TimeBetweenUpdates = new TimeSpan(1000000)
                };

            _accelSensor.ReadingChanged += AccelerometerReadingChanged;
           }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (string.IsNullOrEmpty(App.FacebookId))
                NavigationService.Navigate(new Uri("/FacebookLoginPage.xaml", UriKind.Relative));
            _flag = Guid.NewGuid().ToString();
            var fid = string.IsNullOrEmpty(App.FacebookId) ? "-1" : App.FacebookId;
            _bigCycle = new SleepData(_flag, fid);
            _cycle = new SleepData(_flag, fid);
            timePicker.Value = DateTime.Now.AddHours(7);
        }

        private void ButtonTap(object sender, GestureEventArgs e)
        {
            _accelSensor.Start();
            border1.Visibility = Visibility.Visible;
            ApplicationBar.IsVisible = false;
            var temp = ScheduledActionService.Find("SleepHackAlarm");
            if (temp != null)
                ScheduledActionService.Remove("SleepHackAlarm");

            Alarm alarm = new Alarm("SleepHackAlarm");
            DateTime wakeUpTime;
            if (DateTime.Now.Hour > timePicker.Value.Value.Hour || (DateTime.Now.Hour == timePicker.Value.Value.Hour && DateTime.Now.Minute > timePicker.Value.Value.Minute))
                wakeUpTime = DateTime.Today.AddDays(1).AddHours(timePicker.Value.Value.Hour).AddMinutes(timePicker.Value.Value.Minute);
            else
                wakeUpTime = DateTime.Today.AddHours(timePicker.Value.Value.Hour).AddMinutes(timePicker.Value.Value.Minute);
            alarm.BeginTime = wakeUpTime;
            alarm.Sound = new Uri("/Ringtones/Silly_Snoring-Snore_Man-618028931.wav", UriKind.Relative);
            ScheduledActionService.Add(alarm);
        }
        private void Border1Tap(object sender, GestureEventArgs e)
        {
            border1.Visibility = Visibility.Collapsed;
            ApplicationBar.IsVisible = true;
        }

        public void AppBarSkydriveClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Skydrive.xaml", UriKind.Relative));
        }

        public void AccelerometerReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => ThreadSafeAccelerometerChanged(e));
        }

        private static double _prevX;
        private static double _prevY;
        private static double _prevZ;
        private static Movement _prevMovement = Movement.None;
        private static SleepData _cycle;
        private static SleepData _bigCycle;

        private void ThreadSafeAccelerometerChanged(AccelerometerReadingEventArgs e)
        {
          var move = AccelerometerToMovementParser.AnalyzeToMovement(
               e.X,
               e.Y,
               e.Z,
               _prevX,
               _prevY,
               _prevZ,
               _prevMovement
               );
            _prevX = e.X;
            _prevY = e.Y;
            _prevZ = e.Z;
            _prevMovement = move;
            if (_bigCycle.Sleepmeasures.Count < SleepBigCycles)
            {
                if (_cycle.Sleepmeasures.Count < SleepCycles)
                {
                  _cycle.Sleepmeasures.Add(new Sleepmeasure
                        {
                            MeasureDate = DateTime.Now,
                            Movement = move,
                            ActivityType = "1"
                        });
                }
                else
                {
                    var allTheRightMove =
                        AccelerometerToMovementParser.Analyze(_cycle.Sleepmeasures.Select(x => x.Movement).ToList());
                    var maxIndex = _bigCycle.Sleepmeasures.Count - 1;
                    var curr = allTheRightMove;
                    while (maxIndex > 0)
                    {
                        var prev = _bigCycle.Sleepmeasures[maxIndex];
                        if (prev.Movement < curr)
                            prev.Movement = curr - 1;
                        else
                            break;
                        maxIndex--;
                        curr = prev.Movement;
                    }

                    var r = new PostData
                    {
                        DataType = typeof(SleepData)
                    };
                                      r.SaveData(_cycle);

                    _bigCycle.Sleepmeasures.Add(
                        new Sleepmeasure { MeasureDate = DateTime.Now, Movement = allTheRightMove, ActivityType = "0"});
                    _cycle = new SleepData(_flag, App.FacebookId);
                }
            }
            else
            {
                _bigCycle.EndTime = DateTime.Now;
                var r = new PostData
                {
                    DataType = typeof(SleepData)
                };

                r.SaveData(_bigCycle);
                _bigCycle = new SleepData(_flag, App.FacebookId);
            }
        }

        private void ApplicationBarMenuItemClick(object sender, EventArgs e)
        {

        }

        private void AppBarFacebookClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Facebook.xaml", UriKind.Relative));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _accelSensor.Start();
        }

        private void BluetoothClick(object sender, RoutedEventArgs e)
        {
        }
    }

}
