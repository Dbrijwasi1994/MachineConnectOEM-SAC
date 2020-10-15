using MachineConnectApplication;
using MachineConnectOEM.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
    /// Interaction logic for PPMSettingsChild.xaml
    /// </summary>
    public partial class PPMSettingsChild : UserControl
    {
        List<string> GroupIDList = new List<string> { "Grinding Cycle monitoring", "GRINDING APPLICATION PARAMETERS", "Live dashboard", "Load screen" };
        List<string> DataTypeList = new List<string> { "Bool", "numeric", "Sort", "double", "byte", "text" };
        List<string> TemplateTypeList = new List<string> { "Text", "High/Low Limits" };
        List<double> FreqIDList = new List<double> { 0.5, 1, 2, 4, 6, 10 };
        static ObservableCollection<ProcessParamConfigModelNew> prgListaData = null;
        public PPMSettingsChild()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindProcessParameter();
        }
        private void BindProcessParameter()
        {
            try
            {
                prgListaData = new ObservableCollection<ProcessParamConfigModelNew>();
                prgListaData = DatabaseAccess.BindProcessParametersNew();
                this.DataGridMGT.DataContext = prgListaData;
                this.DataGridMGT.ItemsSource = prgListaData;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
        }


        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridMGT.IsReadOnly = false;
                if (btnNew.Content.ToString().Trim() == "New")
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
                    DataGridMGT.CanUserAddRows = false;
                    BindProcessParameter();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool IsUpdated = false;
            try
            {
                ObservableCollection<ProcessParamConfigModelNew> prgConfigDataList = (DataGridMGT.ItemsSource as ObservableCollection<ProcessParamConfigModelNew>);
                if (prgConfigDataList == null) return;
                foreach (ProcessParamConfigModelNew item in prgConfigDataList)
                {
                    if (item.IsRowChanged)
                    {
                        if (string.IsNullOrEmpty(item.DisplayText))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Display Text !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.LowerValue))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Lower Value !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.HigherValue))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Higher Value !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.HighRedLimit))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the High Red Limit !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.LowRedLimit))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Low Red Limit !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.HighGreenLimit))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the High Green Limit !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.LowGreenLimit))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Low Green Limit !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.HighYellowLimit))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the High Yellow Limit !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (string.IsNullOrEmpty(item.LowYellowLimit))
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please enter the Low Yellow Limit !!");
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
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Please select the Enability !!");
                            dialog.ShowDialog();
                            return;
                        }
                        if (item.SortOrder < 1)
                        {
                            CustomDialogBox dialog = new CustomDialogBox("Information Message", "Sort Order Cannot be 0 or less !!");
                            dialog.ShowDialog();
                            return;
                        }
                        DatabaseAccess.SaveProcessParameterData(item.IDD, item.GroupId, item.ParameterId, item.ParameterName, item.DisplayText, item.LowerValue, item.HigherValue, item.HighRedLimit, item.LowRedLimit, item.DBDataType, item.HighGreenLimit, item.LowGreenLimit, item.HighYellowLimit, item.LowYellowLimit, item.Unit, item.IsVisible, item.SortOrder, item.Freqency, item.Register, item.TemplateType, item.DivideBy, out IsUpdated);
                    }
                }
                if (IsUpdated)
                {
                    CustomDialogBox dialog = new CustomDialogBox("Information Message", "Details added / Updated successfully.");
                    dialog.ShowDialog();
                    BindProcessParameter();
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
                        var selectedRow = DataGridMGT.SelectedItem as ProcessParamConfigModelNew;
                        if (selectedRow != null)
                        {
                            DatabaseAccess.DeleteProcessParamData(selectedRow.IDD, selectedRow.ParameterId, out isDeleted);
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
                BindProcessParameter();
                //BindProcessParamConfigGrid();
            }
        }

        private void cmb_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            cmb.ItemsSource = GroupIDList;
        }

        private void cmbParameterId_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            List<string> PID = new List<string>();
            for (int i = 1; i <= 100; i++)
            {
                PID.Add("P" + i.ToString());
            }
            cmb.ItemsSource = PID;
        }

        private void cmbFreq_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            cmb.ItemsSource = FreqIDList;
        }

        private void cmbDBDatatype_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            cmb.ItemsSource = DataTypeList;
        }

        private void TextBlockDecimal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int count = 0;
            if (e.Source is System.Windows.Controls.TextBox)
            {
                string text = (e.Source as System.Windows.Controls.TextBox).Text + e.Text;
                char ch = e.Text.ToCharArray().Last();
                if (ch.Equals('.'))
                    count = text.ToCharArray().Where(x => x.Equals('.')).Count();
                if ((char.IsDigit(ch) || ch.Equals('.')) && count <= 1)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TextBoxNumeric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char ch = e.Text.ToCharArray().Last();
            if (char.IsDigit(ch))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void cmbtemplateType_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            cmb.ItemsSource = TemplateTypeList;
        }

        private void cmbDivideBy_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            cmb.ItemsSource = new List<int>() { 1, 10, 100, 1000, 10000 };
        }
    }
}
