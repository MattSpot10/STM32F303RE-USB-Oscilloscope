using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Management;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using static USB_AutoLaunchSW.TrayApp;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using USB_AutoLaunchSW;

namespace USB_AutoLaunchSW
{









    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Start the tray application
            Application.Run(new TrayApp());
        }
    }







    // TrayApp will manage the system tray icon
    public class TrayApp : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;

        private string DEVICE_ID1 = "VID_0483";//Device VID
        private string DEVICE_ID2 = "PID_5740";//Device PID
        private string DEVICE_ID3 = "";//Device Serial Number



        private Thread eventThread;




        string configFilePath = "config.json";
        public class USBSettings
        {
            public string VendorID { get; set; }
            public string ProductID { get; set; }
            public string SerialNumber { get; set; }
            public string ExecutablePath { get; set; }
            public bool StartOnInsert { get; set; }
            public bool KillOnRemove { get; set; }
            public Process RunningProcess { get; set; }
        }



        public string ConfigFileExampleText = @"
{
  ""USBSettings"": [
    {
      ""VendorID"": ""VID_0483"",
      ""ProductID"": ""PID_5740"",
      ""SerialNumber"": """",
      ""ExecutablePath"": ""C:\\Program Files (x86)\\EMBO\\EMBO.exe"",
      ""StartOnInsert"": true,
      ""KillOnRemove"": true,
      ""RunningProcess"": """"
    }
  ]
}";

        public class Config
        {
            public List<USBSettings> USBSettings { get; set; }
        }
        Config localConfig;

        public TrayApp()
        {
            // Create a context menu with one item (Exit)
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Restart", null, OnStart);
            trayMenu.Items.Add("Stop", null, OnStop);
            trayMenu.Items.Add("Configure", null, OnConfigure);
            trayMenu.Items.Add("Help", null, OnHelp);
            trayMenu.Items.Add("Exit", null, OnExit);


            trayMenu.Items[1].Enabled = true;  // Enable "Stop"

            // Create the tray icon and set properties
            trayIcon = new NotifyIcon
            {
                Icon = new System.Drawing.Icon("icon.ico"),
                ContextMenuStrip = trayMenu,     // Set the ContextMenuStrip
                Visible = true
            };

            ReadConfigIntoLocal();



            eventThread = new Thread(WatchUSBEvents);
            eventThread.IsBackground = true; // Allow the thread to exit when the app closes
            eventThread.Start();
        }

        public void ReadConfigIntoLocal()
        {
            if (!File.Exists(configFilePath))
            {
                File.WriteAllText(configFilePath, ConfigFileExampleText);

            }
            else
            {
                try
                {
                    // Read the file contents
                    string json = File.ReadAllText(configFilePath);
                    localConfig = JsonConvert.DeserializeObject<Config>(json);
                    foreach (var usbSetting in localConfig.USBSettings)
                    {
                        usbSetting.RunningProcess = null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading config: " + ex.Message);
                }
            }
        }

        ManagementEventWatcher watcher;
        ManagementEventWatcher removeWatcher;


        private void WatchUSBEvents()
        {
            // Create a ManagementEventWatcher to listen for USB insertions and removals
            string insertQuery = "SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_PnPEntity'";
            string removeQuery = "SELECT * FROM __InstanceDeletionEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_PnPEntity'";

            watcher = new ManagementEventWatcher(new WqlEventQuery(insertQuery));
            watcher.EventArrived += new EventArrivedEventHandler(USBInserted);
            watcher.Start();

            removeWatcher = new ManagementEventWatcher(new WqlEventQuery(removeQuery));
            removeWatcher.EventArrived += new EventArrivedEventHandler(USBRemoved);
            removeWatcher.Start();

            //MessageBox.Show("In Thread");

            // Keep the service running
            Thread.Sleep(Timeout.Infinite);

            //MessageBox.Show("Out of Thread");
        }

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private void USBInserted(object sender, EventArrivedEventArgs e)
        {
            System.Threading.Thread.Sleep(500);  // Wait 1/2 second to ensure USB enumeration


            // Handle USB device insertion
            //MessageBox.Show("USB device inserted.");

            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            string pnpDeviceID = targetInstance["PNPDeviceID"]?.ToString(); // Get PNPDeviceID for the COM port
                                                                            //MessageBox.Show(pnpDeviceID);

            foreach (var usbSetting in localConfig.USBSettings)
            {
                if (pnpDeviceID.Contains(usbSetting.VendorID) && pnpDeviceID.Contains(usbSetting.ProductID) && pnpDeviceID.Contains(usbSetting.SerialNumber))
                {
                    if (usbSetting.StartOnInsert)
                    {
                        if (usbSetting.RunningProcess == null || usbSetting.RunningProcess.HasExited)
                        {
                            usbSetting.RunningProcess = new Process();
                            usbSetting.RunningProcess.StartInfo.FileName = usbSetting.ExecutablePath; // Path to the application you want to start
                            usbSetting.RunningProcess.Start();

                            usbSetting.RunningProcess.WaitForInputIdle();
                            SetForegroundWindow(usbSetting.RunningProcess.MainWindowHandle);
                        }

                    }

                }
            }


        }

        private void USBRemoved(object sender, EventArrivedEventArgs e)
        {
            // Handle USB device removal
            //MessageBox.Show("USB device removed.");

            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            string pnpDeviceID = targetInstance["PNPDeviceID"]?.ToString(); // Get PNPDeviceID for the COM port
                                                                            //MessageBox.Show($"removed PNPDeviceID {pnpDeviceID}");

            foreach (var usbSetting in localConfig.USBSettings)
            {
                if (pnpDeviceID.Contains(usbSetting.VendorID) && pnpDeviceID.Contains(usbSetting.ProductID) && pnpDeviceID.Contains(usbSetting.SerialNumber))
                {
                    if (usbSetting.KillOnRemove)
                    {
                        if (usbSetting.RunningProcess != null && !usbSetting.RunningProcess.HasExited)
                        {
                            usbSetting.RunningProcess.Kill();
                            usbSetting.RunningProcess = null; // Optional: reset the process reference after killing
                                              //MessageBox.Show("Process killed!");
                        }
                    }

                }
            }
        }



        private void OnStart(object? sender, EventArgs e)
        {
            trayMenu.Items[0].Text = "Restart";
            trayMenu.Items[1].Enabled = true;  // Enable "Stop"
            //MessageBox.Show("On Start");

            ReadConfigIntoLocal();

            watcher.Start();
            removeWatcher.Start();
        }
        private void OnStop(object? sender, EventArgs e)
        {
            trayMenu.Items[0].Text = "Start";
            trayMenu.Items[1].Enabled = false;  // Disable "Stop"
            //MessageBox.Show("On Stop");
            watcher.Stop();
            removeWatcher.Stop();
        }



        private void OnConfigure(object? sender, EventArgs e)
        {
            trayIcon.Visible = true;

            //ProcessStartInfo info = new ProcessStartInfo();
            //info.FileName = "notepad.exe";
            //info.UseShellExecute = true;
            Process.Start("notepad.exe", configFilePath);
        }
        private void OnHelp(object? sender, EventArgs e)
        {
            string helpFilePath = "README.html";
            Process.Start(new ProcessStartInfo(helpFilePath) { UseShellExecute = true });
        }



        // Handle the Exit menu item
        private void OnExit(object? sender, EventArgs e)
        {
            // Cleanup and close the app
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}



