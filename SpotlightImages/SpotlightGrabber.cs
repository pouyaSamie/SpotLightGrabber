using System;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.ServiceProcess;

namespace SpotlightImages
{
    public partial class SpotlightGrabber : ServiceBase
    {
        private readonly System.Timers.Timer _timeDelay;
        private const string PathToSpotlightImage = @"%userprofile%\AppData\Local\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";
        private const string _defaultDestPath = @"%userprofile%\Pictures\Spotlight";
        private double Interval {
            get
            {
                var stringValue =  System.Configuration.ConfigurationManager.AppSettings["Interval"];
                return !string.IsNullOrEmpty(stringValue) ? double.Parse(stringValue) : 60 * 2 * 60 * 1000; //Default for 2 hours
            }
        }
        private string DestinationPath
        {
            get
            {
                var stringValue = System.Configuration.ConfigurationManager.AppSettings["DestinationPath"];
                return !string.IsNullOrEmpty(stringValue) ? stringValue : _defaultDestPath;
            }
        }
        private bool IsLogEnable
        {
            get
            {
                var stringValue = System.Configuration.ConfigurationManager.AppSettings["EnableLog"];
                if (string.IsNullOrEmpty(stringValue)) return false;
                return stringValue == "1";
            }
        }
        private string LogPath
        {
            get
            {
                var stringValue = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
                return string.IsNullOrEmpty(stringValue) ? "C:/" : stringValue;
            }
        }

        private int _count;
        public SpotlightGrabber()
        {
            InitializeComponent();
            _count = 0;
            _timeDelay = new System.Timers.Timer(Interval);
            _timeDelay.Elapsed += WorkProcess;
        }

        private void WorkProcess(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var files = Directory.GetFiles(Environment.ExpandEnvironmentVariables(PathToSpotlightImage));
                foreach (var file in files)
                {
                    try
                    {
                        using (var img = Image.FromFile(file))
                        {

                            LogService(img.Width.ToString());
                            if (img.Width == 1920)
                                File.Copy(file, $@"{Environment.ExpandEnvironmentVariables(DestinationPath)}\{Path.GetFileName(file)}.jpg", true);

                        }
                    }
                    catch (Exception exception)
                    {
                        LogService(exception.ToString());
                    }
                    



                }

            }
            catch (Exception exception)
            {
                LogService(exception.ToString());
                
            }
            
        }
        protected override void OnStart(string[] args)
        {
            LogService("Service is Started");
            if (!Directory.Exists(Environment.ExpandEnvironmentVariables(DestinationPath)))
                Directory.CreateDirectory(Environment.ExpandEnvironmentVariables(DestinationPath));

            
            _timeDelay.Enabled = true;
        }
        protected override void OnStop()
        {
            LogService("Service Stoped");
            _timeDelay.Enabled = false;
        }
        private void LogService(string content)
        {
            if(!IsLogEnable) return;
            
            var fs = new FileStream($@"{LogPath}\SpotlightGrabberLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            var sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            var wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            var username = wp.Identity.Name;
            sw.WriteLine($"{username}==> {content}");
            sw.Flush();
            sw.Close();
        }
    }
}

