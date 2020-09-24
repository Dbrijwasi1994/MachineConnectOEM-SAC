using FocasGUI;
using MachineConnectApplication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using System.Windows.Threading;

namespace MachineConnectOEM.SAC
{
    /// <summary>
    /// Interaction logic for DashBoard_Sac.xaml
    /// </summary>
    public partial class DashBoard_Sac : UserControl
    {
        public static ObservableCollection<DTO> processParamDashboardData = null;
        public static ObservableCollection<NotificationData> allPendingList = null;
        ObservableCollection<Frequency> freqList = null;
        NotificationDetails notificationData = null;
        public DispatcherTimer paramRefreshTimer = new DispatcherTimer();
        public DateTime pendingActivityStartDate = Convert.ToDateTime(ConfigurationManager.AppSettings["PendingActivitiesStartDate"]);
        public DashBoard_Sac()
        {
            InitializeComponent();
        }

        private void SACDashboard_Loaded(object sender, RoutedEventArgs e)
        {
            BindProcessParamDashboard(HomeScreen.selectedMachine);
            BindNotificationData(HomeScreen.selectedMachine);
            paramRefreshTimer.Stop();
            paramRefreshTimer.Interval = TimeSpan.FromSeconds(Settings.ParamRefreshInterval);
            paramRefreshTimer.Tick += ParamRefreshTimer_Tick;
            paramRefreshTimer.IsEnabled = true;
            paramRefreshTimer.Start();
        }

        public void BindProcessParamDashboard(string SelectedMachine)
        {
            processParamDashboardData = new ObservableCollection<DTO>();
            processParamDashboardData.Clear();
            processParamDashboardData = DataBaseAccess_SAC.GetProcessParamDashboardData(SelectedMachine);
            if (processParamDashboardData != null && processParamDashboardData.Count > 0)
            {
                listBox.ItemsSource = processParamDashboardData;
            }
        }

        private void BindNotificationData(string SelectedMachine)
        {
            DateTime dtNow = DateTime.Now;
            //dtNow = new DateTime(2019, 01, 02); // g: test
            notificationData = new NotificationDetails();
            //freqList = new ObservableCollection<Frequency>();
            //freqList = DataBaseAccess_SAC.GetAllFrequecies();
            notificationData = DataBaseAccess_SAC.GetActivityNotifications(SelectedMachine, dtNow.ToString("yyyy-MM-dd hh:mm:ss"), "");
            //notificationData = DataBaseAccess_SAC.GetActivityNotifications(SelectedMachine, dtNow.ToString("yyyy-MM-dd hh:mm:s"), "", freqList);
            if (notificationData != null)
            {
                listBoxWarning.ItemsSource = notificationData.WarningData;
                listBoxPending.ItemsSource = notificationData.PendingData;
            }
        }

        private void btnAllPending_Click(object sender, RoutedEventArgs e)
        {
            allPendingList = new ObservableCollection<NotificationData>();
            //allPendingList = DataBaseAccess_SAC.GetAllPendingActivities(HomeScreen.selectedMachine, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "Weekly", pendingActivityStartDate.ToString("yyyy-MM-dd hh:mm:ss"));
            allPendingList = DataBaseAccess_SAC.GetAllPendingActivities(HomeScreen.selectedMachine, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "Weekly", pendingActivityStartDate.ToString("yyyy-MM-dd hh:mm:ss"));
            if (allPendingList != null && allPendingList.Count > 0)
            {
                PendingActivities pendingActivities = new PendingActivities(allPendingList, HomeScreen.selectedMachine, pendingActivityStartDate);
                pendingActivities.ShowDialog();
            }
            else
            {
                CustomDialogBox dlgError = new CustomDialogBox("Information Message", "No activities pending after selected date.");
                dlgError.ShowDialog();
            }
        }

        private void ParamRefreshTimer_Tick(object sender, EventArgs e)
        {
            BindProcessParamDashboard(HomeScreen.selectedMachine);
            //BindNotificationData(HomeScreen.selectedMachine);
        }
    }
}
