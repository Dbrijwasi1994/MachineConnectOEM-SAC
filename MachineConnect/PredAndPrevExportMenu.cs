using FocasGUI;
using MachineConnectOEM.SAC;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;

namespace MachineConnectOEM
{
    public partial class PredAndPrevExportMenu : Form
    {
        ObservableCollection<Frequency> frequencyList = new ObservableCollection<Frequency>();
        string _appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public PredAndPrevExportMenu()
        {
            InitializeComponent();
        }

        public PredAndPrevExportMenu(ObservableCollection<Frequency> freqList)
        {
            InitializeComponent();
            frequencyList = freqList;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PredAndPrevExportMenu_Load(object sender, EventArgs e)
        {
            BindFrequencies();
        }

        private void BindFrequencies()
        {
            foreach (Frequency freq in frequencyList)
            {
                clbFrequency.Items.Add(freq.frequency);
            }

            for (int i = 0; i < clbFrequency.Items.Count; i++)
            {
                clbFrequency.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (frequencyList == null || frequencyList.Count < 1)
            {
                DialogBox dlgBox = new DialogBox("No time frequency to export.");
                dlgBox.Show();
                return;
            }
            DateTime fromDate = dtFromdate.Value;
            DateTime toDate = dtToDate.Value;
            if (fromDate > toDate)
            {
                DialogBox dlgBox = new DialogBox("from date cannot be greater than to date.");
                dlgBox.Show();
                return;
            }
            GenerateReport(fromDate, toDate);
        }

        private void GenerateReport(DateTime fromDate, DateTime toDate)
        {
            try
            {
                string generatedFilePath = Path.Combine(_appPath, "ReportTemplates", "Reports");
                string FilePath = string.Empty;
                ExportPreventiveMaintenanceReport(fromDate, toDate, Path.Combine(_appPath, "ReportTemplates", "PreventiveMaintenanceReport.xlsx"), generatedFilePath, "", out FilePath);

                if (FilePath != string.Empty)
                {
                    DialogBox dlgBox = new DialogBox("Your Preventive Maintenance Report Generated Successfully." + Environment.NewLine + "Opening Report....");
                    dlgBox.Show();
                    OpenExe(FilePath);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExportPreventiveMaintenanceReport(DateTime fromDate, DateTime toDate, string reportTemplatePath, string generatedFilePath, string ExportedReportFile, out string filePath)
        {
            filePath = string.Empty;
            try
            {
                if (!File.Exists(reportTemplatePath))
                {
                    DialogBox dlgBox = new DialogBox("Template is not found on " + reportTemplatePath);
                    dlgBox.Show();
                }
                if (!Directory.Exists(generatedFilePath))
                {
                    Directory.CreateDirectory(generatedFilePath);
                }
                ExportedReportFile = Path.Combine(generatedFilePath, "PreventiveMaintenance" + string.Format("{0:ddMMMyyyy_HHmmss}", DateTime.Now) + ".xlsx");
                filePath = ExportedReportFile;
                if (File.Exists(ExportedReportFile))
                {
                    var dirInfo = new DirectoryInfo(ExportedReportFile);
                    dirInfo.Attributes &= ~FileAttributes.ReadOnly;
                    File.Delete(ExportedReportFile);
                }
                File.Copy(reportTemplatePath, ExportedReportFile, true);
                FillPreventiveMaintenanceReport(fromDate, toDate, ref ExportedReportFile, reportTemplatePath);
                filePath = ExportedReportFile;
            }
            catch (Exception ex)
            {
                DialogBox dlgBox = new DialogBox(ex.Message);
                dlgBox.Show();
            }
        }

        private void FillPreventiveMaintenanceReport(DateTime fromDate, DateTime toDate, ref string exportedReportFile, string reportTemplatePath)
        {
            FileInfo newFile = new FileInfo(exportedReportFile);
            ExcelPackage excelPackage = new ExcelPackage(newFile, true);
            int i = 0;
            foreach (object item in clbFrequency.CheckedItems)
            {
                if (i == 0)
                {
                    ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[1];
                    workSheet.Name = item.ToString();
                    workSheet.Cells["A1"].Value = "PREVENTIVE MAINTENANCE REPORT - " + item.ToString().ToUpper();
                    workSheet.Cells["B5"].Value = fromDate.ToString("dd-MM-yyyy");
                    workSheet.Cells["G5"].Value = toDate.ToString("dd-MM-yyyy");
                    PlotDataToExcel(workSheet, "CNC GRINDING", item.ToString(), fromDate.ToString("yyyy-MM-dd hh:mm:ss"), toDate.ToString("yyyy-MM-dd hh:mm:ss"), GetFreqvalueByFreq(item.ToString()));
                    workSheet.Cells.AutoFitColumns();
                }
                else
                {
                    ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets.Add(item.ToString());
                    if (item.ToString().Equals("Weekly", StringComparison.OrdinalIgnoreCase))
                    {
                        MergeExcelCells(workSheet, "PREVENTIVE MAINTENANCE REPORT - WEEKLY");
                        PlotDataToExcel(workSheet, "CNC GRINDING", item.ToString(), fromDate.ToString("yyyy-MM-dd hh:mm:ss"), toDate.ToString("yyyy-MM-dd hh:mm:ss"), GetFreqvalueByFreq(item.ToString()));
                    }
                    else if (item.ToString().Equals("15 Days", StringComparison.OrdinalIgnoreCase))
                    {
                        MergeExcelCells(workSheet, "PREVENTIVE MAINTENANCE REPORT - 15 DAYS");
                        PlotDataToExcel(workSheet, "CNC GRINDING", item.ToString(), fromDate.ToString("yyyy-MM-dd hh:mm:ss"), toDate.ToString("yyyy-MM-dd hh:mm:ss"), GetFreqvalueByFreq(item.ToString()));
                    }
                    else if (item.ToString().Equals("1 Month", StringComparison.OrdinalIgnoreCase))
                    {
                        MergeExcelCells(workSheet, "PREVENTIVE MAINTENANCE REPORT - 1 MONTH");
                        PlotDataToExcel(workSheet, "CNC GRINDING", item.ToString(), fromDate.ToString("yyyy-MM-dd hh:mm:ss"), toDate.ToString("yyyy-MM-dd hh:mm:ss"), GetFreqvalueByFreq(item.ToString()));
                    }
                    else if (item.ToString().Equals("3 Month", StringComparison.OrdinalIgnoreCase))
                    {
                        MergeExcelCells(workSheet, "PREVENTIVE MAINTENANCE REPORT - 3 MONTH");
                        PlotDataToExcel(workSheet, "CNC GRINDING", item.ToString(), fromDate.ToString("yyyy-MM-dd hh:mm:ss"), toDate.ToString("yyyy-MM-dd hh:mm:ss"), GetFreqvalueByFreq(item.ToString()));
                    }
                    else if (item.ToString().Equals("6 Month", StringComparison.OrdinalIgnoreCase))
                    {
                        MergeExcelCells(workSheet, "PREVENTIVE MAINTENANCE REPORT - 6 MONTH");
                        PlotDataToExcel(workSheet, "CNC GRINDING", item.ToString(), fromDate.ToString("yyyy-MM-dd hh:mm:ss"), toDate.ToString("yyyy-MM-dd hh:mm:ss"), GetFreqvalueByFreq(item.ToString()));
                    }
                    else if (item.ToString().Equals("1 Year", StringComparison.OrdinalIgnoreCase))
                    {
                        MergeExcelCells(workSheet, "PREVENTIVE MAINTENANCE REPORT - 1 YEAR");
                        PlotDataToExcel(workSheet, "CNC GRINDING", item.ToString(), fromDate.ToString("yyyy-MM-dd hh:mm:ss"), toDate.ToString("yyyy-MM-dd hh:mm:ss"), GetFreqvalueByFreq(item.ToString()));
                    }
                    else if (item.ToString().Equals("2 Year", StringComparison.OrdinalIgnoreCase))
                    {
                        MergeExcelCells(workSheet, "PREVENTIVE MAINTENANCE REPORT - 2 YEAR");
                        PlotDataToExcel(workSheet, "CNC GRINDING", item.ToString(), fromDate.ToString("yyyy-MM-dd hh:mm:ss"), toDate.ToString("yyyy-MM-dd hh:mm:ss"), GetFreqvalueByFreq(item.ToString()));
                    }
                    else
                    {
                        MergeExcelCells(workSheet, "PREVENTIVE MAINTENANCE REPORT - DAILY");
                        PlotDataToExcel(workSheet, "CNC GRINDING", "Daily", fromDate.ToString("yyyy-MM-dd hh:mm:ss"), toDate.ToString("yyyy-MM-dd hh:mm:ss"), 1);
                    }
                    workSheet.Cells.AutoFitColumns();
                }
                i++;
            }
            excelPackage.SaveAs(newFile);
        }

        private void MergeExcelCells(ExcelWorksheet workSheet, string sheetTitle)
        {
            workSheet.Cells[1, 1, 3, 15].Merge = true;
            workSheet.Cells["A1"].Value = sheetTitle;
            workSheet.Cells["A1"].Style.Font.Size = 20;
            workSheet.Cells["A1"].Style.Font.Bold = true;
            workSheet.Cells["A1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            workSheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#3465A4"));
            workSheet.Cells["A1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
            workSheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        }

        private void PlotDataToExcel(ExcelWorksheet workSheet, string machineId, string frequency, string startTime, string endTime, int freqValue)
        {
            DataTable dt = new DataTable();
            dt = DataBaseAccess_SAC.GetPreventiveMaintenanceExportData(machineId, frequency, startTime, endTime, freqValue);
            List<string> cols = dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
            cols.Remove("Act"); cols.Remove("Frequency"); cols.Remove("ActivityID");
            int row = 8; int pos = 1;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (string colName in cols)
                {
                    workSheet.Cells[row - 1, pos].Value = colName;
                    workSheet.Cells[row - 1, pos].Style.Font.Bold = true;
                    workSheet.Cells[row - 1, pos].Style.Font.Size = 12;
                    workSheet.Cells[row - 1, pos].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    workSheet.Cells[row - 1, pos].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    workSheet.Cells[row - 1, pos].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#2080D0"));
                    workSheet.Cells[row - 1, pos].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    pos++;
                }
                pos = 0;
                foreach (DataRow rowItem in dt.Rows)
                {
                    for (int j = 1; j <= cols.Count; j++)
                    {
                        workSheet.Cells[row, j].Value = rowItem[cols[j - 1]];
                        workSheet.Cells[row, j].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    }
                    row++;
                }
            }
        }

        internal static void OpenExe(string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "\"" + path + "\"";
            Process.Start(startInfo);
        }

        private int GetFreqvalueByFreq(string freq)
        {
            int freqValue = 0;
            switch (freq)
            {
                case "Daily":
                    freqValue = 1;
                    break;
                case "Weekly":
                    freqValue = 7;
                    break;
                case "15 Days":
                    freqValue = 15;
                    break;
                case "1 Month":
                    freqValue = 1;
                    break;
                case "3 Month":
                    freqValue = 3;
                    break;
                case "6 Month":
                    freqValue = 6;
                    break;
                case "1 Year":
                    freqValue = 1;
                    break;
                case "2 Year":
                    freqValue = 2;
                    break;
                default:
                    freqValue = 1;
                    break;
            }
            return freqValue;
        }
    }
}
