using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MachineConnectApplication;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace MachineConnectOEM.SAC
{
    class DataBaseAccess_SAC
    {
        public static bool IsUpdated = false;
        internal static ObservableCollection<Frequency> GetAllFrequecies()
        {
            ObservableCollection<Frequency> frequencies = new ObservableCollection<Frequency>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            try
            {
                cmd = new SqlCommand("select * from ActivityFreq_MGTL order by SortOrder", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Frequency freq = new Frequency();
                    freq.freqID = sdr["FreqID"].ToString();
                    freq.frequency = sdr["Frequency"].ToString();
                    freq.freqValue = sdr["Freqvalue"].ToString();
                    freq.freqType = sdr["Freqtype"].ToString();
                    frequencies.Add(freq);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error - \n " + ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (con != null) con.Close();
            }
            return frequencies;
        }

        internal static DataTable GetActivityInfo_MGTL(string machineID, string frequency, string dateTime, string param, string frequencyValue)
        {
            DataTable dt = new DataTable();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            try
            {
                //cmd = new SqlCommand("[dbo].[s_ViewActivityInfo_MGTL_old]", con);
                cmd = new SqlCommand("[dbo].[s_ViewActivityInfo_MGTL]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@frequency", frequency);
                cmd.Parameters.AddWithValue("@machineid", machineID);
                cmd.Parameters.AddWithValue("@Starttime", dateTime);
                cmd.Parameters.AddWithValue("@Screen", param);
                cmd.Parameters.AddWithValue("@FrequencyValue", frequencyValue);
                sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                dt.AcceptChanges();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error - \n " + ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (con != null) con.Close();
            }
            return GetStringCellsDataTable(dt); 
            // g: was causing "A TwoWay or OneWayToSource binding cannot work on the read-only property" 
            //    exception when the exntries were having datetime and null values
        }

        internal static DataTable GetStringCellsDataTable(DataTable dt)
        {
            DataTable dttmp = new DataTable();
            foreach (DataColumn col in dt.Columns)
            {
                dttmp.Columns.Add(col.ColumnName, typeof(string));
            }
            foreach(DataRow row in dt.Rows)
            {
                DataRow rowtmp = dttmp.NewRow();
                foreach (DataColumn col in dt.Columns)
                {
                    rowtmp[col.ColumnName] = row[col.ColumnName].ToString();
                }
                dttmp.Rows.Add(rowtmp);
            }
            return dttmp;
        }

        internal static ObservableCollection<DTO> GetProcessParamDashboardData(string selectedMachine)
        {
            ObservableCollection<DTO> processParamDasboardDataList = new ObservableCollection<DTO>();
            DTO processParamDasboardData = null;
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            try
            {
                cmd = new SqlCommand("[S_GetProcessParameter_MGTL]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MachineId", selectedMachine);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        processParamDasboardData = new DTO();
                        processParamDasboardData.ParameterId = Convert.ToInt32(sdr["ParameterID"]);
                        processParamDasboardData.ParameterName = sdr["ParameterName"].ToString();
                        processParamDasboardData.MinValue = sdr["MinValue"].ToString();
                        processParamDasboardData.MaxValue = sdr["MaxValue"].ToString();
                        processParamDasboardData.Unit = sdr["Unit"].ToString();
                        if (sdr["TemplateType"].ToString() == "High/Low Limits")
                            processParamDasboardData.TemplateType = 1;
                        else if (sdr["TemplateType"].ToString() == "Text")
                            processParamDasboardData.TemplateType = 2;
                        else
                            processParamDasboardData.TemplateType = 2;
                        processParamDasboardData.BackgroundColor = !string.IsNullOrEmpty(sdr["ParameterColor"].ToString()) ? sdr["ParameterColor"].ToString() : "#FFF000";
                        processParamDasboardData.ParameterValue = sdr["ParameterBitValue"].ToString();
                        processParamDasboardDataList.Add(processParamDasboardData);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error - \n " + ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (con != null) con.Close();
            }
            return processParamDasboardDataList;
        }

        internal static void UpdateIRSchedule(string activityID, string currentFreq, string activityTS, string activityDoneTS, object selectedMachinem, out bool isUpdated)
        {
            isUpdated = false;
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            try
            {
                string query = @"IF EXISTS(Select * from ActivityTransaction_MGTL where ActivityTS = @ActivityTS and ActivityID = @ActivityID)
                               BEGIN
                               select * from ActivityTransaction_MGTL where ActivityTS = @ActivityTS
                               END
                               ELSE
                               BEGIN
                               Insert into ActivityTransaction_MGTL([ActivityID], [Frequency], [ActivityTS], [ActivityDoneTS], [Machineid]) Values(@ActivityID, @Frequency, @ActivityTS, @ActivityDoneTS, @Machineid) 
                              END";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.Parameters.Add(new SqlParameter("@ActivityID", activityID));
                cmd.Parameters.Add(new SqlParameter("@Frequency", currentFreq));
                cmd.Parameters.Add(new SqlParameter("@ActivityTS", activityTS));
                cmd.Parameters.Add(new SqlParameter("@ActivityDoneTS", activityDoneTS));
                cmd.Parameters.Add(new SqlParameter("@Machineid", selectedMachinem));
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    IsUpdated = true;
                isUpdated = IsUpdated;
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

        internal static DataTable GetPreventiveMaintenanceExportData(string machineId, string frequency, string startTime, string endTime, int freqValue)
        {
            DataTable dtPrevMaintenance = new DataTable();
            SqlConnection sqlConn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand("s_ExportActivityInfo_MGTL", sqlConn);
            try
            {
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MachineID", SqlDbType.NVarChar).Value = machineId;
                cmd.Parameters.AddWithValue("@frequency", frequency);
                cmd.Parameters.Add("@Starttime", SqlDbType.DateTime).Value = startTime;
                cmd.Parameters.Add("@Endtime", SqlDbType.DateTime).Value = endTime;
                cmd.Parameters.AddWithValue("@FrequencyValue", freqValue);
                SqlDataReader sdr = cmd.ExecuteReader();
                dtPrevMaintenance.Load(sdr);
                dtPrevMaintenance.AcceptChanges();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error - \n " + ex.ToString());
            }
            finally
            {
                //if (sdr != null) sdr.Close();
                if (sqlConn != null) sqlConn.Close();
            }
            return dtPrevMaintenance;
        }

        internal static NotificationDetails GetActivityNotifications(string selectedMachine, string currDate, string freq)
        {
            ObservableCollection<NotificationData> notificationDataListWarning = new ObservableCollection<NotificationData>();
            ObservableCollection<NotificationData> notificationDataListPending = new ObservableCollection<NotificationData>();
            NotificationData notificationDetails = null;
            NotificationDetails notificationAllDetails = new NotificationDetails();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            try
            {
                cmd = new SqlCommand("[S_GetActivityNotification_MGTL]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MachineId", selectedMachine);
                cmd.Parameters.AddWithValue("@DateTime", currDate);
                cmd.Parameters.AddWithValue("@Param", freq);

                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    notificationDetails = new NotificationData();
                    notificationDetails.MachineID = sdr["MachineID"].ToString();
                    notificationDetails.Activity = sdr["Activity"].ToString();
                    notificationDetails.Frequency = sdr["Frequency"].ToString();
                    notificationDetails.DueDate = Convert.ToDateTime(sdr["ActivityPendingTS"]).ToString("dd-MM-yyyy HH:mm:ss");
                    notificationDetails.NotificationDataTitle = "(" + notificationDetails.Frequency + ")  " + notificationDetails.DueDate;
                    notificationDataListPending.Add(notificationDetails);
                }
                if (sdr.NextResult())
                {
                    while (sdr.Read())
                    {
                        notificationDetails = new NotificationData();
                        notificationDetails.MachineID = sdr["MachineID"].ToString();
                        notificationDetails.Activity = sdr["Activity"].ToString();
                        notificationDetails.Frequency = sdr["Frequency"].ToString();
                        notificationDetails.DueDate = Convert.ToDateTime(sdr["ActivityWarningTS"]).ToString("dd-MM-yyyy HH:mm:ss");
                        notificationDetails.NotificationDataTitle = "(" + notificationDetails.Frequency + ")  " + notificationDetails.DueDate;
                        notificationDataListWarning.Add(notificationDetails);
                    }

                }
                sdr.Close();
                notificationAllDetails.WarningData = notificationDataListWarning;
                notificationAllDetails.PendingData = notificationDataListPending;
            }


            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error - \n " + ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (con != null) con.Close();
            }
            return notificationAllDetails;
        }
        internal static NotificationDetails GetActivityNotificationsOld(string selectedMachine, string currDate, string pendingDate, ObservableCollection<Frequency> freqList)
        {
            ObservableCollection<NotificationData> notificationDataListWarning = new ObservableCollection<NotificationData>();
            ObservableCollection<NotificationData> notificationDataListPending = new ObservableCollection<NotificationData>();
            NotificationData notificationDetails = null;
            NotificationDetails notificationAllDetails = new NotificationDetails();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            try
            {
                cmd = new SqlCommand("[S_GetActivityNotification_MGTL]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MachineId", selectedMachine);
                cmd.Parameters.AddWithValue("@DateTime", currDate);
                cmd.Parameters.AddWithValue("@Param", "");
                //cmd.Parameters.AddWithValue("@StartDate", pendingDate);
                //cmd.Parameters.AddWithValue("@Frequency", "Daily");
                
                foreach (Frequency frequ in freqList)
                {
                    cmd.Parameters["@Frequency"].Value = frequ.frequency;
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            notificationDetails = new NotificationData();
                            notificationDetails.MachineID = sdr["MachineID"].ToString();
                            notificationDetails.Activity = sdr["Activity"].ToString();
                            notificationDetails.Frequency = sdr["Frequency"].ToString();
                            notificationDetails.DueDate = Convert.ToDateTime(sdr["ActivityPendingTS"]).ToString("dd-MM-yyyy");
                            notificationDetails.NotificationDataTitle = "(" + notificationDetails.Frequency + ")  " + notificationDetails.DueDate;
                            notificationDataListPending.Add(notificationDetails);
                        }
                        if (sdr.NextResult())
                        {
                            if (sdr.HasRows)
                            {
                                while (sdr.Read())
                                {
                                    notificationDetails = new NotificationData();
                                    notificationDetails.MachineID = sdr["MachineID"].ToString();
                                    notificationDetails.Activity = sdr["Activity"].ToString();
                                    notificationDetails.Frequency = sdr["Frequency"].ToString();
                                    notificationDetails.DueDate = Convert.ToDateTime(sdr["ActivityWarningTS"]).ToString("dd-MM-yyyy");
                                    notificationDetails.NotificationDataTitle = "(" + notificationDetails.Frequency + ")  " + notificationDetails.DueDate;
                                    notificationDataListWarning.Add(notificationDetails);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (sdr.NextResult())
                        {
                            if (sdr.HasRows)
                            {
                                while (sdr.Read())
                                {
                                    notificationDetails = new NotificationData();
                                    notificationDetails.MachineID = sdr["MachineID"].ToString();
                                    notificationDetails.Activity = sdr["Activity"].ToString();
                                    notificationDetails.Frequency = sdr["Frequency"].ToString();
                                    notificationDetails.DueDate = Convert.ToDateTime(sdr["ActivityWarningTS"]).ToString("dd-MM-yyyy");
                                    notificationDetails.NotificationDataTitle = "(" + notificationDetails.Frequency + ")  " + notificationDetails.DueDate;
                                    notificationDataListWarning.Add(notificationDetails);
                                }
                            }
                        }
                    }
                    sdr.Close();
                }
                notificationAllDetails.WarningData = notificationDataListWarning;
                notificationAllDetails.PendingData = notificationDataListPending;
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error - \n " + ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (con != null) con.Close();
            }
            return notificationAllDetails;
        }

        internal static ObservableCollection<NotificationData> GetAllPendingActivities(string selectedMachine, string now, string freq, string pendingActivityStartDate)
        {
            ObservableCollection<NotificationData> allPendingActivityList = new ObservableCollection<NotificationData>();
            NotificationData pendingActivityData = null;
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            try
            {
                cmd = new SqlCommand("[S_GetActivityNotification_MGTL]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MachineId", selectedMachine);
                cmd.Parameters.AddWithValue("@DateTime", now);
                //cmd.Parameters.AddWithValue("@Frequency", freq);
                cmd.Parameters.AddWithValue("@Param", "All Pending");
                //cmd.Parameters.AddWithValue("@StartDate", pendingActivityStartDate);
                sdr = cmd.ExecuteReader();
                //sdr.NextResult();
                //sdr.NextResult();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        pendingActivityData = new NotificationData();
                        pendingActivityData.MachineID = sdr["MachineID"].ToString();
                        pendingActivityData.Activity = sdr["Activity"].ToString();
                        pendingActivityData.Frequency = sdr["Frequency"].ToString();
                        pendingActivityData.DueDate = Convert.ToDateTime(sdr["ActivityPendingTS"]).ToString("dd-MM-yyyy");
                        pendingActivityData.NotificationDataTitle = "(" + pendingActivityData.Frequency + ")  " + pendingActivityData.DueDate;
                        allPendingActivityList.Add(pendingActivityData);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Error - \n " + ex.ToString());
            }
            finally
            {
                if (sdr != null) sdr.Close();
                if (con != null) con.Close();
            }
            return allPendingActivityList;
        }
    }
}
