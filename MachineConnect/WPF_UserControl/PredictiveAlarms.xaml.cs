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
using System.Data;
using System.IO;
using System.Windows.Interop;
using System.Threading.Tasks;

namespace MachineConnectApplication.WPF_UserControl
{
     
    public partial class PredictiveAlarms : UserControl
    {
        Task backgroundTask = null;       
        TaskScheduler uiThreadScheduler = null;
        string alarmsFolderPath = string.Empty;
        string alarmsMTBPath = string.Empty;
        string machineModel = string.Empty;
        System.Windows.Threading.DispatcherTimer timer1 = new System.Windows.Threading.DispatcherTimer();

        public PredictiveAlarms()
        {
            InitializeComponent();
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            GetDataForDataGrid();
            timer1.Start();
        }


        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {          
            FileInfo[] filesInDir = null;
            string alarmImage = string.Empty;

            DataRowView drv = (DataRowView)PredectiveAlarmGrid.SelectedItem;

            if (drv == null) return;

            String result = System.IO.Path.Combine(alarmsFolderPath, alarmsMTBPath, "Alarms", (drv["AlarmNo"]).ToString()); // Path.combine

            if (string.IsNullOrEmpty(result))
            {
                imageSource.DataContext = System.IO.Path.Combine(Settings.APP_PATH, "Images", "ImageNotFound.png");
                //CustomDialogBox cmb = new CustomDialogBox("Information Message", "No Image Found for Alarm - " + drv["AlarmNo"].ToString());
                //cmb.ShowDialog();
                return;
            }

            if (!Directory.Exists(result))
            {
                imageSource.DataContext = System.IO.Path.Combine(Settings.APP_PATH, "Images", "ImageNotFound.png");
                return;
            }
            DirectoryInfo dirPath = new DirectoryInfo(result);

            try
            {
                filesInDir = dirPath.GetFiles("*" + (drv["AlarmNo"]).ToString() + @"_0" + "*.*",SearchOption.TopDirectoryOnly);
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
                
            }

            if (filesInDir != null && filesInDir.Count() > 0)
            {
                try
                {
                    alarmImage = System.IO.Path.Combine(result , filesInDir[0].ToString());
                }
                catch (Exception ex)
                {
                    Settings.WriteErrorMsg(ex.ToString());
                }
            }

            if (!string.IsNullOrEmpty( alarmImage))
            { 
                imageSource.DataContext = alarmImage;
            }
            else
            {
                 imageSource.DataContext = null;
                 imageSource.DataContext = System.IO.Path.Combine(Settings.APP_PATH, "Images", "ImageNotFound.png");
                //CustomDialogBox cmb = new CustomDialogBox("Information Message", "No Image Found for Alarm - " + drv["AlarmNo"].ToString());
                //cmb.ShowDialog();               
            }
                
        }   

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            timer1.Stop();
            timer1.Interval = TimeSpan.FromMinutes(Settings.AutoRefreshInterval); 
            uiThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();            
            GetDataForDataGrid();
            timer1.Start();
        }


        private void GetDataForDataGrid()
        {
            if (backgroundTask != null && backgroundTask.Status == TaskStatus.Running) return;

            DataTable dt = null;

            var stopageBackgroundTask = new Task(() =>
            {
                dt = DatabaseAccess.GetPredictiveAlarmsData(HomeScreen.selectedMachine);
                alarmsFolderPath = DatabaseAccess.GetGenericMachineConnectFolderPath();
                alarmsMTBPath = DatabaseAccess.GetAlarmsMTBPath(HomeScreen.selectedMachine, out machineModel);


                if (string.IsNullOrEmpty(alarmsFolderPath))
                {
                    alarmsFolderPath = System.IO.Path.Combine(Settings.APP_PATH, "AlarmsAndDocs");
                }

            });

            var stoppageTask = stopageBackgroundTask.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    CustomDialogBox cb = new CustomDialogBox("Error Message", t.Exception.InnerException.Message);
                    cb.ShowDialog();
                    return;
                }

                PredectiveAlarmGrid.DataContext = dt.DefaultView;
                PredectiveAlarmGrid.CanUserAddRows = false;

            }, uiThreadScheduler);
            stopageBackgroundTask.Start();
        }  
      
    }

}
