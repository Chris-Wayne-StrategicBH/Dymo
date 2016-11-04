using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Text;



namespace DPGPP
{

   public class ProgramClientResult
   {
      public System.Int32 AdmissionKey
      { get; set; }
      public System.Nullable<System.DateTime> StartDate
      { get; set; }
      public System.Nullable<System.DateTime> EndDate
      { get; set; }
   }

   public class Result
   {
      public System.Int32 OP__DOCID
      { get; set; }
      public System.Nullable<System.DateTime> Date_Doc
      { get; set; }
      public System.Nullable<System.DateTime> Time_Doc
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

        // This section contains examples of
        // pulling back entire tables from
        // the database

        /// <summary>
        /// Displays the full Employee table
        /// </summary>
        /// <returns></returns>
       public static System.Data.Linq.Table<FD__CLIENT> GetClientsTable()
        {
           DataClasses1DataContext dc = new DataClasses1DataContext();
           return dc.GetTable<FD__CLIENT>();
        }


        


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
       public static List<FD__CLIENT> GetClientByName(String LName, String FName)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = from e in dc.GetTable<FD__CLIENT>()
                        where (e.NameL.StartsWith(LName) && e.NameF.StartsWith(FName))
                        select e;


          return results.ToList<FD__CLIENT>();
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

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__PROGRAM_CLIENT>()
                        where (e.ClientKey == clientkey)
                        select new ProgramClientResult
                        {
                           AdmissionKey = (Int32) e.AdmissionKey,
                           StartDate = e.Create_Date,
                           EndDate = e.Date_Discharged_Program
                        });


          return results.ToList<ProgramClientResult>();
       }

       /// <summary>
       /// Returns list of History and Physicals
       /// where clientkey 
       /// passed in search string
       /// </summary>
       /// <param name="admissionKey"></param>
       /// <returns>The list of History and Physicals for this client's admission</returns>
       public static List<Result> GetHistoryAndPhysicals(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__HISTORY_PHY>()
                        where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.TODAY_DATE});


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of Psych evals
       /// where clientkey 
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of Psych evals for this client admission</returns>
       public static List<Result> GetPsychEvals(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__PSYCH_EVAL>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.TODAY_DATE });


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of Comprehensive Psychosocials
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of Comprehensive Psychosocials for this client admission</returns>
       public static List<Result> GetPsychoSocials(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__Intake_Assess>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.Intake_Date });


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of Discharge Summaries
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of Discharge Summaries for this client admission</returns>
       public static List<Result> GetDischargeSummaries(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__DISCHARGE_SUMMARY>()
                        where (e.AdmissionKey == admissionkey)
                        select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.DateDoc });


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of Nursing Assessments
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of Nursing Assessments for this client admission</returns>
       public static List<Result> GetNursingAssessments(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__NURSING>()
                        where (e.AdmissionKey == admissionkey)
                        select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.TODAY_DATE });


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of Discharge Aftercare Plans
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of Discharge Aftercare Plans for this client admission</returns>
       public static List<Result> GetDischargeAftercarePlans(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__DISCHARGE_AFTERCARE>()
                        where (e.AdmissionKey == admissionkey)
                        select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.DateDoc });


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of Physician Discharge Summary
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of Physician Discharge Summary for this client admission</returns>
       public static List<Result> GetPhysicianDischargeSummary(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__PHYSICIAN_DISCHARGE_SUMMARY>()
                        where (e.AdmissionKey == admissionkey)
                        select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.DateDoc });


          return results.ToList<Result>();
       }


       /// <summary>
       /// Returns list of Nursing Evaluation
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of Nursing Evaluation for this client admission</returns>
       public static List<Result> GetNursingEvaluations(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__MEDICAL_STATUS>()
                        where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.Date_Status });


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of Initial Contact Notes
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of Initial Contact Notes for this client admission</returns>
       public static List<Result> GetInitialContactNotes(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__INITIAL_CONTACT>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.DateService });


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of Psychiatric Progress Notes
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of Psychiatric Progress Notes for this client admission</returns>
       public static List<Result> GetPsychiatricProgressNotes(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__PSYCHIATRIC_PROGRESS_NOTE>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.DateService });


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of General Notes
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of General Notes for this client admission</returns>
       public static List<Result> GetGeneralNotes(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__GENERALNOTE>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.DATESERVICE });


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of Soap Notes
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of Soap Notes for this client admission</returns>
       public static List<Result> GetSoapNotes(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__SOAP_NOTE>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.DateDoc });


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of Contact Notes
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of Contact Notes for this client admission</returns>
       public static List<Result> GetContactNotes(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__CONTACTNOTE>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.DateService});


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of General Orders
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of General Orders for this client admission</returns>
       public static List<Result> GetGeneralOrders(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__PHYSGEN>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.DATE_ORDER});


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of Medication Orders History
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of Medication Orders History for this client admission</returns>
       public static List<Result> GetMedicationOrdersHistory(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__MED_HISTORY>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.Date_Today});


          return results.ToList<Result>();
       }

       /// <summary>
       /// Returns list of Updated Comprehensive assessment
       /// where admissionKey
       /// passed in search string
       /// </summary>
       /// <param name="empId"></param>
       /// <returns>The list of Updated Comprehensive assessment for this client admission</returns>
       public static List<Result> GetUpdatedComprehensiveAssessment(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__PSYCHOSOCIAL_PLAN>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.TODAY_DATE});


          return results.ToList<Result>();
       }

       public static List<Result> GetEvaluationOfRisk(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__INITIAL_ASSESSMENT_OF_RISK>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.Date_Status});


          return results.ToList<Result>();
       }

       public static List<Result> GetFallRiskEval(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__FALL_RISK>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.DateDoc});


          return results.ToList<Result>();
       }

       public static List<Result> GetMasterTreatmentPlan(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<MTP_Master>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = (int)e.MTPKey, Date_Doc = e.DateDoc });


          return results.ToList<Result>();
       }

       public static List<Result> GetCognitiveAssessments(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__COGNITIVE_ASSESS>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.DateDoc });


          return results.ToList<Result>();
       }

       public static List<Result> GetBodyAssessmentChecklist(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__BODY_ASSESSMENT_CHECKLIST>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.DATEDoc});


          return results.ToList<Result>();
       }

       public static List<Result> GetAllergies(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__ALLERGIES>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.Date_Document});


          return results.ToList<Result>();
       }

       public static List<Result> GetPainEvaluations(int admissionkey)
       {

          DataClasses1DataContext dc = new DataClasses1DataContext();
          var results = (from e in dc.GetTable<FD__PAIN_ASSESS>()
                         where (e.AdmissionKey == admissionkey)
                         select new Result { OP__DOCID = e.OP__DOCID, Date_Doc = e.DateDoc});


          return results.ToList<Result>();
       }

#endregion



#region Inserting, Updating, Deleting Data


       


#endregion



#region Stored Procedures


        


#endregion


    }
}
