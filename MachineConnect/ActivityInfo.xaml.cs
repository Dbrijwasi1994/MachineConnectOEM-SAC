using MachineConnectApplication;
using MachineConnectOEM.SAC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace MachineConnectOEM
{
    /// <summary>
    /// Interaction logic for ActivityInfo.xaml
    /// </summary>
    public partial class ActivityInfo : UserControl
    {
        public ObservableCollection<Frequency> frequncy = new ObservableCollection<Frequency>();
        public ObservableCollection<lstActiviti> listFreq { get; set; }
        public ObservableCollection<ActivityInfoEntity> ActivityInfoList { get; set; }
        public ActivityInfo()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                BindFrequencyComboBox();
                BindActivityInfoGrid("");
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
                CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                frm.ShowDialog();
            }
        }

        private void BindActivityInfoGrid(string freq)
        {
            ActivityInfoList = new ObservableCollection<ActivityInfoEntity>();
            ActivityInfoList = DatabaseAccess.GetAllActivityForGrid(freq);
            if (ActivityInfoList != null && ActivityInfoList.Count > 0)
            {
                this.DataGridMGT.ItemsSource = ActivityInfoList;
            }
            else
            {
                this.DataGridMGT.ItemsSource = new ObservableCollection<ActivityInfoEntity>();
                CustomDialogBox dlgInfo = new CustomDialogBox("Information Message", "No data available for selected frequency");
                dlgInfo.ShowDialog();
            }
        }

        private void BindFrequencyComboBox()
        {
            frequncy = DataBaseAccess_SAC.GetAllFrequecies();
            if (frequncy != null)
            {
                foreach (Frequency freq in frequncy)
                {
                    cmbFrequency.Items.Add(freq.frequency);
                }
            }
            cmbFrequency.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool IsUpdated = false;
            try
            {
                ObservableCollection<ActivityInfoEntity> activityInfoDataList = (DataGridMGT.ItemsSource as ObservableCollection<ActivityInfoEntity>);
                if (activityInfoDataList == null) return;
                foreach (ActivityInfoEntity item in activityInfoDataList)
                {
                    string fh = frequncy.Where(i => i.frequency == item.Frequency).Select(j => j.freqID).First();
                    if (item.IsRowChanged)
                    {
                        if (string.IsNullOrEmpty(item.Activity))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Activity cannot be null or empty. !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.Frequency))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Frequency cannot be null or empty. !!");
                            dialog.ShowDialog();
                            return;
                        }
                        DatabaseAccess.UpdateActivityInfoDetails(item.ActivityID, item.Activity, frequncy.Where(i => i.frequency == item.Frequency).Select(j => j.freqID).First(), out IsUpdated);
                    }
                }
                if (IsUpdated)
                {
                    CustomDialogBox dialog = new CustomDialogBox("Information Message", "Details added / Updated successfully.");
                    dialog.ShowDialog();
                    BindActivityInfoGrid("");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
                CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                frm.ShowDialog();
            }
        }

        private T GetFirstChildByType<T>(DependencyObject prop) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(prop); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild((prop), i) as DependencyObject;
                if (child == null)
                    continue;

                T castedProp = child as T;
                if (castedProp != null)
                    return castedProp;

                castedProp = GetFirstChildByType<T>(child);

                if (castedProp != null)
                    return castedProp;
            }
            return null;
        }

        private void comboFreqType_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmFreq = sender as ComboBox;
            if (frequncy != null)
            {
                foreach (Frequency freq in frequncy)
                {
                    cmFreq.Items.Add(freq.frequency);
                }
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridMGT.IsReadOnly = false;
                if (btnNew.Content.ToString() == "New")
                {
                    DataGridMGT.CanUserAddRows = true;
                    var datagridCellInfo = new DataGridCellInfo(DataGridMGT.Items, DataGridMGT.Columns[0]);
                    btnNew.Content = "Cancel";
                    int rowIndex = DataGridMGT.Items.Count - 1;
                    DataGridMGT.CurrentCell = datagridCellInfo;
                    DataGridMGT.SelectedIndex = DataGridMGT.Items.Count - 1;
                    if (DataGridMGT.Items.Count > 0)
                    {
                        var border = VisualTreeHelper.GetChild(DataGridMGT, 0) as Decorator;
                        if (border != null)
                        {
                            var scroll = border.Child as ScrollViewer;
                            if (scroll != null) scroll.ScrollToEnd();
                            DataGridMGT.CurrentCell = new DataGridCellInfo(DataGridMGT.SelectedIndex, DataGridMGT.Columns[1]);
                            int x = DataGridMGT.Items.Count - 1;
                            DataGridMGT.SelectedItem = DataGridMGT.Items[x];
                            DataGridMGT.ScrollIntoView(DataGridMGT.Items[x]);
                            if (ActivityInfoList.Count > 0)
                            {
                                DataGridRow dgrow = (DataGridRow)DataGridMGT.ItemContainerGenerator.ContainerFromItem(DataGridMGT.Items[x]);
                                dgrow.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                            }
                            DataGridMGT.BeginEdit();
                        }
                    }
                }
                else
                {
                    btnNew.Content = "New";
                    DataGridMGT.Columns[0].IsReadOnly = true;
                    DataGridMGT.CancelEdit();
                    DataGridMGT.DataContext = null;
                    DataGridMGT.ItemsSource = null;
                    var changed = DataGridMGT.ItemsSource as DataTable;
                    if (changed != null && changed.Rows.Count > 0)
                    {
                        foreach (DataRow row in changed.Rows.Cast<DataRow>().ToList())
                        {
                            if (row.RowState == DataRowState.Added)
                            {
                                changed.Rows.Remove(row);
                            }
                        }
                        DataGridMGT.DataContext = changed as DataTable;
                    }
                    if (changed == null)
                    {
                        BindActivityInfoGrid("");
                    }
                    DataGridMGT.CanUserAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            bool isDeleted = false;
            MessageBoxResult res = MessageBox.Show("Are you sure you want to delete the data ? Click Yes to continue...", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                if (DataGridMGT.SelectedItems.Count > 0)
                {
                    try
                    {
                        var selectedRow = DataGridMGT.SelectedItem as ActivityInfoEntity;
                        if (selectedRow != null)
                        {
                            DatabaseAccess.DeleteActivityInfoData(selectedRow.ActivityID, out isDeleted);
                        }
                        if (isDeleted)
                        {
                            CustomDialogBox dbSuccess = new CustomDialogBox("Information Message", "Selected record  deleted successfully.");
                            dbSuccess.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        CustomDialogBox dlgError = new CustomDialogBox("Error!!", ex.Message);
                        dlgError.ShowDialog();
                    }
                }
                else
                {
                    CustomDialogBox dbInfo = new CustomDialogBox("Information Message", "Select a record to delete.");
                    dbInfo.ShowDialog();
                }
                BindActivityInfoGrid("");
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            BindActivityInfoGrid(cmbFrequency.SelectedItem.ToString());
        }
    }

    public class lstActiviti
    {
        public string FreqId
        { get; set; }

        public string FreqName
        { get; set; }
    }
}
