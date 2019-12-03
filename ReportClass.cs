#define HARDWARE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Threading;
using DYMO.Label.Framework;
using System.IO;

namespace RPGPP
{

   static class Constants
   {
      public const int PARENT_NODE = 0;
      public const int RETURN_SUCCESS = 0;
      public const int RETURN_FILE_ACCESS_ERROR = -1;


      public const int STARTING = 1;
      public const int DEBUGGING = 999;
      public const int NO_PRINTER = 888;


      public const int CHILD_NODE = 1;
      public const int OP__DOCID_INDEX = 6;
      public const int GENDER_INDEX = 3;
      public const int ADMISSIONKEY_INDEX = 4;
      public const int PROGRAMNAME_INDEX = 0;
      public const int STARTDATE_INDEX = 2; 
      public const int ENDDATE_INDEX = 3;
      public const int STATUS_INDEX = 1;
      public const int FULLNAME_INDEX = 0;

      public const int CHECK_INDEX = 0;
      public const int PRESCRIPTION_OP__DOCID = 10;


      public const int NOT_USED = 9999;
      public const string DEFAULT_PRINTER_NAME = "\\DEFAULT";
      public const string DEFAULT_DB_SERVER_NAME = "srvcostier";
      public const string DEFAULT_DB_NAME = "TIER_PVBH";
      public const string DEFAULT_DB_USER = "TIER";
      public const string DEFAULT_DB_PASSWORD = "38$bH125";
      public const string REPORT_BASE_PATH = @"\\srvcostierx\RPGPP\Reports";
      public const string LABEL_PATH = @"H:\tier-work\c#\PrescriptionPrinting\Labels\prescription.label";
      public const string DEFAULTFEMALEIMAGE = @"H:\tier-work\c#\PrescriptionPrinting\Images\Minnie.jpg";
      public const string DEFAULTMALEIMAGE = @"H:\tier-work\c#\PrescriptionPrinting\Images\Mickey.jpg";

      public const string DB_CONNECTION_TEST = @"global::RPGPP.Properties.Settings.Default.TIER_DEV2ConnectionString";
      public const string DB_CONNECTION_PROD = @"global::RPGPP.Properties.Settings.Default.TIER_DEV2ConnectionString";

   }

   public static class Globals
   {
      public static string Printername = Constants.DEFAULT_PRINTER_NAME;
      public static int mClientKey;
      public static int mAdmissionKey;
      public static DateTime mStartDate, mEndDate;
   }

   // using the enum name itself to identify nodes in treeview
   // using the description to provide the report file name
   public enum DYMOLABEL
   {
      [Description("TEXT_PatientName")]
      PATIENT_NAME,
      [Description("TEXT_PatientDOB")]
      PATIENT_DOB,
      [Description("TEXT_Address")]
      PATIENT_ADDRESS,
      [Description("TEXT_DatePrescribed")]
      DATE_PRESCRIBED,
      [Description("TEXT_TimePrescribed")]
      TIME_PRESCRIBED,
      [Description("TEXT_Credentials")]
      PRESCRIBER,
      [Description("TEXT_NPI")]
      NPI,
      [Description("TEXT_DEA")]
      DEA,
      [Description("TEXT_LBL_Supervisor")]
      SUPERVISOR_LABEL,
      [Description("TEXT_SupervisorCred")]
      SUPERVISOR,
      [Description("TEXT_LBL_Supervisor_NPI")]
      SUPERVISOR_NPI_LABEL,
      [Description("TEXT_SupervisorNPI")]
      SUPERVISOR_NPI,
      [Description("TEXT_LBL_SupervisorDEA")]
      SUPERVISOR_DEA_LABEL,
      [Description("TEXT_SupervisorDEA")]
      SUPERVISOR_DEA,
      [Description("TEXT_Drug")]
      DRUG,
      [Description("TEXT_Generic")]
      GENERIC,
      [Description("TEXT_Strength")]
      STRENGTH,
      [Description("TEXT_Quantity")]
      QUANTITY,
      [Description("TEXT_Dose")]
      DOSE,
      [Description("TEXT_Frequency")]
      FREQUENCY,
      [Description("TEXT_Rationale")]
      RATIONALE,
      [Description("TEXT_Refills")]
      REFILLS,
      [Description("TEXT_Instructions")]
      INSTRUCTIONS
   }


   // using reflection to get description values of enums
   public static class Translation
   {
      public static string GetEnumDescription(Enum value)
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


   public class LabelClass : ILabel
   {
      public IEnumerable<ILabelObject> Objects => throw new NotImplementedException();

      public IEnumerable<string> ObjectNames => throw new NotImplementedException();

      public int AddressObjectCount => throw new NotImplementedException();

      public ILabelObject GetObjectByName(string objectName)
      {
         throw new NotImplementedException();
      }

      public string GetObjectText(string objectName)
      {
         throw new NotImplementedException();
      }

      public void Print(IPrinter printer)
      {
         throw new NotImplementedException();
      }

      public void Print(string printerName)
      {
         throw new NotImplementedException();
      }

      public void Print(IPrinter printer, IPrintParams printParams)
      {
         throw new NotImplementedException();
      }

      public void Print(string printerName, IPrintParams printParams)
      {
         throw new NotImplementedException();
      }

      public void Print(IPrinter printer, IPrintParams printParams, string labelSetXml)
      {
         throw new NotImplementedException();
      }

      public void Print(string printerName, IPrintParams printParams, string labelSetXml)
      {
         throw new NotImplementedException();
      }

      public byte[] RenderAsPng(IPrinter printer, ILabelRenderParams renderParams)
      {
         throw new NotImplementedException();
      }

      public void SaveToFile(string fileName)
      {
         throw new NotImplementedException();
      }

      public void SaveToStream(Stream stream)
      {
         throw new NotImplementedException();
      }

      public string SaveToXml()
      {
         throw new NotImplementedException();
      }

      public void SetAddressText(int index, string text)
      {
         throw new NotImplementedException();
      }

      public void SetImagePngData(string imageName, Stream pngStream)
      {
         throw new NotImplementedException();
      }

      public void SetImageUri(string imageName, string uri)
      {
         throw new NotImplementedException();
      }

      public void SetObjectText(string objectName, string text)
      {
         throw new NotImplementedException();
      }

      public void SetPostnetBarcodePosition(int index, PostnetBarcodePosition position)
      {
         throw new NotImplementedException();
      }
   }

}
