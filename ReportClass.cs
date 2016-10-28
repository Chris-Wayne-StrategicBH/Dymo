using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;

namespace DPGPP
{

   static class Constants
   {
      public const int OP__DOCID_INDEX = 0; // ?
      public const int ADMISSIONKEY_INDEX = 0; // ?
      public const int STARTDATE_INDEX = 1; // 
      public const int ENDDATE_INDEX = 2; // 
      public const int FULLNAME_INDEX = 2; // ?
      public const int NOT_USED = 9999; // indicates not used
      public const PrinterDuplex DEFAULT_PRINTER_DUPLEX = PrinterDuplex.Simplex;
      public const string DEFAULT_PRINTER_NAME = @"\\srvcosad1\Medical Records Printer";
      public const string DEFAULT_DB_SERVER_NAME = "srvcostier";
      public const string DEFAULT_DB_NAME = "TIER_PVBH";
      public const string DEFAULT_DB_USER = "TIER";
      public const string DEFAULT_DB_PASSWORD = "38$bH125";

      // Set this path to your reports
      //public const string REPORT_BASE_PATH = @"C:\tier-work\c#\test\test1\Reports";
      public const string REPORT_BASE_PATH = @"\\srvcosad1\share\DPGPP\Reports";

   }

   public static class Globals
   {
      public static PrinterDuplex duplex = Constants.DEFAULT_PRINTER_DUPLEX;
      public static string name = Constants.DEFAULT_PRINTER_NAME;
      public static int mClientKey;
      public static int mAdmissionKey;
      public static DateTime mStartDate, mEndDate;
   }

   // using the enum name itself to identify nodes in treeview
   // using the description to provide the report file name
   public enum CRYSTALREPORTS
   {
      ROOT,
      [Description("SD - Face Sheet.rpt")]
      FACESHEET,
      [Description("SD - Discharge Summary 20121011.rpt")]
      DISCHARGE_SUMMARY,
      [Description("SD - Physician Discharge Summary 20120813.rpt")]
      PHYSICIAN_DISCHARGE_SUMMARY,
      [Description("SD - Discharge Aftercare Plan 20141121.rpt")]
      DISCHARGE_AFTERCARE_PLAN,
      [Description("SD - History and Physical 20130206.rpt")]
      HISTORY_PHYSICAL,
      [Description("SD - Psychiatric Evaluation 20130201.rpt")]
      PSYCHIATRIC_EVALUATION,
      [Description("SD - Comprehensive PsychoSocial Assesment 20120520.rpt")]
      COMPREHENSIVE_PSYCHOSOCIAL,
      [Description("SD - Nursing Assessment 20120606.rpt")]
      NURSING_ASSESSMENT,
      [Description("SD - NursingEvaluation.rpt")]
      NURSING_EVALUATION,
      [Description("SD - Initial Contact Note 20120512.rpt")]
      INITIAL_CONTACT_NOTE,
      [Description("SD - Psychiatric Progress Note 20150505PROD.rpt")]
      PSYCHIATRIC_PROGRESS_NOTE,
      [Description("SD - General Note 20160802.rpt")]
      GENERAL_NOTE,
      [Description("SD - Soap Note.rpt")]
      SOAP_NOTE,
      [Description("SD - Contact Note 20150506.rpt")]
      CONTACT_NOTE,
      [Description("SD - General Order 20120524.rpt")]
      GENERAL_ORDER,
      [Description("SD - Medication History 20120607.rpt")]
      MEDICATION_ORDERS_HISTORY,
      [Description("SD - Administered Meds History 20130410.rpt")]
      ADMINISTERED_MEDICATION_HISTORY,
      [Description("SD - Psychosocial Plan 20120521.rpt")]
      UPDATED_COMPREHENSIVE_ASSESSMENT,
      [Description("SD - Psychosocial Plan 20120521.rpt")]
      EVALUATION_OF_RISK
   }

   public enum CRYSTALREPORTTYPES
   {
      [Description("OP__DOC_ID Only")]
      OP__DOCID,
      [Description("OP__DOCID and AdmissionKey = 0")]
      OP__DOCID_ADMISSIONKEY0,
      [Description("ClientKey and AdmissionKey")]
      CLIENTKEY_ADMISSIONKEY,
      [Description("Start Date and End Date")]
      STARTDATE_ENDDATE,
   }

   // using reflection to get description values of enums
   public static class ReportTranslation
   {
      public static string GetFileName(Enum value)
      {
         FieldInfo fi = value.GetType().GetField(value.ToString());

         DescriptionAttribute[] attributes =
             (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

         if (attributes != null && attributes.Length > 0)
            return attributes[0].Description;
         else
            return value.ToString();
      }
      public static string GetID(Enum value)
      {
         return value.ToString();
      }
   }

   public abstract class CrystalParamsGeneric
   {
      private string mName;
      public string name
      {
         get { return this.mName; }
         set { this.mName = value; }
      }
      public abstract object GetParamValue();
   
   }

   public class CrystalParams<T> : CrystalParamsGeneric
   {
      public T paramval {get; set;}
      public override object GetParamValue()
      {
         return(paramval);
      }

   }

   public class CrystalParamsInt : CrystalParams<int>
   {
   }

   public class CrystalParamsDate : CrystalParams<DateTime>
   {  
   }

   public abstract class ReportClass
   {
      string mPath;
      public string Path
      {
         get { return this.mPath; }
         set { this.mPath = value; }
      }
      public List<CrystalParamsGeneric> param = new List<CrystalParamsGeneric>();
      public ReportClass(string inPath)
      {
         mPath = inPath;
      }
      abstract public void PrintCrystalReport();
   }

   public class GeneralRpt : ReportClass
   {
      //This type of report requires a ClientKey and an AdmissionKey for parameters
      private GeneralRpt(string inPath, int clientKey, int admissionKey)
         : base(inPath)
      {
         CrystalParamsInt cri1 = new CrystalParamsInt();
         cri1.name = "ClientKey";
         cri1.paramval = clientKey;
         param.Add(cri1);
         CrystalParamsInt cri2 = new CrystalParamsInt();
         cri2.name = "AdmissionKey";
         cri2.paramval = admissionKey;
         param.Add(cri2);
      }

      //This type of report requires an AdmissionKey, startdate and enddate for parameters
      private GeneralRpt(string inPath, int admissionKey, DateTime startDate, DateTime endDate)
         : base(inPath)
      {
         CrystalParamsInt cri = new CrystalParamsInt();
         cri.name = "AdmissionKey";
         cri.paramval = admissionKey;
         param.Add(cri);
         CrystalParamsDate crd1 = new CrystalParamsDate();
         crd1.name = "StartDate";
         crd1.paramval = startDate;
         param.Add(crd1);
         CrystalParamsDate crd2 = new CrystalParamsDate();
         crd2.name = "EndDate";
         crd2.paramval = endDate;
         param.Add(crd2);
      }
      //This type of report requires an OP__DOCID
      private GeneralRpt(string inPath, int OP__DOCID)
         : base(inPath)
      {
         CrystalParamsInt cri = new CrystalParamsInt();
         cri.name = "OP__DOCID";
         cri.paramval = OP__DOCID;
         param.Add(cri);
      }
      //This type of report requires an OP__DOCID and AdmissionKey = 0
      private GeneralRpt(string inPath, int OP__DOCID, string dummy = "WTF")
         : base(inPath)
      {
         CrystalParamsInt cri1 = new CrystalParamsInt();
         cri1.name = "OP__DOCID";
         cri1.paramval = OP__DOCID;
         param.Add(cri1);
         CrystalParamsInt cri2 = new CrystalParamsInt();
         cri2.name = "AdmissionKey";
         cri2.paramval = 0;
         param.Add(cri2);
      }
      public override void PrintCrystalReport()
      {
         Tables CrTables;
         ReportDocument cryRpt = new ReportDocument();
         ConnectionInfo crConnectionInfo = new ConnectionInfo();
         TableLogOnInfo crtablelogoninfo = new TableLogOnInfo();

         crConnectionInfo.ServerName = Constants.DEFAULT_DB_SERVER_NAME;
         crConnectionInfo.DatabaseName = Constants.DEFAULT_DB_NAME;
         crConnectionInfo.UserID = Constants.DEFAULT_DB_USER;
         crConnectionInfo.Password = Constants.DEFAULT_DB_PASSWORD;

         cryRpt.Load(Path);
         foreach (var crystalParameter in param)
         {
            cryRpt.SetParameterValue(crystalParameter.name, crystalParameter.GetParamValue());
         }

         CrTables = cryRpt.Database.Tables;

         foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
         {
            crtablelogoninfo = CrTable.LogOnInfo;
            crtablelogoninfo.ConnectionInfo = crConnectionInfo;
            CrTable.ApplyLogOnInfo(crtablelogoninfo);
         }

         // Select the printer and print
         cryRpt.PrintOptions.PrinterDuplex = Globals.duplex;
         cryRpt.PrintOptions.PrinterName = Globals.name;
         cryRpt.PrintToPrinter(1, false, 0, 0);

         cryRpt.Close();
         cryRpt.Clone();
         cryRpt.Dispose();
         cryRpt = null;
         GC.Collect();
         GC.WaitForPendingFinalizers();

      }
      public static GeneralRpt CreatePrintObject(string inPath, CRYSTALREPORTS reportType, int OP__DOCID)
      {
         switch(reportType)
         {
            case CRYSTALREPORTS.FACESHEET:
               return new GeneralRpt(inPath, Globals.mClientKey, Globals.mAdmissionKey);
            case CRYSTALREPORTS.COMPREHENSIVE_PSYCHOSOCIAL:
            case CRYSTALREPORTS.DISCHARGE_AFTERCARE_PLAN:
            case CRYSTALREPORTS.DISCHARGE_SUMMARY:
            case CRYSTALREPORTS.HISTORY_PHYSICAL:
            case CRYSTALREPORTS.INITIAL_CONTACT_NOTE:
            case CRYSTALREPORTS.NURSING_ASSESSMENT:
            case CRYSTALREPORTS.PHYSICIAN_DISCHARGE_SUMMARY:
            case CRYSTALREPORTS.PSYCHIATRIC_EVALUATION:
            case CRYSTALREPORTS.MEDICATION_ORDERS_HISTORY:
            case CRYSTALREPORTS.UPDATED_COMPREHENSIVE_ASSESSMENT:
               return new GeneralRpt(inPath, OP__DOCID);
            case CRYSTALREPORTS.NURSING_EVALUATION:
            case CRYSTALREPORTS.PSYCHIATRIC_PROGRESS_NOTE:
            case CRYSTALREPORTS.GENERAL_NOTE:
            case CRYSTALREPORTS.SOAP_NOTE:
            case CRYSTALREPORTS.CONTACT_NOTE:
            case CRYSTALREPORTS.GENERAL_ORDER:
               return new GeneralRpt(inPath, OP__DOCID, "WTF");
            case CRYSTALREPORTS.ADMINISTERED_MEDICATION_HISTORY:
               return new GeneralRpt(inPath, Globals.mAdmissionKey, Globals.mStartDate, Globals.mEndDate);
            default:
               return null;

         }
      }
   }
}
