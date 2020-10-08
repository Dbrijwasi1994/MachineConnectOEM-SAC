﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MachineConnectApplication;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Globalization;
using ChartDirector;

namespace MachineConnectOEM
{
    public partial class CycleProfile : UserControl
    {
        private DateTime[] timeStamps;
        private double[] dataSeries;
        private DateTime minDate;
        List<ParameterCycleInfo> cycleProfileData = null;
        Dictionary<string, string> parametersList = null;
        public CycleProfile()
        {
            InitializeComponent();
            parametersList = new Dictionary<string, string>() { { "All", "All" }, { "Work Head Temperature", "C-AxisTemp" }, { "X-Axis Load", "X-AxisLoad" }, { "Z-Axis Load", "Z-AxisLoad" }, { "C-Axis Load", "C-AxisLoad" }, { "C-Axis Speed", "C-AxisSpeed" }, { "Feed Rate", "FeedRate" } };
        }

        private void CycleProfile_Load(object sender, EventArgs e)
        {
            try
            {
                dtpFromDate.Value = DateTime.Now.AddDays(-1);
                dtpToDate.Value = DateTime.Now;
                BindMachineIDs();
                BindParameters();
                BindCycleDetailsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindMachineIDs()
        {
            try
            {
                var machine = DatabaseAccess.GetAllMachines();
                if (machine != null && machine.Count > 0)
                {
                    cmbMachineId.DataSource = machine;
                    cmbMachineId.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindParameters()
        {
            try
            {
                if (parametersList != null && parametersList.Count > 0)
                {
                    cmbParameter.DataSource = new BindingSource(parametersList, null);
                    cmbParameter.DisplayMember = "Key";
                    cmbParameter.ValueMember = "Value";
                    cmbParameter.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindCycleDetailsGrid()
        {
            try
            {
                string MachineID = cmbMachineId.SelectedItem != null ? cmbMachineId.SelectedValue.ToString() : "";
                MachineID = "TEST01";
                if (!string.IsNullOrEmpty(MachineID))
                {
                    var parameterCyclesData = DatabaseAccess.GetCycleDetailsData(MachineID, dtpFromDate.Value, dtpToDate.Value);
                    if (parameterCyclesData != null && parameterCyclesData.Count > 0)
                    {
                        var focas_CycleInfo = GetCycleInfo(parameterCyclesData);
                        if (focas_CycleInfo != null && focas_CycleInfo.Count > 0)
                        {
                            dgvCycleDetails.AutoGenerateColumns = false;
                            dgvCycleDetails.DataSource = focas_CycleInfo;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select Machine ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<CycleInfo> GetCycleInfo(List<ParameterCycleInfo> parameterCyclesData)
        {
            List<CycleInfo> cycleInfoList = new List<CycleInfo>();
            try
            {
                bool HasGotP1 = false;
                bool HasGotP2 = false;
                CycleInfo cycleInfo = null;
                foreach (ParameterCycleInfo parameterCycleInfo in parameterCyclesData)
                {
                    if (parameterCycleInfo.ParameterID.Equals("P1"))
                    {
                        if (!HasGotP1)
                        {
                            cycleInfo = new CycleInfo();
                            cycleInfo.CycleStart = parameterCycleInfo.UpdatedtimeStamp;
                            HasGotP1 = true;
                            HasGotP2 = false;
                        }
                    }
                    if (parameterCycleInfo.ParameterID.Equals("P2"))
                    {
                        HasGotP1 = false;
                        if (cycleInfo != null && !HasGotP2)
                        {
                            cycleInfo.CycleEnd = parameterCycleInfo.UpdatedtimeStamp;
                            cycleInfoList.Add(cycleInfo);
                            HasGotP2 = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(ex.Message);
            }
            return cycleInfoList;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            BindCycleDetailsGrid();
        }

        private void dgvCycleDetails_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex.Equals(0))
                {
                    DialogResult dlgConfirmation = MessageBox.Show("Are you sure you want to view charts for selected cycle ?", "Information Message", MessageBoxButtons.YesNo);
                    if (dlgConfirmation == DialogResult.Yes)
                    {
                        BindParamCycleProfile(dgvCycleDetails.Rows[e.RowIndex]);
                    }
                }
                else
                {
                    MessageBox.Show($"{dgvCycleDetails.Columns[e.ColumnIndex].HeaderText} : {dgvCycleDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindParamCycleProfile(DataGridViewRow dataGridViewRow)
        {
            string MachineID = cmbMachineId.SelectedItem != null ? cmbMachineId.SelectedValue.ToString() : "";
            try
            {
                DateTime cycleStart = Convert.ToDateTime(dataGridViewRow.Cells[1].Value.ToString());
                DateTime cycleEnd = Convert.ToDateTime(dataGridViewRow.Cells[2].Value.ToString());
                if (!string.IsNullOrEmpty(MachineID))
                {
                    cycleProfileData = DatabaseAccess.GetCycleProfileData(MachineID, cycleStart, cycleEnd);
                    if (cycleProfileData != null && cycleProfileData.Count > 0)
                    {
                        string selectedParam = cmbParameter.SelectedItem != null ? cmbParameter.Text : "";
                        if (string.IsNullOrEmpty(selectedParam) || selectedParam.Equals("All"))
                        {
                            chartViewer2.Visible = true;
                            chartViewer3.Visible = true;
                            chartViewer4.Visible = true;
                            chartViewer5.Visible = true;
                            chartViewer6.Visible = true;
                            tableLayoutPanelCharts.RowStyles[0].Height = 200;
                            tableLayoutPanelCharts.RowStyles[1].Height = 200;
                            tableLayoutPanelCharts.RowStyles[2].Height = 200;
                            tableLayoutPanelCharts.RowStyles[3].Height = 200;
                            tableLayoutPanelCharts.RowStyles[4].Height = 200;
                            tableLayoutPanelCharts.RowStyles[5].Height = 200;
                            PlotCycleProfileChart(chartViewer1, "Work Head Temperature");
                            PlotCycleProfileChart(chartViewer2, "X-Axis Load");
                            PlotCycleProfileChart(chartViewer3, "Z-Axis Load");
                            PlotCycleProfileChart(chartViewer4, "C-Axis Load");
                            PlotCycleProfileChart(chartViewer5, "C-Axis Speed");
                            PlotCycleProfileChart(chartViewer6, "Feed Rate");
                            winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer1);
                            winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer2);
                            winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer3);
                            winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer5);
                            winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer5);
                            winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer6);
                        }
                        else
                        {
                            chartViewer2.Visible = false;
                            chartViewer3.Visible = false;
                            chartViewer4.Visible = false;
                            chartViewer5.Visible = false;
                            chartViewer6.Visible = false;
                            tableLayoutPanelCharts.RowStyles[0].Height = 600;
                            tableLayoutPanelCharts.RowStyles[1].Height = 0;
                            tableLayoutPanelCharts.RowStyles[2].Height = 0;
                            tableLayoutPanelCharts.RowStyles[3].Height = 0;
                            tableLayoutPanelCharts.RowStyles[4].Height = 0;
                            tableLayoutPanelCharts.RowStyles[5].Height = 0;
                            PlotCycleProfileChart(chartViewer1, selectedParam);
                            winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveChartControl(DataGridViewRow dataGridViewRow)
        {
            try
            {
                DateTime cycleStart = Convert.ToDateTime(dataGridViewRow.Cells[1].Value.ToString());
                DateTime cycleEnd = Convert.ToDateTime(dataGridViewRow.Cells[2].Value.ToString());
                string controlName = $"ChartViewer{cycleStart:ddMMyyyyHHmmss}";
                List<WinChartViewer> chartControlsList = ChartsPanel.Controls.OfType<WinChartViewer>().ToList();
                if (chartControlsList != null && chartControlsList.Count > 0)
                {
                    Control controlToRemove = chartControlsList.Where(x => x.Name.Equals(controlName)).First();
                    if (controlToRemove != null)
                    {
                        ChartsPanel.Controls.Remove(controlToRemove);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WinChartViewer_ViewPortChanged(object sender, WinViewPortEventArgs e, WinChartViewer winChartViewer)
        {
            try
            {
                if (e.NeedUpdateChart)
                {
                    PlotCycleProfileChart(winChartViewer);
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void PlotCycleProfileChart(WinChartViewer winChartViewer, string parameter = "Feed Rate")
        {
            try
            {
                string paramId = parametersList != null ? parametersList.Where(x => x.Key.Equals(parameter, StringComparison.OrdinalIgnoreCase)).First().Value : "FeedRate";
                if (cycleProfileData.Any(x => x.ParameterID.Equals(paramId)))
                {
                    List<ParameterCycleInfo> parameterCycleData = cycleProfileData.Where(x=>x.ParameterID.Equals(paramId)).ToList();
                    if(parameterCycleData!=null&& parameterCycleData.Count>0)
                    {
                        timeStamps = new DateTime[parameterCycleData.Count];
                        dataSeries = new double[parameterCycleData.Count];
                        timeStamps = parameterCycleData.OrderBy(x => x.UpdatedtimeStamp).Select(x => x.UpdatedtimeStamp).ToArray();
                        dataSeries = parameterCycleData.OrderBy(x => x.UpdatedtimeStamp).Select(x => x.ParameterValue).ToArray();
                        minDate = timeStamps[0];    
                    }
                }

                DateTime viewPortStartDate = minDate;
                DateTime viewPortEndDate = timeStamps[timeStamps.Length - 1];
                int startIndex = Array.BinarySearch(timeStamps, viewPortStartDate);
                if (startIndex < 0) startIndex = (~startIndex) - 1;
                int endIndex = Array.BinarySearch(timeStamps, viewPortEndDate);
                if (endIndex < 0) endIndex = ((~endIndex) < timeStamps.Length) ? ~endIndex : timeStamps.Length - 1;
                int noOfPoints = endIndex - startIndex + 1;
                DateTime[] viewPortTimeStamps = new DateTime[noOfPoints];
                double[] viewPortDataSeries = new double[noOfPoints];
                Array.Copy(timeStamps, startIndex, viewPortTimeStamps, 0, noOfPoints);
                Array.Copy(dataSeries, startIndex, viewPortDataSeries, 0, noOfPoints);

                XYChart c;
                winChartViewer.Location = new Point(5, 25);
                c = new XYChart(tableLayoutPanelMain.Width, (int)tableLayoutPanelCharts.RowStyles[0].Height);
                c.addTitle(parameter, "Segoe UI Bold", 12, 0x2A58A3).setBackground(0xFFFFFF, 0xFFFFFF);
                c.setBackground(Chart.metalColor(0xFFFFFF), 0xFFFFFF);
                c.setPlotArea(55, 35, tableLayoutPanelMain.Width - 82, (int)tableLayoutPanelCharts.RowStyles[0].Height - 80, 0xffffff, 0xFFFFFF, 0xC6C6C8, c.dashLineColor(0xcccccc, Chart.DotLine), c.dashLineColor(0xFFFFFF, Chart.DotLine));
                c.yAxis().setTitle(GetYAxisTitle(parameter), "Segoe UI Bold", 10).setFontAngle(90);
                c.setClipping();
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

                if (chkShowValues.Checked)
                {
                    LineLayer layer = c.addLineLayer();
                    layer.setLineWidth(3);
                    layer.setXData(viewPortTimeStamps);
                    layer.addDataSet(viewPortDataSeries, 0x0000FF, parameter);
                    layer.setHTMLImageMap("", "", "");
                }

                double axisLowerLimit = winChartViewer.ViewPortTop + winChartViewer.ViewPortHeight;
                double axisUpperLimit = winChartViewer.ViewPortTop;
                axisUpperLimit = dataSeries.Max() + 10;
                axisLowerLimit = dataSeries.Min() > 10 ? dataSeries.Min() - 5 : dataSeries.Min();
                c.yAxis().setRounding(false, true);
                c.yAxis().setLinearScale(axisLowerLimit, axisUpperLimit);
                if (chkShowMarkers.Checked)
                {
                    if (cycleProfileData != null && cycleProfileData.Count > 0)
                    {
                        List<string> parameterIDList = new List<string>() { "P3", "P4", "P5", "P6" };
                        List<ParameterCycleInfo> parameterCycleInfos = cycleProfileData.Where(x => parameterIDList.Contains(x.ParameterID)).ToList();
                        if (parameterCycleInfos != null && parameterCycleInfos.Count > 0)
                        {
                            foreach (ParameterCycleInfo parameterCycleInfo in parameterCycleInfos)
                            {
                                if (parameterCycleInfo.ParameterID.Equals("P3"))
                                {
                                    if (parameterCycleInfo.UpdatedtimeStamp != null)
                                    {
                                        Mark markCycleEndTime = c.xAxis2().addMark(Chart.CTime(parameterCycleInfo.UpdatedtimeStamp), 0x8000, "Grinding Start", "Arial", 7);
                                        markCycleEndTime.setLineWidth(2);
                                        markCycleEndTime.setFontAngle(90);
                                        markCycleEndTime.setAlignment(Chart.BottomRight);
                                    }
                                }
                                else if (parameterCycleInfo.ParameterID.Equals("P4"))
                                {
                                    if (parameterCycleInfo.UpdatedtimeStamp != null)
                                    {
                                        Mark markCycleEndTime = c.xAxis2().addMark(Chart.CTime(parameterCycleInfo.UpdatedtimeStamp), 0x8000, "Grinding End", "Arial", 7);
                                        markCycleEndTime.setLineWidth(2);
                                        markCycleEndTime.setFontAngle(90);
                                        markCycleEndTime.setAlignment(Chart.BottomRight);
                                    }
                                }
                                else if (parameterCycleInfo.ParameterID.Equals("P5"))
                                {
                                    if (parameterCycleInfo.UpdatedtimeStamp != null)
                                    {
                                        Mark markCycleEndTime = c.xAxis2().addMark(Chart.CTime(parameterCycleInfo.UpdatedtimeStamp), 0x800000, "Dressing Start", "Arial", 7);
                                        markCycleEndTime.setLineWidth(2);
                                        markCycleEndTime.setFontAngle(90);
                                        markCycleEndTime.setAlignment(Chart.BottomRight);
                                    }
                                }
                                else if (parameterCycleInfo.ParameterID.Equals("P6"))
                                {
                                    if (parameterCycleInfo.UpdatedtimeStamp != null)
                                    {
                                        Mark markCycleEndTime = c.xAxis2().addMark(Chart.CTime(parameterCycleInfo.UpdatedtimeStamp), 0x800000, "Dressing End", "Arial", 7);
                                        markCycleEndTime.setLineWidth(2);
                                        markCycleEndTime.setFontAngle(90);
                                        markCycleEndTime.setAlignment(Chart.BottomRight);
                                    }
                                }
                            }
                        }
                        if (parameter.Equals("Feed Rate", StringComparison.OrdinalIgnoreCase))
                        {
                            string PlotMethod = "Marker";
                            if (PlotMethod.Equals("Marker"))
                            {
                                //EventStartEndTimes eventStartEndTimes = new EventStartEndTimes();
                                EventStartEndTimeStamps eventStartEndTimestamps = new EventStartEndTimeStamps();
                                //eventStartEndTimes = GetEventStartEndTimesList(cycleProfileData);
                                eventStartEndTimestamps = GetEventStartEndTimestamps(cycleProfileData);
                                if (eventStartEndTimestamps.aprFeedRateStartEndTimes != null && eventStartEndTimestamps.aprFeedRateStartEndTimes.Count > 0)
                                {
                                    foreach (EventTimestamp startEndTime in eventStartEndTimestamps.aprFeedRateStartEndTimes.Where(x => x != null))
                                    {
                                        if (startEndTime.ParameterValue.Equals(1))
                                        {
                                            Mark markAprFeedRateStart = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0x4860, "Approach feed rate", "Arial", 7);
                                            markAprFeedRateStart.setLineWidth(2);
                                            markAprFeedRateStart.setFontAngle(90);
                                            markAprFeedRateStart.setAlignment(Chart.BottomRight);
                                        }
                                        if(startEndTime.ParameterValue.Equals(0))
                                        {
                                            Mark markAprFeedRateEnd = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0x4860, "Approach feed rate", "Arial", 7);
                                            markAprFeedRateEnd.setLineWidth(2);
                                            markAprFeedRateEnd.setFontAngle(90);
                                            markAprFeedRateEnd.setAlignment(Chart.BottomRight);
                                        }
                                    }
                                }
                                if (eventStartEndTimestamps.rufFeedRateStartEndTimes != null && eventStartEndTimestamps.rufFeedRateStartEndTimes.Count > 0)
                                {
                                    foreach (EventTimestamp startEndTime in eventStartEndTimestamps.rufFeedRateStartEndTimes.Where(x => x != null))
                                    {
                                        if (startEndTime.ParameterValue.Equals(1))
                                        {
                                            Mark markRoughingFeedRateStart = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0x80FF, "Roughing feed rate", "Arial", 7);
                                            markRoughingFeedRateStart.setLineWidth(2);
                                            markRoughingFeedRateStart.setFontAngle(90);
                                            markRoughingFeedRateStart.setAlignment(Chart.BottomRight);
                                        }
                                        if (startEndTime.ParameterValue.Equals(0))
                                        {
                                            Mark markRoughingFeedRateEnd = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0x80FF, "Roughing feed rate", "Arial", 7);
                                            markRoughingFeedRateEnd.setLineWidth(2);
                                            markRoughingFeedRateEnd.setFontAngle(90);
                                            markRoughingFeedRateEnd.setAlignment(Chart.BottomRight);
                                        }
                                    }
                                }
                                if (eventStartEndTimestamps.semiFinFeedRateStartEndTimes != null && eventStartEndTimestamps.semiFinFeedRateStartEndTimes.Count > 0)
                                {
                                    foreach (EventTimestamp startEndTime in eventStartEndTimestamps.semiFinFeedRateStartEndTimes.Where(x => x != null))
                                    {
                                        if (startEndTime.ParameterValue.Equals(1))
                                        {
                                            Mark markSemiFinFeedRateStart = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0xFFFF, "Semi finishing feed rate", "Arial", 7);
                                            markSemiFinFeedRateStart.setLineWidth(2);
                                            markSemiFinFeedRateStart.setFontAngle(90);
                                            markSemiFinFeedRateStart.setAlignment(Chart.BottomRight);
                                        }
                                        if (startEndTime.ParameterValue.Equals(0))
                                        {
                                            Mark markSemiFinFeedRateEnd = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0xFFFF, "Semi finishing feed rate", "Arial", 7);
                                            markSemiFinFeedRateEnd.setLineWidth(2);
                                            markSemiFinFeedRateEnd.setFontAngle(90);
                                            markSemiFinFeedRateEnd.setAlignment(Chart.BottomRight);
                                        }
                                    }
                                }
                                if (eventStartEndTimestamps.finFeedRateStartEndTimes != null && eventStartEndTimestamps.finFeedRateStartEndTimes.Count > 0)
                                {
                                    foreach (EventTimestamp startEndTime in eventStartEndTimestamps.finFeedRateStartEndTimes.Where(x => x != null))
                                    {
                                        if (startEndTime.ParameterValue.Equals(1))
                                        {
                                            Mark markFinishingFeedRateStart = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0xFF00, "Finishing feed rate", "Arial", 7);
                                            markFinishingFeedRateStart.setLineWidth(2);
                                            markFinishingFeedRateStart.setFontAngle(90);
                                            markFinishingFeedRateStart.setAlignment(Chart.BottomRight);
                                        }
                                        if (startEndTime.ParameterValue.Equals(0))
                                        {
                                            Mark markFinishingFeedRateEnd = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0xFF00, "Finishing feed rate", "Arial", 7);
                                            markFinishingFeedRateEnd.setLineWidth(2);
                                            markFinishingFeedRateEnd.setFontAngle(90);
                                            markFinishingFeedRateEnd.setAlignment(Chart.BottomRight);
                                        }
                                    }
                                }
                                if (eventStartEndTimestamps.dreFeedRateStartEndTimes != null && eventStartEndTimestamps.dreFeedRateStartEndTimes.Count > 0)
                                {
                                    foreach (EventTimestamp startEndTime in eventStartEndTimestamps.dreFeedRateStartEndTimes.Where(x => x != null))
                                    {
                                        if (startEndTime.ParameterValue.Equals(1))
                                        {
                                            Mark markDressingFeedRateStart = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0xFFFF00, "Dressing feed rate", "Arial", 7);
                                            markDressingFeedRateStart.setLineWidth(2);
                                            markDressingFeedRateStart.setFontAngle(90);
                                            markDressingFeedRateStart.setAlignment(Chart.BottomRight);
                                        }
                                        if (startEndTime.ParameterValue.Equals(0))
                                        {
                                            Mark markDressingFeedRateEnd = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0xFFFF00, "Dressing feed rate", "Arial", 7);
                                            markDressingFeedRateEnd.setLineWidth(2);
                                            markDressingFeedRateEnd.setFontAngle(90);
                                            markDressingFeedRateEnd.setAlignment(Chart.BottomRight);
                                        }
                                    }
                                }
                            }
                            if (PlotMethod.Equals("Band"))
                            {
                                EventStartEndTimes eventStartEndTimes = new EventStartEndTimes();
                                eventStartEndTimes = GetEventStartEndTimesList(cycleProfileData);
                                if (eventStartEndTimes.aprFeedRateStartEndTimes != null && eventStartEndTimes.aprFeedRateStartEndTimes.Count > 0)
                                {
                                    foreach (StartEndTimes startEndTime in eventStartEndTimes.aprFeedRateStartEndTimes)
                                    {
                                        if (startEndTime.EventEnd > startEndTime.EventStart)
                                        {
                                            for (int ii = 0; ii < (startEndTime.EventEnd - startEndTime.EventStart).TotalSeconds; ii++)
                                            {
                                                Mark markAprFeedRate = c.xAxis2().addMark(Chart.CTime(startEndTime.EventStart.AddSeconds(ii)), 0x4860, "", "Arial", 7);
                                                markAprFeedRate.setLineWidth(2);
                                                markAprFeedRate.setFontAngle(90);
                                                markAprFeedRate.setAlignment(Chart.BottomRight);
                                            }
                                        }
                                    }
                                }
                                if (eventStartEndTimes.rufFeedRateStartEndTimes != null && eventStartEndTimes.rufFeedRateStartEndTimes.Count > 0)
                                {
                                    foreach (StartEndTimes startEndTime in eventStartEndTimes.rufFeedRateStartEndTimes)
                                    {
                                        if (startEndTime.EventEnd > startEndTime.EventStart)
                                        {
                                            for (int ii = 0; ii < (startEndTime.EventEnd - startEndTime.EventStart).TotalSeconds; ii++)
                                            {
                                                Mark markRoughingFeedRate = c.xAxis2().addMark(Chart.CTime(startEndTime.EventStart.AddSeconds(ii)), 0x80FF, "", "Arial", 7);
                                                markRoughingFeedRate.setLineWidth(2);
                                                markRoughingFeedRate.setFontAngle(90);
                                                markRoughingFeedRate.setAlignment(Chart.BottomRight);
                                            }
                                        }
                                    }
                                }
                                if (eventStartEndTimes.semiFinFeedRateStartEndTimes != null && eventStartEndTimes.semiFinFeedRateStartEndTimes.Count > 0)
                                {
                                    foreach (StartEndTimes startEndTime in eventStartEndTimes.semiFinFeedRateStartEndTimes)
                                    {
                                        if (startEndTime.EventEnd > startEndTime.EventStart)
                                        {
                                            for (int ii = 0; ii < (startEndTime.EventEnd - startEndTime.EventStart).TotalSeconds; ii++)
                                            {
                                                Mark markSemiFinFeedRate = c.xAxis2().addMark(Chart.CTime(startEndTime.EventStart.AddSeconds(ii)), 0xFFFF, "", "Arial", 7);
                                                markSemiFinFeedRate.setLineWidth(2);
                                                markSemiFinFeedRate.setFontAngle(90);
                                                markSemiFinFeedRate.setAlignment(Chart.BottomRight);
                                            }
                                        }
                                    }
                                }
                                if (eventStartEndTimes.finFeedRateStartEndTimes != null && eventStartEndTimes.finFeedRateStartEndTimes.Count > 0)
                                {
                                    foreach (StartEndTimes startEndTime in eventStartEndTimes.finFeedRateStartEndTimes)
                                    {
                                        if (startEndTime.EventEnd > startEndTime.EventStart)
                                        {
                                            for (int ii = 0; ii < (startEndTime.EventEnd - startEndTime.EventStart).TotalSeconds; ii++)
                                            {
                                                Mark markFinishingFeedRate = c.xAxis2().addMark(Chart.CTime(startEndTime.EventStart.AddSeconds(ii)), 0xFF00, "", "Arial", 7);
                                                markFinishingFeedRate.setLineWidth(2);
                                                markFinishingFeedRate.setFontAngle(90);
                                                markFinishingFeedRate.setAlignment(Chart.BottomRight);
                                            }
                                        }
                                    }
                                }
                                if (eventStartEndTimes.dreFeedRateStartEndTimes != null && eventStartEndTimes.dreFeedRateStartEndTimes.Count > 0)
                                {
                                    foreach (StartEndTimes startEndTime in eventStartEndTimes.dreFeedRateStartEndTimes)
                                    {
                                        if (startEndTime.EventEnd > startEndTime.EventStart)
                                        {
                                            for (int ii = 0; ii < (startEndTime.EventEnd - startEndTime.EventStart).TotalSeconds; ii++)
                                            {
                                                Mark markDressingFeedRate = c.xAxis2().addMark(Chart.CTime(startEndTime.EventStart.AddSeconds(ii)), 0xFFFF00, "", "Arial", 7);
                                                markDressingFeedRate.setLineWidth(2);
                                                markDressingFeedRate.setFontAngle(90);
                                                markDressingFeedRate.setAlignment(Chart.BottomRight);
                                            }
                                        }
                                    }
                                }
                                if (eventStartEndTimes.sparkOutStartEndTimes != null && eventStartEndTimes.sparkOutStartEndTimes.Count > 0)
                                {
                                    foreach (StartEndTimes startEndTime in eventStartEndTimes.sparkOutStartEndTimes)
                                    {
                                        if (startEndTime.EventEnd > startEndTime.EventStart)
                                        {
                                            for (int ii = 0; ii < (startEndTime.EventEnd - startEndTime.EventStart).TotalSeconds; ii++)
                                            {
                                                Mark markSparkOutTime = c.xAxis2().addMark(Chart.CTime(startEndTime.EventStart.AddSeconds(ii)), 0xFF80FF, "", "Arial", 7);
                                                markSparkOutTime.setLineWidth(2);
                                                markSparkOutTime.setFontAngle(90);
                                                markSparkOutTime.setAlignment(Chart.BottomRight);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                winChartViewer.Chart = c;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
        }

        private string GetYAxisTitle(string parameter)
        {
            string YAxisTitle;
            if (parameter.Equals("Work Head Temperature", StringComparison.OrdinalIgnoreCase))
            {
                YAxisTitle = "Temperature (°C)";
            }
            else if (parameter.Equals("X-Axis Load", StringComparison.OrdinalIgnoreCase))
            {
                YAxisTitle = "Load (KW)";
            }
            else if (parameter.Equals("Z-Axis Load", StringComparison.OrdinalIgnoreCase))
            {
                YAxisTitle = "Load (KW)";
            }
            else if (parameter.Equals("C-Axis Load", StringComparison.OrdinalIgnoreCase))
            {
                YAxisTitle = "Load (KW )";
            }
            else if (parameter.Equals("C-Axis Speed", StringComparison.OrdinalIgnoreCase))
            {
                YAxisTitle = "Speed (RPM)";
            }
            else if (parameter.Equals("Feed Rate", StringComparison.OrdinalIgnoreCase))
            {
                YAxisTitle = "Feed Rate (MPM)";
            }
            else
            {
                YAxisTitle = "Feed Rate (MPM)";
            }
            return YAxisTitle;
        }

        private void winChartViewer_MouseEnter(object sender, EventArgs e, WinChartViewer winChartViewer)
        {
            GetToolTipInformation(winChartViewer, "%");
        }

        private void GetToolTipInformation(WinChartViewer viewer, string type)
        {
            try
            {
                if (viewer.ImageMap == null)
                {
                    var tooltip = "title='{dataSetName}: {value|2} " + type + "'";
                    if (viewer.Chart != null)
                        viewer.ImageMap = viewer.Chart.getHTMLImageMap("clickable", "", tooltip);
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
        }

        private void chkShowMarkers_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                List<WinChartViewer> chartControlsList = tableLayoutPanelCharts.Controls.OfType<WinChartViewer>().Where(x => x.Visible).ToList();
                if (chartControlsList != null && chartControlsList.Count > 0)
                {
                    string selectedParam = cmbParameter.SelectedItem != null ? cmbParameter.Text : "";
                    if (chartControlsList.Count > 1)
                    {
                        PlotCycleProfileChart(chartViewer1, "Work Head Temperature");
                        PlotCycleProfileChart(chartViewer2, "X-Axis Load");
                        PlotCycleProfileChart(chartViewer3, "Z-Axis Load");
                        PlotCycleProfileChart(chartViewer4, "C-Axis Load");
                        PlotCycleProfileChart(chartViewer5, "C-Axis Speed");
                        PlotCycleProfileChart(chartViewer6, "Feed Rate");
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer1);
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer2);
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer3);
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer5);
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer5);
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer6);
                    }
                    else
                    {
                        PlotCycleProfileChart(chartViewer1, selectedParam);
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkShowValues_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                List<WinChartViewer> chartControlsList = tableLayoutPanelCharts.Controls.OfType<WinChartViewer>().Where(x => x.Visible).ToList();
                if (chartControlsList != null && chartControlsList.Count > 0)
                {
                    string selectedParam = cmbParameter.SelectedItem != null ? cmbParameter.Text : "";
                    if (chartControlsList.Count > 1)
                    {
                        PlotCycleProfileChart(chartViewer1, "Work Head Temperature");
                        PlotCycleProfileChart(chartViewer2, "X-Axis Load");
                        PlotCycleProfileChart(chartViewer3, "Z-Axis Load");
                        PlotCycleProfileChart(chartViewer4, "C-Axis Load");
                        PlotCycleProfileChart(chartViewer5, "C-Axis Speed");
                        PlotCycleProfileChart(chartViewer6, "Feed Rate");
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer1);
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer2);
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer3);
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer5);
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer5);
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer6);
                    }
                    else
                    {
                        PlotCycleProfileChart(chartViewer1, selectedParam);
                        winChartViewer_MouseEnter(null, EventArgs.Empty, chartViewer1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private EventStartEndTimes GetEventStartEndTimesList(List<ParameterCycleInfo> cycleProfileData)
        {
            bool aprFeedRateStarted = false;
            bool rufFeedRateStarted = false;
            bool semiFinFeedRateStarted = false;
            bool finFeedRateStarted = false;
            bool dreFeedRateStarted = false;
            bool sparkOutTimeStarted = false;
            StartEndTimes aprFeedRateStartEndTime = null;
            StartEndTimes rufFeedRateStartEndTime = null;
            StartEndTimes semiFinFeedRateStartEndTime = null;
            StartEndTimes finFeedRateStartEndTime = null;
            StartEndTimes dreFeedRateStartEndTime = null;
            StartEndTimes sparkOutStartEndTime = null;
            List<StartEndTimes> aprFeedRateStartEndTimes = new List<StartEndTimes>();
            List<StartEndTimes> rufFeedRateStartEndTimes = new List<StartEndTimes>();
            List<StartEndTimes> semiFinFeedRateStartEndTimes = new List<StartEndTimes>();
            List<StartEndTimes> finFeedRateStartEndTimes = new List<StartEndTimes>();
            List<StartEndTimes> dreFeedRateStartEndTimes = new List<StartEndTimes>();
            List<StartEndTimes> sparkOutStartEndTimes = new List<StartEndTimes>();
            EventStartEndTimes eventStartEndTimes = new EventStartEndTimes();
            try
            {
                List<string> parameterIDList = new List<string>() { "P7", "P8", "P9", "P10", "P11", "P12" };
                List<ParameterCycleInfo> parameterCycleInfos = cycleProfileData.Where(x => parameterIDList.Contains(x.ParameterID)).ToList();
                if (parameterCycleInfos != null && parameterCycleInfos.Count > 0)
                {
                    foreach (ParameterCycleInfo parameterCycleInfo in parameterCycleInfos)
                    {
                        if (parameterCycleInfo.ParameterID.Equals("P7"))
                        {
                            if (!aprFeedRateStarted)
                            {
                                aprFeedRateStarted = true;
                                aprFeedRateStartEndTime = new StartEndTimes();
                                aprFeedRateStartEndTime.EventStart = parameterCycleInfo.UpdatedtimeStamp;
                            }
                            if (rufFeedRateStarted)
                            {
                                rufFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                rufFeedRateStartEndTimes.Add(rufFeedRateStartEndTime);
                                rufFeedRateStarted = false;
                            }
                            if (semiFinFeedRateStarted)
                            {
                                semiFinFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                semiFinFeedRateStartEndTimes.Add(semiFinFeedRateStartEndTime);
                                semiFinFeedRateStarted = false;
                            }
                            if (finFeedRateStarted)
                            {
                                finFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                finFeedRateStartEndTimes.Add(finFeedRateStartEndTime);
                                finFeedRateStarted = false;
                            }
                            if (dreFeedRateStarted)
                            {
                                dreFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                dreFeedRateStartEndTimes.Add(dreFeedRateStartEndTime);
                                dreFeedRateStarted = false;
                            }
                            if (sparkOutTimeStarted)
                            {
                                sparkOutStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                sparkOutStartEndTimes.Add(sparkOutStartEndTime);
                                sparkOutTimeStarted = false;
                            }
                        }
                        else if (parameterCycleInfo.ParameterID.Equals("P8"))
                        {
                            if (aprFeedRateStarted)
                            {
                                aprFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                aprFeedRateStartEndTimes.Add(aprFeedRateStartEndTime);
                                aprFeedRateStarted = false;
                            }
                            if (!rufFeedRateStarted)
                            {
                                rufFeedRateStarted = true;
                                rufFeedRateStartEndTime = new StartEndTimes();
                                rufFeedRateStartEndTime.EventStart = parameterCycleInfo.UpdatedtimeStamp;
                            }
                            if (semiFinFeedRateStarted)
                            {
                                semiFinFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                semiFinFeedRateStartEndTimes.Add(semiFinFeedRateStartEndTime);
                                semiFinFeedRateStarted = false;
                            }
                            if (finFeedRateStarted)
                            {
                                finFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                finFeedRateStartEndTimes.Add(finFeedRateStartEndTime);
                                finFeedRateStarted = false;
                            }
                            if (dreFeedRateStarted)
                            {
                                dreFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                dreFeedRateStartEndTimes.Add(dreFeedRateStartEndTime);
                                dreFeedRateStarted = false;
                            }
                            if (sparkOutTimeStarted)
                            {
                                sparkOutStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                sparkOutStartEndTimes.Add(sparkOutStartEndTime);
                                sparkOutTimeStarted = false;
                            }
                        }
                        else if (parameterCycleInfo.ParameterID.Equals("P9"))
                        {
                            if (aprFeedRateStarted)
                            {
                                aprFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                aprFeedRateStartEndTimes.Add(aprFeedRateStartEndTime);
                                aprFeedRateStarted = false;
                            }
                            if (rufFeedRateStarted)
                            {
                                rufFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                rufFeedRateStartEndTimes.Add(rufFeedRateStartEndTime);
                                rufFeedRateStarted = false;
                            }
                            if (!semiFinFeedRateStarted)
                            {
                                semiFinFeedRateStarted = true;
                                semiFinFeedRateStartEndTime = new StartEndTimes();
                                semiFinFeedRateStartEndTime.EventStart = parameterCycleInfo.UpdatedtimeStamp;
                            }
                            if (finFeedRateStarted)
                            {
                                finFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                finFeedRateStartEndTimes.Add(finFeedRateStartEndTime);
                                finFeedRateStarted = false;
                            }
                            if (dreFeedRateStarted)
                            {
                                dreFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                dreFeedRateStartEndTimes.Add(dreFeedRateStartEndTime);
                                dreFeedRateStarted = false;
                            }
                            if (sparkOutTimeStarted)
                            {
                                sparkOutStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                sparkOutStartEndTimes.Add(sparkOutStartEndTime);
                                sparkOutTimeStarted = false;
                            }
                        }
                        else if (parameterCycleInfo.ParameterID.Equals("P10"))
                        {
                            if (aprFeedRateStarted)
                            {
                                aprFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                aprFeedRateStartEndTimes.Add(aprFeedRateStartEndTime);
                                aprFeedRateStarted = false;
                            }
                            if (rufFeedRateStarted)
                            {
                                rufFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                rufFeedRateStartEndTimes.Add(rufFeedRateStartEndTime);
                                rufFeedRateStarted = false;
                            }
                            if (semiFinFeedRateStarted)
                            {
                                semiFinFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                semiFinFeedRateStartEndTimes.Add(semiFinFeedRateStartEndTime);
                                semiFinFeedRateStarted = false;
                            }
                            if (!finFeedRateStarted)
                            {
                                finFeedRateStarted = true;
                                finFeedRateStartEndTime = new StartEndTimes();
                                finFeedRateStartEndTime.EventStart = parameterCycleInfo.UpdatedtimeStamp;
                            }
                            if (dreFeedRateStarted)
                            {
                                dreFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                dreFeedRateStartEndTimes.Add(dreFeedRateStartEndTime);
                                dreFeedRateStarted = false;
                            }
                            if (sparkOutTimeStarted)
                            {
                                sparkOutStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                sparkOutStartEndTimes.Add(sparkOutStartEndTime);
                                sparkOutTimeStarted = false;
                            }
                        }
                        else if (parameterCycleInfo.ParameterID.Equals("P11"))
                        {
                            if (aprFeedRateStarted)
                            {
                                aprFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                aprFeedRateStartEndTimes.Add(aprFeedRateStartEndTime);
                                aprFeedRateStarted = false;
                            }
                            if (rufFeedRateStarted)
                            {
                                rufFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                rufFeedRateStartEndTimes.Add(rufFeedRateStartEndTime);
                                rufFeedRateStarted = false;
                            }
                            if (semiFinFeedRateStarted)
                            {
                                semiFinFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                semiFinFeedRateStartEndTimes.Add(semiFinFeedRateStartEndTime);
                                semiFinFeedRateStarted = false;
                            }
                            if (finFeedRateStarted)
                            {
                                finFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                finFeedRateStartEndTimes.Add(finFeedRateStartEndTime);
                                finFeedRateStarted = false;
                            }
                            if (!dreFeedRateStarted)
                            {
                                dreFeedRateStarted = true;
                                dreFeedRateStartEndTime = new StartEndTimes();
                                dreFeedRateStartEndTime.EventStart = parameterCycleInfo.UpdatedtimeStamp;
                            }
                            if (sparkOutTimeStarted)
                            {
                                sparkOutStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                sparkOutStartEndTimes.Add(sparkOutStartEndTime);
                                sparkOutTimeStarted = false;
                            }
                        }
                        else if (parameterCycleInfo.ParameterID.Equals("P12"))
                        {
                            if (aprFeedRateStarted)
                            {
                                aprFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                aprFeedRateStartEndTimes.Add(aprFeedRateStartEndTime);
                                aprFeedRateStarted = false;
                            }
                            if (rufFeedRateStarted)
                            {
                                rufFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                rufFeedRateStartEndTimes.Add(rufFeedRateStartEndTime);
                                rufFeedRateStarted = false;
                            }
                            if (semiFinFeedRateStarted)
                            {
                                semiFinFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                semiFinFeedRateStartEndTimes.Add(semiFinFeedRateStartEndTime);
                                semiFinFeedRateStarted = false;
                            }
                            if (finFeedRateStarted)
                            {
                                finFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                finFeedRateStartEndTimes.Add(finFeedRateStartEndTime);
                                finFeedRateStarted = false;
                            }
                            if (dreFeedRateStarted)
                            {
                                dreFeedRateStartEndTime.EventEnd = parameterCycleInfo.UpdatedtimeStamp;
                                dreFeedRateStartEndTimes.Add(dreFeedRateStartEndTime);
                                dreFeedRateStarted = false;
                            }
                            if (!sparkOutTimeStarted)
                            {
                                sparkOutTimeStarted = true;
                                sparkOutStartEndTime = new StartEndTimes();
                                sparkOutStartEndTime.EventStart = parameterCycleInfo.UpdatedtimeStamp;
                            }
                        }
                    }
                    if (!aprFeedRateStartEndTimes.Any(x => x.EventStart.Equals(aprFeedRateStartEndTime.EventStart))) aprFeedRateStartEndTimes.Add(aprFeedRateStartEndTime);
                    if (!rufFeedRateStartEndTimes.Any(x => x.EventStart.Equals(rufFeedRateStartEndTime.EventStart))) rufFeedRateStartEndTimes.Add(rufFeedRateStartEndTime);
                    if (!semiFinFeedRateStartEndTimes.Any(x => x.EventStart.Equals(semiFinFeedRateStartEndTime.EventStart))) semiFinFeedRateStartEndTimes.Add(semiFinFeedRateStartEndTime);
                    if (!finFeedRateStartEndTimes.Any(x => x.EventStart.Equals(finFeedRateStartEndTime.EventStart))) finFeedRateStartEndTimes.Add(finFeedRateStartEndTime);
                    if (!dreFeedRateStartEndTimes.Any(x => x.EventStart.Equals(dreFeedRateStartEndTime.EventStart))) dreFeedRateStartEndTimes.Add(dreFeedRateStartEndTime);
                    if (!sparkOutStartEndTimes.Any(x => x.EventStart.Equals(sparkOutStartEndTime.EventStart))) sparkOutStartEndTimes.Add(sparkOutStartEndTime);
                }
                eventStartEndTimes.aprFeedRateStartEndTimes = aprFeedRateStartEndTimes;
                eventStartEndTimes.rufFeedRateStartEndTimes = rufFeedRateStartEndTimes;
                eventStartEndTimes.semiFinFeedRateStartEndTimes = semiFinFeedRateStartEndTimes;
                eventStartEndTimes.finFeedRateStartEndTimes = finFeedRateStartEndTimes;
                eventStartEndTimes.dreFeedRateStartEndTimes = dreFeedRateStartEndTimes;
                eventStartEndTimes.sparkOutStartEndTimes = sparkOutStartEndTimes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error plotting chart : " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return eventStartEndTimes;
        }

        private EventStartEndTimeStamps GetEventStartEndTimestamps(List<ParameterCycleInfo> cycleProfileData)
        {
            List<EventTimestamp> aprFeedRateTimestamps = new List<EventTimestamp>();
            List<EventTimestamp> rufFeedRateTimestamps = new List<EventTimestamp>();
            List<EventTimestamp> semiFinFeedRateTimestamps = new List<EventTimestamp>();
            List<EventTimestamp> finFeedRateTimestamps = new List<EventTimestamp>();
            List<EventTimestamp> dreFeedRateTimestamps = new List<EventTimestamp>();
            EventStartEndTimeStamps eventStartEndTimes = new EventStartEndTimeStamps();
            try
            {
                List<string> parameterIDList = new List<string>() { "P7", "P8", "P9", "P10", "P11" };
                List<ParameterCycleInfo> parameterCycleInfos = cycleProfileData.Where(x => parameterIDList.Contains(x.ParameterID)).ToList();
                if (parameterCycleInfos != null && parameterCycleInfos.Count > 0)
                {
                    aprFeedRateTimestamps = parameterCycleInfos.Where(x => x.ParameterID.Equals("P7")).Select(x => new EventTimestamp() { ParameterValue = x.ParameterValue, EventTimeStamp = x.UpdatedtimeStamp }).ToList();
                    rufFeedRateTimestamps = parameterCycleInfos.Where(x => x.ParameterID.Equals("P8")).Select(x => new EventTimestamp() { ParameterValue = x.ParameterValue, EventTimeStamp = x.UpdatedtimeStamp }).ToList();
                    semiFinFeedRateTimestamps = parameterCycleInfos.Where(x => x.ParameterID.Equals("P9")).Select(x => new EventTimestamp() { ParameterValue = x.ParameterValue, EventTimeStamp = x.UpdatedtimeStamp }).ToList();
                    finFeedRateTimestamps = parameterCycleInfos.Where(x => x.ParameterID.Equals("P10")).Select(x => new EventTimestamp() { ParameterValue = x.ParameterValue, EventTimeStamp = x.UpdatedtimeStamp }).ToList();
                    dreFeedRateTimestamps = parameterCycleInfos.Where(x => x.ParameterID.Equals("P11")).Select(x => new EventTimestamp() { ParameterValue = x.ParameterValue, EventTimeStamp = x.UpdatedtimeStamp }).ToList();
                }
                eventStartEndTimes.aprFeedRateStartEndTimes = aprFeedRateTimestamps;
                eventStartEndTimes.rufFeedRateStartEndTimes = rufFeedRateTimestamps;
                eventStartEndTimes.semiFinFeedRateStartEndTimes = semiFinFeedRateTimestamps;
                eventStartEndTimes.finFeedRateStartEndTimes = finFeedRateTimestamps;
                eventStartEndTimes.dreFeedRateStartEndTimes = dreFeedRateTimestamps;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error plotting chart : " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return eventStartEndTimes;
        }

        private void dgvCycleDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvCycleDetails.Rows)
                {
                    DataGridViewButtonCell cell = row.Cells[0] as DataGridViewButtonCell;
                    cell.Value = "Select";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error - " + ex.Message);
            }
        }
    }
}
