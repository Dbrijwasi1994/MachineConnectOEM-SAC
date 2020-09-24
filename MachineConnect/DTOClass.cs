using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MachineConnectApplication
{
  
    // Added

    public class getGridValues
    {
        public string machineId { get; set; }
        public string programNumber { get; set; }
        public string toolNumber { get; set; }
        public double NominalDimension { get; set; }
        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; }
        public string wearOffsetNumber { get; set; }
        public float wearOffsetValueOriginal { get; set; }
        public float newWearOffsetValue { get; set; }
        public float MeasuredDimension { get; set; }
        public DateTime measuredTime { get; set; }
    }

    // Addded

    public class PieChartVals
    {
        public double PowerOnTime { get; set; }
        public double CuttingTime { get; set; }
        public double OperatingTime { get; set; }    
    }

    // Master Tabel vals

    public class MasterTabelVals
    {
        public string machineId { get; set; }
        public string programNumber { get; set; }
        public string toolNumber { get; set; }
        public string wearOffsetNumber { get; set; }
        public float DefaultwearOffsetValue { get; set; }
        public float DimensionIdVal { get; set; }
        public double NominalDimension { get; set; }
        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; } 
        public float gaugeVal { get; set; }    
    }

    public class ValuesForDoWork
    {
        public string MachineId { get; set; }
        public string ProgramNo { get; set; }
        public string ToolNo { get; set; }
        public string OffsetNo { get; set; }
        public DateTime Date { get; set; }
        public string Plant { get; set; }        
        public string shift { get; set; }

        public object ProductionAnalysisSummaryDataObj { get; set; }

        public int calculatedHeight { get; set; }
        public int calculatedWidth { get; set; }
    }

    public class EmployeeDetails
    {
        public string EmpId { get; set; }
        public string InterfaceId { get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public string Qualification { get; set; }
        public string Plants { get; set; }
        public string isAdmin { get; set; }
        public string Password { get; set; }   
    }

    public class TimeAnalysisSummary
    {
        public double PowerOnTime { get; set; }
        public double CuttingTime { get; set; } // grinding
        public double DressingTime { get; set; } // grinding
        public double TotalTime { get; set; }
        public double OperatingWithoutCutting { get; set; }
        public double NonOperatingTime { get; set; }
        public double OperatingTime { get; set; }
        public double PowerOffTime { get; set; }  
    }

    public static class RestorePlantMachineData
    {
        public static string MachineId { get; set; }
        public static string PlantId { get; set; }
        public static string DateVal { get; set; }
        //Added
        public static string ShiftVal { get; set; }
        public static Form MdiForm { get; set; }
    }

    // Added
    public class ShiftDetails
    {
        public string shiftId { get; set; }
        public string ShiftName { get; set; }
        public string FromDay { get; set; }
        public string FromTime { get; set; }
        public string ToDay { get; set; }
        public string ToTime { get; set; }
    }

    public class ProductionAnalysisForExcelSummary
    {
        public double PowerOnTime { get; set; }
        public double CuttingTime { get; set; }  
        public double OperatingTime { get; set; }

        public double TotalTime { get; set; }
        public double OperatingWithoutCutting { get; set; }
        public double NonOperatingTime { get; set; }     
        public double PowerOffTime { get; set; }

        public double PartsCount { get; set; }
       // public double PowerOffTime { get; set; }  
    }

    public class partCountValues
    {
        public string programNo { get; set; }
        public string partCount { get; set; }       
    }

    public class DragDropDTO
    {
        public string programNo { get; set; }
        public string Comment { get; set; }
    }

    public class ChartArrays
    {
        public List<string> startTime { get; set; }
        public List<double> powerOnTime { get; set; }
        public List<double> OperatingTime { get; set; }
        public List<double> cuttingTime { get; set; } // grinding
        public List<double> dressingTime { get; set; }
        public List<double> partProgramCount { get; set; }
        public List<double> TotalHours { get; set; }

        public ChartArrays()
        {
            startTime = new List<string>();
            powerOnTime = new List<double>();
            OperatingTime = new List<double>();
            cuttingTime = new List<double>(); // grinding
            dressingTime = new List<double>();
            partProgramCount = new List<double>();
            TotalHours = new List<double>();
        }
    }
          

     public class PartCountArrays
    {
        public List<string> dateTime { get; set; }
        public List<string> ProgNo1 { get; set; }
        public List<string> ProgNo2 { get; set; }
        public List<string> ProgNo3 { get; set; }

        public List<double> partCount1 { get; set; }
        public List<double> partCount2 { get; set; }
        public List<double> partCount3 { get; set; }

        public PartCountArrays()
        {
            dateTime = new List<string>();

            ProgNo1 = new List<string>();
            ProgNo2 = new List<string>();
            ProgNo3 = new List<string>();

            partCount1 = new List<double>();
            partCount2 = new List<double>();
            partCount3 = new List<double>();
        }
    }

     public class StepLineArray
     { 
         public List<DateTime> DownStartTime { get; set; }
         public List<DateTime> ProductionStartTime { get; set; }
         public List<DateTime> noDataStartTime { get; set; }

         public List<DateTime> DownEndTime { get; set; }
         public List<DateTime> ProductionEndTime { get; set; }
         public List<DateTime> noDataEndTime { get; set; }

         public StepLineArray()
         {
             DownStartTime = new List<DateTime>();
             ProductionStartTime = new List<DateTime>();
             noDataStartTime = new List<DateTime>();

             DownEndTime = new List<DateTime>();
             ProductionEndTime = new List<DateTime>();
             noDataEndTime = new List<DateTime>(); 
         }
     }

     public class ShiftStartEndVals
     {
         public string StartTime { get; set; }
         public string EndTime { get; set; }
     }

     public class GenParameter
     {
         public String MachineID { get; set; }
         public String PlantID { get; set; }
         public String FromDate { get; set; }
         public String ToDate { get; set; }
         public String ShiftName { get; set; }
     }

     public class PowerCaliMachineInfo
     {
         public string SpindleType { get; set; }
         public double PowerRating { get; set; }
         public double ContinuousPowerRating { get; set; }
         public double TorqueRange { get; set; }
         public double BaseSpeed1 { get; set; }
         public double BaseSpeed2 { get; set; }
         public double BaseSpeedSrtTerm { get; set; }
         public double MotorPulley { get; set; }
         public double SpindlePulley { get; set; }
     }

     public class PowerCaliMachineInfoGreen
     {
         public double ShortTermTorqueRange { get; set; }
         public double CuttingVal { get; set; }
         public double PulleyRatio { get; set; }
         public double BaseSpeedVal1 { get; set; }
         public double BaseSpeedVal2 { get; set; }
     }

     public class PowerCaliCuttingConditions
     {
         public float SpindleSpped { get; set; }
         public float ComponentDia { get; set; }
         public float ComponentFeed { get; set; }
         public float ComponentDept { get; set; }
         public float MaterialUsed { get; set; }
         public float ToolRakeAngle { get; set; }
         public float ToolApproachAngle { get; set; }
     }

     public class CalculatedValues
     {
         public double SpecificCuttingForce { get; set; }
         public double PowerRequired { get; set; }
         public double ChipThickness { get; set; }
         public double ContPowerRating { get; set; }
         public double ShortTermPowerRating { get; set; }
         public double SpecificCuttingForceForremoving { get; set; }
         public double curveRaise { get; set; }
     }

    //Added

     public class MachineInformationVals
     {
         public string MachineID { get; set; }
         public string PortNo { get; set; }
         public string IPAddress { get; set; }
         public string Interfaceid { get; set; }
         public string Description { get; set; }
         public string MTB { get; set; }
         public string Model { get; set; }
         public string MachineType { get; set; }
         public bool EthernetEnabled { get; set; }
         public bool ProgramFoldersEnabled { get; set; }

         //super Admin
         public string PartCountByMacro { get; set; }
         public string SpindleAxisNumber { get; set; }
     }


     public class ServiceSettingsVals
     {
         public string live { get; set; }
         public string spindle { get; set; }
         public string alarm { get; set; }
     }

     public class ApplicationSettingsVals
     {
         public string AutoRefreshInterval { get; set; }
         public string GraphTypeVal { get; set; }
         public string AlarmsFolderPath { get; set; }
         public string StoppagesThreshold { get; set; }
         public string ProgramsPath { get; set; }
         
     }

     public class PowerCalculatorVals
     {
         public string TangentialForce { get; set; }
         public string RPM { get; set; }
         public string Torque { get; set; }
         public string PowerRequired { get; set; }

         public string PAC { get; set; }
         public string PAS { get; set; }
         public string TAC { get; set; }
         public string TAS { get; set; }

         public string ContPower { get; set; }
         public string StMinPower { get; set; }
         public string ContTorque { get; set; }
         public string StMinTorque { get; set; }

         public string BaseSpeedOnMotor { get; set; }
         public string BaseSpeedOnSpindle { get; set; }
         public string StMin { get; set; }
     }

}
