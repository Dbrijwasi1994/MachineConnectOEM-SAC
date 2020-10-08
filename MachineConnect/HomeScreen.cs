using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using ChartDirector;
using System.IO;

namespace MachineConnectApplication
{
    public partial class HomeScreen : UserControl
    {
        #region   

        string GraphType = string.Empty;
        double TotalTime,PowerOnTime, PowerOffTime,CuttingTime,DressingTime, NotCuttingTime,OperatingTime,NonOperatingTime;
        static string CURRENT_DATE_TIME = MainScreen.CURRENT_DATE_TIME;// "2015-09-08 " +

        DateTime ArrayStartDate; //Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).AddHours(-4);
        DateTime ArrayEndDate;//= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        DateTime ArrayStartDateConstant;
        DateTime ArrayEndDateConstant;

        public static string selectedMachine = string.Empty;       
        Task backgroundTask = null;
        TaskScheduler uiThreadScheduler = null;

        ChartArrays chartArrays = new ChartArrays();
        PartCountArrays partCountArrays = new PartCountArrays();
        TimeAnalysisSummary timeAnalysisSummary = new TimeAnalysisSummary();

        StepLineArray stepLineArray = new StepLineArray();
        DateTime[] DateTimeArray = new DateTime[0];
        List<double> dateTimeArray = new List<double>();

        DateTime[] DownStartData = new DateTime[0]; // Start
        DateTime[] ProductionStartData = new DateTime[0];
        DateTime[] NoDataStartData = new DateTime[0];

        DateTime[] DownEndData = new DateTime[0]; // End
        DateTime[] ProductionEndData = new DateTime[0];
        DateTime[] NoDataEndData = new DateTime[0];
        public static string currentDateTime;

        private int StartIndex = 0;
        private int EndIndex = 12;


        #endregion

        //added by satya on 15 may 2016 to solve the WPF control disappear issue
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        public HomeScreen(Form formVal)
        {
            //added by satya on 15 may 2016 to solve the WPF control disappear issue
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.ContainerControl |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.SupportsTransparentBackColor
                          , true);
            InitializeComponent();
            analyticsPicturebox.Visible = false;
            dataGridStoppage.AutoGenerateColumns = false;
            SetButtonVisibility(false);

            ArrayStartDateConstant = Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME);
            ArrayEndDateConstant = Convert.ToDateTime(DatabaseAccess.GetShiftStartEndTimeForDay(0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

            ArrayStartDate = Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME);
            ArrayEndDate = Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME).AddHours(+4);

            this.btnRunChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnPrevStop.png"));
            this.btnPartsCountPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnPrevStop.png"));
            this.btnAnalysisChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnPrevStop.png"));

            timer1.Interval = (int)TimeSpan.FromMinutes(Settings.AutoRefreshInterval).TotalMilliseconds;
            uiThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            GraphType = DatabaseAccess.GetProductionGraphType();
            timer1_Tick(this, EventArgs.Empty);
        }

        private void SetButtonVisibility(bool val)
        {
            btnPartsCountNext.Visible = val;
            btnPartsCountPrev.Visible = val;

            btnRunChartNext.Visible = val;
            btnRunChartPrev.Visible = val;

            btnAnalysisChartNext.Visible = val;
            btnAnalysisChartPrev.Visible = val;
        }

        #region -- Prev-Next Button Opacity Type Functionality

        private void chartPartsCount_MouseHover(object sender, EventArgs e)
        {
            btnPartsCountPrev.Visible = true;
            btnPartsCountNext.Visible = true;

            GetToolTipInformation(chartPartsCount,"Part Count:", "");
        }

        private void chartPartsCount_MouseLeave(object sender, EventArgs e)
        {
            btnPartsCountPrev.Visible = false;
            btnPartsCountNext.Visible = false;
        }

        private void chartProductionanalysis_MouseLeave(object sender, EventArgs e)
        {
            btnAnalysisChartNext.Visible = false;
            btnAnalysisChartPrev.Visible = false;
        }        
       
        private void chartProductionanalysis_MouseHover(object sender, EventArgs e)
        {
            btnAnalysisChartNext.Visible = true;
            btnAnalysisChartPrev.Visible = true;
            GetToolTipInformation(chartProductionanalysis, "{dataSetName}:", " mins.");
        }

        private void btnRunChartPrev_Click(object sender, EventArgs e)
        { 
            TimeSpan difference = ArrayStartDate - ArrayStartDateConstant;
            if (difference.TotalHours <= 1)
            {
                this.btnRunChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnPrevStop.png"));
                return;
            }
            else
            {
                this.btnRunChartNext.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Next.png"));
                this.btnRunChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Prev.png"));
            }

            ArrayEndDate = ArrayStartDate;
            ArrayStartDate = ArrayStartDate.AddHours(-4);

            stepLineArray = DatabaseAccess.GetRuntimeDowntimeData(ArrayStartDate.ToString("yyyy-MM-dd HH:mm:ss"), "", "", HomeScreen.selectedMachine);

            PlotStepLineGraph();

            difference = ArrayStartDate - ArrayStartDateConstant;
            if (difference.TotalHours <= 1)
            {
                this.btnRunChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnPrevStop.png"));
            }
        }

        private void btnRunChartNext_Click(object sender, EventArgs e)
        {            
            TimeSpan difference = ArrayEndDate - ArrayStartDateConstant;
            if (difference.TotalHours >= (ArrayEndDateConstant - ArrayStartDateConstant).TotalHours)
            {
                this.btnRunChartNext.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnNextStop.png"));
                return;
            }
            else
            {
                this.btnRunChartNext.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Next.png"));
                this.btnRunChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Prev.png"));
            }


            ArrayStartDate = ArrayEndDate; //ArrayEndDate;
            ArrayEndDate = ArrayEndDate.AddHours(+4);

            stepLineArray = DatabaseAccess.GetRuntimeDowntimeData(ArrayStartDate.ToString("yyyy-MM-dd HH:mm:ss"), "", "", HomeScreen.selectedMachine);
            PlotStepLineGraph();
            difference = ArrayEndDate - ArrayStartDateConstant;
            if (difference.TotalHours >= (ArrayEndDateConstant - ArrayStartDateConstant).TotalHours)
            {
                this.btnRunChartNext.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnNextStop.png"));
            }

        }

        private void chartRunChart_MouseHover(object sender, EventArgs e)
        {
            btnRunChartNext.Visible = true;
            btnRunChartPrev.Visible = true;
        }

        private void chartRunChart_MouseLeave(object sender, EventArgs e)
        {
            btnRunChartNext.Visible = false;
            btnRunChartPrev.Visible = false;
        }

        private void chartTimeAnalysis_MouseEnter(object sender, EventArgs e)
        {
           // GetToolTipInformation(chartTimeAnalysis, "Time: ", "");
        }

        private void chartRunChart_MouseEnter(object sender, EventArgs e)
        {
           // GetToolTipInformation(chartRunChart, "Time:", "");
        }

        private void GetToolTipInformation(WinChartViewer viewer, string val, string type)
        {
            if (viewer.ImageMap == null)
            {
                try
                {
                    var tooltip = "title='" + val + " {value} " + type + "'";
                    if(viewer.Chart != null)
                    viewer.ImageMap = viewer.Chart.getHTMLImageMap("clickable", "", tooltip);
                }
                catch (Exception ex)
                {
                    Logger.WriteErrorLog(ex);
                }
            }
        }

        #endregion
              
        #region Prev Next Buttons Functionality

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (EndIndex >= 24)
            {
                this.btnPartsCountNext.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnNextStop.png"));
                this.btnAnalysisChartNext.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnNextStop.png"));
                return;
            }
            else
            {
                this.btnPartsCountPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Prev.png"));
                this.btnAnalysisChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Prev.png"));

                this.btnPartsCountNext.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Next.png"));
                this.btnAnalysisChartNext.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Next.png"));
            }

            InitializeStartEndIndexz();
            PlotPartsCountGraph();
            PlotProductionGraph();

            if (EndIndex >= 24)
            {
                this.btnPartsCountNext.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnNextStop.png"));
                this.btnAnalysisChartNext.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnNextStop.png"));
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (StartIndex == 0)
            {
                this.btnPartsCountPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnPrevStop.png"));
                this.btnAnalysisChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnPrevStop.png"));
                return;
            }
            else
            {
                this.btnPartsCountPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Prev.png"));
                this.btnAnalysisChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Prev.png"));

                this.btnPartsCountNext.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Next.png"));
                this.btnAnalysisChartNext.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Next.png"));
            }

            ReInitializeStartEndIndexz();
            PlotPartsCountGraph();
            PlotProductionGraph();

            if (StartIndex == 0)
            {
                this.btnPartsCountPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnPrevStop.png"));
                this.btnAnalysisChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnPrevStop.png"));
            }


        }

        private void InitializeStartEndIndexz()
        {
            StartIndex = EndIndex;
            EndIndex = EndIndex + 12;
        }

        private void ReInitializeStartEndIndexz()
        {
            StartIndex = 0;
            EndIndex = 12;
        }

        #endregion
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;           
            GetDataForAllControls();
            if (Settings.InAndonMode == true) timer1.Enabled = false; 
        }

        private void HomeScreen_Load(object sender, EventArgs e)
        {
            //this.btnRunChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnPrevStop.png"));
            //this.btnPartsCountPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnPrevStop.png"));
            //this.btnAnalysisChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "BtnPrevStop.png")); 
            
            //timer1.Interval = (int)TimeSpan.FromMinutes(Settings.AutoRefreshInterval).TotalMilliseconds;
            //uiThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();           
         
            //GraphType = DatabaseAccess.GetProductionGraphType();
            //timer1_Tick(this, EventArgs.Empty);
        }

        private void GetDataForAllControls()
        {
            if (backgroundTask != null && backgroundTask.Status == TaskStatus.Running) return;
            this.UseWaitCursor = true;
            DataTable stoppageData = null;
            string defaultThreshold = string.Empty;            
            // Added 
            var stopageBackgroundTask = new Task(() =>
            {
                GetDataForGraphs(MainScreen.CURRENT_DATE_TIME, "", "", selectedMachine);
                stoppageData = DatabaseAccess.GetStopagedata(MainScreen.CURRENT_DATE_TIME, "", "", selectedMachine); // Pick Machine from the login screen (Last Param)
                defaultThreshold = Settings.StoppagesThreshold;// DatabaseAccess.GetDefaultThreshold();
            });

            var stoppageTask = stopageBackgroundTask.ContinueWith(t =>
            {
                timer1.Enabled = true;  
                if (t.IsFaulted)
                {
                    this.UseWaitCursor = false;                  
                    CustomDialogBox cmb = new CustomDialogBox("Error Message", t.Exception.InnerException.Message);
                    cmb.ShowDialog();
                    return;
                }
            //    lblStoppage.Text =  "STOPPAGES ( > " + defaultThreshold + " Mins. )";
                lblStoppage.Text = "STOPPAGES ( > " + defaultThreshold + " Secs. )";
               
                PlotAllGraphs();
                dataGridStoppage.DataSource = stoppageData;
                this.UseWaitCursor = false;

            }, uiThreadScheduler);
            //stopageBackgroundTask.Start();
            stopageBackgroundTask.RunSynchronously(uiThreadScheduler);
        }

        private void GetDataForGraphs(string dateVal, string shiftVal, string plantId, string machineId)
        {
            try
            {               
                chartArrays = DatabaseAccess.GetProductionData(dateVal, shiftVal, plantId, machineId, out timeAnalysisSummary);
                var shift = GetShiftString(shiftVal);
                string machine = "'" + machineId + "'";
                partCountArrays = DatabaseAccess.GetPartsCountData(dateVal, "", plantId, machine);

                if (MainScreen.RunChartDateTime)
                {
                    ArrayStartDate = Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME);
                    ArrayEndDate = Convert.ToDateTime(ArrayStartDate).AddHours(+4);

                    #region

                    if (DateTime.Now >= ArrayStartDate && DateTime.Now < ArrayEndDate)
                    {

                    }
                    else
                    {
                        ArrayStartDate = ArrayEndDate;
                        ArrayEndDate = Convert.ToDateTime(ArrayStartDate).AddHours(+4);
                        this.btnRunChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Prev.png"));
                    }

                    if (DateTime.Now >= ArrayStartDate && DateTime.Now < ArrayEndDate)
                    {

                    }
                    else
                    {
                        ArrayStartDate = ArrayEndDate;
                        ArrayEndDate = Convert.ToDateTime(ArrayStartDate).AddHours(+4);
                        this.btnRunChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Prev.png"));

                    }

                    if (DateTime.Now >= ArrayStartDate && DateTime.Now < ArrayEndDate)
                    {

                    }
                    else
                    {
                        ArrayStartDate = ArrayEndDate;
                        ArrayEndDate = Convert.ToDateTime(ArrayStartDate).AddHours(+4);
                        this.btnRunChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Prev.png"));

                    }

                    if (DateTime.Now >= ArrayStartDate && DateTime.Now < ArrayEndDate)
                    {

                    }
                    else
                    {
                        ArrayStartDate = ArrayEndDate;
                        ArrayEndDate = Convert.ToDateTime(ArrayStartDate).AddHours(+4);
                        this.btnRunChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Prev.png"));

                    }

                    if (DateTime.Now >= ArrayStartDate && DateTime.Now < ArrayEndDate)
                    {

                    }
                    else
                    {
                        ArrayStartDate = ArrayEndDate;
                        ArrayEndDate = Convert.ToDateTime(ArrayStartDate).AddHours(+4);
                        this.btnRunChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Prev.png"));

                    }
                    if (DateTime.Now >= ArrayStartDate && DateTime.Now < ArrayEndDate)
                    {

                    }
                    else
                    {
                        ArrayStartDate = ArrayEndDate;
                        ArrayEndDate = Convert.ToDateTime(ArrayStartDate).AddHours(+4);
                        this.btnRunChartPrev.BackgroundImage = Image.FromFile(Path.Combine(Settings.APP_PATH, "Images", "Prev.png"));
                    }
                    #endregion

                }

                stepLineArray = DatabaseAccess.GetRuntimeDowntimeData(ArrayStartDate.ToString("yyyy-MM-dd HH:mm:ss"), "", plantId, HomeScreen.selectedMachine);              
                
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void PlotAllGraphs()
        {
            try
            {
                PlotPartsCountGraph();
                PlotTimeAnalysisChart();
                PlotStepLineGraph();
                PlotProductionGraph();

                if (MainScreen.RunChartDateTime)
                {
                    RefreshGraphView();
                }

                chartProductionanalysis_MouseHover(null, EventArgs.Empty);
                chartPartsCount_MouseHover(null,EventArgs.Empty);

            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void PlotPartsCountGraph()
        {
            try
            {
                string[] viewPortDataVals = new string[EndIndex - StartIndex];

                double[] viewPortDataSeriesA = new double[EndIndex - StartIndex];
                double[] viewPortDataSeriesB = new double[EndIndex - StartIndex];
                double[] viewPortDataSeriesC = new double[EndIndex - StartIndex];

                string[] viewPortDataSeriesAA = new string[EndIndex - StartIndex];
                string[] viewPortDataSeriesBB = new string[EndIndex - StartIndex];
                string[] viewPortDataSeriesCC = new string[EndIndex - StartIndex];

                Array.Copy((partCountArrays.partCount1).ToArray(), StartIndex, viewPortDataSeriesA, 0, EndIndex - StartIndex);
                Array.Copy((partCountArrays.partCount2).ToArray(), StartIndex, viewPortDataSeriesB, 0, EndIndex - StartIndex);
                Array.Copy((partCountArrays.partCount3).ToArray(), StartIndex, viewPortDataSeriesC, 0, EndIndex - StartIndex);

                Array.Copy((partCountArrays.ProgNo1).ToArray(), StartIndex, viewPortDataSeriesAA, 0, EndIndex - StartIndex);
                Array.Copy((partCountArrays.ProgNo2).ToArray(), StartIndex, viewPortDataSeriesBB, 0, EndIndex - StartIndex);
                Array.Copy((partCountArrays.ProgNo3).ToArray(), StartIndex, viewPortDataSeriesCC, 0, EndIndex - StartIndex);

                Array.Copy((partCountArrays.dateTime).ToArray(), StartIndex, viewPortDataVals, 0, EndIndex - StartIndex);

                XYChart POT;
                chartPartsCount.Location = new Point(5, 25);
                POT = new XYChart(pnlPartsCountChart.Width, pnlPartsCountChart.Height);
                POT.addTitle("Hourly Part Count", "Segoe UI Bold", 12, 0x2A58A3).setBackground(0xFFFFFF, 0xFFFFFF);
                POT.setBackground(Chart.metalColor(0xFFFFFF), 0xFFFFFF);//, 1);
                POT.setPlotArea(45, 30, pnlPartsCountChart.Width - 75, pnlPartsCountChart.Height - 79, 0xffffff, 0xFFFFFF, 0xC6C6C8, POT.dashLineColor(0xcccccc, Chart.DotLine), POT.dashLineColor(0xFFFFFF, Chart.DotLine));

                POT.yAxis().setTitle("Part Count", "Segoe UI Bold", 10).setFontAngle(90);
                //POT.xAxis().setTitle("Time (Hrs)", "Segoe UI Bold", 10).setFontAngle(0);
                POT.xAxis().setLabels(viewPortDataVals);
                BarLayer layer = POT.addBarLayer2(Chart.Stack, 0);

                var collectionWithDistinctElements = partCountArrays.ProgNo1[1].Distinct().ToArray();

                layer.addDataSet(viewPortDataSeriesA, 0x3A72FF, "Prog #1 ");//blue// + partCountArrays.ProgNo1[1].ToString()
                layer.addDataSet(viewPortDataSeriesB, 0x008000, "Prog #2 ");//+ partCountArrays.ProgNo2[1].ToString());//green
                layer.addDataSet(viewPortDataSeriesC, 0xff6666, "Prog #3 ");//+  + partCountArrays.ProgNo3[1].ToString()); //red

                
                layer.setBorderColor(0xffffff);
                POT.xAxis().setLabelStyle("Segoe UI", 8, 0x008000).setFontAngle(10);


                for (int i = 0; i < viewPortDataSeriesAA.Length; i++)//string font, double fontSize, int fontColor, double fontAngle
                {                   
                    layer.addCustomDataLabel(0, i,viewPortDataSeriesAA[i].ToString(), "Segoe UI Bold", 9, 0xFFFFFF).setAlignment(Chart.Center); //.setFontAngle(45);
                  
                }

                for (int i = 0; i < viewPortDataSeriesBB.Length; i++)
                {
                    layer.addCustomDataLabel(1, i, viewPortDataSeriesBB[i].ToString(), "Segoe UI Bold", 9, 0xFFFFFF).setAlignment(Chart.Center); //.setFontAngle(45);
                }

                for (int i = 0; i < viewPortDataSeriesCC.Length; i++)
                {
                    layer.addCustomDataLabel(2, i, viewPortDataSeriesCC[i].ToString(), "Segoe UI Bold", 9, 0xFFFFFF).setAlignment(Chart.Center); //.setFontAngle(45);
                }
                layer.setDataLabelStyle();
                layer.setAggregateLabelStyle();
                chartPartsCount.Image = POT.makeImage();
                layer.setHTMLImageMap("", "", "");
                chartPartsCount.Chart = POT;  
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

        }

        private void RefreshGraphView()
        {   
            if (ArrayStartDateConstant.AddHours(12) <= DateTime.Now)
            {
                btnNext_Click(null, EventArgs.Empty);
            }
        }

        private void PlotProductionGraph()
        {
            try
            {
                string[] viewPortDataVals = new string[EndIndex - StartIndex];

                double[] viewPortDataSeriesA = new double[EndIndex - StartIndex];
                double[] viewPortDataSeriesB = new double[EndIndex - StartIndex];
                double[] viewPortDataSeriesC = new double[EndIndex - StartIndex];
                double[] viewPortDataSeriesD = new double[EndIndex - StartIndex];

                Array.Copy((chartArrays.powerOnTime).ToArray(), StartIndex, viewPortDataSeriesA, 0, EndIndex - StartIndex);
                Array.Copy((chartArrays.OperatingTime).ToArray(), StartIndex, viewPortDataSeriesB, 0, EndIndex - StartIndex);
                Array.Copy((chartArrays.cuttingTime).ToArray(), StartIndex, viewPortDataSeriesC, 0, EndIndex - StartIndex);
                Array.Copy((chartArrays.dressingTime).ToArray(), StartIndex, viewPortDataSeriesD, 0, EndIndex - StartIndex);

                Array.Copy((chartArrays.startTime).ToArray(), StartIndex, viewPortDataVals, 0, EndIndex - StartIndex);

                XYChart POT;
                chartProductionanalysis.Location = new Point(5, 25);
                POT = new XYChart(pnlProductionanalysis.Width, pnlProductionanalysis.Height);

                POT.addTitle("Hourly Run Times", "Segoe UI Bold", 12, 0x2A58A3).setBackground(0xFFFFFF, 0xFFFFFF);
                POT.setBackground(Chart.metalColor(0xFFFFFF), 0xFFFFFF);//, 1);
                POT.setPlotArea(44, 44, pnlPartsCountChart.Width - 75, pnlPartsCountChart.Height - 95, 0xffffff, 0xFFFFFF, 0xC6C6C8, POT.dashLineColor(0xcccccc, Chart.DotLine), POT.dashLineColor(0xFFFFFF, Chart.DotLine));


                POT.xAxis().setLabels((viewPortDataVals).ToArray());
                POT.yAxis().setLinearScale(0, 69, 20);

                if (GraphType.Equals("Bar"))
                {
                    BarLayer layer = POT.addBarLayer2();

                    layer.addDataSet(viewPortDataSeriesA, 0x3A72FF, " Power On Time").setDataSymbol(Chart.CircleSymbol, 9);
                    layer.addDataSet(viewPortDataSeriesB, 0xFFFF00, " Operating Time").setDataSymbol(Chart.CircleSymbol, 9);
                    layer.addDataSet(viewPortDataSeriesC, 0x40ff40, " Cutting Time").setDataSymbol(Chart.CircleSymbol, 9);
                    //if (DatabaseAccess.GetMTB(HomeScreen.selectedMachine) == "MGTL")
                    //{
                    //    layer.addDataSet(viewPortDataSeriesC, 0x40ff40, " Grinding Time").setDataSymbol(Chart.CircleSymbol, 9);
                    //    layer.addDataSet(viewPortDataSeriesD, 0xEE82EE, " Dressing Time").setDataSymbol(Chart.CircleSymbol, 9);
                    //}
                    //else
                    //{
                    //    layer.addDataSet(viewPortDataSeriesC, 0x40ff40, " Cutting Time").setDataSymbol(Chart.CircleSymbol, 9);
                    //}
                    layer.setLineWidth(3);
                    layer.setHTMLImageMap("", "", "");                
                }

                else
                {
                    LineLayer layer = POT.addLineLayer2();

                    layer.addDataSet(viewPortDataSeriesA, 0x3A72FF, " Power On Time").setDataSymbol(Chart.CircleSymbol, 9);
                    layer.addDataSet(viewPortDataSeriesB, 0xFFFF00, " Operating Time").setDataSymbol(Chart.CircleSymbol, 9);
                    layer.addDataSet(viewPortDataSeriesC, 0x40ff40, " Cutting Time").setDataSymbol(Chart.CircleSymbol, 9);
                    //if (DatabaseAccess.GetMTB(HomeScreen.selectedMachine) == "MGTL")
                    //{
                    //    layer.addDataSet(viewPortDataSeriesC, 0x40ff40, " Grinding Time").setDataSymbol(Chart.CircleSymbol, 9);
                    //    layer.addDataSet(viewPortDataSeriesD, 0xEE82EE, " Dressing Time").setDataSymbol(Chart.CircleSymbol, 9);
                    //}
                    //else
                    //{
                    //    layer.addDataSet(viewPortDataSeriesC, 0x40ff40, " Cutting Time").setDataSymbol(Chart.CircleSymbol, 9);
                    //}
                    layer.setLineWidth(3);
                    layer.setHTMLImageMap("", "", "");
                }

                POT.yAxis().setTitle("Time (min)", "Segoe UI Bold", 10);
                POT.xAxis().setTitle("Time (Hrs)", "Segoe UI Bold", 10);
                POT.xAxis().setLabelStyle("Segoe UI", 8, 0x008000).setFontAngle(10);               
                //TrendLayer trendLayer = POT.addTrendLayer(viewPortDataSeriesA, 0xFF0000, "Trend Line");
                //trendLayer.setLineWidth(5);
                POT.addLegend(45, 12, false, "Segoe UI Bold", 10).setBackground(Chart.Transparent);
                chartProductionanalysis.Image = POT.makeImage();               
                chartProductionanalysis.Chart = POT;
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void PlotTimeAnalysisChart()
        {
            try
            {
                XYChart POT;
                chartTimeAnalysis.Location = new Point(5, 25);
                POT = new XYChart(pnlTimeAnalysis.Width, pnlTimeAnalysis.Height);
                POT.addTitle("Time Analysis ( "+  GetTimeInHourMinutes(TimeSpan.FromHours(timeAnalysisSummary.TotalTime)) +" Hours )", "Segoe UI Bold", 12, 0x2A58A3).setBackground(0xFFFFFF, 0xFFFFFF);
                string[] labels = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
                POT.setBackground(Chart.metalColor(0xFFFFFF), 0xFFFFFF);//, 1);
                POT.setPlotArea(90, 30, pnlTimeAnalysis.Width - 120, pnlTimeAnalysis.Height - 79, 0xffffff, 0xFFFFFF, 0xC6C6C8,
                    POT.dashLineColor(0xcccccc, Chart.DotLine), POT.dashLineColor(0xFFFFFF, Chart.DotLine));

                var result = timeAnalysisSummary;

                var operatingTime = (result.CuttingTime + result.OperatingWithoutCutting);
                TotalTime = result.TotalTime;
                PowerOnTime = result.PowerOnTime;
                PowerOffTime = result.PowerOffTime;
                CuttingTime = result.CuttingTime;
                DressingTime = result.DressingTime;

                NotCuttingTime = result.OperatingWithoutCutting;
                OperatingTime = result.OperatingTime;
                NonOperatingTime = result.NonOperatingTime;
              
                double[] data0 = { 0, 0, PowerOnTime };
                double[] data1 = { 0, 0, PowerOffTime };
                double[] data2 = { 0, OperatingTime, 0 };
                double[] data3 = { 0, NonOperatingTime, 0 };
                double[] data4 = { CuttingTime, 0, 0 };
                double[] data5 = { DressingTime, 0, 0 };
                //double[] data5 = { NotCuttingTime, 0, 0 };
                //double[] data6 = { DressingTime, 0, 0 };
                string P1; string P2;
                P1 = "CuttingTime/";
                P2 = "Non-Cutting";
                //if (DatabaseAccess.GetMTB(HomeScreen.selectedMachine) == "MGTL")
                //{
                //    P1 = "GrindingTime/";
                //    P2 = "DressingTime";
                //}
                //else
                //{
                //    P1 = "CuttingTime/";
                //    P2 = "Non-Cutting";
                //}
                string[] labelsz = { P1 + "\n" + P2 + "\n" + "( " + GetTimeInHourMinutes(TimeSpan.FromHours(CuttingTime)) + " / " + GetTimeInHourMinutes(TimeSpan.FromHours(DressingTime)) + " )", "OperatingTime/" + "\n" + "Non-Operating" + "\n" + "( " + GetTimeInHourMinutes(TimeSpan.FromHours(OperatingTime)) + " / " + GetTimeInHourMinutes(TimeSpan.FromHours(NonOperatingTime)) + " )", "PowerOn/Off" + "\n" + "( " + GetTimeInHourMinutes(TimeSpan.FromHours(PowerOnTime)) + " / " + GetTimeInHourMinutes(TimeSpan.FromHours(PowerOffTime)) + " )" };
                //string[] labelsz = { P1 + "\n" + P2 + "\n" + "( " + GetTimeInHourMinutes(TimeSpan.FromHours(CuttingTime)) + " / " + GetTimeInHourMinutes(TimeSpan.FromHours(NotCuttingTime)) + " )", "OperatingTime/" + "\n" + "Non-Operating" + "\n" + "( " + GetTimeInHourMinutes(TimeSpan.FromHours(operatingTime)) + " / " + GetTimeInHourMinutes(TimeSpan.FromHours(NonOperatingTime)) + " )", "PowerOn/Off" + "\n" + "( " + GetTimeInHourMinutes(TimeSpan.FromHours(PowerOnTime)) + " / " + GetTimeInHourMinutes(TimeSpan.FromHours(PowerOffTime)) + " )" };

                BarLayer layer = POT.addBarLayer2(Chart.Stack);

                layer.addDataSet(data0, 0x6B95FF, "#0"); // Blue Color
                layer.addDataSet(data1, 0xC8D7FF, "#2");
                layer.addDataSet(data2, 0xFFFF40, "#2");
                layer.addDataSet(data3, 0xFFFFC0, "#3");
                layer.addDataSet(data4, 0x40FF40, "#4");
                layer.addDataSet(data5, 0xDCFFDC, "#5");
                //layer.addDataSet(data6, 0xDCFFDC, "#6");

                {  
                    layer.addCustomDataLabel(0, 0, GetTimeInHourMinutes(TimeSpan.FromHours(PowerOnTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    layer.addCustomDataLabel(0, 1, GetTimeInHourMinutes(TimeSpan.FromHours(PowerOnTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    layer.addCustomDataLabel(0, 2, GetTimeInHourMinutes(TimeSpan.FromHours(PowerOnTime)), "Segoe UI ", 8).setAlignment(Chart.Center);

                    layer.addCustomDataLabel(1, 0, GetTimeInHourMinutes(TimeSpan.FromHours(PowerOffTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    layer.addCustomDataLabel(1, 1, GetTimeInHourMinutes(TimeSpan.FromHours(PowerOffTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    layer.addCustomDataLabel(1, 2, GetTimeInHourMinutes(TimeSpan.FromHours(PowerOffTime)), "Segoe UI ", 8).setAlignment(Chart.Center);

                    layer.addCustomDataLabel(2, 0, "", "Segoe UI ", 8).setAlignment(Chart.Center);
                    layer.addCustomDataLabel(2, 1, GetTimeInHourMinutes(TimeSpan.FromHours(OperatingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    layer.addCustomDataLabel(2, 2, GetTimeInHourMinutes(TimeSpan.FromHours(OperatingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);

                    layer.addCustomDataLabel(3, 0, "", "Segoe UI ", 8).setAlignment(Chart.Center);
                    layer.addCustomDataLabel(3, 1, GetTimeInHourMinutes(TimeSpan.FromHours(NonOperatingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    layer.addCustomDataLabel(3, 2, GetTimeInHourMinutes(TimeSpan.FromHours(NonOperatingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);

                    layer.addCustomDataLabel(4, 0, GetTimeInHourMinutes(TimeSpan.FromHours(CuttingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    layer.addCustomDataLabel(4, 1, GetTimeInHourMinutes(TimeSpan.FromHours(CuttingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    layer.addCustomDataLabel(4, 2, GetTimeInHourMinutes(TimeSpan.FromHours(DressingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    //layer.addCustomDataLabel(4, 2, GetTimeInHourMinutes(TimeSpan.FromHours(NotCuttingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);

                    //layer.addCustomDataLabel(5, 0, GetTimeInHourMinutes(TimeSpan.FromHours(NotCuttingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    //layer.addCustomDataLabel(5, 1, GetTimeInHourMinutes(TimeSpan.FromHours(NotCuttingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    layer.addCustomDataLabel(5, 0, GetTimeInHourMinutes(TimeSpan.FromHours(DressingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    layer.addCustomDataLabel(5, 1, GetTimeInHourMinutes(TimeSpan.FromHours(DressingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    layer.addCustomDataLabel(5, 2, GetTimeInHourMinutes(TimeSpan.FromHours(CuttingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);

                    //layer.addCustomDataLabel(6, 0, "", "Segoe UI ", 8).setAlignment(Chart.Center);
                    //layer.addCustomDataLabel(6, 1, GetTimeInHourMinutes(TimeSpan.FromHours(DressingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                    //layer.addCustomDataLabel(6, 2, GetTimeInHourMinutes(TimeSpan.FromHours(DressingTime)), "Segoe UI ", 8).setAlignment(Chart.Center);
                }

                POT.xAxis().setLabels(labelsz);
                layer.setBorderColor(Chart.Transparent);

                POT.yAxis().setLabelStyle("Segoe UI", 8, 0x008000).setFontAngle(10);
                POT.yAxis().setLinearScale(0.0, timeAnalysisSummary.TotalTime + .4,1,1);//24);
                POT.swapXY(true);
                chartTimeAnalysis.Image = POT.makeImage(); 
                layer.setHTMLImageMap("", "", "");
                chartTimeAnalysis.Chart = POT;
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private string GetTimeInHourMinutes(TimeSpan timeSpan)
        {
            return ((int)timeSpan.TotalHours).ToString("0#") + ":" + timeSpan.Minutes.ToString("0#");         
        }

        private string GetShiftString(string shiftVal)
        {           
            string shift = "";
            try
            {
                List<string> allShifts = DatabaseAccess.GetAllShifts();

                if (shiftVal.Equals("Day"))
                {
                    foreach (string item in allShifts)
                    {
                        if (shift == string.Empty)
                        {
                            shift = "'" + item.ToString() + "'";
                        }
                        else
                        {
                            if (!(item.ToString()).Equals("Day"))
                                shift = shift + "," + "'" + item.ToString() + "'";
                        }

                    }
                }
                else
                {
                    shift = "'" + shiftVal + "'";
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            return shift;
        }

        private void PlotStepLineGraph()
        {
            try
            {
                XYChart POT;
                analyticsPicturebox.Visible = true;
                analyticsPicturebox.Location = new Point(30, 4);
                chartRunChart.Location = new Point(5, 25);
                POT = new XYChart(pnlRunchart.Width, pnlRunchart.Height);
                POT.addTitle("Run Chart", "Segoe UI Bold", 12, 0x2A58A3).setBackground(0xFFFFFF, 0xFFFFFF);
                POT.setBackground(Chart.metalColor(0xFFFFFF), 0xFFFFFF);//, 1);
                POT.setPlotArea(40, 25, pnlRunchart.Width - 45, pnlRunchart.Height - 90, 0xffffff, 0xFFFFFF, 0xFFFFFF,POT.dashLineColor(0xFFFFFF, Chart.DotLine), POT.dashLineColor(0xFFFFFF, Chart.DotLine));
                
                int items = 0;

                DateTime StartTimeVal = new DateTime();
                DateTime EndTimeVal = new DateTime();

                dateTimeArray.Clear();
                List<double> dataY = new List<double>();

                StartTimeVal = ArrayStartDate;
                EndTimeVal = ArrayEndDate;

                DownStartData = stepLineArray.DownStartTime.ToArray(); // Red Start
                ProductionStartData = stepLineArray.ProductionStartTime.ToArray();
                NoDataStartData = stepLineArray.noDataStartTime.ToArray();

                DownEndData = stepLineArray.DownEndTime.ToArray(); // Red Start
                ProductionEndData = stepLineArray.ProductionEndTime.ToArray();
                NoDataEndData = stepLineArray.noDataEndTime.ToArray();

                // To Build X-Axis String From-Time to To-Time

                while (EndTimeVal > StartTimeVal)
                {
                    GenerateTimeArray(ref DateTimeArray, items + 1);
                    DateTimeArray[items] = StartTimeVal;
                    StartTimeVal = StartTimeVal.AddMinutes(1);
                    items++;
                }

                PlotColorSteps(DownStartData, DownEndData, POT, 0xFF0000, "Down Time");//Red

                PlotColorSteps(ProductionStartData, ProductionEndData, POT, 0x00FF00, "In Production");//Yellow

                PlotColorSteps(NoDataStartData, NoDataEndData, POT, 0x000000, "No Data");//Green

                // Graph for From to To-Time
                #region
                for (int i = 0; i < DateTimeArray.Length; i++)
                {
                    dateTimeArray.Add(1);
                }

                StepLineLayer layer = POT.addStepLineLayer(dateTimeArray.ToArray(), 0x000000, "No Data");
                layer.setXData(DateTimeArray);
                layer.setLineWidth(40);
                
                #endregion

                POT.xAxis().setLabelFormat("{value|hh:nn a}");
                POT.xAxis().setLabelStyle("Segoe UI", 8, 0x008000).setFontAngle(45);            
                POT.xAxis().setDateScale3("");
                POT.yAxis().setLabelStyle("Segoe UI", 0);
                POT.yAxis().setTickColor(0xffffff);
           
                chartRunChart.Image = POT.makeImage();
                layer.setHTMLImageMap("", "", "");
                chartRunChart.Chart = POT;
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void GenerateTimeArray<T>(ref T[] arr, int length)
        {
            try
            {
                T[] arrTemp = new T[length];
                if (length > arr.Length)
                {
                    Array.Copy(arr, 0, arrTemp, 0, arr.Length);
                    arr = arrTemp;
                }
                else
                {
                    Array.Copy(arr, 0, arrTemp, 0, length);
                    arr = arrTemp;
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void PlotColorSteps(DateTime[] ColorStartData, DateTime[] ColorEndData, XYChart POT, int colorCode, string dataType)
        {
            DateTime[] ColorStart = new DateTime[0];
            DateTime[] ColorEnd = new DateTime[0];
            StepLineLayer[] ColorLayer = new StepLineLayer[0];
            DateTime[] ColorDateData = new DateTime[0];
            List<double> ColorData = new List<double>();
            int rec = 0;

            for (int i = 0; i < ColorStartData.Length; i++)
            {
                ColorData.Add(1);
            }
            try
            {
                for (int i = 0; i < ColorStartData.Length; i++)
                {
                    GenerateTimeArray(ref ColorStart, rec + 1);
                    GenerateTimeArray(ref ColorEnd, rec + 1);
                    GenerateTimeArray(ref ColorLayer, rec + 1);
                    GenerateTimeArray(ref ColorDateData, 2);
                    ColorStart[rec] = ColorStartData[i];
                    ColorEnd[rec] = ColorEndData[i];

                    double[] IdataY1 = { 1, 1 };

                    ColorDateData[0] = ColorStart[rec];
                    ColorDateData[1] = ColorEnd[rec];

                    ColorLayer[rec] = POT.addStepLineLayer(IdataY1, colorCode, dataType);
                    ColorLayer[rec].setXData(ColorDateData);
                    ColorLayer[rec++].setLineWidth(40);
                }
            }
            catch (Exception ex) 
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void EnableTimer()
        {
            try
            {
                timer1.Interval = (int) TimeSpan.FromMinutes(Settings.AutoRefreshInterval).TotalMilliseconds;
                timer1.Enabled = true;
                timer1.Start();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        internal void DisposeTimer()
        {
            try
            {
                timer1.Enabled = false;
                timer1.Dispose();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }             

        private void dataGridStoppage_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //DataTable dt = dataGridStoppage.DataSource as DataTable;
            //foreach (DataColumn dc in dt.Columns)
            //{
            //    if (dc.ColumnName == "Status")
            //    {
            //        foreach (DataGridViewRow row in dataGridStoppage.Rows)
            //        {
            //            string machineStatus = dataGrid[6, row.Index].Value.ToString();
            //        }
            //    }
            //}
        }

        private void dataGridStoppage_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridStoppage.ClearSelection();
        }

    

    }
}
