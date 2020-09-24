using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using MachineConnectOEM.DTO;
using System.Collections.ObjectModel;
using MachineConnectOEM;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Serializers;

namespace MachineConnectApplication
{
    class DatabaseAccess
    {
        public static string MongoConnectionString = ConfigurationManager.AppSettings["MongoConnectionString"].ToString();
        public static string MongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"].ToString();
        static MongoClient dbClient = new MongoClient(MongoConnectionString);

        public DatabaseAccess()
        {
            MongoDB.Bson.Serialization.BsonSerializer.RegisterSerializer(typeof(DateTime), DateTimeSerializer.LocalInstance);
        }

        #region -- Home Screen Functions
        internal static List<string> GetAllMachines()
        {
            List<string> list = new List<string>();
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "[s_GetLookups]";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", "Machine");
                cmd.Parameters.AddWithValue("@Filter", "");
                cmd.CommandTimeout = 120;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["MachineId"]))
                        {
                            list.Add(sdr["MachineId"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return list;
        }

        internal static List<string> GetAllPlants1()
        {
            List<string> list = new List<string>();
            list.Add("All Plant");
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "[s_GetLookups]";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", "Plant");
                cmd.Parameters.AddWithValue("@Filter", "");
                cmd.CommandTimeout = 120;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["Plantid"]))
                        {
                            list.Add(sdr["Plantid"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return list;
        }

        internal static List<string> GetMachinesByPlant1(string PlantID)
        {
            List<string> list = new List<string>();
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "[s_GetLookups]";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", "Machine");
                cmd.Parameters.AddWithValue("@Filter", PlantID);
                cmd.CommandTimeout = 120;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["MachineId"]))
                        {
                            list.Add(sdr["MachineId"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return list;
        }

        internal static bool CheckEmployeeDetail(string username, string password, out string isAdmin)
        {
            bool allreadyPresent = false;
            SqlConnection conn = ConnectionManager.GetConnection();
            isAdmin = string.Empty;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = " select EmployeeId,IsAdmin from dbo.employeeinformation  where [EmployeeId] = @employeeId and [upassword] = @upassword";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue(@"employeeId", username);
                cmd.Parameters.AddWithValue(@"Upassword", password);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    allreadyPresent = true;
                    isAdmin = (rdr["isAdmin"]).ToString();
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return allreadyPresent;
        }

        internal static DateTime GetShiftStartEndTimeForDay(int startOrEnd, string dateTime)
        {
            DateTime StartEndTime = new DateTime();
            SqlCommand cmd = null;
            var conn = ConnectionManager.GetConnection();
            try
            {
                if (startOrEnd == 1)
                {
                    cmd = new SqlCommand("SELECT [dbo].[f_GetLogicalDayStart] ('" + dateTime + "') as date", conn);
                }
                else
                {
                    cmd = new SqlCommand("SELECT [dbo].[f_GetLogicalDayEnd] ('" + dateTime + "')  as date", conn);
                }
                cmd.CommandTimeout = 120;
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.Parameters.AddWithValue("@FromDate", dateTime);

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (!Convert.IsDBNull(rdr["date"]))
                        {
                            StartEndTime = (Convert.ToDateTime(rdr["date"]));
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
            return StartEndTime;
        }

        internal static List<string> GetAllShifts()
        {
            List<string> shiftList = new List<string>();
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(@"select shiftname from shiftdetails where running=1 order by shiftid", sqlConn);
                cmd.CommandTimeout = 120;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    shiftList.Add(rdr["shiftname"].ToString());
                }
                shiftList.Add("Day");
                rdr.Close();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return shiftList;
        }

        internal static DateTime GetShiftStartEndTime(string dateVal, string shiftVal, out DateTime endTime)
        {
            DateTime StartTime = new DateTime();
            endTime = new DateTime();
            var conn = ConnectionManager.GetConnection();
            try
            {
                var cmd = new SqlCommand("[dbo].[s_GetShiftTime]", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDateTime", dateVal);
                cmd.Parameters.AddWithValue("@Shift", shiftVal);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (!Convert.IsDBNull(rdr["StartTime"]))
                        {
                            StartTime = (Convert.ToDateTime(rdr["StartTime"]));
                        }

                        if (!Convert.IsDBNull(rdr["EndTime"]))
                        {
                            endTime = (Convert.ToDateTime(rdr["EndTime"]));
                        }

                    }
                }

            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
            return StartTime;
        }

        internal static DataTable GetStopagedata(string dateVal, string shiftVal, string plantId, string machineId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                var cmd = new SqlCommand("[dbo].[s_GetFocasLiveDetailsForMultipleMac]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("@date", dateVal);
                cmd.Parameters.AddWithValue("@ShiftName", shiftVal);
                cmd.Parameters.AddWithValue("@plantId", plantId);
                cmd.Parameters.AddWithValue("@MachineId", machineId);
                cmd.Parameters.AddWithValue("@param", "Oemstoppages");
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                dt.AcceptChanges();
                rdr.Close();
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
            return dt;
        }

        internal static string GetDefaultThreshold()
        {
            string downtimeThreshold = string.Empty;
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(@"select * from Focas_Defaults where Parameter = 'DowntimeThreshold'", sqlConn);
                cmd.CommandTimeout = 120;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    downtimeThreshold = (rdr["ValueInText"].ToString());
                }

                rdr.Close();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return downtimeThreshold;
        }

        internal static ChartArrays GetProductionData(string dateVal, string shiftVal, string plantId, string machineId, out TimeAnalysisSummary Summaryresult)
        {
            SqlConnection _sqlConn = ConnectionManager.GetConnection();
            ChartArrays chartVals = new ChartArrays();
            Summaryresult = new TimeAnalysisSummary();
            DateTime tempDate = DateTime.MinValue;
            string tempMachine = string.Empty;
            SqlDataReader rdr = null;
            if (shiftVal.Equals("Day")) shiftVal = "";

            try
            {
                SqlCommand cmd = new SqlCommand("[s_GetFocasLiveDetailsForMultipleMac]", _sqlConn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@date", dateVal);
                cmd.Parameters.AddWithValue("@ShiftName", shiftVal);
                cmd.Parameters.AddWithValue("@plantId", plantId);
                cmd.Parameters.AddWithValue("@MachineId", machineId);
                cmd.Parameters.AddWithValue("@param", "Summary");
                //cmd.Parameters.AddWithValue("@type", "OemHour");
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {

                        if (!Convert.IsDBNull(rdr["From Time"]))
                        {
                            chartVals.startTime.Add(rdr.GetDateTime(1).ToString("hh") + "-" + rdr.GetDateTime(2).ToString("hh tt"));
                        }
                        if (!Convert.IsDBNull(rdr["PowerOnTime"]))
                        {
                            chartVals.powerOnTime.Add(Math.Round(Convert.ToDouble(rdr["PowerOnTime"]), 2));
                        }
                        if (!Convert.IsDBNull(rdr["Operating time"]))
                        {
                            chartVals.OperatingTime.Add(Math.Round(Convert.ToDouble(rdr["Operating time"]), 2));
                        }

                        if (!Convert.IsDBNull(rdr["GrindingTime"]))
                        {
                            chartVals.cuttingTime.Add(Math.Round(Convert.ToDouble(rdr["GrindingTime"]), 2));
                        }

                        if (!Convert.IsDBNull(rdr["DressingTime"]))
                        {
                            chartVals.dressingTime.Add(Math.Round(Convert.ToDouble(rdr["DressingTime"]), 2));
                        }
                    }
                    rdr.NextResult();
                    while (rdr.Read())
                    {
                        if (!Convert.IsDBNull(rdr["PowerOnTime"]))
                        {
                            Summaryresult.PowerOnTime = Convert.ToDouble(rdr["PowerOnTime"]);
                        }

                        if (!Convert.IsDBNull(rdr["GrindingTime"]))
                        {
                            Summaryresult.CuttingTime = Convert.ToDouble(rdr["GrindingTime"]);
                        }

                        if (!Convert.IsDBNull(rdr["DressingTime"]))
                        {
                            Summaryresult.DressingTime = Convert.ToDouble(rdr["DressingTime"]);
                        }

                        if (!Convert.IsDBNull(rdr["TotalTime"]))
                        {
                            Summaryresult.TotalTime = Convert.ToDouble(rdr["TotalTime"]);
                        }

                        if (!Convert.IsDBNull(rdr["Operating Time"]))
                        {
                            Summaryresult.OperatingTime = Convert.ToDouble(rdr["Operating Time"]);
                        }
                        if (!Convert.IsDBNull(rdr["OperatingWithoutCutting"]))
                        {
                            Summaryresult.OperatingWithoutCutting = Convert.ToDouble(rdr["OperatingWithoutCutting"]);
                        }
                        if (!Convert.IsDBNull(rdr["NonOperatingTime"]))
                        {
                            Summaryresult.NonOperatingTime = Convert.ToDouble(rdr["NonOperatingTime"]);
                        }
                        if (!Convert.IsDBNull(rdr["PowerOffTime"]))
                        {
                            Summaryresult.PowerOffTime = Convert.ToDouble(rdr["PowerOffTime"]);
                        }
                        if (!Convert.IsDBNull(rdr["TotalHours"]))
                        {
                            Summaryresult.TotalTime = Convert.ToDouble(rdr["TotalHours"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (rdr != null) if (_sqlConn != null) _sqlConn.Close();
            }
            return chartVals;
        }

        internal static PartCountArrays GetPartsCountData(string dateVal, string shiftVal, string plantId, string machineId)
        {
            PartCountArrays partCountArray = new PartCountArrays();
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            int count = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(@"[dbo].[s_GetFocasShiftwiseLiveDetails]", sqlConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("@Starttime", dateVal);
                cmd.Parameters.AddWithValue("@Endtime", MainScreen.LOGICAL_DAY_END);
                cmd.Parameters.AddWithValue("@Shiftname", shiftVal);
                cmd.Parameters.AddWithValue("@PlantID", plantId);
                cmd.Parameters.AddWithValue("@Machineid", machineId);
                cmd.Parameters.AddWithValue("@Param", "hour");

                string currentDate = "";
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {

                            if (!Convert.IsDBNull(rdr["From Time"]))
                            {
                                currentDate = Convert.ToString(rdr.GetDateTime(3).ToString("hh") + "-" + rdr.GetDateTime(4).ToString("hh tt"));
                                if (!partCountArray.dateTime.Contains(currentDate))
                                {
                                    count = 0;
                                    partCountArray.dateTime.Add(currentDate);
                                    partCountArray.partCount1.Add(0);
                                    partCountArray.partCount2.Add(0);
                                    partCountArray.partCount3.Add(0);

                                    partCountArray.ProgNo1.Add("");
                                    partCountArray.ProgNo2.Add("");
                                    partCountArray.ProgNo3.Add("");
                                }
                            }
                            count++;

                            if (!Convert.IsDBNull(rdr["ProgramNo"]))
                            {
                                if (count == 1)
                                {
                                    if (!Convert.IsDBNull(rdr["PartsCount"]))
                                    {
                                        partCountArray.partCount1[partCountArray.dateTime.Count - 1] = Convert.ToInt16(rdr["PartsCount"]);
                                        partCountArray.ProgNo1[partCountArray.dateTime.Count - 1] = (rdr["ProgramNo"].ToString());
                                    }
                                }

                                else if (count == 2)
                                {
                                    if (!Convert.IsDBNull(rdr["PartsCount"]))
                                    {
                                        partCountArray.partCount2[partCountArray.dateTime.Count - 1] = Convert.ToInt16(rdr["PartsCount"]);
                                        partCountArray.ProgNo2[partCountArray.dateTime.Count - 1] = (rdr["ProgramNo"].ToString());
                                    }
                                }

                                else if (count == 3)
                                {
                                    if (!Convert.IsDBNull(rdr["PartsCount"]))
                                    {
                                        partCountArray.partCount3[partCountArray.dateTime.Count - 1] = Convert.ToInt16(rdr["PartsCount"]);
                                        partCountArray.ProgNo3[partCountArray.dateTime.Count - 1] = (rdr["ProgramNo"].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            if (sqlConn != null) sqlConn.Close();
            return partCountArray;// table;
        }


        //internal static PartCountArrays GetPartsCountData(string dateVal, string shiftVal, string plantId, string machineId)
        //{
        //    PartCountArrays partCountArray = new PartCountArrays();
        //    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["TPMTrakConnString"].ConnectionString);
        //    string currentProgNo = "";
        //    int count = 0;

        //    try
        //    {
        //        sqlConn.Open();
        //        SqlCommand cmd = new SqlCommand(@"[dbo].[s_GetFocasShiftwiseLiveDetails]", sqlConn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 120;
        //        cmd.Parameters.AddWithValue("@Starttime", dateVal);
        //        cmd.Parameters.AddWithValue("@Endtime", dateVal);
        //        cmd.Parameters.AddWithValue("@Shiftname", shiftVal);
        //        cmd.Parameters.AddWithValue("@PlantID", plantId);
        //        cmd.Parameters.AddWithValue("@Machineid", machineId);
        //        cmd.Parameters.AddWithValue("@Param", "hour");

        //        string currentDate = "";
        //        string programPrefix = "";
        //        if (shiftVal.Contains(",")) programPrefix = "";
        //        using (SqlDataReader rdr = cmd.ExecuteReader())
        //        {
        //            if (rdr.HasRows)
        //            {
        //                while (rdr.Read())
        //                {

        //                    if (!Convert.IsDBNull(rdr["From Time"]))
        //                    {
        //                        currentDate = Convert.ToString(rdr.GetDateTime(3).ToString("hh") + "-" + rdr.GetDateTime(4).ToString("hh tt"));
        //                        if (!partCountArray.dateTime.Contains(currentDate))
        //                        {
        //                            count = 0;
        //                            partCountArray.dateTime.Add(currentDate);
        //                            partCountArray.partCount1.Add(0);
        //                            partCountArray.partCount2.Add(0);
        //                            partCountArray.partCount3.Add(0);

        //                            partCountArray.ProgNo1.Add("");
        //                            partCountArray.ProgNo2.Add("");
        //                            partCountArray.ProgNo3.Add("");
        //                        }
        //                    }
        //                    count++;

        //                    if (!Convert.IsDBNull(rdr["ProgramNo"]))
        //                    {
        //                        currentProgNo = Convert.ToString(rdr["ProgramNo"]);
        //                        currentProgNo = currentProgNo == "0" ? "" : programPrefix + currentProgNo;
        //                        if (count == 1)
        //                        {
        //                            if (!Convert.IsDBNull(rdr["PartsCount"]))
        //                            {
        //                                var X = Convert.ToInt16(rdr["PartsCount"]);
        //                                partCountArray.partCount1[partCountArray.dateTime.Count - 1] = X;
        //                                partCountArray.ProgNo1[partCountArray.dateTime.Count - 1] = currentProgNo;
        //                            }
        //                        }

        //                        else if (count == 2)
        //                        {
        //                            if (!Convert.IsDBNull(rdr["PartsCount"]))
        //                            {
        //                                var X = Convert.ToInt16(rdr["PartsCount"]);
        //                                partCountArray.partCount2[partCountArray.dateTime.Count - 1] = Convert.ToInt16(rdr["PartsCount"]);
        //                                partCountArray.ProgNo2[partCountArray.dateTime.Count - 1] = currentProgNo;
        //                            }
        //                        }

        //                        else if (count == 3)
        //                        {
        //                            if (!Convert.IsDBNull(rdr["PartsCount"]))
        //                            {
        //                                var X = Convert.ToInt16(rdr["PartsCount"]);
        //                                partCountArray.partCount3[partCountArray.dateTime.Count - 1] = Convert.ToInt16(rdr["PartsCount"]);
        //                                partCountArray.ProgNo3[partCountArray.dateTime.Count - 1] = currentProgNo;
        //                            }
        //                        }

        //                    }


        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteErrorLog(ex.Message);
        //        throw;
        //    }
        //    if (sqlConn != null) sqlConn.Close();
        //    return partCountArray;// table;
        //}

        internal static StepLineArray GetRuntimeDowntimeData(string dateVal, string shiftVal, string plantId, string machineId)
        {
            StepLineArray stepLineArray = new StepLineArray();
            var conn = ConnectionManager.GetConnection();
            if (machineId.Equals("All")) machineId = string.Empty;
            try
            {
                var cmd = new SqlCommand("[dbo].[s_GetFocasLiveDetailsForMultipleMac]", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@date", dateVal);
                cmd.Parameters.AddWithValue("@ShiftName", shiftVal);
                cmd.Parameters.AddWithValue("@plantId", plantId);
                cmd.Parameters.AddWithValue("@MachineId", machineId);
                cmd.Parameters.AddWithValue("@param", "OEMruntimechart");
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        string statusReason = Convert.ToString(rdr["Reason"]);

                        if (statusReason.Equals("Down"))
                        {
                            stepLineArray.DownStartTime.Add(Convert.ToDateTime(rdr["BatchStart"]));
                            stepLineArray.DownEndTime.Add(Convert.ToDateTime(rdr["BatchEnd"]));

                        }
                        else if (statusReason.Equals("Prod"))
                        {
                            stepLineArray.ProductionStartTime.Add(Convert.ToDateTime(rdr["BatchStart"]));
                            stepLineArray.ProductionEndTime.Add(Convert.ToDateTime(rdr["BatchEnd"]));

                        }
                        else if (statusReason.Equals("NoData"))
                        {
                            stepLineArray.noDataStartTime.Add(Convert.ToDateTime(rdr["BatchStart"]));
                            stepLineArray.noDataEndTime.Add(Convert.ToDateTime(rdr["BatchEnd"]));
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
            return stepLineArray;

        }

        internal static string GetMachineStatus(string machineId, out string connectionStatus, out string lastDateTime)
        {
            string machineStatus = string.Empty;
            connectionStatus = "Stopped";
            lastDateTime = string.Empty;
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(@"SELECT TOP 1 MachineMode + ' - ' + [MachineStatus] as MachineStatus, CASE when DATEDIFF(Second,CNCTimestamp,GETDATE()) > 180 then 'Stopped' else 'Running'  end as ConnectionStatus , CNCTimestamp FROM [dbo].[Focas_LiveData] where [MachineID] = @MachineId order by [CNCTimeStamp] desc", sqlConn);
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("@MachineId", machineId);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    machineStatus = rdr["MachineStatus"].ToString();
                    connectionStatus = rdr["ConnectionStatus"].ToString();
                    lastDateTime = rdr["CNCTimestamp"].ToString();
                }
                else
                {
                    machineStatus = "Unavailable";
                }

                rdr.Close();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return machineStatus;
        }

        #endregion

        internal static DataTable GetPredectiveDataGirdData(string dateVal, string machineId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                var cmd = new SqlCommand("Focas_ViewPreventiveAlarm", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("@StartTime", dateVal);
                cmd.Parameters.AddWithValue("@EndTime", MainScreen.LOGICAL_DAY_END);//Convert.ToDateTime(dateVal).AddHours(24).ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@AlarmGroup", "All");
                cmd.Parameters.AddWithValue("@MachineId", machineId);
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                dt.Columns["color"].ReadOnly = false;
                dt.AcceptChanges();
                rdr.Close();
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
            return dt;
        }

        internal static DataTable GetCNCDataGirdData(string dateVal, string machineId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                var cmd = new SqlCommand("Focas_GetAlarmReport", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("@StartTime", dateVal); // "2015-01-17 06:00:00 AM");//
                cmd.Parameters.AddWithValue("@EndTime", MainScreen.LOGICAL_DAY_END);//(Convert.ToDateTime(dateVal).AddHours(-24)).ToString("yyyy-MM-dd HH:mm:ss"));//"2016-01-18 06:00:00 AM");//
                cmd.Parameters.AddWithValue("@MachineId", machineId);
                cmd.Parameters.AddWithValue("@AlarmGroup", "ALL");
                cmd.Parameters.AddWithValue("@Param", "Summary");
                //cmd.Parameters.AddWithValue("@ShiftName", "DAY");
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                dt.Columns["color"].ReadOnly = false;
                dt.AcceptChanges();
                rdr.Close();
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
            return dt;
        }

        internal static DataTable GetSpindleLoadSpeedTemp(string machineID, string fromDate, string toDate, string Axis)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add("SpindleLoad", typeof(string));
            dt.Columns.Add("SpindleSpeed", typeof(string));
            dt.Columns.Add("CNCTimeStamp", typeof(DateTime));
            dt.Columns.Add("Temperature", typeof(string));

            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                var cmd = new SqlCommand("[Focas_GetSpindleDetails]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartTime", fromDate);
                cmd.Parameters.AddWithValue("@EndTime", toDate);
                cmd.Parameters.AddWithValue("@MachineID", HomeScreen.selectedMachine);
                cmd.Parameters.AddWithValue("@Param", (Axis == "Spindle") ? "Spindle" : "OemSpindleDetails");
                cmd.Parameters.AddWithValue("@AxisNo", (Axis == "Spindle") ? "" : Axis);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                    dt.AcceptChanges();
                    rdr.Close();
                }
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
            return dt;
        }

        internal static string GetCNCOrPreventiveAlaramCount(string machineID, string dateVal, string param)
        {
            SqlConnection _sqlConn = ConnectionManager.GetConnection();
            string count = string.Empty;
            SqlDataReader rdr = null;
            try
            {
                SqlCommand cmd = new SqlCommand("[s_GetFocasLiveDetailsForMultipleMac]", _sqlConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("@date", dateVal);
                cmd.Parameters.AddWithValue("@ShiftName", "");
                cmd.Parameters.AddWithValue("@plantId", "");
                cmd.Parameters.AddWithValue("@MachineId", machineID);
                cmd.Parameters.AddWithValue("@param", param);
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (!Convert.IsDBNull(rdr["NOofOccurences"]))
                    {
                        count = rdr["NOofOccurences"].ToString();

                    }

                }
                if (string.IsNullOrEmpty(count))
                {
                    count = "0";
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (rdr != null) if (_sqlConn != null) _sqlConn.Close();
            }
            return count;
        }

        internal static void UpdateAlarmStatus(string MachineId, string AlaramNo)
        {
            string downtimeThreshold = string.Empty;
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(@"update Focas_AlarmHistory set AckStatus = @Status where MachineID=@Machineid and AlarmNo=@AlarmNo and Ackstatus IS NULL", sqlConn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("@AlarmNo", AlaramNo);
                cmd.Parameters.AddWithValue("@MachineId", MachineId);
                cmd.Parameters.AddWithValue("@Status", "1");
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
        }

        internal static List<string> GetAllSubSystems(string machineID)
        {
            List<string> list = new List<string>();
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "select distinct subSystem from [dbo].[MacMaintSchedules] where machine = @machine";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@machine", machineID);
                cmd.CommandTimeout = 120;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["subSystem"]))
                        {
                            list.Add(Convert.ToString(sdr["subSystem"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return list;
        }

        public static void GetCurrentShiftDetails(out string shiftStartTime, out string shiftEndTime, out string shiftName)
        {
            shiftStartTime = string.Empty;
            shiftEndTime = string.Empty;
            shiftName = string.Empty;
            SqlConnection Con = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand("s_GetCurrentShiftTime", Con);  /* returns only current shift Start-End Time */
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME);
            cmd.Parameters.Add("@Param", SqlDbType.NVarChar).Value = "";
            SqlDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult);
                if (dr.HasRows)
                {
                    dr.Read();
                    shiftStartTime = dr["Starttime"].ToString();
                    shiftEndTime = dr["Endtime"].ToString();
                    shiftName = dr["shiftname"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.ToString());
            }
            finally
            {
                if (dr != null) dr.Dispose();
                if (Con != null) Con.Close();
            }
        }

        internal static PowerCaliMachineInfo GetPowerCalciMachineInfo(string machineId, string paramVal)
        {
            PowerCaliMachineInfo CalciMachineInfo = new PowerCaliMachineInfo();
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "dbo.s_NammaVantageElectricalInfo";
                cmd = new SqlCommand(sqlQuery, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(@"MachineId", machineId);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"MachineId"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"Param", paramVal);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"Param"].Direction = ParameterDirection.Input;

                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["TypeOfSpindleMotor"]))
                        {
                            CalciMachineInfo.SpindleType = Convert.ToString(sdr["TypeOfSpindleMotor"]);
                        }
                        if (!Convert.IsDBNull(sdr["PowerRating"]))
                        {
                            CalciMachineInfo.PowerRating = Convert.ToInt16(sdr["PowerRating"]);
                        }
                        if (!Convert.IsDBNull(sdr["ContinuousRating"]))
                        {
                            CalciMachineInfo.ContinuousPowerRating = Convert.ToInt16(sdr["ContinuousRating"]);
                        }

                        if (!Convert.IsDBNull(sdr["Torque"]))
                        {
                            CalciMachineInfo.TorqueRange = Convert.ToInt16(sdr["Torque"]);
                        }
                        if (!Convert.IsDBNull(sdr["BaseSpeed1"]))
                        {
                            CalciMachineInfo.BaseSpeed1 = Convert.ToInt16(sdr["BaseSpeed1"]);
                        }
                        if (!Convert.IsDBNull(sdr["BaseSpeed2"]))
                        {
                            CalciMachineInfo.BaseSpeed2 = Convert.ToInt16(sdr["BaseSpeed2"]);
                        }

                        if (!Convert.IsDBNull(sdr["BaseSpeedForShortTerm"]))
                        {
                            CalciMachineInfo.BaseSpeedSrtTerm = Convert.ToInt16(sdr["BaseSpeedForShortTerm"]);
                        }
                        if (!Convert.IsDBNull(sdr["MotorPulleyDia in mm"]))
                        {
                            CalciMachineInfo.MotorPulley = Convert.ToInt16(sdr["MotorPulleyDia in mm"]);
                        }
                        if (!Convert.IsDBNull(sdr["SpindlePulleyDia in mm"]))
                        {
                            CalciMachineInfo.SpindlePulley = Convert.ToInt16(sdr["SpindlePulleyDia in mm"]);
                        }


                    }

                }

            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return CalciMachineInfo;
        }

        internal static PowerCaliMachineInfoGreen GetPowerCalciMachineInfoGreen(string MachineId, string componentDia, string SpindleSpeed, string feed, string gamaDeg, string kDeg, string dept, string materialUsed, string paramVal)
        {
            PowerCaliMachineInfoGreen CalciMachineInfo = new PowerCaliMachineInfoGreen();
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "dbo.s_NammaVantageElectricalInfo";
                cmd = new SqlCommand(sqlQuery, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(@"MachineId", MachineId);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"MachineId"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"Diameter", componentDia);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"Diameter"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"SpindleSpeed", SpindleSpeed);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"MachineId"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"Feed", feed);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"Feed"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"ToolRakeAngle", gamaDeg);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"ToolRakeAngle"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"ToolApproachAngle", kDeg);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"ToolApproachAngle"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"Depth", dept);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"Depth"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"Material", materialUsed);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"Material"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"Param", paramVal);
                cmd.Parameters[@"Param"].Direction = ParameterDirection.Input;



                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["TorqueInShortTerm"]))
                        {
                            CalciMachineInfo.ShortTermTorqueRange = Convert.ToDouble(sdr["TorqueInShortTerm"]);
                        }

                        if (!Convert.IsDBNull(sdr["PulleyRatio"]))
                        {
                            CalciMachineInfo.PulleyRatio = Convert.ToDouble(sdr["PulleyRatio"]);
                        }
                        if (!Convert.IsDBNull(sdr["BaseSpeed1"]))
                        {
                            CalciMachineInfo.BaseSpeedVal1 = Convert.ToDouble(sdr["BaseSpeed1"]);
                        }

                        if (!Convert.IsDBNull(sdr["BaseSpeed2"]))
                        {
                            CalciMachineInfo.BaseSpeedVal2 = Convert.ToDouble(sdr["BaseSpeed2"]);
                        }
                        if (!Convert.IsDBNull(sdr["CuttingSpeed"]))
                        {
                            CalciMachineInfo.CuttingVal = Convert.ToDouble(sdr["CuttingSpeed"]);
                        }

                    }

                }

            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return CalciMachineInfo;
        }

        internal static CalculatedValues PowerCalciCutConditions(string machineId, string componentDia, string SpindleSpeed, string feed, string gamaDeg, string kDeg, string dept, string materialUsed, string paramVal)
        {

            CalculatedValues CalciMachineInfo = new CalculatedValues();
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "dbo.s_NammaVantageElectricalInfo";
                cmd = new SqlCommand(sqlQuery, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(@"MachineId", machineId);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"MachineId"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"Diameter", componentDia);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"Diameter"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"SpindleSpeed", SpindleSpeed);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"SpindleSpeed"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"Feed", feed);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"Feed"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"ToolRakeAngle", gamaDeg);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"ToolRakeAngle"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"ToolApproachAngle", kDeg);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"ToolApproachAngle"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"Depth", dept);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"Depth"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"Material", materialUsed);//"2014-06-19 04:00:11 PM");
                cmd.Parameters[@"Material"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue(@"Param", paramVal);
                cmd.Parameters[@"Param"].Direction = ParameterDirection.Input;

                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {

                        CalciMachineInfo.SpecificCuttingForce = Convert.ToDouble(sdr["SpecificCuttingForce"].ToString());

                        CalciMachineInfo.PowerRequired = Convert.ToDouble(sdr["PowerRequired"].ToString());

                        CalciMachineInfo.ChipThickness = Convert.ToDouble(sdr["ChipThickness"].ToString());

                        CalciMachineInfo.ContPowerRating = Convert.ToDouble(sdr["ContinousPowerRating"].ToString());

                        CalciMachineInfo.ShortTermPowerRating = Convert.ToDouble(sdr["ShortTermPowerRating"].ToString());

                        CalciMachineInfo.SpecificCuttingForceForremoving = Convert.ToDouble(sdr["SpecificCuttingForceForRemoving"].ToString());

                        CalciMachineInfo.curveRaise = Convert.ToDouble(sdr["curveRaise"].ToString());




                    }

                }

            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return CalciMachineInfo;
        }

        internal static List<string> GetPowerCalciConstants()
        {
            List<string> list = new List<string>();
            SqlConnection conn = ConnectionManager.GetConnection();

            try
            {
                if (conn != null) { conn.Close(); }
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"select MaterialUsed from dbo.PowerCalculatorConstant", conn);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(rdr["MaterialUsed"].ToString());
                    }
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return list;
        }

        internal static DataTable GetSubsystemDetails(string machineVal, string subSystemVal, string param, out bool isToProceed, out string imagePath, out string imageNotes)
        {
            SqlDataReader sdr = null;
            string currentShift = string.Empty;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            string sqlQuery = string.Empty;
            isToProceed = false;
            imagePath = string.Empty;
            imageNotes = string.Empty;
            try
            {
                sqlQuery = "s_ViewCheckListDetails";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActivityID", "");
                cmd.Parameters.AddWithValue("@Machine", machineVal);
                cmd.Parameters.AddWithValue("@SubSystem", subSystemVal);
                cmd.Parameters.AddWithValue("@PartName", "");
                cmd.Parameters.AddWithValue("@Activity", "");
                cmd.Parameters.AddWithValue("@frequency", "");
                cmd.Parameters.AddWithValue("@Status", "");
                cmd.Parameters.AddWithValue("@Remarks", "");
                cmd.Parameters.AddWithValue("@Time", Convert.ToDateTime(MainScreen.CURRENT_DATE_TIME).ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Standards", "");
                cmd.Parameters.AddWithValue("@Description", "");
                cmd.Parameters.AddWithValue("@param", param);
                cmd.CommandTimeout = 120;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["Procced"]))
                        {
                            if ((Convert.ToString(sdr["Procced"])).Equals("Enable"))
                            {
                                isToProceed = true;
                            }
                            if ((Convert.ToString(sdr["Procced"])).Equals("Disable"))
                            {
                                isToProceed = false;
                            }
                        }
                    }
                }

                sdr.NextResult();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["ImagePath"]))
                        {
                            imagePath = Convert.ToString(sdr["ImagePath"]);

                        }

                        if (!Convert.IsDBNull(sdr["ImageNotes"]))
                        {
                            imageNotes = Convert.ToString(sdr["ImageNotes"]);

                        }
                    }
                }

                sdr.NextResult();
                if (sdr.HasRows)
                {
                    dt.Load(sdr);
                    dt.AcceptChanges();
                }


            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }

            return dt;

        }

        internal static string GetCompanyLogo(string companyName)
        {
            SqlConnection _sqlConn = ConnectionManager.GetConnection();
            string companyLogoPath = string.Empty;
            SqlDataReader rdr = null;
            try
            {
                SqlCommand cmd = new SqlCommand("[s_GetFocasLiveDetailsForMultipleMac]", _sqlConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("@companyName", companyName);

                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (!Convert.IsDBNull(rdr["companyLogoPath"]))
                    {
                        companyLogoPath = rdr["companyLogoPath"].ToString();

                    }

                }
                if (string.IsNullOrEmpty(companyLogoPath))
                {
                    companyLogoPath = (Path.Combine(Settings.APP_PATH, @"CompanyLogo", "CompanyLogo.png"));
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (rdr != null) if (_sqlConn != null) _sqlConn.Close();
            }
            return companyLogoPath;
        }



        internal static List<string> GetAllMachinesData(string param)
        {
            List<string> list = new List<string>();
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "select * from focas_Defaults where parameter = @parameter";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@parameter", param);
                cmd.CommandTimeout = 120;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["ValueInText"]))
                        {
                            list.Add(sdr["ValueInText"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return list;
        }

        internal static List<string> GetAllMachineModelData(string param)
        {
            List<string> list = new List<string>();
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "select * from Focas_MachineModels where MTB = @MTB";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@MTB", param);
                cmd.CommandTimeout = 120;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["ModelName"]))
                        {
                            list.Add(sdr["ModelName"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return list;
        }

        internal static void UpdateServiceSettings(string value, string valueInText)
        {
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "Update Focas_Defaults set valueInText2 = @valueInText2 where valueInText = @valueInText and parameter = 'servicedata'";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@valueInText2", value);
                cmd.Parameters.AddWithValue("@valueInText", valueInText);
                cmd.CommandTimeout = 120;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
        }

        internal static ServiceSettingsVals GetAllServiceSettingsData()
        {
            List<string> list = new List<string>();
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            ServiceSettingsVals val = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "select * from Focas_defaults where parameter = 'ServiceData'";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 120;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {

                    val = new ServiceSettingsVals();
                    while (sdr.Read())
                    {
                        if ((sdr["ValueInText"]).Equals("SpindleDataInterval"))
                        {
                            val.spindle = (sdr["ValueInText2"].ToString());
                        }
                        else if ((sdr["ValueInText"]).Equals("LiveDataInterval"))
                        {
                            val.live = (sdr["ValueInText2"].ToString());
                        }
                        else if ((sdr["ValueInText"]).Equals("AlarmDataInterval"))
                        {
                            val.alarm = (sdr["ValueInText2"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return val;
        }

        //Added

        public static bool CheckUniqueIPInterfacePort(string machineId, string val, string IP)
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            SqlConnection con = sqlConn;
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string valz = string.Empty;
            bool isPresent = false;
            try
            {
                if (val.Equals("IP"))
                {
                    cmd = new SqlCommand("Select IP as Value from machineInformation where machineId !='" + machineId + "' and IP = '" + IP + "'", con);
                }

                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    isPresent = true;
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return isPresent;
        }

        internal static List<string> GetAllModels()
        {
            List<string> list = new List<string>();
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "select ModelName from Focas_MachineModels";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 120;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["ModelName"]))
                        {
                            list.Add(sdr["ModelName"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return list;
        }

        internal static bool CheckMachine(string MachineId)
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            Boolean isPresent = false;
            string adminVal = string.Empty;
            try
            {
                string query = "select MachineId from machineinformation where MachineId = @MachineId";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.Parameters.AddWithValue("@MachineId", MachineId);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (rdr.HasRows)
                {
                    isPresent = true;
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                sqlConn.Close();
            }
            return isPresent;
        }

        public static MachineInformationVals GetMachineInfoForMachine(string machineid)
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            MachineInformationVals mi = new MachineInformationVals();
            try
            {
                sqlConn.Open();
                string sqlQuery = "SELECT Machineid,description,InterfaceID,IP,IPPortNO,TPMTrakEnabled,EthernetEnabled,ProgramFoldersEnabled,MachineMTB,MachineType,MachineModel,EnablePartCountByMacro,SpindleAxisNumber FROM [dbo].[machineinformation] where machineid='" + machineid + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    mi.MachineID = Convert.ToString(rdr["machineid"]);
                    mi.Description = Convert.ToString(rdr["description"]);

                    mi.MTB = Convert.ToString(rdr["MachineMTB"]);
                    mi.MachineType = Convert.ToString(rdr["MachineType"]);
                    mi.Model = Convert.ToString(rdr["MachineModel"]);


                    mi.Interfaceid = Convert.ToString(rdr["InterfaceID"]);
                    mi.IPAddress = Convert.ToString(rdr["IP"]);
                    mi.PortNo = Convert.ToString(rdr["IPPortNO"]);
                    mi.EthernetEnabled = Convert.ToBoolean(Convert.ToString(rdr["EthernetEnabled"]));
                    mi.ProgramFoldersEnabled = Convert.ToBoolean(Convert.ToString(rdr["ProgramFoldersEnabled"]));

                    mi.SpindleAxisNumber = Convert.ToString(rdr["SpindleAxisNumber"]);
                    mi.PartCountByMacro = Convert.ToString(rdr["EnablePartCountByMacro"]);

                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return mi;
        }


        public static void UpdateMachineInfoForMachine(string Machineid, string Description, string MachineMTB, string MachineType, string MachineModel,
             string Ip, string IPPortNO, string InterfaceId, short EthernetEnabled, short ProgramFoldersEnabled, string EnablePartCountByMacro, string SpindleAxisNumber)
        {

            string sqlQuery = string.Empty;
            SqlDataReader sdr = null;
            string currentShift = string.Empty;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            try
            {
                sqlQuery = "Update machineinformation  SET InterfaceId = @InterfaceId , Description = @description ,DNCIP = @Ip, DNCIPPortNo = @PortNO,EthernetEnabled = @EthernetEnabled, MachineMTB = @MachineMTB, MachineType = @MachineType, MachineModel = @MachineModel,EnablePartCountByMacro = @EnablePartCountByMacro,SpindleAxisNumber = @SpindleAxisNumber,ProgramFoldersEnabled=@ProgramFoldersEnabled WHERE Machineid = @MachineId";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@MachineId", Machineid);
                cmd.Parameters.AddWithValue("@InterfaceId", InterfaceId);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Ip", Ip);
                cmd.Parameters.AddWithValue("@PortNO", IPPortNO);
                cmd.Parameters.AddWithValue("@EthernetEnabled", EthernetEnabled);
                cmd.Parameters.AddWithValue("@ProgramFoldersEnabled", ProgramFoldersEnabled);
                cmd.Parameters.AddWithValue("@MachineMTB", MachineMTB);
                cmd.Parameters.AddWithValue("@MachineType", MachineType);
                cmd.Parameters.AddWithValue("@MachineModel", MachineModel);
                cmd.Parameters.AddWithValue("@EnablePartCountByMacro", EnablePartCountByMacro);
                cmd.Parameters.AddWithValue("@SpindleAxisNumber", SpindleAxisNumber);
                cmd.CommandTimeout = 120;
                sdr = cmd.ExecuteReader();

            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }

        }

        internal static void InsertDataForMachine(string Machineid, string Description, string MachineMTB, string MachineType, string MachineModel,
             string Ip, string IPPortNO, string InterfaceId, short EthernetEnabled, short ProgramFoldersEnabled, string EnablePartCountByMacro, string SpindleAxisNumber)
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            int rowsAffected = 0;
            SqlConnection conn = sqlConn;
            SqlCommand cmd = null;
            string sqlQuery = "Insert into machineinformation ([machineid],[dncip],[interfaceId],[Description],[dncipportno],[TPMTrakEnabled],[EthernetEnabled],[MachineMTB],[MachineType],[MachineModel],[EnablePartCountByMacro],[SpindleAxisNumber],ProgramFoldersEnabled) values (@machineid,@IP,@interfaceId,@Description,@IPPortNO,@TPMTrakEnabled,@EthernetEnabled, @MachineMTB, @Machinetype, @MachineModel,@EnablePartCountByMacro,@SpindleAxisNumber,@ProgramFoldersEnabled)";
            try
            {
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@MachineId", Machineid);
                cmd.Parameters.AddWithValue("@InterfaceId", InterfaceId);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Ip", Ip);
                cmd.Parameters.AddWithValue("@IPPortNO", IPPortNO);
                cmd.Parameters.AddWithValue("@EthernetEnabled", EthernetEnabled);
                cmd.Parameters.AddWithValue("@MachineMTB", MachineMTB);
                cmd.Parameters.AddWithValue("@MachineType", MachineType);
                cmd.Parameters.AddWithValue("@MachineModel", MachineModel);
                cmd.Parameters.AddWithValue("@ProgramFoldersEnabled", ProgramFoldersEnabled);
                cmd.Parameters.AddWithValue("@EnablePartCountByMacro", EnablePartCountByMacro);
                cmd.Parameters.AddWithValue("@SpindleAxisNumber", SpindleAxisNumber);
                cmd.Parameters.AddWithValue("@TPMTrakEnabled", "1");
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        internal static List<string> GetMachineIdForMachineInfo()
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            List<string> machineIDList = new List<string>();
            try
            {
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(@"select distinct MachineId from machineInformation", sqlConn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    machineIDList.Add(rdr["MachineId"].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return machineIDList;
        }

        internal static DataTable GetAllMachinesDataForGrid()
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(@"select * from machineInformation", sqlConn);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                    dt.AcceptChanges();

                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return dt;
        }

        internal static void SetApplicationUISettings(string valueInText, string value)
        {
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {

                if (valueInText == "DowntimeThreshold")
                {
                    sqlQuery = "Update Focas_Defaults set valueInText = @valueInText  where parameter = 'DowntimeThreshold'";
                    cmd = new SqlCommand(sqlQuery, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@valueInText", value);
                }
                else
                {
                    sqlQuery = "Update Focas_Defaults set valueInText2 = @valueInText2  where valueInText = @valueInText and parameter = 'MachineConnectSettings'";
                    cmd = new SqlCommand(sqlQuery, conn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.AddWithValue("@valueInText2", value);
                    cmd.Parameters.AddWithValue("@valueInText", valueInText);
                }

                cmd.CommandTimeout = 120;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
        }

        internal static ApplicationSettingsVals GetApplicationUISettings()
        {

            SqlConnection sqlConn = ConnectionManager.GetConnection();
            ApplicationSettingsVals valz = new ApplicationSettingsVals() { AutoRefreshInterval = "1", StoppagesThreshold = "5", GraphTypeVal = "Line", ProgramsPath = @"C:\TPMDNC\programs\" };

            try
            {

                string sqlQuery = "select * from focas_defaults where parameter = 'MachineConnectSettings' OR parameter = 'DowntimeThreshold'";
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if ((rdr["ValueInText"].ToString()).Equals("AutoRefreshInterval"))
                    {
                        valz.AutoRefreshInterval = Convert.ToString(rdr["ValueInText2"]);
                    }
                    else if ((rdr["ValueInText"].ToString()).Equals("GraphType"))
                    {
                        valz.GraphTypeVal = Convert.ToString(rdr["ValueInText2"]);
                    }

                    else if ((rdr["ValueInText"].ToString()).Equals("AlarmsFolderPath"))
                    {
                        valz.AlarmsFolderPath = Convert.ToString(rdr["ValueInText2"]);
                    }
                    else if (rdr["parameter"].Equals("DowntimeThreshold"))
                    {
                        valz.StoppagesThreshold = (rdr["ValueInText"]).ToString();
                    }
                    else if (rdr["ValueInText"].Equals("ProgramsPath"))
                    {
                        valz.ProgramsPath = (rdr["ValueInText2"]).ToString();
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return valz;
        }

        internal static DataTable GetPredictiveAlarmsData(string machineId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                var cmd = new SqlCommand("[Focas_PredictiveMaintenanceReport]", conn);//[Focas_PredictiveMaintenanceReport]
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("@MachineId", machineId);
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                dt.AcceptChanges();
                rdr.Close();
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        internal static DataTable GetAlarmCausesAndSolution(string AlarmNo, string MachineMTB, string MachineId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                //conn.Open();
                var cmd = new SqlCommand("Focas_ViewCNCAlarm", conn);//[Focas_PredictiveMaintenanceReport]
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("@alarmNo", AlarmNo);
                cmd.Parameters.AddWithValue("@param", MachineMTB);
                cmd.Parameters.AddWithValue("@Machineid", MachineId);
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                dt.AcceptChanges();
                rdr.Close();
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        internal static DataTable GetSpindleProcessOutput(string param)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                var cmd = new SqlCommand("[Focas_GetSpindleProcessParameters]", conn);//[Focas_PredictiveMaintenanceReport]
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@param", param);
                cmd.Parameters.AddWithValue("@Machineid", HomeScreen.selectedMachine);
                cmd.CommandTimeout = 120;

                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                dt.AcceptChanges();
                rdr.Close();
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        internal static string GetGenericMachineConnectFolderPath()
        {
            string alarmPath = string.Empty;
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                //conn.Open();
                var cmd = new SqlCommand("select ValueInText2 from Focas_Defaults where parameter = 'MachineConnectSettings' and ValueInText = 'AlarmsFolderPath'", conn);//[Focas_PredictiveMaintenanceReport]
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 120;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        alarmPath = Convert.ToString(rdr["ValueInText2"]);
                    }
                }
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
            return alarmPath;
        }

        internal static List<string> GetAllPlants()
        {
            List<string> list = new List<string>();
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "selec distinct plantId from plantInformation";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 120;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["plantId"]))
                        {
                            list.Add(sdr["plantId"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return list;
        }

        internal static List<string> GetAllMachines(string plantId)
        {
            List<string> list = new List<string>();
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "selec distinct MachineId from MachineInformation ";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 120;
                sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["MachineId"]))
                        {
                            list.Add(sdr["MachineId"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return list;
        }

        internal static string GetAlarmsMTBPath(string MachineId, out string MachineModel)
        {
            string alarmPath = string.Empty;
            MachineModel = string.Empty;
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                //conn.Open();
                var cmd = new SqlCommand("select MachineMTB,MachineModel,MachineMTB from machineinformation where MachineId = @MachineId", conn);//[Focas_PredictiveMaintenanceReport]
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@MachineId", MachineId);
                cmd.CommandTimeout = 120;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        alarmPath = Convert.ToString(rdr["MachineMTB"]);
                        MachineModel = rdr["MachineModel"].ToString();

                    }
                }
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
            return alarmPath;
        }

        internal static PowerCalculatorVals CalculatePowerForMachine(string machineId, string feed, string DepthOfCut, string Diameter, string CuttingSpeed, string SpecificCuttingForce)
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            PowerCalculatorVals valz = new PowerCalculatorVals();
            try
            {

                string sqlQuery = "s_GetPowerCalculatorS";
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlConn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Model", machineId);
                cmd.Parameters.AddWithValue("@Feed", Convert.ToDouble(feed));
                cmd.Parameters.AddWithValue("@DepthOfCut", Convert.ToDouble(DepthOfCut));
                cmd.Parameters.AddWithValue("@Diameter", Convert.ToDouble(Diameter));
                cmd.Parameters.AddWithValue("@CuttingSpeed", Convert.ToDouble(CuttingSpeed));
                cmd.Parameters.AddWithValue("@SpecificCuttingForce", Convert.ToDouble(SpecificCuttingForce));
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (!string.IsNullOrEmpty(rdr["Tf"].ToString()))
                        {
                            valz.TangentialForce = Convert.ToString(rdr["Tf"]);
                        }
                        else
                        {
                            valz.TangentialForce = "0";
                        }
                        if (!string.IsNullOrEmpty(rdr["N"].ToString()))
                        {
                            valz.RPM = Convert.ToString(rdr["N"]);
                        }
                        else
                        {
                            valz.RPM = "0";
                        }

                        if (!string.IsNullOrEmpty(rdr["Torque"].ToString()))
                        {
                            valz.Torque = Convert.ToString(rdr["Torque"]);
                        }
                        else
                        {
                            valz.Torque = "0";
                        }

                        if (!string.IsNullOrEmpty(rdr["Pr"].ToString()))
                        {
                            valz.PowerRequired = Convert.ToString(rdr["Pr"]);
                        }
                        else
                        {
                            valz.PowerRequired = "0";
                        }
                        if (!string.IsNullOrEmpty(rdr["pac"].ToString()))
                        {
                            valz.PAC = Convert.ToString(rdr["pac"]);
                        }
                        else
                        {
                            valz.PAC = "0";
                        }
                        if (!string.IsNullOrEmpty(rdr["pas"].ToString()))
                        {
                            valz.PAS = Convert.ToString(rdr["pas"]);
                        }
                        else
                        {
                            valz.PAS = "0";
                        }

                        if (!string.IsNullOrEmpty(rdr["tac"].ToString()))
                        {
                            valz.TAC = Convert.ToString(rdr["tac"]);
                        }
                        else
                        {
                            valz.TAC = "0";
                        }
                        if (!string.IsNullOrEmpty(rdr["tas"].ToString()))
                        {
                            valz.TAS = Convert.ToString(rdr["tas"]);
                        }
                        else
                        {
                            valz.TAS = "0";
                        }

                        //NewlyAdded

                        if (!string.IsNullOrEmpty(rdr["BaseSpeedOnMotor"].ToString()))
                        {
                            valz.BaseSpeedOnMotor = Convert.ToString(rdr["BaseSpeedOnMotor"]);
                        }
                        else
                        {
                            valz.BaseSpeedOnMotor = "0";
                        }
                        if (!string.IsNullOrEmpty(rdr["BaseSpeedOnSpindle"].ToString()))
                        {
                            valz.BaseSpeedOnSpindle = Convert.ToString(rdr["BaseSpeedOnSpindle"]);
                        }
                        else
                        {
                            valz.BaseSpeedOnSpindle = "0";
                        }
                        if (!string.IsNullOrEmpty(rdr["ContPower"].ToString()))
                        {
                            valz.ContPower = Convert.ToString(rdr["ContPower"]);
                        }
                        else
                        {
                            valz.ContPower = "0";
                        }
                        if (!string.IsNullOrEmpty(rdr["StMinPower"].ToString()))
                        {
                            valz.StMinPower = Convert.ToString(rdr["StMinPower"]);
                        }
                        else
                        {
                            valz.StMinPower = "0";
                        }
                        if (!string.IsNullOrEmpty(rdr["ContTorque"].ToString()))
                        {
                            valz.ContTorque = Convert.ToString(rdr["ContTorque"]);
                        }
                        else
                        {
                            valz.ContTorque = "0";
                        }
                        if (!string.IsNullOrEmpty(rdr["StMinTorque"].ToString()))
                        {
                            valz.StMinTorque = Convert.ToString(rdr["StMinTorque"]);
                        }
                        else
                        {
                            valz.StMinTorque = "0";
                        }
                        if (!string.IsNullOrEmpty(rdr["StMin"].ToString()))
                        {
                            valz.StMin = Convert.ToString(rdr["StMin"]);
                        }
                        else
                        {
                            valz.StMin = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return valz;
        }

        internal static DataTable GetOperatorMaintenanceCheckList()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                //conn.Open();
                var cmd = new SqlCommand("select * from OperatorMaintenanceChecklist", conn);//[Focas_PredictiveMaintenanceReport]
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 120;
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                dt.AcceptChanges();
                rdr.Close();
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        internal static string GetModelForMachine(string MachineId)
        {
            string MachineModel = string.Empty;
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                //conn.Open();
                var cmd = new SqlCommand("select MachineModel from machineinformation where MachineId = @MachineId", conn);//[Focas_PredictiveMaintenanceReport]
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@MachineId", MachineId);
                cmd.CommandTimeout = 120;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        MachineModel = Convert.ToString(rdr["MachineModel"]);
                    }
                }
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
            return MachineModel;
        }

        internal static string GetProductPathForMTB(string MachineId, string manualsMTBPath)
        {
            string pathString = string.Empty;
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                //conn.Open();
                var cmd = new SqlCommand("select MachineMTB,MachineModel from machineinformation where MachineId = @MachineId", conn);//[Focas_PredictiveMaintenanceReport]
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@MachineId", MachineId);
                cmd.CommandTimeout = 120;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        pathString = Convert.ToString(rdr["MachineMTB"]) + @"\" + Convert.ToString(rdr["MachineModel"]);
                    }
                }
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
            return pathString;
        }

        internal static string GetProductionGraphType()
        {
            string graphType = string.Empty;
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                var cmd = new SqlCommand("select valueInText2 from focas_defaults where parameter = 'MachineConnectSettings' and valueInText = 'GraphType'", conn);//[Focas_PredictiveMaintenanceReport]
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 120;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        graphType = Convert.ToString(rdr["valueInText2"]);
                    }
                }
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
            return graphType;
        }


        #region


        //Shift Details
        internal static List<ShiftDetails> GetAllshiftDetails()
        {
            List<ShiftDetails> list = new List<ShiftDetails>();
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            DataTable table = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(@"select * from shiftdetails where running=1 order by shiftid", sqlConn);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ShiftDetails shftVals = new ShiftDetails();
                        if (!Convert.IsDBNull(rdr["ShiftId"]))
                        {
                            shftVals.shiftId = Convert.ToString(rdr["ShiftId"]);
                        }

                        if (!Convert.IsDBNull(rdr["ShiftName"]))
                        {
                            shftVals.ShiftName = Convert.ToString(rdr["ShiftName"]);
                        }

                        if (!Convert.IsDBNull(rdr["FromDay"]))
                        {
                            shftVals.FromDay = Convert.ToString(rdr["FromDay"]);
                        }
                        if (!Convert.IsDBNull(rdr["FromTime"]))
                        {
                            DateTime dt = Convert.ToDateTime(rdr["FromTime"]);
                            shftVals.FromTime = dt.ToString("hh:mm:ss tt");
                        }

                        if (!Convert.IsDBNull(rdr["ToDay"]))
                        {
                            shftVals.ToDay = Convert.ToString(rdr["ToDay"]);
                        }

                        if (!Convert.IsDBNull(rdr["ToTime"]))
                        {
                            DateTime dt = Convert.ToDateTime(rdr["ToTime"]);
                            shftVals.ToTime = dt.ToString("hh:mm:ss tt");
                        }

                        list.Add(shftVals);
                    }

                }
                else
                {
                    list = null;
                }
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return list;
        }

        internal static List<string> GetAllShiftIds()
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            List<string> shiftIdList = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand(@"select distinct ShiftId from shiftdetails Order By ShiftId", sqlConn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    shiftIdList.Add(rdr["ShiftId"].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return shiftIdList;
        }

        //internal static bool CheckForShiftIDName(string shiftId, string shiftName)
        //{
        //    SqlConnection sqlConn = ConnectionManager.GetConnection();
        //    SqlConnection DBcon = sqlConn;
        //    SqlCommand cmd = new SqlCommand();
        //    SqlDataReader reader;
        //    string valz = string.Empty;
        //    bool isPresent = false;
        //    try
        //    {
        //        DBcon.Open();
        //        cmd = new SqlCommand("Select shiftId from ShiftDetails where shiftId = @shiftId ", DBcon);
        //        cmd.Parameters.AddWithValue("@shiftId", shiftId);
        //        cmd.Parameters.AddWithValue("@shiftName", shiftName);

        //        reader = cmd.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            isPresent = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error,.!!"+ex.Message);
        //    }
        //    finally
        //    {
        //        DBcon.Close();
        //    }
        //    return isPresent;
        //}

        internal static void UpdateShiftDetails(string shiftId, string shiftName, string fromDay, string toDay, DateTime fromTime, DateTime toTime)
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            int rowsAffected = 0, fDay = 0, tDay = 0;

            SqlConnection conn = sqlConn;
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            sqlQuery = "update ShiftDetails set shiftName = @shiftName ,fromDay =@fromDay, toDay= @toDay, fromTime= @fromTime,Totime= @Totime where shiftId= @shiftId ";

            try
            {
                cmd = new SqlCommand(sqlQuery, conn);

                cmd.Parameters.AddWithValue("@shiftId", shiftId);
                cmd.Parameters.AddWithValue("@shiftName", shiftName);

                if (fromDay.Equals("Tomorrow")) fDay = 1;
                else if (fromDay.Equals("Yesterday")) fDay = 2;

                cmd.Parameters.AddWithValue("@fromDay", fDay);

                if (toDay.Equals("Tomorrow")) tDay = 1;
                else if (toDay.Equals("Yesterday")) tDay = 2;

                cmd.Parameters.AddWithValue("@toDay", tDay);

                cmd.Parameters.AddWithValue("@fromTime", fromTime);
                cmd.Parameters.AddWithValue("@toTime", toTime);

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        internal static bool CheckShiftId(string shiftId)
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            bool allreadyPresent = false;
            object obj = null;
            SqlConnection conn = sqlConn;
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "select shiftId from shiftDetails where shiftId = @shiftId and Running = 1";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@shiftId", shiftId);
                obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    allreadyPresent = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return allreadyPresent;
        }

        internal static void InsertShiftDetails(string shiftId, string shiftName, string fromDay, string toDay, DateTime fromTime, DateTime toTime)
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            int rowsAffected = 0, fDay = 0, tDay = 0;

            SqlConnection conn = sqlConn;
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            sqlQuery = "Insert into ShiftDetails ([shiftName],[fromDay] ,[toDay],[fromTime] ,[toTime], [shiftId],Running) values  (@shiftName , @fromDay, @toDay,  @fromTime, @Totime,@shiftId,1 ) ";

            try
            {
                cmd = new SqlCommand(sqlQuery, conn);

                cmd.Parameters.AddWithValue("@shiftId", shiftId);
                cmd.Parameters.AddWithValue("@shiftName", shiftName);

                if (fromDay.Equals("Tomorrow")) fDay = 1;
                else if (fromDay.Equals("Yesterday")) fDay = 2;

                cmd.Parameters.AddWithValue("@fromDay", fDay);

                if (toDay.Equals("Tomorrow")) tDay = 1;
                else if (toDay.Equals("Yesterday")) tDay = 2;

                cmd.Parameters.AddWithValue("@toDay", tDay);

                cmd.Parameters.AddWithValue("@fromTime", fromTime);
                cmd.Parameters.AddWithValue("@toTime", toTime);

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        internal static void RemoveAllShiftdata()
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            int rowsAffected = 0;
            SqlConnection conn = sqlConn;
            SqlCommand cmd = null;
            string sqlQuery = "Update shiftDetails SET Running = 0 where Running = 1";
            try
            {
                cmd = new SqlCommand(sqlQuery, conn);
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        internal static bool CheckForShiftName(string shiftName, string shiftId)
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand();
            object obj = null;// reader;
            string valz = string.Empty;
            bool isPresent = false;
            try
            {
                cmd = new SqlCommand("Select shiftId from ShiftDetails where  shiftName = @shiftName and shiftId != @shiftId ", sqlConn);
                cmd.Parameters.AddWithValue("@shiftId", shiftId);
                cmd.Parameters.AddWithValue("@shiftName", shiftName);

                obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    isPresent = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }
            return isPresent;
        }

        internal static ShiftDetails GetShiftDetails(string shiftId)
        {

            ShiftDetails shftVals = new ShiftDetails();
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            DataTable table = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(@"select * from shiftdetails where running=1 and ShiftId = @ShiftId ", sqlConn);
                cmd.Parameters.AddWithValue("@shiftId", shiftId);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        //
                        if (!Convert.IsDBNull(rdr["ShiftId"]))
                        {
                            shftVals.shiftId = Convert.ToString(rdr["ShiftId"]);
                        }

                        if (!Convert.IsDBNull(rdr["ShiftName"]))
                        {
                            shftVals.ShiftName = Convert.ToString(rdr["ShiftName"]);
                        }

                        if (!Convert.IsDBNull(rdr["FromDay"]))
                        {
                            shftVals.FromDay = Convert.ToString(rdr["FromDay"]);
                        }
                        if (!Convert.IsDBNull(rdr["FromTime"]))
                        {
                            shftVals.FromTime = Convert.ToString(rdr["FromTime"]);
                        }

                        if (!Convert.IsDBNull(rdr["ToDay"]))
                        {
                            shftVals.ToDay = Convert.ToString(rdr["ToDay"]);
                        }

                        if (!Convert.IsDBNull(rdr["ToTime"]))
                        {
                            shftVals.ToTime = Convert.ToString(rdr["ToTime"]);
                        }
                    }

                }
                else
                {
                    shftVals = null;
                }

            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return shftVals;
        }

        internal static bool CheckForTheTimeEntry(string fromTime, string toTime)
        {
            bool isPresent = false;
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            DataTable table = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(@"s_GetCurrentShiftTime", sqlConn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", fromTime);
                //cmd.Parameters.AddWithValue("@toTime", toTime);


                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        isPresent = true;
                    }
                }

            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return isPresent;
        }

        #endregion


        public static byte[] PreviousDatesSpindleLoadTemp(string MachineId, string dateVal, string AxisNo)
        {
            SqlDataReader rdr = null;
            byte[] fileData = null;
            SqlConnection Con = ConnectionManager.GetConnection();

            try
            {

                string sqlQuery = "select SpindleData  from [dbo].[CompressData] where Machine=@Machine and Date=@Date and AxisNo =@AxisNo ";
                Con = ConnectionManager.GetConnection();
                SqlCommand cmd = new SqlCommand(sqlQuery, Con);
                cmd.Parameters.Add(new SqlParameter("@Machine", MachineId));
                cmd.Parameters.Add(new SqlParameter("@date", dateVal));
                cmd.Parameters.Add(new SqlParameter("@AxisNo", AxisNo));
                cmd.CommandTimeout = 360;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    fileData = (byte[])rdr[0];
                }
            }

            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (Con != null) Con.Close();
            }

            return fileData;
        }


        internal static DataTable GetAllPowerCalciConstant()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                //conn.Open();
                var cmd = new SqlCommand("select * from PowerCalculatorConstant", conn);//[Focas_PredictiveMaintenanceReport]
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 120;
                SqlDataReader rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                dt.AcceptChanges();
                rdr.Close();
            }

            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        internal static string GetMTB(string MachineId)
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            string MTB = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand(@"select MachineMTB from machineinformation where MachineID=@Machine", sqlConn);
                cmd.Parameters.Add(new SqlParameter("@Machine", MachineId));
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    MTB = rdr["MachineMTB"].ToString();
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return MTB;
        }

        internal static int GetSpindleAxisNumber(string MachineId)
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            int SpindleAxisNumber = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(@"select SpindleAxisNumber from machineinformation where MachineID=@Machine", sqlConn);
                cmd.Parameters.Add(new SqlParameter("@Machine", MachineId));
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    SpindleAxisNumber = Convert.ToInt32(rdr["SpindleAxisNumber"].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return SpindleAxisNumber;
        }

        internal static ObservableCollection<ProcessParamConfigModel> BindProcessParameters()
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            ObservableCollection<ProcessParamConfigModel> listProcess = new ObservableCollection<ProcessParamConfigModel>();
            ProcessParamConfigModel model = null;
            try
            {
                SqlCommand cmd = new SqlCommand(@"select ROW_NUMBER() OVER (order by IDD) as SerialNum, * from ProcessParameterMaster_MGTL", sqlConn);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        model = new ProcessParamConfigModel();
                        if (rdr["SerialNum"] != DBNull.Value)
                            model.SerialNum = Convert.ToInt32(rdr["SerialNum"].ToString());
                        if (rdr["IDD"] != DBNull.Value)
                            model.IDD = Convert.ToInt32(rdr["IDD"].ToString());
                        if (rdr["ParameterID"] != DBNull.Value)
                            model.ParameterId = Convert.ToInt32(rdr["ParameterID"].ToString());
                        model.ParameterName = rdr["ParameterName"].ToString();
                        model.MinValue = rdr["MinValue"].ToString();
                        model.MaxValue = rdr["MaxValue"].ToString();
                        model.WarningValue = rdr["WarningValue"].ToString();
                        model.RedBit = rdr["RedBit"].ToString();
                        model.RedValue = rdr["Redvalue"].ToString();
                        model.GreenBit = rdr["Greenbit"].ToString();
                        model.GreenValue = rdr["GreenValue"].ToString();
                        model.YellowBit = rdr["YellowBit"].ToString();
                        model.YellowValue = rdr["YellowValue"].ToString();
                        model.Red1Bit = rdr["Red1bit"].ToString();
                        model.Red1HValue = rdr["Red1HValue"].ToString();
                        model.Red1LValue = rdr["Red1LValue"].ToString();
                        model.Unit = rdr["Unit"].ToString();
                        model.TemplateType = rdr["TemplateType"].ToString();
                        if (!string.IsNullOrEmpty(rdr["IsVisible"].ToString()))
                        {
                            model.IsVisible = rdr["IsVisible"].ToString() == "True" || rdr["IsVisible"].ToString() == "true" ? true : false;
                        }
                        else
                            model.IsVisible = false;
                        if (rdr["SortOrder"] != DBNull.Value)
                            model.SortOrder = Convert.ToInt32(rdr["SortOrder"].ToString());
                        listProcess.Add(model);
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return listProcess;
        }

        internal static void SavePRGData(int serialNum, int ParameterId, string ParameterName, string MinValue, string MaxValue, string WarningValue, string Redvalue, string GreenValue, string YellowValue, string RedBit, string GreenBit, string YellowBit, string Red1Bit, string RedHigherValue, string RedLowerValue, string Unit, string templateType, bool isVisible, int sortOrder, out bool isUpdated)
        {
            isUpdated = false;
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            try
            {
                string query = @"IF EXISTS(Select * from ProcessParameterMaster_MGTL where IDD = @IDD)
                               BEGIN
                               UPDATE ProcessParameterMaster_MGTL set [ParameterID] = @ParameterID, [ParameterName] = @ParameterName, [MinValue] =@MinValue, [MaxValue] = @MaxValue, [WarningValue] = @WarningValue, [Redvalue] = @Redvalue, [GreenValue] = @GreenValue, [YellowValue] = @YellowValue, [RedBit]=@RedBit, [Greenbit]=@GreenBit, [YellowBit]=@YellowBit, [Red1bit]=@Red1Bit, [Red1HValue]=@RedHigherValue, [Red1LValue]=@RedLowerValue,[Unit]=@Unit, [TemplateType] = @TemplateType, [IsVisible] = @IsVisible, [SortOrder] = @SortOrder where IDD = @IDD
                               END
                               ELSE
                               BEGIN
                               Insert into ProcessParameterMaster_MGTL([ParameterID], [ParameterName], [MinValue], [MaxValue], [WarningValue], [Redvalue], [GreenValue], [YellowValue], [RedBit], [Greenbit], [YellowBit], [Red1bit], [Red1HValue], [Red1LValue] , [Unit], [TemplateType], [IsVisible], [SortOrder]) Values(@ParameterID, @ParameterName, @MinValue, @MaxValue, @WarningValue, @Redvalue, @GreenValue, @YellowValue, @RedBit, @GreenBit, @YellowBit, @Red1Bit, @RedHigherValue, @RedLowerValue, @Unit, @TemplateType, @IsVisible, @SortOrder) 
                              END";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.Parameters.Add(new SqlParameter("@IDD", serialNum));
                cmd.Parameters.Add(new SqlParameter("@ParameterID", ParameterId));
                cmd.Parameters.Add(new SqlParameter("@ParameterName", ParameterName));
                cmd.Parameters.Add(new SqlParameter("@MinValue", MinValue));
                cmd.Parameters.Add(new SqlParameter("@MaxValue", MaxValue));
                cmd.Parameters.Add(new SqlParameter("@WarningValue", WarningValue));
                cmd.Parameters.Add(new SqlParameter("@Redvalue", Redvalue));
                cmd.Parameters.Add(new SqlParameter("@GreenValue", GreenValue));
                cmd.Parameters.Add(new SqlParameter("@YellowValue", YellowValue));
                cmd.Parameters.Add(new SqlParameter("@RedBit", RedBit));
                cmd.Parameters.Add(new SqlParameter("@GreenBit", GreenBit));
                cmd.Parameters.Add(new SqlParameter("@YellowBit", YellowBit));
                cmd.Parameters.Add(new SqlParameter("@Red1Bit", Red1Bit));
                cmd.Parameters.Add(new SqlParameter("@RedHigherValue", RedHigherValue));
                cmd.Parameters.Add(new SqlParameter("@RedLowerValue", RedLowerValue));
                cmd.Parameters.Add(new SqlParameter("@Unit", Unit));
                cmd.Parameters.Add(new SqlParameter("@TemplateType", templateType));
                cmd.Parameters.Add(new SqlParameter("@IsVisible", isVisible.ToString()));
                cmd.Parameters.Add(new SqlParameter("@SortOrder", sortOrder));
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    isUpdated = true;
                else
                    isUpdated = false;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
                isUpdated = false;
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
        }


        internal static ObservableCollection<lstActiviti> ListFreqData1()
        {
            ObservableCollection<lstActiviti> graphType = new ObservableCollection<lstActiviti>();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                var cmd = new SqlCommand("select FreqID, Frequency from ActivityFreq_MGTL", conn);//[Focas_PredictiveMaintenanceReport]
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 120;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        lstActiviti value = new lstActiviti();
                        value.FreqId = rdr["FreqID"].ToString();
                        value.FreqName = rdr["Frequency"].ToString();
                        graphType.Add(value);
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return graphType;
        }

        internal static List<Tuple<string, string>> ListFreqData()
        {
            List<Tuple<string, string>> graphType = new List<Tuple<string, string>>();
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                var cmd = new SqlCommand("select FreqID, Frequency from ActivityFreq_MGTL", conn);//[Focas_PredictiveMaintenanceReport]
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 120;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Tuple<string, string> value = new Tuple<string, string>(rdr["FreqID"].ToString(), rdr["Frequency"].ToString());
                        graphType.Add(value);
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return graphType;
        }


        internal static void SaveActivityMasterData(int ActivityID, string Activity, string Frequency, out int rowCount)
        {
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            try
            {
                string query = string.Empty;

                if (ActivityID <= 0)
                {
                    query = @"if not exists(select * from ActivityMaster_MGTL where Activity = @Activity)
                                    BEGIN
                                    INSERT INTO [dbo].[ActivityMaster_MGTL] ([Activity],[FreqID]) VALUES (@Activity,@Frequency)
                                    END
                                    else
                                    BEGIN
                                    UPDATE [dbo].[ActivityMaster_MGTL] SET [Activity] = @Activity ,[FreqID] =@Frequency WHERE [ActivityID] = @ActivityID
                                    END ";
                }
                else
                {
                    query = (@"UPDATE [dbo].[ActivityMaster_MGTL] SET [Activity] = @Activity ,[FreqID] =@Frequency WHERE [ActivityID] = @ActivityID");
                }
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.Parameters.Add(new SqlParameter("@ActivityID", ActivityID));
                cmd.Parameters.Add(new SqlParameter("@Activity", Activity));
                cmd.Parameters.Add(new SqlParameter("@Frequency", Frequency));
                rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
                throw;
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
        }

        internal static ObservableCollection<ActivityInfoEntity> GetAllActivityForGrid(string Frequency)
        {
            ObservableCollection<ActivityInfoEntity> activityInfoList = new ObservableCollection<ActivityInfoEntity>();
            ActivityInfoEntity activityInfoData = null;
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(@"select row_number() OVER (ORDER BY main.ActivityID) AS SlNo, main.ActivityID,main.Activity,chield.Frequency,chield.FreqID  from ActivityMaster_MGTL main inner join ActivityFreq_MGTL chield on main.FreqID=chield.FreqID  where (chield.Frequency=@Frequency or @Frequency='')", sqlConn);
                cmd.Parameters.Add(new SqlParameter("@Frequency", Frequency));
                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        activityInfoData = new ActivityInfoEntity();
                        activityInfoData.SerialNum = Convert.ToInt32(rdr["SlNo"]);
                        activityInfoData.ActivityID = Convert.ToInt32(rdr["ActivityID"]);
                        activityInfoData.Activity = rdr["Activity"].ToString();
                        activityInfoData.Frequency = rdr["Frequency"].ToString();
                        activityInfoData.FreqID = Convert.ToInt32(rdr["FreqID"]);
                        activityInfoList.Add(activityInfoData);
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
                throw;
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return activityInfoList;
        }

        internal static void DeleteProcessParamData(int slNo, out bool isDeleted)
        {
            isDeleted = false;
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            string query = @"Delete from [dbo].[ProcessParameterMaster_MGTL] where IDD = @IDD";
            try
            {
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IDD", slNo);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {
                isDeleted = false;
                Logger.WriteErrorLog("Error in Deleting Grid Data From [dbo].[ProcessParameterMaster_MGTL] - \n" + ex.ToString());
                throw;
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
        }

        internal static void UpdateActivityInfoDetails(int activityID, string activity, string frequency, out bool isUpdated)
        {
            isUpdated = false;
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            try
            {
                string query = @"IF EXISTS(Select * from ActivityMaster_MGTL where ActivityID = @ActivityID)
                               BEGIN
                               UPDATE ActivityMaster_MGTL set [Activity] = @Activity, [FreqID] = @FreqID where ActivityID = @ActivityID
                               END
                               ELSE
                               BEGIN
                               Insert into ActivityMaster_MGTL([Activity], [FreqID]) Values(@Activity, @FreqID) 
                              END";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.Parameters.Add(new SqlParameter("@ActivityID", activityID));
                cmd.Parameters.Add(new SqlParameter("@Activity", activity));
                cmd.Parameters.Add(new SqlParameter("@FreqID", Convert.ToInt32(frequency)));
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    isUpdated = true;
                else
                    isUpdated = false;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
                isUpdated = false;
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
        }

        internal static void DeleteActivityInfoData(int activityID, out bool isDeleted)
        {
            isDeleted = false;
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            string query = @"Delete from [dbo].[ActivityMaster_MGTL] where ActivityID = @ActivityID";
            try
            {
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ActivityID", activityID);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {
                isDeleted = false;
                Logger.WriteErrorLog("Error in Deleting Grid Data From [dbo].[ActivityMaster_MGTL] - \n" + ex.ToString());
                throw;
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
        }

        internal static bool IRSListLogin(string employeeId, string password)
        {
            bool IsAuthorized = false;
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                string qry = @"select * from Employeeinformation where Employeeid=@employee and upassword = @password ";
                cmd = new SqlCommand(qry, sqlConn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@employee", employeeId);
                SqlDataReader rdr = cmd.ExecuteReader();
                bool str = rdr.HasRows;
                if (str == true)
                {
                    IsAuthorized = true;
                }
                else
                {
                    IsAuthorized = false;
                }
                rdr.Close();
                cmd.CommandTimeout = 120;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.ToString());
                IsAuthorized = false;
                throw;
            }
            finally
            {
                if (sqlConn != null) sqlConn.Close();
            }
            return IsAuthorized;
        }

        internal static DataTable GetSpindleCycleStartEndTimes(string machineID, string fromDate, string toDate, string Axis)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            try
            {
                var cmd = new SqlCommand("[Focas_GetSpindleDetails]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartTime", fromDate);
                cmd.Parameters.AddWithValue("@EndTime", toDate);
                cmd.Parameters.AddWithValue("@MachineID", HomeScreen.selectedMachine);
                cmd.Parameters.AddWithValue("@Param", (Axis == "Spindle") ? "Spindle" : "OemSpindleDetails");
                cmd.Parameters.AddWithValue("@AxisNo", (Axis == "Spindle") ? "" : Axis);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.NextResult())
                {
                    if (rdr.HasRows)
                    {
                        dt.Load(rdr);
                        dt.AcceptChanges();
                        rdr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Settings.WriteErrorMsg(ex.ToString());
            }

            finally
            {
                conn.Close();
            }
            return dt;
        }

        internal static DataTable GetActivityData(string freq, string year, string from, string to)
        {
            DataTable dt = new DataTable();
            SqlDataReader sdr = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "[S_GetActivityMasterYearlyData_MGTL]";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Frequency", freq);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@SDate", from);
                cmd.Parameters.AddWithValue("@NewDate", to);
                cmd.Parameters.AddWithValue("@Param", "View");

                cmd.CommandTimeout = 120;
                sdr = cmd.ExecuteReader();
                dt.Load(sdr);
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (conn != null) conn.Close();
            }
            return dt;
        }

        internal static bool UpdateActivityData(string freq, string year, string oldDate, string newDate, string activity)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            bool updated = false;

            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "[S_GetActivityMasterYearlyData_MGTL]";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Frequency", freq);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Activity", activity);
                cmd.Parameters.AddWithValue("@SDate", oldDate);
                cmd.Parameters.AddWithValue("@NewDate", newDate);
                cmd.Parameters.AddWithValue("@Param", "Update");
                cmd.CommandTimeout = 120;
                int ret = cmd.ExecuteNonQuery();
                if (ret > 0) updated = true;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return updated;
        }

        internal static bool? GenerateActivityData(string freq, string year, string startTime)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            bool updated = false;

            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "[S_GetActivityMasterYearlyData_MGTL]";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Frequency", freq);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@SDate", startTime);
                cmd.CommandTimeout = 120;
                int ret = cmd.ExecuteNonQuery();
                if (ret > 0) updated = true;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return updated;
        }

        #region "Bajaj IOT"
        internal static List<ParameterCycleInfo> GetCycleDetailsData(string machineID, DateTime cycleStart, DateTime cycleEnd)
        {
            List<ParameterCycleInfo> cycleDetailsList = new List<ParameterCycleInfo>();
            try
            {
                IMongoDatabase db = dbClient.GetDatabase(MongoDatabaseName);
                IMongoCollection<ParameterCycleInfo> collection = db.GetCollection<ParameterCycleInfo>("ProcessParameterTransaction_BajajIoT");
                if (collection != null && collection.CountDocuments(Builders<ParameterCycleInfo>.Filter.Empty) > 0)
                {
                    cycleDetailsList = db.GetCollection<ParameterCycleInfo>("ProcessParameterTransaction_BajajIoT").AsQueryable().Where(x => x.MachineID.Equals(machineID) && (x.ParameterID.Equals("P13") || x.ParameterID.Equals("P14"))).Where(x => x.UpdatedtimeStamp > cycleStart && x.UpdatedtimeStamp < cycleEnd).OrderBy(x => x.UpdatedtimeStamp).Take(10).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
            return cycleDetailsList;
        }

        internal static List<ParameterCycleInfo> GetCycleProfileData(string machineID, DateTime cycleStart, DateTime cycleEnd)
        {
            List<ParameterCycleInfo> cycleProfileList = new List<ParameterCycleInfo>();
            try
            {
                IMongoDatabase db = dbClient.GetDatabase(MongoDatabaseName);
                cycleProfileList = db.GetCollection<ParameterCycleInfo>("ProcessParameterTransaction_BajajIoT").AsQueryable().Where(x => x.MachineID.Equals(machineID)).Where(x => x.UpdatedtimeStamp > cycleStart && x.UpdatedtimeStamp < cycleEnd).OrderBy(x => x.UpdatedtimeStamp).ToList();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
            return cycleProfileList;
        }
        #endregion
    }
}
