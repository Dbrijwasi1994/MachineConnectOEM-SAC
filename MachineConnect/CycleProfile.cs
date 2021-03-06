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
using MachineConnectOEM.Properties;
using Settings = MachineConnectApplication.Settings;

namespace MachineConnectOEM
{
    public partial class CycleProfile : UserControl
    {
        private DateTime[] timeStamps;
        private double[] dataSeries;
        private DateTime minDate;
        private double currentDuration = 2;
        private double dateRange = 86400;
        DataGridViewRow selectedRow = new DataGridViewRow();
        List<ParameterCycleInfo> cycleProfileData = null;
        Dictionary<string, string> parametersList = null;
        public CycleProfile()
        {
            InitializeComponent();
            parametersList = new Dictionary<string, string>() { { "All", "All" }, { "Work Head Temperature", "C-AxisTemp" }, { "X-Axis Load", "X-AxisLoad" }, { "Z-Axis Load", "Z-AxisLoad" }, { "C-Axis Load", "C-AxisLoad" }, { "C-Axis Speed", "C-AxisSpeed" }, { "Spindle Speed", "WheelSpindleRPM" }, { "Spindle Load", "WheelMotorKW" }, { "Actual Feed Rate", "ActualFeedRate" }, { "Program Feed Rate", "ProgramFeedRate" } };
        }

        private void CycleProfile_Load(object sender, EventArgs e)
        {
            try
            {
                dtpFromDate.Value = DateTime.Now.AddDays(-1);
                dtpToDate.Value = DateTime.Now;
                cmbInterval.SelectedIndex = 0;
                BindMachineIDs();
                BindParameters();
                BindCycleDetailsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetChartValues(string Parameter)
        {
            try
            {
                string prm = Parameter.Contains("SingleParam") ? Parameter.Replace("SingleParam-", "") : Parameter;
                string paramId = parametersList != null ? parametersList.Where(x => x.Key.Equals(prm, StringComparison.OrdinalIgnoreCase)).First().Value : prm;
                if (cycleProfileData.Any(x => x.ParameterID.Equals(paramId)))
                {
                    List<ParameterCycleInfo> parameterCycleData = cycleProfileData.Where(x => x.ParameterID.Equals(paramId)).ToList();
                    if (parameterCycleData != null && parameterCycleData.Count > 0)
                    {
                        timeStamps = new DateTime[parameterCycleData.Count];
                        timeStamps = parameterCycleData.OrderBy(x => x.UpdatedtimeStamp).Select(x => x.UpdatedtimeStamp).ToArray();
                    }
                }
                dateRange = timeStamps[timeStamps.Length - 1].Subtract(minDate).TotalSeconds;
                if (timeStamps != null)
                {
                    switch (Parameter)
                    {
                        case "Work Head Temperature":
                            chartViewer1.ViewPortWidth = currentDuration / dateRange;
                            chartViewer1.ViewPortLeft = 1 - chartViewer1.ViewPortWidth;
                            chartViewer1.updateViewPort(true, true);
                            break;
                        case "X-Axis Load":
                            chartViewer2.ViewPortWidth = currentDuration / dateRange;
                            chartViewer2.ViewPortLeft = 1 - chartViewer2.ViewPortWidth;
                            chartViewer2.updateViewPort(true, true);
                            break;
                        case "Z-Axis Load":
                            chartViewer3.ViewPortWidth = currentDuration / dateRange;
                            chartViewer3.ViewPortLeft = 1 - chartViewer3.ViewPortWidth;
                            chartViewer3.updateViewPort(true, true);
                            break;
                        case "C-Axis Load":
                            chartViewer4.ViewPortWidth = currentDuration / dateRange;
                            chartViewer4.ViewPortLeft = 1 - chartViewer4.ViewPortWidth;
                            chartViewer4.updateViewPort(true, true);
                            break;
                        case "C-Axis Speed":
                            chartViewer5.ViewPortWidth = currentDuration / dateRange;
                            chartViewer5.ViewPortLeft = 1 - chartViewer5.ViewPortWidth;
                            chartViewer5.updateViewPort(true, true);
                            break;
                        case "Spindle Speed":
                            chartViewer6.ViewPortWidth = currentDuration / dateRange;
                            chartViewer6.ViewPortLeft = 1 - chartViewer6.ViewPortWidth;
                            chartViewer6.updateViewPort(true, true);
                            break;
                        case "Spindle Load":
                            chartViewer7.ViewPortWidth = currentDuration / dateRange;
                            chartViewer7.ViewPortLeft = 1 - chartViewer7.ViewPortWidth;
                            chartViewer7.updateViewPort(true, true);
                            break;
                        case "Actual Feed Rate":
                            chartViewer8.ViewPortWidth = currentDuration / dateRange;
                            chartViewer8.ViewPortLeft = 1 - chartViewer8.ViewPortWidth;
                            chartViewer8.updateViewPort(true, true);
                            break;
                        case "Program Feed Rate":
                            chartViewer9.ViewPortWidth = currentDuration / dateRange;
                            chartViewer9.ViewPortLeft = 1 - chartViewer9.ViewPortWidth;
                            chartViewer9.updateViewPort(true, true);
                            break;
                        default:
                            chartViewer1.ViewPortWidth = currentDuration / dateRange;
                            chartViewer1.ViewPortLeft = 1 - chartViewer1.ViewPortWidth;
                            chartViewer1.updateViewPort(true, true);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.ToString());
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
                if (!string.IsNullOrEmpty(MachineID))
                {
                    var parameterCyclesData = DatabaseAccess.GetCycleDetailsData(MachineID, dtpFromDate.Value, dtpToDate.Value);
                    if (parameterCyclesData != null && parameterCyclesData.Count > 0)
                    {
                        var focas_CycleInfo = GetCycleInfo(parameterCyclesData);
                        if (focas_CycleInfo != null && focas_CycleInfo.Count > 0)
                        {
                            dgvCycleDetails.AutoGenerateColumns = false;
                            dgvCycleDetails.DataSource = focas_CycleInfo.OrderByDescending(x => x.CycleStart).Take(10).ToList();
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

        private void btnGetCycles_Click(object sender, EventArgs e)
        {
            BindCycleDetailsGrid();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCycleDetails.SelectedRows.Count > 0)
                {
                    selectedRow = dgvCycleDetails.SelectedRows[0] as DataGridViewRow;
                    if (selectedRow != null)
                    {
                        BindParamCycleProfile(selectedRow);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a cycle to view.", "information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        selectedRow = dgvCycleDetails.Rows[e.RowIndex];
                        BindParamCycleProfile(selectedRow);
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
                            ShowAllCharts();
                            PlotAllCharts();
                            SetMouseEnterEventForAllCharts();
                        }
                        else
                        {
                            HideCharts();
                            PlotCycleProfileChart(chartViewer1, selectedParam);
                            SetChartValues($"SingleParam-{selectedParam}");
                            chartViewer1.MouseEnter += (sender, e) => winChartViewer_MouseEnter(sender, EventArgs.Empty, chartViewer1, selectedParam);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PlotCycleProfileChart(WinChartViewer winChartViewer, string parameter = "Actual Feed Rate")
        {
            try
            {
                string paramId = parametersList != null ? parametersList.Where(x => x.Key.Equals(parameter, StringComparison.OrdinalIgnoreCase)).First().Value : "ActualFeedRate";
                if (cycleProfileData.Any(x => x.ParameterID.Equals(paramId)))
                {
                    List<ParameterCycleInfo> parameterCycleData = cycleProfileData.Where(x => x.ParameterID.Equals(paramId)).ToList();
                    if (parameterCycleData != null && parameterCycleData.Count > 0)
                    {
                        timeStamps = new DateTime[parameterCycleData.Count];
                        dataSeries = new double[parameterCycleData.Count];
                        timeStamps = parameterCycleData.OrderBy(x => x.UpdatedtimeStamp).Select(x => x.UpdatedtimeStamp).ToArray();
                        dataSeries = parameterCycleData.OrderBy(x => x.UpdatedtimeStamp).Select(x => x.ParameterValue).ToArray();
                        minDate = timeStamps[0];
                    }
                }

                DateTime viewPortStartDate = minDate.AddSeconds(Math.Round(winChartViewer.ViewPortLeft * dateRange));
                DateTime viewPortEndDate = viewPortStartDate.AddSeconds(Math.Round(winChartViewer.ViewPortWidth * dateRange));
                //DateTime viewPortStartDate = minDate;
                //DateTime viewPortEndDate = timeStamps[timeStamps.Length-1];
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
                c.yAxis().setAutoScale();
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
                    if (parameter.Contains("Temperature"))
                    {
                        AreaLayer layer = c.addAreaLayer();
                        layer.setGapColor(0xffffff);
                        layer.setLineWidth(0);
                        layer.setXData(viewPortTimeStamps);
                        layer.addDataSet(viewPortDataSeries, 0x0000FF, parameter);
                        layer.setHTMLImageMap("", "", "");

                        TrendLayer trendLayer = c.addTrendLayer(viewPortDataSeries, 0x008080, "TrendLine " + parameter, 1);
                        trendLayer.setLineWidth(3);
                        trendLayer.setXData(viewPortTimeStamps);
                        trendLayer.setHTMLImageMap("", "", "");
                    }
                    else
                    {
                        LineLayer layer = c.addLineLayer();
                        layer.setLineWidth(3);
                        layer.setXData(viewPortTimeStamps);
                        layer.addDataSet(viewPortDataSeries, 0x0000FF, parameter);
                        layer.setHTMLImageMap("", "", "");
                    }
                }

                double axisLowerLimit = winChartViewer.ViewPortTop + winChartViewer.ViewPortHeight;
                double axisUpperLimit = winChartViewer.ViewPortTop;
                axisUpperLimit = dataSeries.Max() + 10;
                axisLowerLimit = dataSeries.Min() > 10 ? dataSeries.Min() - 5 : dataSeries.Min();
                c.yAxis().setRounding(false, true);
                //c.yAxis().setLinearScale(axisLowerLimit, axisUpperLimit);
                if (chkShowMarkers.Checked)
                {
                    if (cycleProfileData != null && cycleProfileData.Count > 0)
                    {
                        List<string> parameterIDList = new List<string>() { "P1", "P2", "P3", "P4", "P5", "P6" };
                        List<ParameterCycleInfo> parameterCycleInfos = cycleProfileData.Where(x => parameterIDList.Contains(x.ParameterID)).ToList();
                        if (parameterCycleInfos != null && parameterCycleInfos.Count > 0)
                        {
                            foreach (ParameterCycleInfo parameterCycleInfo in parameterCycleInfos)
                            {
                                if (parameterCycleInfo.ParameterID.Equals("P1"))
                                {
                                    if (parameterCycleInfo.UpdatedtimeStamp != null)
                                    {
                                        Mark markCycleStartTime = c.xAxis2().addMark(Chart.CTime(parameterCycleInfo.UpdatedtimeStamp), 0x2C6101, "Cycle Start", "Arial", 7);
                                        markCycleStartTime.setLineWidth(3);
                                        markCycleStartTime.setFontAngle(90);
                                        markCycleStartTime.setAlignment(Chart.TopLeft2);
                                    }
                                }
                                if (parameterCycleInfo.ParameterID.Equals("P2"))
                                {
                                    if (parameterCycleInfo.UpdatedtimeStamp != null)
                                    {
                                        Mark markCycleEndTime = c.xAxis2().addMark(Chart.CTime(parameterCycleInfo.UpdatedtimeStamp), 0x2C6101, "Cycle End", "Arial", 7);
                                        markCycleEndTime.setLineWidth(3);
                                        markCycleEndTime.setFontAngle(90);
                                        markCycleEndTime.setAlignment(Chart.BottomRight);
                                    }
                                }
                                if (parameterCycleInfo.ParameterID.Equals("P3"))
                                {
                                    if (parameterCycleInfo.UpdatedtimeStamp != null)
                                    {
                                        Mark markCycleEndTime = c.xAxis2().addMark(Chart.CTime(parameterCycleInfo.UpdatedtimeStamp), 0x4D4D4D, "Grinding Start", "Arial", 7);
                                        markCycleEndTime.setLineWidth(3);
                                        markCycleEndTime.setFontAngle(90);
                                        markCycleEndTime.setAlignment(Chart.TopLeft2);
                                    }
                                }
                                else if (parameterCycleInfo.ParameterID.Equals("P4"))
                                {
                                    if (parameterCycleInfo.UpdatedtimeStamp != null)
                                    {
                                        Mark markCycleEndTime = c.xAxis2().addMark(Chart.CTime(parameterCycleInfo.UpdatedtimeStamp), 0x4D4D4D, "Grinding End", "Arial", 7);
                                        markCycleEndTime.setLineWidth(3);
                                        markCycleEndTime.setFontAngle(90);
                                        markCycleEndTime.setAlignment(Chart.BottomRight);
                                    }
                                }
                                else if (parameterCycleInfo.ParameterID.Equals("P5"))
                                {
                                    if (parameterCycleInfo.UpdatedtimeStamp != null)
                                    {
                                        Mark markCycleEndTime = c.xAxis2().addMark(Chart.CTime(parameterCycleInfo.UpdatedtimeStamp), 0xBFBF, "Dressing Start", "Arial", 7);
                                        markCycleEndTime.setLineWidth(3);
                                        markCycleEndTime.setFontAngle(90);
                                        markCycleEndTime.setAlignment(Chart.TopLeft2);
                                    }
                                }
                                else if (parameterCycleInfo.ParameterID.Equals("P6"))
                                {
                                    if (parameterCycleInfo.UpdatedtimeStamp != null)
                                    {
                                        Mark markCycleEndTime = c.xAxis2().addMark(Chart.CTime(parameterCycleInfo.UpdatedtimeStamp), 0xBFBF, "Dressing End", "Arial", 7);
                                        markCycleEndTime.setLineWidth(3);
                                        markCycleEndTime.setFontAngle(90);
                                        markCycleEndTime.setAlignment(Chart.BottomRight);
                                    }
                                }
                            }
                        }
                        if (parameter.Contains("Feed Rate"))
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
                                            Mark markAprFeedRateStart = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0x40, "Approach feed rate", "Arial", 7);
                                            markAprFeedRateStart.setLineWidth(3);
                                            markAprFeedRateStart.setFontAngle(90);
                                            markAprFeedRateStart.setAlignment(Chart.TopLeft2);
                                        }
                                        if (startEndTime.ParameterValue.Equals(0))
                                        {
                                            Mark markAprFeedRateEnd = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0x40, "Approach feed rate", "Arial", 7);
                                            markAprFeedRateEnd.setLineWidth(3);
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
                                            Mark markRoughingFeedRateStart = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0x80, "Roughing feed rate", "Arial", 7);
                                            markRoughingFeedRateStart.setLineWidth(3);
                                            markRoughingFeedRateStart.setFontAngle(90);
                                            markRoughingFeedRateStart.setAlignment(Chart.TopLeft2);
                                        }
                                        if (startEndTime.ParameterValue.Equals(0))
                                        {
                                            Mark markRoughingFeedRateEnd = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0x80, "Roughing feed rate", "Arial", 7);
                                            markRoughingFeedRateEnd.setLineWidth(3);
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
                                            Mark markSemiFinFeedRateStart = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0xFF, "Semi finishing feed rate", "Arial", 7);
                                            markSemiFinFeedRateStart.setLineWidth(3);
                                            markSemiFinFeedRateStart.setFontAngle(90);
                                            markSemiFinFeedRateStart.setAlignment(Chart.TopLeft2);
                                        }
                                        if (startEndTime.ParameterValue.Equals(0))
                                        {
                                            Mark markSemiFinFeedRateEnd = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0xFF, "Semi finishing feed rate", "Arial", 7);
                                            markSemiFinFeedRateEnd.setLineWidth(3);
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
                                            Mark markFinishingFeedRateStart = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0x80FF, "Finishing feed rate", "Arial", 7);
                                            markFinishingFeedRateStart.setLineWidth(3);
                                            markFinishingFeedRateStart.setFontAngle(90);
                                            markFinishingFeedRateStart.setAlignment(Chart.TopLeft2);
                                        }
                                        if (startEndTime.ParameterValue.Equals(0))
                                        {
                                            Mark markFinishingFeedRateEnd = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0x80FF, "Finishing feed rate", "Arial", 7);
                                            markFinishingFeedRateEnd.setLineWidth(3);
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
                                            Mark markDressingFeedRateStart = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0x5DBF4, "Dressing feed rate", "Arial", 7);
                                            markDressingFeedRateStart.setLineWidth(3);
                                            markDressingFeedRateStart.setFontAngle(90);
                                            markDressingFeedRateStart.setAlignment(Chart.TopLeft2);
                                        }
                                        if (startEndTime.ParameterValue.Equals(0))
                                        {
                                            Mark markDressingFeedRateEnd = c.xAxis2().addMark(Chart.CTime(startEndTime.EventTimeStamp), 0x5DBF4, "Dressing feed rate", "Arial", 7);
                                            markDressingFeedRateEnd.setLineWidth(3);
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
                                                markAprFeedRate.setLineWidth(3);
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
                                                markRoughingFeedRate.setLineWidth(3);
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
                                                markSemiFinFeedRate.setLineWidth(3);
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
                                                markFinishingFeedRate.setLineWidth(3);
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
                                                markDressingFeedRate.setLineWidth(3);
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
                                                markSparkOutTime.setLineWidth(3);
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
            else if (parameter.Contains("Load"))
            {
                YAxisTitle = "Load (KW)";
            }
            else if (parameter.Contains("Speed"))
            {
                YAxisTitle = "Speed (RPM)";
            }
            else if (parameter.Contains("Feed Rate"))
            {
                YAxisTitle = "Feed Rate (mm/min)";
            }
            else
            {
                YAxisTitle = "Feed Rate (mm/min)";
            }
            return YAxisTitle;
        }

        private void winChartViewer_MouseEnter(object sender, EventArgs e, WinChartViewer winChartViewer, string param = "Feed Rate")
        {
            GetToolTipInformation(winChartViewer, RPM.GetUnitType(param));
        }

        private void GetToolTipInformation(WinChartViewer viewer, string type)
        {
            try
            {
                if (viewer != null)
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
            ShowHideMarkersAndValues();
        }

        private void chkShowValues_CheckedChanged(object sender, EventArgs e)
        {
            ShowHideMarkersAndValues();
        }

        private void ShowHideMarkersAndValues()
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
                        PlotCycleProfileChart(chartViewer6, "Spindle Speed");
                        PlotCycleProfileChart(chartViewer7, "Spindle Load");
                        PlotCycleProfileChart(chartViewer8, "Actual Feed Rate");
                        PlotCycleProfileChart(chartViewer9, "Program Feed Rate");
                    }
                    else
                    {
                        PlotCycleProfileChart(chartViewer1, selectedParam);
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
                    cell.Style.BackColor = Color.FromArgb(24, 96, 156);
                    cell.Style.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error - " + ex.Message);
            }
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            chartViewer1.ViewPortLeft = ((double)(hScrollBar1.Value - hScrollBar1.Minimum)) / (hScrollBar1.Maximum - hScrollBar1.Minimum);
            chartViewer1.updateViewPort(true, false);
            chartViewer2.ViewPortLeft = ((double)(hScrollBar1.Value - hScrollBar1.Minimum)) / (hScrollBar1.Maximum - hScrollBar1.Minimum);
            chartViewer2.updateViewPort(true, false);
            chartViewer3.ViewPortLeft = ((double)(hScrollBar1.Value - hScrollBar1.Minimum)) / (hScrollBar1.Maximum - hScrollBar1.Minimum);
            chartViewer3.updateViewPort(true, false);
            chartViewer4.ViewPortLeft = ((double)(hScrollBar1.Value - hScrollBar1.Minimum)) / (hScrollBar1.Maximum - hScrollBar1.Minimum);
            chartViewer4.updateViewPort(true, false);
            chartViewer5.ViewPortLeft = ((double)(hScrollBar1.Value - hScrollBar1.Minimum)) / (hScrollBar1.Maximum - hScrollBar1.Minimum);
            chartViewer5.updateViewPort(true, false);
            chartViewer6.ViewPortLeft = ((double)(hScrollBar1.Value - hScrollBar1.Minimum)) / (hScrollBar1.Maximum - hScrollBar1.Minimum);
            chartViewer6.updateViewPort(true, false);
            chartViewer7.ViewPortLeft = ((double)(hScrollBar1.Value - hScrollBar1.Minimum)) / (hScrollBar1.Maximum - hScrollBar1.Minimum);
            chartViewer7.updateViewPort(true, false);
            chartViewer8.ViewPortLeft = ((double)(hScrollBar1.Value - hScrollBar1.Minimum)) / (hScrollBar1.Maximum - hScrollBar1.Minimum);
            chartViewer8.updateViewPort(true, false);
            chartViewer9.ViewPortLeft = ((double)(hScrollBar1.Value - hScrollBar1.Minimum)) / (hScrollBar1.Maximum - hScrollBar1.Minimum);
            chartViewer9.updateViewPort(true, false);
        }

        private void chartViewer1_ViewPortChanged(object sender, WinViewPortEventArgs e)
        {
            WinChartViewer winChartViewer = sender as WinChartViewer;
            currentDuration = Convert.ToInt32(cmbInterval.SelectedItem.ToString());
            hScrollBar1.Enabled = winChartViewer.ViewPortWidth < 1;
            hScrollBar1.LargeChange = (int)Math.Ceiling(winChartViewer.ViewPortWidth * (hScrollBar1.Maximum - hScrollBar1.Minimum));
            hScrollBar1.SmallChange = (int)Math.Ceiling(hScrollBar1.LargeChange * 0.1);
            hScrollBar1.Value = (int)Math.Round(winChartViewer.ViewPortLeft * (hScrollBar1.Maximum - hScrollBar1.Minimum)) + hScrollBar1.Minimum;
            switch (winChartViewer.Name)
            {
                case "chartViewer1":
                    PlotCycleProfileChart(chartViewer1, "Work Head Temperature");
                    break;
                case "chartViewer2":
                    PlotCycleProfileChart(chartViewer2, "X-Axis Load");
                    break;
                case "chartViewer3":
                    PlotCycleProfileChart(chartViewer3, "Z-Axis Load");
                    break;
                case "chartViewer4":
                    PlotCycleProfileChart(chartViewer4, "C-Axis Load");
                    break;
                case "chartViewer5":
                    PlotCycleProfileChart(chartViewer5, "C-Axis Speed");
                    break;
                case "chartViewer6":
                    PlotCycleProfileChart(chartViewer6, "Spindle Speed");
                    break;
                case "chartViewer7":
                    PlotCycleProfileChart(chartViewer7, "Spindle Load");
                    break;
                case "chartViewer8":
                    PlotCycleProfileChart(chartViewer8, "Actual Feed Rate");
                    break;
                case "chartViewer9":
                    PlotCycleProfileChart(chartViewer9, "Program Feed Rate");
                    break;
                default:
                    PlotCycleProfileChart(chartViewer8, "Actual Feed Rate");
                    break;
            }
        }

        private void cmbInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentDuration = Convert.ToInt32(cmbInterval.SelectedItem.ToString());
            if (selectedRow != null && selectedRow.Cells.Count > 1)
                BindParamCycleProfile(selectedRow);
            else
            {
                if (dgvCycleDetails.SelectedRows.Count > 0)
                {
                    selectedRow = dgvCycleDetails.SelectedRows[0] as DataGridViewRow;
                    if (selectedRow != null)
                    {
                        BindParamCycleProfile(selectedRow);
                    }
                }
            }
        }

        private void ShowAllCharts()
        {
            chartViewer1.Visible = true;
            chartViewer2.Visible = true;
            chartViewer3.Visible = true;
            chartViewer4.Visible = true;
            chartViewer5.Visible = true;
            chartViewer6.Visible = true;
            chartViewer7.Visible = true;
            chartViewer8.Visible = true;
            chartViewer9.Visible = true;
            tableLayoutPanelCharts.RowStyles[0].Height = 200;
            tableLayoutPanelCharts.RowStyles[1].Height = 200;
            tableLayoutPanelCharts.RowStyles[2].Height = 200;
            tableLayoutPanelCharts.RowStyles[3].Height = 200;
            tableLayoutPanelCharts.RowStyles[4].Height = 200;
            tableLayoutPanelCharts.RowStyles[5].Height = 200;
            tableLayoutPanelCharts.RowStyles[6].Height = 200;
            tableLayoutPanelCharts.RowStyles[7].Height = 200;
            tableLayoutPanelCharts.RowStyles[8].Height = 200;
        }

        private void HideCharts()
        {
            chartViewer2.Visible = false;
            chartViewer3.Visible = false;
            chartViewer4.Visible = false;
            chartViewer5.Visible = false;
            chartViewer6.Visible = false;
            chartViewer7.Visible = false;
            chartViewer8.Visible = false;
            chartViewer9.Visible = false;
            tableLayoutPanelCharts.RowStyles[0].Height = 500;
            tableLayoutPanelCharts.RowStyles[1].Height = 0;
            tableLayoutPanelCharts.RowStyles[2].Height = 0;
            tableLayoutPanelCharts.RowStyles[3].Height = 0;
            tableLayoutPanelCharts.RowStyles[4].Height = 0;
            tableLayoutPanelCharts.RowStyles[5].Height = 0;
            tableLayoutPanelCharts.RowStyles[6].Height = 0;
            tableLayoutPanelCharts.RowStyles[7].Height = 0;
            tableLayoutPanelCharts.RowStyles[8].Height = 0;
        }

        private void PlotAllCharts()
        {
            try
            {
                PlotCycleProfileChart(chartViewer1, "Work Head Temperature");
                SetChartValues("Work Head Temperature");
                PlotCycleProfileChart(chartViewer2, "X-Axis Load");
                SetChartValues("X-Axis Load");
                PlotCycleProfileChart(chartViewer3, "Z-Axis Load");
                SetChartValues("Z-Axis Load");
                PlotCycleProfileChart(chartViewer4, "C-Axis Load");
                SetChartValues("C-Axis Load");
                PlotCycleProfileChart(chartViewer5, "C-Axis Speed");
                SetChartValues("C-Axis Speed");
                PlotCycleProfileChart(chartViewer6, "Spindle Speed");
                SetChartValues("Spindle Speed");
                PlotCycleProfileChart(chartViewer7, "Spindle Load");
                SetChartValues("Spindle Load");
                PlotCycleProfileChart(chartViewer8, "Actual Feed Rate");
                SetChartValues("Actual Feed Rate");
                PlotCycleProfileChart(chartViewer9, "Program Feed Rate");
                SetChartValues("Program Feed Rate");
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
        }

        private void SetMouseEnterEventForAllCharts()
        {
            chartViewer1.MouseEnter += (sender, e) => winChartViewer_MouseEnter(sender, EventArgs.Empty, chartViewer1, "Work Head Temperature");
            chartViewer2.MouseEnter += (sender, e) => winChartViewer_MouseEnter(sender, EventArgs.Empty, chartViewer2, "X-Axis Load");
            chartViewer3.MouseEnter += (sender, e) => winChartViewer_MouseEnter(sender, EventArgs.Empty, chartViewer3, "Z-Axis Load");
            chartViewer4.MouseEnter += (sender, e) => winChartViewer_MouseEnter(sender, EventArgs.Empty, chartViewer4, "C-Axis Load");
            chartViewer5.MouseEnter += (sender, e) => winChartViewer_MouseEnter(sender, EventArgs.Empty, chartViewer5, "C-Axis Speed");
            chartViewer6.MouseEnter += (sender, e) => winChartViewer_MouseEnter(sender, EventArgs.Empty, chartViewer6, "Spindle Speed");
            chartViewer7.MouseEnter += (sender, e) => winChartViewer_MouseEnter(sender, EventArgs.Empty, chartViewer7, "Spindle Load");
            chartViewer8.MouseEnter += (sender, e) => winChartViewer_MouseEnter(sender, EventArgs.Empty, chartViewer8, "Actual Feed Rate");
            chartViewer9.MouseEnter += (sender, e) => winChartViewer_MouseEnter(sender, EventArgs.Empty, chartViewer9, "Program Feed Rate");
        }

        private void pictureBoxExpandContract_Click(object sender, EventArgs e)
        {
            try
            {
                if (tableLayoutPanelMain.RowStyles[1].Height > 150)
                {
                    tableLayoutPanelMain.RowStyles[1].Height = 40;
                    pictureBoxExpandContract.Image = Resources.Expand_Btn;
                }
                else
                {
                    tableLayoutPanelMain.RowStyles[1].Height = 200;
                    pictureBoxExpandContract.Image = Resources.Contract_Button;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
        }

        private void pictureBoxExpandContract_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            if (tableLayoutPanelMain.RowStyles[1].Height > 150)
            {
                toolTip.SetToolTip(this.pictureBoxExpandContract, "Contract cycle details");
            }
            else
            {
                toolTip.SetToolTip(this.pictureBoxExpandContract, "Expand cycle details");
            }            
        }

        public T GetT<T>(string name)
        {
            T result = default(T);
            return result;
        }
    }
}
