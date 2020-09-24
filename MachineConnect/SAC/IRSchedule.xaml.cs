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
using System.Dynamic;
using System.Data;
using MachineConnectApplication;
using System.Collections.ObjectModel;

namespace MachineConnectOEM.SAC
{
    /// <summary>
    /// Interaction logic for IRSchedule.xaml
    /// </summary>
    public partial class IRSchedule : UserControl
    {
        string currentFreq = string.Empty;
        string currentFreqValue = "1";
        DateTime currentSelectedDate = DateTime.Now;
        ObservableCollection<Frequency> frequncy = null;
        public IRSchedule()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindFrequency();
            LoadActivities();
        }

        private void LoadActivities()
        {
            if (listBoxFrequency.Items.Count > 0)
            {
                var frst = listBoxFrequency.Items.CurrentItem as Frequency;
                if (frst != null)
                {
                    currentFreq = frst.frequency;
                    BindGrid(HomeScreen.selectedMachine, frst.frequency, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "1");
                }
            }
        }

        private void BindFrequency()
        {
            frequncy = new ObservableCollection<Frequency>();
            frequncy = DataBaseAccess_SAC.GetAllFrequecies();
            frequncy[0].Color = "Red";
            listBoxFrequency.ItemsSource = frequncy;
        }

        private string prevmachineid, prevfreq, prevdatetime, prevparam, prevfreqval;

        private void BindGrid(string mcId, string frequency, string dateTime, string Param, string frequencyValue)
        {
            try
            {
                DataTable DT = DataBaseAccess_SAC.GetActivityInfo_MGTL(mcId, frequency, dateTime, Param, frequencyValue);
                string[] columnNames = null;
                if (DT != null && DT.Rows.Count != 0)
                {
                    columnNames = DT.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
                    prevmachineid = mcId;
                    prevfreq = frequency;
                    prevdatetime = dateTime;
                    prevparam = Param;
                    prevfreqval = frequencyValue;
                }
                else
                {
                    CustomDialogBox dlgErr = new CustomDialogBox("No Data", "Data not available for selected machine and frequency");
                    dlgErr.ShowDialog();
                    return;
                }

                datagrid.ItemsSource = null;
                datagrid.Columns.Clear();
                datagrid.Items.Clear();
                datagrid.Items.Refresh();
                Mouse.OverrideCursor = Cursors.Wait;
                
                columnNames = columnNames.Where(val => val != "Act").ToArray();
                GenerateGrid(columnNames);
                datagrid.ItemsSource = DT.DefaultView;
                currentFreq = frequency;
                currentFreqValue = frequencyValue;
            }
            catch (Exception e)
            {
                Logger.WriteErrorLog(e.ToString());
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void GenerateGrid(string[] columnNames)
        {
            if (columnNames == null)
                return;

            var col = new DataGridTextColumn();
            foreach (var item in columnNames)
            {
                if (item != "ActivityID")
                {
                    var ElementStyle = new Style(typeof(TextBlock));
                    ElementStyle.Setters.Add(new Setter(FrameworkElement.MarginProperty, new Thickness(8)));
                    ElementStyle.Setters.Add(new Setter(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center));
                    col.Header = item;
                    if (item == "Sl No")
                    {
                        col.Binding = new Binding(item) { NotifyOnSourceUpdated = true, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Mode = BindingMode.OneWay };
                    }
                    else
                    {
                        col.Binding = new Binding(item) { NotifyOnSourceUpdated = true, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Mode = BindingMode.TwoWay };
                    }
                    if (item.Equals("Activity", StringComparison.OrdinalIgnoreCase))
                        col.Width = new DataGridLength(1.0, DataGridLengthUnitType.Auto);

                    if (item != "Sl No" && item != "Activity" && item != "Act" && item != "Frequency" && item != "LastUpdate" && item != "ActivityID")
                    {
                        DataTrigger trigger = new DataTrigger()
                        {
                            Binding = new Binding(item),
                            Value = "P"
                        };
                        trigger.Setters.Add(new Setter(ForegroundProperty, Brushes.Red));
                        ElementStyle.Triggers.Add(trigger);

                        trigger = new DataTrigger()
                        {
                            Binding = new Binding(item),
                            Value = "C"
                        };
                        trigger.Setters.Add(new Setter(ForegroundProperty, new SolidColorBrush(Colors.DarkGreen)));
                        ElementStyle.Triggers.Add(trigger);

                        trigger = new DataTrigger()
                        {
                            Binding = new Binding(item),
                            Value = "U"
                        };
                        trigger.Setters.Add(new Setter(ForegroundProperty, new SolidColorBrush(Colors.Orange)));
                        ElementStyle.Triggers.Add(trigger);

                        // g:
                        trigger = new DataTrigger()
                        {
                            Binding = new Binding(item),
                            Value = "NON"
                        };
                        trigger.Setters.Add(new Setter(ForegroundProperty, new SolidColorBrush(Colors.Orange)));
                        ElementStyle.Triggers.Add(trigger);
                    }

                    col.ElementStyle = ElementStyle;
                    datagrid.Columns.Add(col);
                    col = new DataGridTextColumn();
                }
            }
            datagrid.FrozenColumnCount = 2;
        }

        private void btnFrequency_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            if (btn != null)
            {
                currentSelectedDate = DateTime.Now;
                if (btn.Content.ToString() == "Daily")
                {
                    BindGrid(HomeScreen.selectedMachine, "Daily", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "1");
                }
                else if (btn.Content.ToString() == "Weekly")
                {
                    currentSelectedDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
                    BindGrid(HomeScreen.selectedMachine, "Weekly", currentSelectedDate.ToString("yyyy-MM-dd HH:mm:ss"), "current", "7");
                }
                else if (btn.Content.ToString() == "15 Days")
                {
                    BindGrid(HomeScreen.selectedMachine, "15 Days", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "15");
                    // btn.Background = new SolidColorBrush(Colors.Red);
                }
                else if (btn.Content.ToString() == "1 Month")
                {
                    BindGrid(HomeScreen.selectedMachine, "1 Month", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "1");
                }
                else if (btn.Content.ToString() == "3 Month")
                {
                    BindGrid(HomeScreen.selectedMachine, "3 Month", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "3");
                }
                else if (btn.Content.ToString() == "6 Month")
                {
                    BindGrid(HomeScreen.selectedMachine, "6 Month", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "6");
                }
                else if (btn.Content.ToString() == "1 Year")
                {
                    BindGrid(HomeScreen.selectedMachine, "1 Year", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "1");
                }
                else if (btn.Content.ToString() == "2 Year")
                {
                    BindGrid(HomeScreen.selectedMachine, "2 Year", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "2");
                }
                else
                {
                    BindGrid(HomeScreen.selectedMachine, "Daily", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "1");
                }
                foreach (Frequency freq in frequncy)
                {
                    if (freq.frequency.Equals(btn.Content.ToString()))
                    {
                        freq.Color = "Red";
                    }
                    else
                    {
                        freq.Color = "Black";
                    }
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool IsUpdated = false;
            DataTable dt = ((DataView)datagrid.ItemsSource).ToTable();
            if (dt != null)
            {
                DataTable changedDt = dt.GetChanges();
                if (changedDt != null && changedDt.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataRow row in changedDt.Rows)
                        {
                            for (int i = 5; i < row.ItemArray.Length; i++)
                            {
                                if (row[i].ToString() == "C")
                                {
                                    string Header = dt.Columns[i].ColumnName;
                                    if (currentFreq == "Daily" || currentFreq == "Weekly" || currentFreq == "15 Days" || currentFreq == "1 Month" || currentFreq == "3 Month" || currentFreq == "6 Month")
                                    {
                                        DataBaseAccess_SAC.UpdateIRSchedule(row["ActivityID"].ToString(), currentFreq, Convert.ToDateTime(Header).AddHours(6).ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), HomeScreen.selectedMachine, out IsUpdated);
                                    }
                                    else if (currentFreq == "1 Year" || currentFreq == "2 Year")
                                    {
                                        DataBaseAccess_SAC.UpdateIRSchedule(row["ActivityID"].ToString(), currentFreq, Convert.ToDateTime("Jan " + Header).AddHours(6).ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), HomeScreen.selectedMachine, out IsUpdated);
                                    }
                                    else
                                    {
                                        CustomDialogBox cdb = new CustomDialogBox("Information Message", "No I&R Schedule to save");
                                        cdb.ShowDialog();
                                    }
                                }
                            }
                        }
                        if (IsUpdated)
                        {
                            CustomDialogBox dlgSuccess = new CustomDialogBox("Information Message", "Inspection List Master Information Updated Successfully.");
                            dlgSuccess.ShowDialog();
                            RefreshIRSchedule();
                            DataBaseAccess_SAC.IsUpdated = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        CustomDialogBox cdb = new CustomDialogBox("Error", ex.ToString());
                        cdb.ShowDialog();
                    }
                }
                else
                {
                    CustomDialogBox cdb = new CustomDialogBox("Information Message", "No I&R Schedule completed to save");
                    cdb.ShowDialog();
                }
            }
        }

        private void dataGrid_HeaderDoubleClick(object sender, RoutedEventArgs e)
        {
            DateTime dtSelected = DateTime.Now;
            try
            {
                System.Windows.Controls.Primitives.DataGridColumnHeader col = sender as System.Windows.Controls.Primitives.DataGridColumnHeader;
                string colName = col.Column.Header.ToString();
                IRSLogin loginWindow = new IRSLogin();
                loginWindow.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                loginWindow.ShowDialog();
                if (Settings.AccesAllowed)
                {
                    Settings.AccesAllowed = false;

                    foreach (DataRowView item in datagrid.Items)
                    {
                        if (item.Row[colName].ToString().Equals("P"))
                        {
                            item.Row[colName] = "C";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("dataGrid_HeaderDoubleClick: " + ex.ToString());
            }
        }

        private void dataGrid_CellDoubleClick(object sender, RoutedEventArgs e)
        {
            DateTime dtSelected = DateTime.Now;
            var cell = sender as DataGridCell;
            if (cell != null && cell.Content is TextBlock)
            {
                string val = string.Empty;
                var textBlock = cell.Content as TextBlock;
                if (textBlock.Text.Equals("P", StringComparison.OrdinalIgnoreCase))
                {
                    IRSLogin loginWindow = new IRSLogin();
                    loginWindow.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    loginWindow.ShowDialog();
                    if (Settings.AccesAllowed)
                    {
                        Settings.AccesAllowed = false;
                        string Header = datagrid.SelectedCells[0].Column.Header.ToString();
                        if (currentFreq == "Daily" || currentFreq == "Weekly" || currentFreq == "15 Days")
                        {
                            if (!string.IsNullOrEmpty(Header) && DateTime.TryParse(Header, out dtSelected))
                            {
                                if (DateTime.Compare(dtSelected, DateTime.Now) <= 0)
                                {
                                    val = "C";
                                    textBlock.SetValue(TextBlock.TextProperty, val);
                                }
                            }
                        }
                        else if (currentFreq == "1 Month" || currentFreq == "3 Month" || currentFreq == "6 Month")
                        {
                            if (!string.IsNullOrEmpty(Header) && DateTime.TryParse(Header, out dtSelected))
                            {
                                if (DateTime.Compare(dtSelected, DateTime.Now) <= 0)
                                {
                                    val = "C";
                                    textBlock.SetValue(TextBlock.TextProperty, val);
                                }
                            }
                        }
                        else if (currentFreq == "1 Year" || currentFreq == "2 Year")
                        {
                            if (!string.IsNullOrEmpty(Header) && DateTime.TryParse("Jan " + Header, out dtSelected))
                            {
                                if (DateTime.Compare(dtSelected, DateTime.Now) <= 0)
                                {
                                    val = "C";
                                    textBlock.SetValue(TextBlock.TextProperty, val);
                                }
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            //BindGrid(HomeScreen.selectedMachine, currentFreq, currentSelectedDate.AddDays(5).ToString("yyyy-MM-dd HH:mm:ss"), "next", currentFreqValue);
            //if (currentFreq == "Daily")
            //{
            //    currentSelectedDate = currentSelectedDate.AddDays(10);
            //}
            //if (currentFreq == "Weekly")
            //{
            //    currentSelectedDate = currentSelectedDate.AddDays(70);
            //}
            //if (currentFreq == "15 Days")
            //{
            //    currentSelectedDate = currentSelectedDate.AddDays(150);
            //}
            //if (currentFreq == "1 Month")
            //{
            //    currentSelectedDate = currentSelectedDate.AddMonths(10);
            //}
            //if (currentFreq == "3 Month")
            //{
            //    currentSelectedDate = currentSelectedDate.AddMonths(30);
            //}
            //if (currentFreq == "6 Month")
            //{
            //    currentSelectedDate = currentSelectedDate.AddMonths(60);
            //}
            //if (currentFreq == "1 Year")
            //{
            //    currentSelectedDate = currentSelectedDate.AddYears(10);
            //}
            //if (currentFreq == "2 Year")
            //{
            //    currentSelectedDate = currentSelectedDate.AddYears(20);
            //}
            DateTime dt;
            List<DateTime> lst = datagrid.Columns.Where(x => DateTime.TryParse(x.Header.ToString(), out dt)).Select(x => Convert.ToDateTime(x.Header.ToString())).ToList();
            if (lst.Count == 0)
            {
                return;
            }
            currentSelectedDate = lst.Max();
            if (currentFreq.ToLower().Contains("month"))
            {
                currentSelectedDate = currentSelectedDate.AddMonths(1).AddSeconds(-1);
            }
            if (currentFreq.ToLower().Contains("year"))
            {
                currentSelectedDate = currentSelectedDate.AddYears(1).AddSeconds(-1);
            }
            BindGrid(HomeScreen.selectedMachine, currentFreq, currentSelectedDate.ToString("yyyy-MM-dd HH:mm:ss"), "Next", currentFreqValue);
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            //if (currentFreq == "Daily")
            //{
            //    currentSelectedDate = currentSelectedDate.AddDays(-10);
            //}
            //if (currentFreq == "Weekly")
            //{
            //    currentSelectedDate = currentSelectedDate.AddDays(-70);
            //}
            //if (currentFreq == "15 Days")
            //{
            //    currentSelectedDate = currentSelectedDate.AddDays(-150);
            //}
            //if (currentFreq == "1 Month")
            //{
            //    currentSelectedDate = currentSelectedDate.AddMonths(-10);
            //}
            //if (currentFreq == "3 Month")
            //{
            //    currentSelectedDate = currentSelectedDate.AddMonths(-30);
            //}
            //if (currentFreq == "6 Month")
            //{
            //    currentSelectedDate = currentSelectedDate.AddMonths(-60);
            //}
            //if (currentFreq == "1 Year")
            //{
            //    currentSelectedDate = currentSelectedDate.AddYears(-10);
            //}
            //if (currentFreq == "2 Year")
            //{
            //    currentSelectedDate = currentSelectedDate.AddYears(-20);
            //}
            //BindGrid(HomeScreen.selectedMachine, currentFreq, currentSelectedDate.AddDays(5).ToString("yyyy-MM-dd HH:mm:ss"), "Previous", currentFreqValue);
            DateTime dt;
            List<DateTime> lst = datagrid.Columns.Where(x => DateTime.TryParse(x.Header.ToString(), out dt)).Select(x => Convert.ToDateTime(x.Header.ToString())).ToList();
            if (lst.Count == 0)
            {
                return;
            }
            currentSelectedDate = lst.Min();
            //if (currentFreq.ToLower().Contains("month"))
            //{
            //    currentSelectedDate = currentSelectedDate.AddMonths(-1).AddSeconds(1);
            //}
            //if (currentFreq.ToLower().Contains("year"))
            //{
            //    currentSelectedDate = currentSelectedDate.AddYears(-1).AddSeconds(1);
            //}
            BindGrid(HomeScreen.selectedMachine, currentFreq, currentSelectedDate.ToString("yyyy-MM-dd HH:mm:ss"), "Previous", currentFreqValue);
        }

        private void RefreshIRSchedule()
        {
            //if (currentFreq == "Daily")
            //{
            //    BindGrid(HomeScreen.selectedMachine, "Daily", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "1");
            //}
            //if (currentFreq == "Weekly")
            //{
            //    BindGrid(HomeScreen.selectedMachine, "Weekly", currentSelectedDate.ToString("yyyy-MM-dd HH:mm:ss"), "current", "7");
            //}
            //if (currentFreq == "15 Days")
            //{
            //    BindGrid(HomeScreen.selectedMachine, "15 Days", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "15");
            //}
            //if (currentFreq == "1 Month")
            //{
            //    BindGrid(HomeScreen.selectedMachine, "1 Month", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "1");
            //}
            //if (currentFreq == "3 Month")
            //{
            //    BindGrid(HomeScreen.selectedMachine, "3 Month", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "3");
            //}
            //if (currentFreq == "6 Month")
            //{
            //    BindGrid(HomeScreen.selectedMachine, "6 Month", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "6");
            //}
            //if (currentFreq == "1 Year")
            //{
            //    BindGrid(HomeScreen.selectedMachine, "1 Year", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "1");
            //}
            //if (currentFreq == "2 Year")
            //{
            //    BindGrid(HomeScreen.selectedMachine, "2 Year", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "current", "2");
            //}
            BindGrid(prevmachineid, prevfreq, prevdatetime, prevparam, prevfreqval); // g:
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (frequncy != null)
            {
                PredAndPrevExportMenu exportMenu = new PredAndPrevExportMenu(frequncy);
                exportMenu.ShowDialog();
            }
        }
    }
}
