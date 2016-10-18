using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

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
      public const string CURRENT_PRINTER = @"\\srvcosad1\Medical Records Printer";

      // Set this path to your reports
      //public const string REPORT_BASE_PATH = @"C:\tier-work\c#\test\test1\Reports";
      public const string REPORT_BASE_PATH = @"\\srvcosad1\share\DPGPP\Reports";

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
   public static class ReportInterface
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

   /*
   public class CrystalParams
   {
      private string mName;
      public string name
      {
         get
         {
            return this.mName;
         }
         set
         {
            this.mName = value;
         }
      }
      private int mVal;
      public int paramval
      {
         get
         {
            return this.mVal;
         }
         set
         {
            this.mVal = value;
         }
      }
   }

   
   public abstract class ReportClass
   {
      string mPath;
      public string Path
      {
         get
         {
            return this.mPath;
         }
         set
         {
            this.mPath = value;
         }
      }
      public List<CrystalParams> param = new List<CrystalParams>();

      public ReportClass(string inPath)
      {
         mPath = inPath;
      }
      public void AddParam(string inPath, int inVal)
      {
         param.Add(new CrystalParams { name = inPath, paramval = inVal });
      }
   }

   public class FacesheetRpt : ReportClass
   {
      public FacesheetRpt(string inPath, int clientKey, int admissionKey) : base(inPath)
      {
         base.AddParam("ClientKey", clientKey);
         base.AddParam("AdmissionKey", admissionKey);
      }
   }

   public class DateRangeRpt : ReportClass
   {
      public DateRangeRpt(string inPath, int admissionKey, DateTime startdate, DateTime enddate)
         : base(inPath)
      {
         base.AddParam("AdmissionKey", admissionKey);
         //base.AddParam("StartDate", startdate);
         //base.AddParam("EndDate", enddate);
      }
   }

   public class OtherRpt : ReportClass
   {
      public OtherRpt(string inPath) : base(inPath)
      {
      }
      public void AddParam(string inName, int inValue)
      {
         base.AddParam(inName, inValue);

      }
   }


    */
   public class CrystalParams2
   {
      private int mOP__DOCID;
      public int OP__DOCID
      {
         get
         {
            return this.mOP__DOCID;
         }
         set
         {
            this.mOP__DOCID = value;
         }
      }
      private int mClientKey;
      public int ClientKey
      {
         get
         {
            return this.mClientKey;
         }
         set
         {
            this.mClientKey = value;
         }
      }
      private int mAdmissionKey;
      public int AdmissionKey
      {
         get
         {
            return this.mAdmissionKey;
         }
         set
         {
            this.mAdmissionKey = value;
         }
      }
      private DateTime mStartDate;
      public DateTime StartDate
      {
         get
         {
            return this.mStartDate;
         }
         set
         {
            this.mStartDate = value;
         }
      }
      private DateTime mEndDate;
      public DateTime EndDate
      {
         get
         {
            return this.mEndDate;
         }
         set
         {
            this.mEndDate = value;
         }
      }
   }
      

   public abstract class ReportClassGeneric
   {
      string mPath;
      public string Path
      {
         get
         {
            return this.mPath;
         }
         set
         {
            this.mPath = value;
         }
      }
      CRYSTALREPORTTYPES mReportType;
      public CRYSTALREPORTTYPES reportType
      {
         get { return this.mReportType; }
         set { this.mReportType = value; }
      }

      public ReportClassGeneric(string inPath)
      {
         mPath = inPath;
      }
   }

   public class RptInterface : ReportClassGeneric
   {
      CrystalParams2 cr = new CrystalParams2();
      
      //This type of report requires a ClientKey and an AdmissionKey for parameters
      public RptInterface(string inPath, int clientkey, int admissionKey)
         : base(inPath)
      {
         cr.ClientKey = clientkey;
         cr.AdmissionKey = admissionKey;
         reportType = CRYSTALREPORTTYPES.CLIENTKEY_ADMISSIONKEY;
      }
      //This type of report requires an AdmissionKey, startdate and enddate for parameters
      public RptInterface(string inPath, int admissionKey, DateTime startdate, DateTime enddate)
         : base(inPath)
      {
         cr.StartDate = startdate;
         cr.EndDate = enddate;
         cr.AdmissionKey = admissionKey;
         reportType = CRYSTALREPORTTYPES.STARTDATE_ENDDATE;
      }
      //This type of report requires an OP__DOCID
      public RptInterface(string inPath, int OP__DOCID)
         : base(inPath)
      {
         cr.OP__DOCID = OP__DOCID;
         reportType = CRYSTALREPORTTYPES.OP__DOCID;
      }
      //This type of report requires an OP__DOCID and AdmissionKey = 0
      public RptInterface(string inPath, int OP__DOCID, string dummy)
         : base(inPath)
      {
         cr.OP__DOCID = OP__DOCID;
         cr.AdmissionKey = 0;
         reportType = CRYSTALREPORTTYPES.OP__DOCID_ADMISSIONKEY0;
      }
      public int Get_OP__DOCID()
      {
         return (cr.OP__DOCID);
      }
      public int Get_ClientKey()
      {
         return (cr.ClientKey);
      }
      public int Get_AdmissionKey()
      {
         return (cr.AdmissionKey);
      }
      public DateTime Get_StartDate()
      {
         return (cr.StartDate);
      }
      public DateTime Get_EndDate()
      {
         return (cr.EndDate);
      }
   }
}
