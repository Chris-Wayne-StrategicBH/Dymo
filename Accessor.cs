using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Text;
using System.Drawing;
using System.IO;
using System.ComponentModel;



namespace RPGPP
{

   public class ClientResult
   {
      public System.String FullName
         { get; set; }
      public System.Nullable<System.Int32> Age
         { get; set; }
      public System.Nullable<System.DateTime> DOB
         { get; set; }
      public System.String Gender
      { get; set; }
      public System.String Status
      { get; set; }
      public System.String Unit
      { get; set; }
      public System.Int32 OP__DOCID
         { get; set; }
   }

   public class ProgramClientResult
   {
      public System.String Program
      { get; set; }
      public System.String Status
      { get; set; }
      public System.Nullable<System.DateTime> StartDate
      { get; set; }
      public System.Nullable<System.DateTime> EndDate
      { get; set; }
      public System.Int32 AdmissionKey
      { get; set; }
   }

   public class Result
   {
      public System.Nullable<System.DateTime> Date_Doc
      { get; set; }
      public System.Nullable<System.DateTime> Time_Doc
      { get; set; }
      public System.Int32 OP__DOCID
      { get; set; }
   }

   public class PrescriptionResult : Result
   {
      public System.Boolean Select
      { get; set; }
      public System.String Drug
      { get; set; }
      public System.String Dose
      { get; set; }
      public System.String Frequency
      { get; set; }
      public System.String Instructions
      { get; set; }
      public System.String Rationale
      { get; set; }
      public System.String Route
      { get; set; }
      public System.Int32 AdmissionKey
      { get; set; }

   }

   public class MedPrescriptionResult
   {
      public System.String Patient
      { get; set; }
      public System.Nullable<System.DateTime> Dob
      { get; set; }
      public System.String Address
      { get; set; }
      public System.Nullable<System.DateTime> DatePrescribed
      { get; set; }
      public System.Nullable<System.DateTime> TimePrescribed
      { get; set; }
      public System.String Prescriber
      { get; set; }
      public System.String PrescriberNPI
      { get; set; }
      public System.String PrescriberDEA
      { get; set; }
      public System.String Supervisor
      { get; set; }
      public System.String SupervisorNPI
      { get; set; }
      public System.String SupervisorDEA
      { get; set; }
      public System.String Drug
      { get; set; }
      public System.String Generic
      { get; set; }
      public System.String Strength
      { get; set; }
      public System.Nullable<System.Int16>  Quantity
      { get; set; }
      public System.String Dose
      { get; set; }
      public System.String Frequency
      { get; set; }
      public System.String Rationale
      { get; set; }
      public System.String Refills
      { get; set; }
      public System.String Instructions
      { get; set; }

   }

   /// <summary>
   /// This class defines functions used to
   /// select, insert, update, and delete data
   /// using LINQ to SQL and the defined
   /// data context
   /// </summary>
   public class Accessor
    {


#region  Full Table

#endregion



#region Queries
       // This region contains examples of some
       // of the sorts of queries that can be
       // executed using LINQ to SQL

       /// <summary>
       /// Example:  Where Clause
       /// Returns rows from FD__CLIENTS
       /// where last name is matched to 
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of matching names</returns>
       public static Library.SortableBindingList<ClientResult> GetClientByName(String LName, String FName, Boolean Active)
       {
         string active = "";
         if (Active)
            active = "A";
         DateTime dateobj = DateTime.Now.AddMonths(-3);


         DataClasses1DataContext dc = new DataClasses1DataContext(global::RPGPP.Properties.Settings.Default.TIER_DEV2ConnectionString);
         var results = (from c in dc.GetTable<FD__CLIENT>()
                        from pc in dc.GetTable<FD__PROGRAM_CLIENT>() 
                           .Where(o => o.ClientKey == c.ClientKey)
                           .OrderByDescending(o => o.OP__DOCID).Take(1)
                        from f in dc.GetTable<FD__FACILITIES>()
                           .Where(o => o.OP__DOCID == pc.FacilityKey)

                        where (c.NameL.StartsWith(LName) && 
                           c.NameF.StartsWith(FName) && 
                           pc.Program_Status.StartsWith(active) &&
                           pc.Date_Form > dateobj &&
                           pc.DeptKey == 1)
                        orderby c.Fullname ascending
                        select new ClientResult
                        {
                           FullName = c.Fullname,
                           Age = (Int32)c.Age,
                           DOB = c.DOB,
                           Gender = c.Gender,
                           Status = pc.Program_Status,
                           Unit = f.FacilityName,
                           OP__DOCID = c.OP__DOCID
                        });

         //return results.ToList<ClientResult>();
         return new Library.SortableBindingList<ClientResult>(results.ToList());
       }

      public static Image ByteArrayToImage(byte[] byteArrayIn)
      {
         using (MemoryStream ms = new MemoryStream(byteArrayIn))
         {
            Image returnImage = Image.FromStream(ms);
            return (returnImage);
         }

      }
      public static Image GetClientPhoto(Int32 ClientKey)
      {

         DataClasses1DataContext dc = new DataClasses1DataContext(global::RPGPP.Properties.Settings.Default.TIER_DEV2ConnectionString);
         var results = (from c in dc.GetTable<FD__IDENT_INFO>()
                        where (c.ClientKey == ClientKey)
                        select c.Photo).FirstOrDefault();

         if (results != null && results is System.Data.Linq.Binary)
         {
            byte[] array = results.ToArray();
            return ByteArrayToImage(results.ToArray());
         }
         else
            return (null);
      }

      /// <summary>
      /// Returns list of admissions
      /// where clientkey 
      /// passed in search string
      /// </summary>
      /// <param name="empId"></param>
      /// <returns>The list of admissions for this client</returns>
      public static List<ProgramClientResult> GetAdmissionsFromClientKey(int clientkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext(global::RPGPP.Properties.Settings.Default.TIER_DEV2ConnectionString);
          var results = (from pc in dc.GetTable<FD__PROGRAM_CLIENT>()
                         join p in dc.GetTable<BLU_Program>() on pc.PgmKey equals p.PgmKey
                         where (pc.ClientKey == clientkey)
                         orderby pc.Create_Date descending
                         select new ProgramClientResult
                         {
                            Program = p.PgmName,
                            Status = pc.Program_Status,
                            StartDate = pc.Create_Date,
                            EndDate = pc.Date_Discharged_Program,
                            AdmissionKey = (Int32)pc.AdmissionKey,
                         });


          return results.ToList<ProgramClientResult>();
       }

       
      public static List<PrescriptionResult> GetPrescriptions(int admissionkey)
      {

         DataClasses1DataContext dc = new DataClasses1DataContext(global::RPGPP.Properties.Settings.Default.TIER_DEV2ConnectionString);
         var results = (from e in dc.GetTable<FD__MEDS_PRESCRIPTION>()
                        where (e.AdmissionKey == admissionkey)
                        select new PrescriptionResult
                        {
                           AdmissionKey = (Int32)e.AdmissionKey,
                           Drug = e.DrugName1,
                           Dose = e.DOSE1,
                           Frequency = e.Frequency_Med,
                           Instructions = e.INSTRUCTIONS,
                           Rationale = e.Rationale,
                           Route = e.Route,
                           Date_Doc = e.DATE_FORM,
                           OP__DOCID = e.OP__DOCID
                        });

         return results.ToList<PrescriptionResult>();
      }

      public static MedPrescriptionResult GetPrescription(int OP__DOCID)
      {

         DataClasses1DataContext dc = new DataClasses1DataContext(global::RPGPP.Properties.Settings.Default.TIER_DEV2ConnectionString);
         var results = (from e in dc.GetTable<VWr__Medication_Prescription>()
                        where (e.OP__DOCID == OP__DOCID)
                        select new MedPrescriptionResult
                        {
                           Patient = e.Patient,
                           Dob = e.DOB,
                           Address = e.ClAddress,
                           DatePrescribed = e.DATE_FORM,
                           TimePrescribed = e.TIME1,
                           Prescriber = e.Staff,
                           PrescriberNPI = e.NPI_ID,
                           PrescriberDEA = e.Dea,
                           Supervisor = e.Supervisor,
                           SupervisorNPI = e.Supervisor_NPI,
                           SupervisorDEA = e.Supervisor_DEA,
                           Drug = e.DrugName1,
                           Generic = e.Generic,
                           Strength = e.Strength,
                           Quantity = (Int16)e.NumOfPills,
                           Dose = e.DOSE1,
                           Frequency = e.Frequency_Med,
                           Rationale = e.Rationale,
                           Refills = e.Refills,
                           Instructions = e.INSTRUCTIONS
                        }).Single();

         return (MedPrescriptionResult)results;
      }

      #endregion



      #region Inserting, Updating, Deleting Data





      #endregion



      #region Stored Procedures





      #endregion


   }
}
