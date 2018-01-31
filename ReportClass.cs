#define HARDWARE

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
using System.Windows.Forms;
using System.Threading;

namespace DPGPP
{

   static class Constants
   {
      public const int PARENT_NODE = 0;
      public const int RETURN_SUCCESS = 0;
      public const int RETURN_FILE_ACCESS_ERROR = -1;
      public const int STARTING = 1;
      public const int CHILD_NODE = 1;
      public const int OP__DOCID_INDEX = 0;
      public const int ADMISSIONKEY_INDEX = 0;
      public const int STARTDATE_INDEX = 1; 
      public const int ENDDATE_INDEX = 2; 
      public const int FULLNAME_INDEX = 2;
      public const int NOT_USED = 9999;
      public const PrinterDuplex DEFAULT_PRINTER_DUPLEX = PrinterDuplex.Simplex;
      public const string DEFAULT_PRINTER_NAME = "\\DEFAULT";
      public const string DEFAULT_DB_SERVER_NAME = "srvcostier";
      public const string DEFAULT_DB_NAME = "TIER_PVBH";
      public const string DEFAULT_DB_USER = "TIER";
      public const string DEFAULT_DB_PASSWORD = "38$bH125";
      public const string REPORT_BASE_PATH = @"\\srvcosad1\Share\DPGPP-2.0\Reports";

   }

   public static class Globals
   {
      public static PrinterDuplex duplex = Constants.DEFAULT_PRINTER_DUPLEX;
      public static string Printername = Constants.DEFAULT_PRINTER_NAME;
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
      [Description("SD - History and Physical.rpt")]
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
      [Description("SD - Administered Meds History.rpt")]
      ADMINISTERED_MEDICATION_HISTORY,
      [Description("SD - Psychosocial Plan 20120521.rpt")]
      UPDATED_COMPREHENSIVE_ASSESSMENT,
      [Description("SD - Initial Assesment of Risk 20120806.rpt")]
      EVALUATION_OF_RISK,
      [Description("SD - Fall Risk  Assessment 20160609.rpt")]
      FALL_RISK_EVALUATION,
      [Description("SD - Master Treatment Plan-08-18-15.rpt")]
      MASTER_TREATMENT_PLAN,
      [Description("Cognitive Assessment.rpt")]
      COGNITIVE_ASSESSMENT,
      [Description("SD - Body Assessment Checklist 20120606.rpt")]
      BODY_ASSESSMENT_CHECKLIST,
      [Description("SD - Allergies.rpt")]
      ALLERGIES,
      [Description("SD - Pain Evaluation 20160414.rpt")]
      PAIN_EVALUATION,
      [Description("SD - Comprehensive Mental  Status 20120519.rpt")]
      COMPREHENSIVE_MENTAL_STATUS,
      [Description("SD - Mini Mental Status Exam 20120902.rpt")]
      MINI_MENTAL_STATUS,
      [Description("FollowupAppointment.rpt")]
      FOLLOWUP_APPOINTMENT
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
      abstract public int PrintCrystalReport(ref string errorString);
   }

   public class GeneralRpt : ReportClass
   {
      //This type of report requires 2 integer parameters
      private GeneralRpt(string inPath, string paramstring1, int param1, string paramstring2, int param2)
         : base(inPath)
      {
         CrystalParamsInt cri1 = new CrystalParamsInt();
         cri1.name = paramstring1;
         cri1.paramval = param1;
         param.Add(cri1);
         CrystalParamsInt cri2 = new CrystalParamsInt();
         cri2.name = paramstring2;
         cri2.paramval = param2;
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
      //This type of report requires 1 integer parameter
      private GeneralRpt(string inPath, string paramstring, int paramint)
         : base(inPath)
      {
         CrystalParamsInt cri = new CrystalParamsInt();
         cri.name = paramstring;
         cri.paramval = paramint;
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
      public override int PrintCrystalReport(ref string errorString)
      {
         int returnCode = Constants.RETURN_SUCCESS;
         Tables CrTables;
         ReportDocument cryRpt = new ReportDocument();
         ConnectionInfo crConnectionInfo = new ConnectionInfo();
         TableLogOnInfo crtablelogoninfo = new TableLogOnInfo();

         crConnectionInfo.ServerName = Constants.DEFAULT_DB_SERVER_NAME;
         crConnectionInfo.DatabaseName = Constants.DEFAULT_DB_NAME;
         crConnectionInfo.UserID = Constants.DEFAULT_DB_USER;
         crConnectionInfo.Password = Constants.DEFAULT_DB_PASSWORD;

         errorString = "";

         try
         {
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
            cryRpt.PrintOptions.PrinterName = Globals.Printername;

#if(HARDWARE)
            cryRpt.PrintToPrinter(1, false, 0, 0);
#else
            for (int i = 0; i < 100; i++)
            {
               Thread.Sleep(100);
            }
#endif

            cryRpt.Close();
            cryRpt.Clone();
            cryRpt.Dispose();
            cryRpt = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
         }
         catch(CrystalReportsException engEx)
         {
            string localErrorString;
            string exceptionString = engEx.ToString();
            localErrorString = string.Format("File Not found.. " + Path.Substring(0, 50) + "... " + exceptionString.Substring(0, 20));
            errorString = localErrorString;
            cryRpt.Close();
            cryRpt.Clone();
            cryRpt.Dispose();
            cryRpt = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            returnCode = Constants.RETURN_FILE_ACCESS_ERROR;

         }
         return (returnCode);

      }
      public static GeneralRpt CreatePrintObject(string inPath, CRYSTALREPORTS reportType, int OP__DOCID, int nodeType)
      {
         switch(reportType)
         {
            case CRYSTALREPORTS.FACESHEET:
               return new GeneralRpt(inPath, "ClientKey", Globals.mClientKey, "AdmissionKey", Globals.mAdmissionKey);
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
            case CRYSTALREPORTS.EVALUATION_OF_RISK:
            case CRYSTALREPORTS.COGNITIVE_ASSESSMENT:
            case CRYSTALREPORTS.BODY_ASSESSMENT_CHECKLIST:
            case CRYSTALREPORTS.PAIN_EVALUATION:
            case CRYSTALREPORTS.COMPREHENSIVE_MENTAL_STATUS:
            case CRYSTALREPORTS.MINI_MENTAL_STATUS:
            case CRYSTALREPORTS.FOLLOWUP_APPOINTMENT:
               return new GeneralRpt(inPath, "OP__DOCID", OP__DOCID);
            case CRYSTALREPORTS.NURSING_EVALUATION:
            case CRYSTALREPORTS.PSYCHIATRIC_PROGRESS_NOTE:
            case CRYSTALREPORTS.GENERAL_NOTE:
            case CRYSTALREPORTS.SOAP_NOTE:
            case CRYSTALREPORTS.CONTACT_NOTE:
            case CRYSTALREPORTS.GENERAL_ORDER:
            case CRYSTALREPORTS.FALL_RISK_EVALUATION:
               if (nodeType == Constants.CHILD_NODE)
                  return new GeneralRpt(inPath, "OP__DOCID", OP__DOCID, "AdmissionKey", 0);
               else
                  return new GeneralRpt(inPath, "OP__DOCID", 0, "AdmissionKey", Globals.mAdmissionKey);

            case CRYSTALREPORTS.ADMINISTERED_MEDICATION_HISTORY:
               return new GeneralRpt(inPath, Globals.mAdmissionKey, Globals.mStartDate, Globals.mEndDate);
            case CRYSTALREPORTS.MASTER_TREATMENT_PLAN:
               return new GeneralRpt(inPath, "MTPkey", OP__DOCID);
            case CRYSTALREPORTS.ALLERGIES:
               return new GeneralRpt(inPath, "ClientKey", Globals.mClientKey);
            default:
               return null;

         }
      }
   }
}
