using MachineConnectApplication;
using MachineConnectOEM.DTO;
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
    /// Interaction logic for PRGPage.xaml
    /// </summary>
    public partial class PRGPage : UserControl
    {
        static ObservableCollection<ProcessParamConfigModel> prgListaData = null;
        public PRGPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindProcessParamConfigGrid();
        }

        private void BindProcessParamConfigGrid()
        {
            try
            {
                this.Cursor = Cursors.Wait;
                prgListaData = new ObservableCollection<ProcessParamConfigModel>();
                prgListaData = DatabaseAccess.BindProcessParameters();
                this.DataGridMGT.ItemsSource = prgListaData;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
                CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                frm.ShowDialog();
            }
            this.Cursor = Cursors.Arrow;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            bool IsUpdated = false;
            try
            {
                ObservableCollection<ProcessParamConfigModel> prgConfigDataList = (DataGridMGT.ItemsSource as ObservableCollection<ProcessParamConfigModel>);
                if (prgConfigDataList == null) return;
                foreach (ProcessParamConfigModel item in prgConfigDataList)
                {
                    if (item.IsRowChanged)
                    {
                        if (string.IsNullOrEmpty(item.ParameterName))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Parameter !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.MinValue))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the MinValue !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.MaxValue))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the MaxValue !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.WarningValue))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the WarningValue !!");
                            dialog.ShowDialog();
                            return;
                        }
                        else
                        {
                            if (!int.TryParse(item.WarningValue, out result))
                            {
                                CustomDialogBox dialog = new CustomDialogBox("Information Message", "Warning Value must be an Integer !!");
                                dialog.ShowDialog();
                                return;
                            }
                        }
                        if (string.IsNullOrEmpty(item.RedValue))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the RedValue !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.GreenValue))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the GreenValue !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.YellowValue))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the YellowValue !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.GreenBit))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Green Bit !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.RedBit))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Red Bit !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.YellowBit))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Yellow Bit !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.Red1Bit))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Red1 Bit !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.Red1HValue))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Red Higher Value !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.Red1LValue))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Red Lower Value !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.Unit))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Unit Value !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.IsVisible.ToString()))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please select the Visisbility !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (item.SortOrder < 1)
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Sort Order Cannot be 0 or less !!");
                            dialog.ShowDialog();
                            return;
                        }
                        DatabaseAccess.SavePRGData(item.IDD, item.ParameterId, item.ParameterName, item.MinValue, item.MaxValue, item.WarningValue, item.RedValue, item.GreenValue, item.YellowValue, item.RedBit, item.GreenBit, item.YellowBit, item.Red1Bit, item.Red1HValue, item.Red1LValue, item.Unit, item.TemplateType, item.IsVisible, item.SortOrder, out IsUpdated);
                    }
                }
                if (IsUpdated)
                {
                    CustomDialogBox dialog = new CustomDialogBox("Information Message", "Details added / Updated successfully.");
                    dialog.ShowDialog();
                    BindProcessParamConfigGrid();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
                CustomDialogBox frm = new CustomDialogBox("Error Message", ex.Message);
                frm.ShowDialog();
            }
            this.Cursor = Cursors.Arrow;
        }

        private void comboTemplateType_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmbTemplateType = sender as ComboBox;
            cmbTemplateType.Items.Add("Text");
            cmbTemplateType.Items.Add("High/Low Limits");
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

                            DataGridMGT.Columns[8].IsReadOnly = false;
                            if (prgListaData.Count > 0)
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
                        BindProcessParamConfigGrid();
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
                        var selectedRow = DataGridMGT.SelectedItem as ProcessParamConfigModel;
                        if(selectedRow!=null)
                        {
                            DatabaseAccess.DeleteProcessParamData(selectedRow.IDD, out isDeleted);
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
                BindProcessParamConfigGrid();
            }
        }
    }

    public class NameToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string input = value as string;
            switch (input)
            {
                case "Yellow":
                    return Brushes.Yellow;
                case "Green":
                    return Brushes.Green;
                case "Red":
                    return Brushes.Red;
                default:
                    return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
