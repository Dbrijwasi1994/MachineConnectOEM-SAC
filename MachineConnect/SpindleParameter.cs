using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChartDirector;
using System.Data.SqlClient;
using System.Configuration;
//using PCT.TPMTrak.Manager;
//using PCT.TPMTrak.Common;
//using PCT.TPMTrak.DataTransferObjects;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using MachineConnectOEM;

namespace MachineConnectApplication
{
    public partial class RPM : UserControl
    {
        int flag;
        private DateTime[] timeStamps;
        private double[] dataSeriesA;
        private double maxLoad = 0.0;
        private double[] dataSeriesB;
        private double maxSpeed = 0.0;
        private double[] dataSeriesC;
        private double maxTemp = 0.0;
        private DateTime minDate;
        private double dateRange;
        private double maxValue = 0;
        private double minValue = 0;
        public ProcessDoc userControl = null;
        private double currentDuration = 360 * 86400;
        private string currentDurationSelected = string.Empty;
        private bool hasFinishedInitialization = false;
        private ProcessDoc processDoc;
        string chart1Title = string.Empty;
        string chart2Title = string.Empty;
        string chart3Title = string.Empty;
        string MTB = "ACE";


        public RPM()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.ContainerControl |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.SupportsTransparentBackColor
                          , true);
            InitializeComponent();
        }

        public RPM(ProcessDoc processDoc)
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.ContainerControl |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.SupportsTransparentBackColor
                          , true);
            InitializeComponent();
            this.processDoc = processDoc;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        #region -- Data Binding

        private void loadData()
        {
            DateTime lastDate = Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME).AddHours(-8); //DateTime.Now.AddHours(-8);//"05-02-2016 10:00:00 PM"
            DateTime firstDate = Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME);//DateTime.Now;//Convert.ToDateTime("05-02-2015 10:00:00 PM");

            currentDuration = lastDate.Subtract(firstDate.AddHours(-8)).TotalSeconds;
            DataTable dt = new DataTable();
            List<SpindleData> valz = null;
            if (Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME).ToString("yyyy-MM-dd") != (DatabaseAccess.GetShiftStartEndTimeForDay(1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))).ToString("yyyy-MM-dd"))
            {
                byte[] byteArray = DatabaseAccess.PreviousDatesSpindleLoadTemp(HomeScreen.selectedMachine, MainScreen.CURRENT_DATE_TIME, cmbAxis.Text.ToString());
                if (byteArray != null)
                {
                    valz = DeSerialize(Decompress(byteArray));
                }
                if (valz == null || valz.Count == 0)
                {
                    dt = CheckData();
                    timeStamps = new DateTime[dt.Rows.Count];
                    dataSeriesA = new double[dt.Rows.Count];
                    dataSeriesB = new double[dt.Rows.Count];
                    dataSeriesC = new double[dt.Rows.Count];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        timeStamps.SetValue(Convert.ToDateTime(dt.Rows[i]["CNCTimeStamp"]), i);
                        dataSeriesA.SetValue(Convert.ToDouble(dt.Rows[i]["SpindleLoad"].ToString()), i);
                        dataSeriesB.SetValue(Convert.ToDouble(dt.Rows[i]["SpindleSpeed"].ToString()), i);
                        dataSeriesC.SetValue(Convert.ToDouble(dt.Rows[i]["Temperature"].ToString()), i);
                    }

                    //CustomDialogBox cmb = new CustomDialogBox("Information Message","No Data Found for the selected date");
                    //cmb.ShowDialog();
                }
                else
                {
                    //DeserailizeByteArrayToDataTable(dataTbl);
                    //var valz =  DeSerialize(Decompress(byteArray));
                    if (valz != null && valz.Count > 0)
                    {
                        try
                        {
                            timeStamps = new DateTime[valz.Count];
                            dataSeriesA = new double[valz.Count];
                            dataSeriesB = new double[valz.Count];
                            dataSeriesC = new double[valz.Count];

                            for (int i = 0; i < valz.Count; i++)
                            {
                                timeStamps.SetValue((valz[i].ts), i);
                                dataSeriesA.SetValue(valz[i].sl, i);
                                dataSeriesB.SetValue((valz[i].ss), i);
                                dataSeriesC.SetValue((valz[i].st), i);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteErrorLog(ex.ToString());
                        }
                    }
                }
            }
            else
            {
                dt = CheckData();
                timeStamps = new DateTime[dt.Rows.Count];
                dataSeriesA = new double[dt.Rows.Count];
                dataSeriesB = new double[dt.Rows.Count];
                dataSeriesC = new double[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    timeStamps.SetValue(Convert.ToDateTime(dt.Rows[i]["CNCTimeStamp"]), i);
                    dataSeriesA.SetValue(Convert.ToDouble(dt.Rows[i]["SpindleLoad"].ToString()), i);
                    dataSeriesB.SetValue(Convert.ToDouble(dt.Rows[i]["SpindleSpeed"].ToString()), i);
                    dataSeriesC.SetValue(Convert.ToDouble(dt.Rows[i]["Temperature"].ToString()), i);
                }
            }

            //currentDuration = Convert.ToInt32(cmbDuration.Text) * 60 * 60;         //DurationSettings.DurationSelected  

            if (cmbDurationType.SelectedIndex == 0)
            {
                currentDuration = Convert.ToInt32(currentDurationSelected) * 60 * 60;
            }

            if (cmbDurationType.SelectedIndex == 1)
            {
                currentDuration = Convert.ToInt32(currentDurationSelected) * 60;
            }
            if (cmbDurationType.SelectedIndex == 2)
            {
                currentDuration = Convert.ToInt32(currentDurationSelected);
            }
        }


        private void DeserailizeByteArrayToDataTable(byte[] byteArrayData)
        {
            string csvData = string.Empty;
            try
            {
                using (MemoryStream stream = new MemoryStream(byteArrayData))
                {
                    using (MemoryStream stream2 = new MemoryStream())
                    {
                        using (GZipStream decompressionStream = new GZipStream(stream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(stream2);
                            csvData = Encoding.UTF8.GetString(stream2.ToArray());

                            char[] stringSeparators = new char[] { '\r', '\n' };
                            string[] val = csvData.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                            var list = val.ToList();
                            list.RemoveAt(0);
                            val = list.ToArray();

                            timeStamps = new DateTime[val.Length];
                            dataSeriesA = new double[val.Length];
                            dataSeriesB = new double[val.Length];
                            dataSeriesC = new double[val.Length];

                            for (int i = 0; i < val.Length; i++)
                            {
                                try
                                {
                                    var rowVals = val[i].Split(',');
                                    if (rowVals.Length < 4) continue;
                                    timeStamps.SetValue(Convert.ToDateTime(rowVals[2]), i);
                                    dataSeriesA.SetValue(Convert.ToDouble(rowVals[1]), i);
                                    dataSeriesB.SetValue(Convert.ToDouble(rowVals[0]), i);
                                    dataSeriesC.SetValue(Convert.ToDouble(rowVals[3]), i);
                                    Console.WriteLine(i.ToString());
                                }
                                catch (Exception ex)
                                {
                                    int aa = 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private DataTable CheckData()
        {
            // #region byme
            DateTime fromDate;
            DateTime toDate;
            DateTime dataStarted = Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME);//DateTime.Now;
            DateTime dataEnded = Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME);// DateTime.Now;
            int lastDuration = 0;
            fromDate = DatabaseAccess.GetShiftStartEndTimeForDay(1, dtpStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));// DateTime.Now.AddHours(-24);
            toDate = Convert.ToDateTime(MainScreen.LOGICAL_DAY_END);//.AddHours(24);;//DateTime.Now;
            lastDuration = Convert.ToInt32(cmbDuration.Text.ToString()) * 60 * 60;
            int CompareDate = Convert.ToInt32((toDate.Ticks - fromDate.Ticks) / 10000000);
            if (lastDuration > CompareDate)
            {
                lastDuration = CompareDate;
            }

            DataTable dt = DatabaseAccess.GetSpindleLoadSpeedTemp(HomeScreen.selectedMachine, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), cmbAxis.Text.ToString());

            if (dt.Rows.Count > 1)
            {
                dataStarted = Convert.ToDateTime(dt.Rows[0]["CNCTimeStamp"].ToString());
                dataEnded = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["CNCTimeStamp"].ToString());
                if ((Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME) - dataEnded).TotalMinutes > 1)
                {
                    //DataRow drnew = dt.NewRow();
                    //drnew["SpindleLoad"] = "0.0";
                    //drnew["SpindleSpeed"] = "0.0";
                    //drnew["Temperature"] = "0.0";
                    //drnew["CNCTimeStamp"] = dataEnded.AddSeconds(1);
                    //dt.Rows.Add(drnew);
                    //drnew = dt.NewRow();
                    //drnew["SpindleLoad"] = "0.0";
                    //drnew["SpindleSpeed"] = "0.0";
                    //drnew["Temperature"] = "0.0";
                    //drnew["CNCTimeStamp"] = toDate;
                    //dt.Rows.Add(drnew);
                }
            }
            else
            {
                dataEnded = Convert.ToDateTime(toDate);
                dataStarted = Convert.ToDateTime(toDate).AddSeconds(-lastDuration);
                DataRow dr1 = dt.NewRow();
                dr1["SpindleLoad"] = "0.0";
                dr1["SpindleSpeed"] = "0.0";
                dr1["CNCTimeStamp"] = fromDate;
                dr1["Temperature"] = "0.0";
                dt.Rows.Add(dr1);

                DataRow drnew = dt.NewRow();
                drnew["SpindleLoad"] = "0.0";
                drnew["SpindleSpeed"] = "0.0";
                drnew["Temperature"] = "0.0";
                drnew["CNCTimeStamp"] = toDate;
                dt.Rows.Add(drnew);

            }
            return dt;
        }

        private void BindDuration()
        {
            try
            {
                if (cmbDurationType.SelectedIndex == 0)
                {
                    cmbDuration.Items.Clear();
                    for (int j = 1; j < 24; j++)
                    {
                        cmbDuration.Items.Add(j.ToString());
                    }
                    cmbDuration.SelectedIndex = Settings.CmbDurationSelectedIndex > cmbDuration.Items.Count ? 1 : Settings.CmbDurationSelectedIndex;
                }

                if (cmbDurationType.SelectedIndex == 1)
                {
                    cmbDuration.Items.Clear();
                    for (int i = 1; i < 61; i++)
                    {
                        cmbDuration.Items.Add(i.ToString());
                    }
                    cmbDuration.SelectedIndex = Settings.CmbDurationSelectedIndex > cmbDuration.Items.Count ? 20 : Settings.CmbDurationSelectedIndex;
                }

                if (cmbDurationType.SelectedIndex == 2)
                {
                    if (Settings.CmbDurationSelectedIndex >= 3)
                    {
                        Settings.CmbDurationSelectedIndex = 0;
                    }
                    cmbDuration.Items.Clear();
                    int val = 20;
                    for (int i = 1; i < 4; i++)
                    {
                        cmbDuration.Items.Add((val).ToString());
                        val = val + 20;
                    }
                    cmbDuration.SelectedIndex = Settings.CmbDurationSelectedIndex > cmbDuration.Items.Count ? 1 : Settings.CmbDurationSelectedIndex;
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void setMaxLimitChart()
        {
            if (dataSeriesA.Length == 0 || dataSeriesB.Length == 0 || dataSeriesC.Length == 0) return;
            maxLoad = dataSeriesA.Max();
            maxLoad = maxLoad + 20;

            maxSpeed = dataSeriesB.Max();
            if (maxSpeed > 2500)
            {
                maxSpeed = 4005;
            }
            else
            {
                maxSpeed = maxSpeed + 100;
            }
            maxTemp = dataSeriesC.Max();
            maxTemp = maxTemp + 20;
        }

        public void LoadRunningParameter()
        {
            try
            {
                flag = 1;
                currentDuration = 360 * 86400;
                SetChartTitles();
                loadData();
                setMaxLimitChart();
                minDate = timeStamps[0];
                dateRange = timeStamps[timeStamps.Length - 1].Subtract(minDate).TotalSeconds;

                hasFinishedInitialization = true;
                winChartViewer1.ViewPortWidth = currentDuration / dateRange;
                winChartViewer1.ViewPortLeft = 1 - winChartViewer1.ViewPortWidth;
                winChartViewer1.updateViewPort(true, true);

                hasFinishedInitialization = true;
                winChartViewer2.ViewPortWidth = currentDuration / dateRange;
                winChartViewer2.ViewPortLeft = 1 - winChartViewer2.ViewPortWidth;
                winChartViewer2.updateViewPort(true, true);

                hasFinishedInitialization = true;
                winChartViewer3.ViewPortWidth = currentDuration / dateRange;
                winChartViewer3.ViewPortLeft = 1 - winChartViewer3.ViewPortWidth;
                winChartViewer3.updateViewPort(true, true);

                hasFinishedInitialization = true;

                winChartViewer1_MouseEnter(null, EventArgs.Empty);
                winChartViewer2_MouseEnter(null, EventArgs.Empty);
                    

                if (cmbAxis.SelectedItem.ToString() == "X")
                {
                    winChartViewer2.Hide();
                    winChartViewer3.Show();
                    pictureBoxSpindleLegend.Visible = false;
                    pictureBoxSpindleLegend1.Visible = false;
                }
                if (cmbAxis.SelectedItem.ToString() == "C")
                {
                    winChartViewer2.Show();
                    winChartViewer3.Show();
                    pictureBoxSpindleLegend.Visible = false;
                    pictureBoxSpindleLegend1.Visible = false;
                }
                if (cmbAxis.SelectedItem.ToString() == "Z")
                {
                    winChartViewer2.Hide();
                    winChartViewer3.Show();
                    pictureBoxSpindleLegend.Visible = false;
                    pictureBoxSpindleLegend1.Visible = false;
                }
                if (cmbAxis.SelectedItem.ToString() == "Spindle")
                {
                    winChartViewer2.Show();
                    winChartViewer3.Hide();
                    pictureBoxSpindleLegend.Visible = true;
                    pictureBoxSpindleLegend1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

        }

        private DataTable GetChartMarkersData()
        {
            DateTime fromDate;
            DateTime toDate;
            fromDate = Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME);
            toDate = Convert.ToDateTime(MainScreen.LOGICAL_DAY_END);
            DataTable dt = DatabaseAccess.GetSpindleCycleStartEndTimes(HomeScreen.selectedMachine, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), cmbAxis.Text.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return new DataTable();
            }
        }

        private void SetChartTitles()
        {
            if (cmbAxis.SelectedItem.ToString() == "X")
            {
                chart1Title = "X-Axis Load";
                chart2Title = "Work Head Speed";
                chart3Title = "X- Axis Temperature";
            }
            if (cmbAxis.SelectedItem.ToString() == "C")
            {
                chart1Title = "Work Head Load";
                chart2Title = "Work Head Speed";
                chart3Title = "Work Head Temperature";
            }
            if (cmbAxis.SelectedItem.ToString() == "Z")
            {
                chart1Title = "Z-Axis Load";
                chart2Title = "Work Head Speed";
                chart3Title = "Z-Axis Temperature";
            }
            if (cmbAxis.SelectedItem.ToString() == "Spindle")
            {
                chart1Title = "Spindle Load";
                chart2Title = "Spindle Speed";
                chart3Title = "Spindle Temperature";
            }
        }

        #endregion

        #region -- All View Ports

        private void winChartViewer1_ViewPortChanged(object sender, WinViewPortEventArgs e)
        {
            currentDuration = Math.Round(winChartViewer1.ViewPortWidth * dateRange);

            hScrollBar1.Enabled = winChartViewer1.ViewPortWidth < 1;
            hScrollBar1.LargeChange = (int)Math.Ceiling(winChartViewer1.ViewPortWidth *
                (hScrollBar1.Maximum - hScrollBar1.Minimum));
            hScrollBar1.SmallChange = (int)Math.Ceiling(hScrollBar1.LargeChange * 0.1);
            //hasFinishedInitialization = false;
            hScrollBar1.Value = (int)Math.Round(winChartViewer1.ViewPortLeft *
                (hScrollBar1.Maximum - hScrollBar1.Minimum)) + hScrollBar1.Minimum;

            if (e.NeedUpdateChart)
            {
                MTB = DatabaseAccess.GetMTB(HomeScreen.selectedMachine);
                if (MTB.Equals("MGTL", StringComparison.OrdinalIgnoreCase))
                {
                    DrawLoadSpeedTempChart(winChartViewer1, panel1.Width, panel1.Height, chart1Title, cmbAxis.SelectedItem.ToString() == "Spindle" ? " kW " : " % ", dataSeriesA, 0x0000FF);
                }
                else
                {
                    DrawLoadSpeedTempChart(winChartViewer1, panel1.Width, panel1.Height, "Spindle Load", cmbAxis.SelectedItem.ToString() == "Spindle" ? " kW " : " % ", dataSeriesA, 0x0000FF);
                }
            }
            //drawChart2(winChartViewer2);
            if (e.NeedUpdateImageMap)
                GetToolTipInformation(winChartViewer1, "%");
            // updateImageMap2(winChartViewer2);

        }

        private void winChartViewer2_ViewPortChanged(object sender, WinViewPortEventArgs e)
        {
            //DateTime currentStartDate = minDate.AddSeconds(Math.Round(winChartViewer2.ViewPortLeft * dateRange));
            currentDuration = Math.Round(winChartViewer1.ViewPortWidth * dateRange);
            hScrollBar2.Enabled = winChartViewer2.ViewPortWidth < 1;
            hScrollBar2.LargeChange = (int)Math.Ceiling(winChartViewer2.ViewPortWidth *
                (hScrollBar2.Maximum - hScrollBar2.Minimum));
            hScrollBar2.SmallChange = (int)Math.Ceiling(hScrollBar2.LargeChange * 0.1);
            //hasFinishedInitialization = false;
            hScrollBar2.Value = (int)Math.Round(winChartViewer2.ViewPortLeft *
                (hScrollBar2.Maximum - hScrollBar2.Minimum)) + hScrollBar2.Minimum;

            if (e.NeedUpdateChart)
            {
                MTB = DatabaseAccess.GetMTB(HomeScreen.selectedMachine);
                if (MTB.Equals("MGTL", StringComparison.OrdinalIgnoreCase))
                {
                    DrawLoadSpeedTempChart(winChartViewer2, panel2.Width, panel2.Height, chart2Title, " RPM ", dataSeriesB, 0x990000);
                }
                else
                {
                    DrawLoadSpeedTempChart(winChartViewer2, panel2.Width, panel2.Height, "Spindle Speed", " RPM ", dataSeriesB, 0x990000);
                }
            }
            if (e.NeedUpdateImageMap)
                GetToolTipInformation(winChartViewer2, "RPM");

        }

        private void winChartViewer3_ViewPortChanged(object sender, WinViewPortEventArgs e)
        {
            //DateTime currentStartDate = minDate.AddSeconds(Math.Round(winChartViewer3.ViewPortLeft * dateRange));
            currentDuration = Math.Round(winChartViewer1.ViewPortWidth * dateRange);

            hScrollBar3.Enabled = winChartViewer3.ViewPortWidth < 1;
            hScrollBar3.LargeChange = (int)Math.Ceiling(winChartViewer3.ViewPortWidth *
                (hScrollBar3.Maximum - hScrollBar3.Minimum));
            hScrollBar3.SmallChange = (int)Math.Ceiling(hScrollBar3.LargeChange * 0.1);
            //hasFinishedInitialization = false;
            hScrollBar3.Value = (int)Math.Round(winChartViewer3.ViewPortLeft *
                (hScrollBar3.Maximum - hScrollBar3.Minimum)) + hScrollBar3.Minimum;

            if (e.NeedUpdateChart)
            {
                MTB = DatabaseAccess.GetMTB(HomeScreen.selectedMachine);
                if (MTB.Equals("MGTL", StringComparison.OrdinalIgnoreCase))
                {
                    DrawLoadSpeedTempChart(winChartViewer3, panel3.Width, panel3.Height, chart3Title, " Celsius ", dataSeriesC, 0xFFC000);
                }
                else
                {
                    DrawLoadSpeedTempChart(winChartViewer3, panel3.Width, panel3.Height, "Temperature", " Celsius ", dataSeriesC, 0xFFC000);
                }
            }
            if (e.NeedUpdateImageMap)
                GetToolTipInformation(winChartViewer3, "Celsius");
        }

        #endregion

        #region -- All ToolTips data

        private void winChartViewer1_MouseEnter(object sender, EventArgs e)
        {
            GetToolTipInformation(winChartViewer1, "%");
        }

        private void winChartViewer2_MouseEnter(object sender, EventArgs e)
        {
            GetToolTipInformation(winChartViewer2, "RPM");
        }

        private void winChartViewer3_MouseEnter(object sender, EventArgs e)
        {
            GetToolTipInformation(winChartViewer3, "Celsius");
        }

        #endregion

        #region -- All Horizantal Scrolling

        private void hScrollBar1_ValueChanged_1(object sender, EventArgs e)
        {
            if (hasFinishedInitialization)
            {
                winChartViewer2.ViewPortLeft = ((double)(hScrollBar1.Value - hScrollBar1.Minimum))
                    / (hScrollBar1.Maximum - hScrollBar1.Minimum);
                winChartViewer2.updateViewPort(true, false);
            }
        }

        private void hScrollBar2_ValueChanged_1(object sender, EventArgs e)
        {
            if (hasFinishedInitialization)
            {
                winChartViewer3.ViewPortLeft = ((double)(hScrollBar2.Value - hScrollBar2.Minimum))
                     / (hScrollBar2.Maximum - hScrollBar2.Minimum);
                winChartViewer3.updateViewPort(true, false);
            }
        }

        private void hScrollBar3_ValueChanged_1(object sender, EventArgs e)
        {
            if (hasFinishedInitialization)
            {
                winChartViewer1.ViewPortLeft = ((double)(hScrollBar3.Value - hScrollBar3.Minimum))
                  / (hScrollBar3.Maximum - hScrollBar3.Minimum);
                winChartViewer1.updateViewPort(true, false);

            }

        }

        #endregion

        #region -- All Other Events

        private void GetToolTipInformation(WinChartViewer viewer, string type)
        {
            if (viewer.ImageMap == null)
            {
                try
                {
                    var tooltip = "title='{dataSetName}: {value|2} " + type + "'";
                    viewer.ImageMap = viewer.Chart.getHTMLImageMap("clickable", "", tooltip);
                }
                catch (Exception ex)
                {
                    Settings.WriteErrorMsg(ex.ToString());
                }
            }
        }


        private void RPM_Load(object sender, EventArgs e)
        {            
            MTB = DatabaseAccess.GetMTB(HomeScreen.selectedMachine);
            if (MTB.Equals("MGTL", StringComparison.OrdinalIgnoreCase))
            {
                btnUpgrade.Visible = true;
            }
            else
            {
                btnUpgrade.Visible = true;
            }

            int axisNumber = DatabaseAccess.GetSpindleAxisNumber(HomeScreen.selectedMachine);
            if (axisNumber <= 1)
            {
                cmbAxis.Visible = false;
                lblAxis.Visible = false;
            }
            else
            {
                cmbAxis.Visible = true;
                lblAxis.Visible = true;
            }

            cmbAxis.Items.Clear();
            if (axisNumber <= 2)
            {
                cmbAxis.Items.Add("X"); cmbAxis.Items.Add("Z");
            }
            else if (axisNumber <= 3)
            {
                cmbAxis.Items.Add("X"); cmbAxis.Items.Add("Y"); cmbAxis.Items.Add("Z"); cmbAxis.Items.Add("Spindle");
            }

            if (MTB.Equals("MGTL", StringComparison.OrdinalIgnoreCase))
            {
                cmbAxis.Items.Clear();
                cmbAxis.Items.Add("X"); cmbAxis.Items.Add("C"); cmbAxis.Items.Add("Z"); cmbAxis.Items.Add("Spindle");
            }

            cmbAxis.SelectedIndex = 0;
            cmbDurationType.SelectedIndex = Settings.CmbDurationTypeSelectedIndex;
            BindDuration();
            currentDurationSelected = cmbDuration.Text;
            LoadRunningParameter();
            timer1.Interval = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;// Settings.AutoRefreshInterval;
            if (Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME).ToString("yyyy-MM-dd") == (DatabaseAccess.GetShiftStartEndTimeForDay(1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))).ToString("yyyy-MM-dd"))
            {
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
            hScrollBar1_ValueChanged_1(null, EventArgs.Empty);
            hScrollBar2_ValueChanged_1(null, EventArgs.Empty);
            hScrollBar3_ValueChanged_1(null, EventArgs.Empty);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            LoadRunningParameter();

            timer1.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            LoadRunningParameter();
            timer1.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void cmbDurationType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Settings.CmbDurationTypeSelectedIndex = cmbDurationType.SelectedIndex;
            BindDuration();
            currentDurationSelected = cmbDuration.Text;
            timer1_Tick(this, EventArgs.Empty);
            this.Cursor = Cursors.Default;
        }

        private void cmbDuration_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Settings.CmbDurationSelectedIndex = cmbDuration.SelectedIndex;
            currentDurationSelected = cmbDuration.Text;
            timer1_Tick(this, EventArgs.Empty);
            this.Cursor = Cursors.Default;
        }

        private void DrawLoadSpeedTempChart(WinChartViewer viewer, int width, int height, string chartTitle, string yAxisLabel, double[] dataSeries, int seriesColor)
        {
            DateTime viewPortStartDate = minDate.AddSeconds(Math.Round(viewer.ViewPortLeft * dateRange));
            DateTime viewPortEndDate = viewPortStartDate.AddSeconds(Math.Round(viewer.ViewPortWidth * dateRange));

            int startIndex = Array.BinarySearch(timeStamps, viewPortStartDate);

            if (startIndex < 0) startIndex = (~startIndex) - 1;

            int endIndex = Array.BinarySearch(timeStamps, viewPortEndDate);
            if (endIndex < 0) endIndex = ((~endIndex) < timeStamps.Length) ? ~endIndex : timeStamps.Length - 1;

            int noOfPoints = endIndex - startIndex + 1;

            DateTime[] viewPortTimeStamps = new DateTime[noOfPoints];
            double[] viewPortDataSeries = new double[noOfPoints];

            Array.Copy(timeStamps, startIndex, viewPortTimeStamps, 0, noOfPoints);
            Array.Copy(dataSeries, startIndex, viewPortDataSeries, 0, noOfPoints);

            if (viewPortTimeStamps.Length >= 520)
            {
                ArrayMath m = new ArrayMath(viewPortTimeStamps);
                m.selectRegularSpacing(viewPortTimeStamps.Length / 260);
                viewPortTimeStamps = m.aggregate(viewPortTimeStamps, Chart.AggregateFirst);
                viewPortDataSeries = m.aggregate(viewPortDataSeries, Chart.AggregateAvg);
            }

            XYChart c;
            viewer.Location = new Point(5, 25);
            c = new XYChart(width, height);
            c.addTitle(chartTitle, "Segoe UI Bold", 12, 0x2A58A3).setBackground(0xFFFFFF, 0xFFFFFF);
            c.setBackground(Chart.metalColor(0xFFFFFF), 0xFFFFFF);
            c.setPlotArea(55, 35, width - 82, height - 79, 0xffffff, 0xFFFFFF, 0xC6C6C8, c.dashLineColor(0xcccccc, Chart.DotLine), c.dashLineColor(0xFFFFFF, Chart.DotLine));

            c.yAxis().setTitle(yAxisLabel, "Segoe UI Bold", 10).setFontAngle(90);
            c.setClipping();
            c.setClipping();

            double durationint;
            string durationstring = GetDurationString(viewPortStartDate, viewPortEndDate, out durationint);

            lblScrollRange.Text = viewPortStartDate.ToString("hh:mm:ss tt") + " to " + viewPortEndDate.ToString("hh:mm:ss tt") + " ( Duration " + durationint.ToString() + " " + durationstring.ToString() + " )";

            if (chartTitle.Equals("Temperature"))
            {
                AreaLayer layer = c.addAreaLayer();
                layer.setGapColor(0xffffff);
                layer.setLineWidth(0);
                layer.setXData(viewPortTimeStamps);
                layer.addDataSet(viewPortDataSeries, seriesColor, chartTitle);
                layer.setHTMLImageMap("", "", "");

                TrendLayer trendLayer = c.addTrendLayer(viewPortDataSeries, 0x008080, "TrendLine " + chartTitle, 1);
                trendLayer.setLineWidth(3);
                trendLayer.setXData(viewPortTimeStamps);
                trendLayer.setHTMLImageMap("", "", "");
            }
            else
            {
                LineLayer layer = c.addLineLayer();
                layer.setLineWidth(3);
                layer.setXData(viewPortTimeStamps);
                layer.addDataSet(viewPortDataSeries, seriesColor, chartTitle);
                layer.setHTMLImageMap("", "", "");
            }

            #region

            c.xAxis().setDateScale(viewPortStartDate, viewPortEndDate);
            c.xAxis().setFormatCondition("align", 360 * 86400);
            c.xAxis().setLabelFormat("{value|yyyy}");
            c.xAxis().setFormatCondition("align", 30 * 86400);
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "<*font=bold*>{value|mmm yyyy}", Chart.AllPassFilter(), "{value|mmm}");

            c.xAxis().setFormatCondition("align", 86400);
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "<*block,halign=left*><*font=bold*>{value|mmm dd<*br*>yyyy}", Chart.StartOfMonthFilter(), "<*font=bold*>{value|mmm dd}");
            c.xAxis().setMultiFormat2(Chart.AllPassFilter(), "{value|dd}");
            c.xAxis().setFormatCondition("else");
            c.xAxis().setMultiFormat(Chart.StartOfDayFilter(), "<*font=bold*>{value|hh:nn:ss<*br*>mmm dd}", Chart.AllPassFilter(), "{value|hh:nn:ss}");
            //c.xAxis().setLabelStyle("Arial", 8, 0x008000);//.setFontAngle(20);  

            #endregion

            double axisLowerLimit = maxValue - (maxValue - minValue) * (viewer.ViewPortTop + viewer.ViewPortHeight);
            double axisUpperLimit = maxValue - (maxValue - minValue) * viewer.ViewPortTop;

            axisUpperLimit = dataSeries.Max() + 10;
            axisLowerLimit = dataSeries.Min() > 10 ? dataSeries.Min() - 5 : dataSeries.Min();

            //c.yAxis().setLinearScale(axisLowerLimit, axisUpperLimit);
            c.yAxis().setRounding(false, false);

            DataTable dt = GetChartMarkersData();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["OperationType"].ToString().Equals("Dressing", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!string.IsNullOrEmpty(dr["cyclestart"].ToString()) && !string.IsNullOrEmpty(dr["cycleend"].ToString()))
                        {
                            Mark markCycleStartTime = c.xAxis2().addMark(Chart.CTime(Convert.ToDateTime(dr["cyclestart"])), 0x8000, "Start", "Arial", 7);
                            markCycleStartTime.setLineWidth(2);
                            markCycleStartTime.setFontAngle(90);
                            markCycleStartTime.setAlignment(Chart.TopRight2); // right

                            Mark markCycleEndTime = c.xAxis2().addMark(Chart.CTime(Convert.ToDateTime(dr["cycleend"])), 0x8000, "End", "Arial", 7);
                            markCycleEndTime.setLineWidth(2);
                            markCycleEndTime.setFontAngle(90);
                            markCycleEndTime.setAlignment(Chart.BottomRight);// right
                        }
                    }

                    if (dr["OperationType"].ToString().Equals("Grinding", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!string.IsNullOrEmpty(dr["cyclestart"].ToString()) && !string.IsNullOrEmpty(dr["cycleend"].ToString()))
                        {
                            Mark markCycleStartTime = c.xAxis2().addMark(Chart.CTime(Convert.ToDateTime(dr["cyclestart"])), 0x80FF, "Start", "Arial", 7);
                            markCycleStartTime.setLineWidth(2);
                            markCycleStartTime.setFontAngle(90);
                            markCycleStartTime.setAlignment(Chart.TopRight2);

                            Mark markCycleEndTime = c.xAxis2().addMark(Chart.CTime(Convert.ToDateTime(dr["cycleend"])), 0x80FF, "End", "Arial", 7);
                            markCycleEndTime.setLineWidth(2);
                            markCycleEndTime.setFontAngle(90);
                            markCycleEndTime.setAlignment(Chart.BottomRight);
                        }
                    }

                    if (dr["OperationType"].ToString().Equals("Cycle", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!string.IsNullOrEmpty(dr["cyclestart"].ToString()) && !string.IsNullOrEmpty(dr["cycleend"].ToString()))
                        {
                            Mark markCycleStartTime = c.xAxis2().addMark(Chart.CTime(Convert.ToDateTime(dr["cyclestart"])), 0x0, "Start", "Arial", 7);
                            markCycleStartTime.setLineWidth(2);
                            markCycleStartTime.setFontAngle(90);
                            markCycleStartTime.setAlignment(Chart.TopLeft);

                            Mark markCycleEndTime = c.xAxis2().addMark(Chart.CTime(Convert.ToDateTime(dr["cycleend"])), 0x0, "End", "Arial", 7);
                            markCycleEndTime.setLineWidth(2);
                            markCycleEndTime.setFontAngle(90);
                            markCycleEndTime.setAlignment(Chart.BottomLeft);
                        }
                    }
                }
            }
            viewer.Chart = c;
        }

        private string GetDurationString(DateTime viewPortStartDate, DateTime viewPortEndDate, out double durationint)
        {
            string durationstring = string.Empty;
            double diff = 0.0;
            diff = (viewPortEndDate.Ticks - viewPortStartDate.Ticks) / 10000000;

            if (diff / (365 * 24 * 3600) >= 1)
            {
                durationint = Math.Round(diff / (365 * 24 * 3600));
                if (durationint > 1)
                {
                    durationstring = "years";
                }
                else
                {
                    durationstring = "year";
                }
            }
            else if (diff / (30 * 24 * 3600) >= 1)
            {
                durationint = Math.Round(diff / (30 * 24 * 3600));

                if (durationint > 1)
                {
                    durationstring = "months";
                }
                else
                {
                    durationstring = "month";
                }
            }
            else if (diff / (24 * 3600) >= 1)
            {
                durationint = Math.Round(diff / (24 * 3600));
                if (durationint > 1)
                {
                    durationstring = "days";
                }
                else
                {
                    durationstring = "day";
                }
            }

            else if (diff / (3600) >= 1)
            {
                durationint = Math.Round(diff / (3600));
                if (durationint > 1)
                {
                    durationstring = "hours";
                }
                else
                {
                    durationstring = "hour";
                }
            }
            else if (diff / (60) >= 1)
            {
                durationint = Math.Round(diff / 60);
                if (durationint > 1)
                {
                    durationstring = "mintues";
                }
                else
                {
                    durationstring = "mintue";
                }
            }

            else
            {
                durationint = diff;
                durationstring = "Seconds";
            }

            return durationstring;
        }

        #endregion

        #region

        public static List<SpindleData> DeSerialize(byte[] items)
        {
            List<SpindleData> list = new List<SpindleData>();
            if (items == null || items.Count() == 0) return list;
            try
            {
                using (MemoryStream ms = new MemoryStream(items))
                {
                    BinaryFormatter bnfmt = new BinaryFormatter();
                    bnfmt.Binder = new CurrentAssemblyDeserializationBinder();
                    object value = bnfmt.Deserialize(ms);
                    if (value is List<SpindleData>)
                        return (List<SpindleData>)value;
                    else
                        throw new Exception("Invalid compress data Type!");
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public sealed class CurrentAssemblyDeserializationBinder : System.Runtime.Serialization.SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                if (assemblyName.Contains("mscorlib") && typeName.Contains("[FocasSmartDataCollection.SpindleData"))
                {
                    return Type.GetType("System.Collections.Generic.List`1[[MachineConnectApplication.SpindleData, MachineConnectOEM]]");
                }
                else if (assemblyName.Contains("MachineConnectSmartDataService") && typeName.Contains("FocasSmartDataCollection.SpindleData"))
                {
                    typeName = "MachineConnectApplication.SpindleData";
                    return Type.GetType(String.Format("{0}, {1}", typeName, Assembly.GetExecutingAssembly().FullName));
                }
                else
                {
                    return Type.GetType(String.Format("{0}, {1}", typeName, Assembly.GetExecutingAssembly().FullName));
                }
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }

        #endregion

        private void btnUpgrade_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DisposePanelControls();
            pnlContainer.Controls.Clear();
            ProcessDoc ctrl = new ProcessDoc(this);
            ctrl.userControl = this;
            ctrl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(ctrl);

            this.Cursor = Cursors.Default;

        }

        private void DisposePanelControls()
        {
            foreach (Control p in pnlContainer.Controls)
            {
                p.Dispose();
            }
        }

        private void cmbAxis_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            timer1_Tick(this, EventArgs.Empty);
            this.Cursor = Cursors.Default;
        }

        private void btnCycleProfile_Click(object sender, EventArgs e)
        {
            //CycleProfileWindow cycleProfile = new CycleProfileWindow();
            //cycleProfile.Dock = DockStyle.Fill;
            //cycleProfile.ShowDialog();
            this.Cursor = Cursors.WaitCursor;
            DisposePanelControls();
            pnlContainer.Controls.Clear();
            CycleProfile control = new CycleProfile();
            control.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(control);
            this.Cursor = Cursors.Default;
        }
    }
}
