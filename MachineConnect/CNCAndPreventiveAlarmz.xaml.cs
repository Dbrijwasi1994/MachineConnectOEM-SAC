using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MachineConnectApplication.WPF_UserControl
{
    /// <summary>
    /// Interaction logic for CNCAndPreventiveAlarmz.xaml
    /// </summary>

    public partial class CNCAndPreventiveAlarmz : UserControl
    {
        string machineModel = string.Empty;
        string alarmsFolderPath = string.Empty;
        string alarmsMTBPath = string.Empty;
        Task backgroundTask = null;
        TaskScheduler uiThreadScheduler = null;
        System.Windows.Threading.DispatcherTimer timer1 = new System.Windows.Threading.DispatcherTimer();
        string CURRENT_DATE_TIME = MainScreen.CURRENT_DATE_TIME;


        public CNCAndPreventiveAlarmz()
        {
            InitializeComponent();
            DataGridCNC.AutoGenerateColumns = false;
            DataGridPreventive.AutoGenerateColumns = false;
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            GetDataForAllDataGrids();
        }

        private void DataGridCNCRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridCNC.SelectedIndex >= 0)
            {
                imageSource.DataContext = null;
                DataGridAlarmCauses.Visibility = System.Windows.Visibility.Hidden;
                DataGridAlarmCauseAMS.Visibility = System.Windows.Visibility.Hidden;
                AlarmCauseGrid.RowDefinitions[1].Height = new GridLength(230, GridUnitType.Pixel);

                DataRowView drv = (DataRowView)DataGridCNC.SelectedItem;
                if (drv == null) return;
                SetImageForAlarmNumber(drv);
                if (alarmsMTBPath == "AMS")
                {
                    List<AlarmAMS> lstAlarmAMS = new List<AlarmAMS>();
                    DataTable dataAMS = DatabaseAccess.GetAlarmCausesAndSolution((drv["AlarmNo"]).ToString(), alarmsMTBPath, HomeScreen.selectedMachine);
                    foreach (DataRow row in dataAMS.Rows)
                    {
                        AlarmAMS alarmAMS = new AlarmAMS();
                        alarmAMS.AlarmNo = Convert.ToInt32(row["AlarmNo"].ToString());
                        alarmAMS.SlNo = Convert.ToInt32(row["slno"].ToString());
                        alarmAMS.Text = row["Cause"].ToString();
                        alarmAMS.Type = "Alarm Cause";
                        alarmAMS.BackColor = "Blue";
                        alarmAMS.FontWeight = "Medium";
                        alarmAMS.Visibility = "Visible";
                        lstAlarmAMS.Add(alarmAMS);

                        char[] delimiterChars = { '^' };
                        var value = row["Solution"].ToString();
                        string[] array = value.Split(delimiterChars, StringSplitOptions.None);
                        int slNo = 1;
                        foreach (string item in array)
                        {
                            AlarmAMS alarmCause = new AlarmAMS();
                            alarmCause.AlarmNo = Convert.ToInt32(row["AlarmNo"].ToString());
                            alarmCause.Text = item;
                            alarmCause.Type = "Alarm Solution";
                            alarmCause.BackColor = "Black";
                            alarmCause.FontWeight = "Normal";
                            alarmCause.Visibility = "Hidden";
                            alarmCause.SlNo = slNo++;
                            lstAlarmAMS.Add(alarmCause);
                        }

                    }
                    DataGridAlarmCauseAMS.Visibility = System.Windows.Visibility.Visible;
                    DataGridAlarmCauseAMS.Columns[0].Visibility = Visibility.Collapsed;
                    DataGridAlarmCauseAMS.ItemsSource = lstAlarmAMS;

                    //AlarmAMSCauseData();
                }
                else
                {
                    DataGridAlarmCauses.Visibility = System.Windows.Visibility.Visible;
                    DataGridAlarmCauses.DataContext = DatabaseAccess.GetAlarmCausesAndSolution((drv["AlarmNo"]).ToString(), alarmsMTBPath, HomeScreen.selectedMachine).DefaultView;
                }
            }

        }
        public class AlarmAMS
        {
            public int SlNo { get; set; }
            public string Text { get; set; }
            public string Type { get; set; }
            public string BackColor { get; set; }
            public int AlarmNo { get; set; }
            //--------Extra Property------
            public string FontWeight { get; set; }
            public string Visibility { get; set; }
        }


        private void DataGridAlarmCauses_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridAlarmCauses.SelectedIndex >= 0)
            {
                FileInfo[] filesInDir = null;
                string alarmImage = string.Empty;
                string alarmNo = string.Empty;

                AlarmCauses.Visibility = System.Windows.Visibility.Visible;
                AlarmCauseGrid.RowDefinitions[1].Height = new GridLength(250, GridUnitType.Pixel);

                DataRowView drv = (DataRowView)DataGridAlarmCauses.SelectedItem;
                if (drv == null) return;
                //TODO - first check the alarm images inside modal (jobber) folder, if found show the image
                // if not found, existing logic
                String resultModel = System.IO.Path.Combine(alarmsFolderPath, alarmsMTBPath, "Alarms", machineModel, drv["AlarmNo"].ToString());
                if (!Directory.Exists(resultModel))
                {
                    String result = System.IO.Path.Combine(alarmsFolderPath, alarmsMTBPath, "Alarms", drv["AlarmNo"].ToString());
                    if (!Directory.Exists(result))
                    {
                        alarmImage = System.IO.Path.Combine(Settings.APP_PATH, "Images", "ImageNotFound.png");
                    }
                    else
                    {

                        ImageUpload(ref filesInDir, ref alarmImage, ref alarmNo, drv, result);
                    }
                }
                else
                {
                    ImageUpload(ref filesInDir, ref alarmImage, ref alarmNo, drv, resultModel);
                }

                if (!string.IsNullOrEmpty(alarmImage))
                {
                    imageSource.DataContext = alarmImage;
                }
            }

        }

        private void ImageUpload(ref FileInfo[] filesInDir, ref string alarmImage, ref string alarmNo, DataRowView drv, String result)
        {
            DirectoryInfo dirPath = new DirectoryInfo(result);
            try
            {
                alarmNo = (Convert.ToInt16((drv["slno"]).ToString())).ToString();
                filesInDir = dirPath.GetFiles("*" + (drv["AlarmNo"]).ToString() + @"_" + alarmNo + "*.*");
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            if (filesInDir != null && filesInDir.Length > 0)
            {
                alarmImage = (result + @"\" + filesInDir[0].ToString());
            }

            if (string.IsNullOrEmpty(alarmImage))
            {
                alarmImage = System.IO.Path.Combine(Settings.APP_PATH, "Images", "ImageNotFound.png");
            }
            imageSource.DataContext = alarmImage;
            dirPath = null;
        }

        private void DataGridPreventiveRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridPreventive.SelectedIndex >= 0)
            {
                imageSource.DataContext = null;
                DataGridAlarmCauses.Visibility = System.Windows.Visibility.Hidden;
                DataGridAlarmCauseAMS.Visibility = System.Windows.Visibility.Hidden;
                AlarmCauseGrid.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Pixel);
                DataRowView drv = (DataRowView)DataGridPreventive.SelectedItem;
                if (drv == null) return;

                SetImageForAlarmNumber(drv);
            }

        }

        private void SetImageForAlarmNumber(DataRowView drv)
        {

            string alarmImage = string.Empty;
            FileInfo[] filesInDir = null;
            String resiltModel = System.IO.Path.Combine(alarmsFolderPath, alarmsMTBPath, "Alarms", machineModel, (drv["AlarmNo"]).ToString());
            //"D:\\SairojAlamProject\\SATYA\\MachineConnectOEM-2016\\MachineConnectOEM-2016\\MachineConnect\\AlarmsAndDocs\\ACE\\Alarms\\Jobber\\1000";
            if (!Directory.Exists(resiltModel))
            {
                String result = System.IO.Path.Combine(alarmsFolderPath, alarmsMTBPath, "Alarms", (drv["AlarmNo"]).ToString());
                if (!Directory.Exists(result))
                {
                    alarmImage = System.IO.Path.Combine(Settings.APP_PATH, "Images", "ImageNotFound.png");
                }
                else
                {
                    ImageUpload2(drv, ref alarmImage, ref filesInDir, result);
                }
            }
            else
            {
                ImageUpload2(drv, ref alarmImage, ref filesInDir, resiltModel);
            }

            if (!string.IsNullOrEmpty(alarmImage))
            {
                imageSource.DataContext = alarmImage;
            }

        }

        private static void ImageUpload2(DataRowView drv, ref string alarmImage, ref FileInfo[] filesInDir, String result)
        {
            DirectoryInfo dirPath = new DirectoryInfo(result);
            try
            {
                filesInDir = dirPath.GetFiles("*" + (drv["AlarmNo"]).ToString() + @"_0" + "*.*", SearchOption.TopDirectoryOnly);
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            if (filesInDir != null && filesInDir.Count() > 0)
            {
                try
                {
                    alarmImage = System.IO.Path.Combine(result, filesInDir[0].ToString());
                }
                catch (Exception ex)
                {
                    Settings.WriteErrorMsg(ex.ToString());
                }
            }
            else
            {
                alarmImage = System.IO.Path.Combine(Settings.APP_PATH, "Images", "ImageNotFound.png");
            }
            dirPath = null;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridAlarmCauses.Visibility = System.Windows.Visibility.Hidden;
            DataGridAlarmCauseAMS.Visibility = System.Windows.Visibility.Hidden;
            AlarmCauseGrid.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Pixel);

            uiThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            timer1.Stop();
            timer1.Interval = TimeSpan.FromMinutes(Settings.AutoRefreshInterval);
            GetDataForAllDataGrids();

            imageSource.DataContext = null;

            timer1.Start();
        }

        private void GetDataForAllDataGrids()
        {
            if (backgroundTask != null && backgroundTask.Status == TaskStatus.Running) return;

            DataTable CNCDataGirdData = null;
            DataTable PredectiveDataGirdData = null;
            string defaultThreshold = string.Empty;

            var stopageBackgroundTask = new Task(() =>
            {
                CNCDataGirdData = DatabaseAccess.GetCNCDataGirdData(MainScreen.CURRENT_DATE_TIME, HomeScreen.selectedMachine); // Pick Machine from the login screen (Last Param)
                PredectiveDataGirdData = DatabaseAccess.GetPredectiveDataGirdData(MainScreen.CURRENT_DATE_TIME, HomeScreen.selectedMachine);
                alarmsFolderPath = DatabaseAccess.GetGenericMachineConnectFolderPath();

                if (string.IsNullOrEmpty(alarmsFolderPath))
                {
                    alarmsFolderPath = System.IO.Path.Combine(Settings.APP_PATH, "AlarmsAndDocs");
                }
                //TODO - get Modal (Jobber) from machineInformation tble
                alarmsMTBPath = DatabaseAccess.GetAlarmsMTBPath(HomeScreen.selectedMachine, out machineModel);


            });

            var stoppageTask = stopageBackgroundTask.ContinueWith(t =>
            {
                timer1.Start();
                if (t.IsFaulted)
                {
                    CustomDialogBox cb = new CustomDialogBox("Error Message", t.Exception.InnerException.Message);
                    cb.ShowDialog();
                    return;
                }

                DataGridCNC.DataContext = CNCDataGirdData.DefaultView;
                DataGridPreventive.DataContext = PredectiveDataGirdData.DefaultView;

            }, uiThreadScheduler);
            stopageBackgroundTask.Start();
        }

        private void CNCGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView drv = (DataRowView)DataGridCNC.SelectedItem;
            if (drv != null)
            {
                if (drv["color"].Equals("1"))
                {
                    drv["color"] = "0";
                    DatabaseAccess.UpdateAlarmStatus(HomeScreen.selectedMachine, drv["AlarmNo"].ToString());
                }
            }
        }

        private void PreventiveGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView drv = (DataRowView)DataGridPreventive.SelectedItem;
            if (drv != null)
            {
                if (drv["color"].Equals("1"))
                {
                    drv["color"] = "0";
                    DatabaseAccess.UpdateAlarmStatus(HomeScreen.selectedMachine, drv["AlarmNo"].ToString());
                }
            }
        }


        private void DataGridAlarmCauseAMS_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridAlarmCauseAMS.SelectedIndex >= 0)
            {
                FileInfo[] filesInDir = null;
                string alarmImage = string.Empty;
                string alarmNo = string.Empty;


                AlarmAMS alarmValue = (AlarmAMS)DataGridAlarmCauseAMS.SelectedItem;
                alarmNo = alarmValue.AlarmNo.ToString();
                var SlNo = alarmValue.SlNo;

                String resultModel = System.IO.Path.Combine(alarmsFolderPath, alarmsMTBPath, "Alarms", machineModel, alarmNo);
                if (!Directory.Exists(resultModel))
                {
                    String result = System.IO.Path.Combine(alarmsFolderPath, alarmsMTBPath, "Alarms", alarmNo);
                    if (!Directory.Exists(result))
                    {
                        alarmImage = System.IO.Path.Combine(Settings.APP_PATH, "Images", "ImageNotFound.png");
                    }
                    else
                    {

                        ImageUploadAMS(ref filesInDir, ref alarmImage, ref alarmNo, SlNo.ToString(), result);
                    }
                }
                else
                {
                    ImageUploadAMS(ref filesInDir, ref alarmImage, ref alarmNo, SlNo.ToString(), resultModel);
                }

                if (!string.IsNullOrEmpty(alarmImage))
                {
                    imageSource.DataContext = alarmImage;
                }
            }
        }

        private void ImageUploadAMS(ref FileInfo[] filesInDir, ref string alarmImage, ref string alarmNo, string upperSlNo, String result)
        {
            DirectoryInfo dirPath = new DirectoryInfo(result);
            try
            {
                filesInDir = dirPath.GetFiles("*" + alarmNo + @"_" + upperSlNo + "*.*");
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            if (filesInDir != null && filesInDir.Length > 0)
            {
                alarmImage = (result + @"\" + filesInDir[0].ToString());
            }

            if (string.IsNullOrEmpty(alarmImage))
            {
                alarmImage = System.IO.Path.Combine(Settings.APP_PATH, "Images", "ImageNotFound.png");
            }

            imageSource.DataContext = alarmImage;
            dirPath = null;
        }

    }


}
