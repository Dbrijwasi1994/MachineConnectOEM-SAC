using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MachineConnectApplication;
using MachineConnectOEMSAC;

namespace MachineConnectOEM.SAC
{
    /// <summary>
    /// Interaction logic for DefinePMRules.xaml
    /// </summary>
    public partial class DefinePMRules : UserControl
    {
        string freq;
        string year;
        string oldDate;
        string newDate;
        string activity;
        bool updateActivity = false;
        public ObservableCollection<DataGridColumn> ColumnCollection
        {
            get;
            set;
        }

        public DefinePMRules()
        {
            InitializeComponent();
        }

        private void btnView_Clicked(object sender, RoutedEventArgs e)
        {
            string fromTime = dtpFrom.Text;
            string toTime = dtpTo.Text;

            DataTable dttmp = DatabaseAccess.GetActivityData(cmbFreq.SelectedValue.ToString(), cmbYear.SelectedValue.ToString(), fromTime, toTime);
            if (cmbFreq.SelectedValue.ToString().Equals("weekly", StringComparison.OrdinalIgnoreCase))
            { 
                dgPMR.MinColumnWidth = 150; 
            }
            else
            {
                dgPMR.MinColumnWidth = 120;
            }
            dgPMR.ItemsSource = dttmp.AsDataView();
        }

        private void btnGenerate_Clicked(object sender, RoutedEventArgs e)
        {
            string startTime = dtpStartDate.Text;
            if (!startTime.Equals(""))
            {
                DialogBox db = new DialogBox("Info", "Delete existing data and generate information?");
                bool? res = db.ShowDialog();
                if (res == true)
                {
                    res = DatabaseAccess.GenerateActivityData(cmbFreq.SelectedValue.ToString(), cmbYear.SelectedValue.ToString(), startTime);
                    if (res == true)
                    {
                        db = new DialogBox("Info", "Activity information generated successfully");
                        db.ShowDialog();
                        
                        btnView_Clicked(sender, e);
                    }
                    else
                    {
                        db = new DialogBox("Info", "Activity information could not be generated");
                        db.ShowDialog();
                    }
                }
            }
        }

        private void PMRules_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            List<string> yrlst = Enumerable.Range(2010, 30).Select(x => x.ToString()).ToList();
            cmbYear.ItemsSource = yrlst;
            cmbYear.SelectedItem = now.Year.ToString();
            List<string> freqlst = (new [] { "Daily", "Weekly", "15 Days", "1 Month", "3 Month", "6 Month", "1 Year", "2 Year" }).ToList();
            cmbFreq.ItemsSource = freqlst;
            cmbFreq.SelectedIndex = 0;
            dtpStartDate.Text = now.ToString("yyyy-MM-dd HH:mm");
            now = new DateTime(now.Year, now.Month, 1);
            dtpFrom.Text = now.ToString("yyyy-MM-dd HH:mm");
            dtpTo.Text = now.AddMonths(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm");
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "yyyy-MM-dd\nHH:mm";
            }
        }


        private void dgPMR_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGridCellTarget = sender as DataGridCell;
            if (dataGridCellTarget != null)
            {
                var txtbox = dataGridCellTarget.Content as TextBlock;
                if (txtbox != null)
                {
                    oldDate = txtbox.Text;
                    year = cmbYear.SelectedValue.ToString();
                    NewDateTime ndwin = new NewDateTime(oldDate.Replace("\n", " "));
                    if (ndwin.ShowDialog() == true)
                    {
                        newDate = ndwin.NewDate;
                        updateActivity = true;
                    }
                    else
                    {
                        updateActivity = false;
                    }
                    Logger.WriteDebugLog(txtbox.Text);
                }
            }
        }
        // [S_GetActivityMasterYearlyData_MGTL] freq,year,olddate,newdate,activity,'Update'
        private void dgPMR_Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid x = sender as DataGrid;
            if (x != null && x.SelectedIndex > -1)
            {
                DataTable dttmp = ((DataView)dgPMR.ItemsSource).ToTable();
                if (dttmp != null)
                {
                    DataRow row = dttmp.Rows[x.SelectedIndex];
                    freq = row["Frequency"].ToString();
                    activity = row["Activity"].ToString();

                    if (updateActivity)
                    {
                        bool proceed = true;
                        DateTime oldd = Convert.ToDateTime(oldDate);
                        DateTime newd = Convert.ToDateTime(newDate);
                        switch(freq.ToLower())
                        {
                            case "daily":
                                if (oldd.Day != newd.Day)
                                {
                                    proceed = false;
                                    DialogBox db = new DialogBox("Info", "The new date cannot be on a different day");
                                    db.ShowDialog();
                                    
                                }
                                break;
                            case "weekly":
                                if (CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(oldd, CalendarWeekRule.FirstDay, DayOfWeek.Monday) != CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(newd, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                                {
                                    proceed = false;
                                    DialogBox db = new DialogBox("Info", "The new date cannot be on a different week");
                                    db.ShowDialog();
                                    
                                }
                                break;
                            case "monthly":
                                if (oldd.Month != newd.Month)
                                {
                                    proceed = false;
                                    DialogBox db = new DialogBox("Info", "The new date cannot be on a different month");
                                    db.ShowDialog();
                                    
                                }
                                break;
                            case "yearly":
                                if (oldd.Year != newd.Year)
                                {
                                    proceed = false;
                                    DialogBox db = new DialogBox("Info", "The new date cannot be on a different year");
                                    db.ShowDialog();
                                    
                                }
                                break;
                                // include 15 days/ 3, 6 month / 2 years if they mean fortnight, quarter etc.
                        }
                        if (proceed)
                        {
                            DialogBox db = null;
                            if (DatabaseAccess.UpdateActivityData(freq, year, oldDate.Replace("\n", " "), newDate, activity))
                            {
                                db = new DialogBox("Info", "Updated successfully");
                                string fromTime = dtpFrom.Text;
                                string toTime = dtpTo.Text;
                                dgPMR.ItemsSource = DatabaseAccess.GetActivityData(cmbFreq.SelectedValue.ToString(), cmbYear.SelectedValue.ToString(), fromTime, toTime).AsDataView();
                            }
                            else
                            {
                                db = new DialogBox("Info", "Could not update");
                            }
                            db.ShowDialog();
                        }
                        else
                        {
                            newDate = oldDate;
                        }
                    }
                }
            }
        }
    }
}
