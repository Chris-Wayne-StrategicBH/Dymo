using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Reflection;
using System.IO;
using DYMO.Label.Framework;
using System.Deployment.Application;
using System.Drawing.Printing;
using System.Threading;
using Color = System.Drawing.Color;

namespace RPGPP
{

   public partial class Form1 : Form
   {

      PrinterSettings printerSettings = new PrinterSettings();
      BackgroundWorker m_Worker;
      Library.SortableBindingList<ClientResult> mClients;
      //private ILabel _label;


      public Form1()
      {
         InitializeComponent();
         if (ApplicationDeployment.IsNetworkDeployed)
         {
            this.Text = string.Format("Regime Prescription Printing Program - v{0}", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
         }

         m_Worker = new BackgroundWorker();

         // Create a background worker thread that ReportsProgress &
         // SupportsCancellation
         // Hook up the appropriate events.
         m_Worker.DoWork += new DoWorkEventHandler(m_Worker_Print_Reports);
         m_Worker.ProgressChanged += new ProgressChangedEventHandler
            (m_Worker_ProgressChanged);
         m_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
            (m_Worker_RunWorkerCompleted);
         m_Worker.WorkerReportsProgress = true;
         m_Worker.WorkerSupportsCancellation = true;
      }

      private void BTN_Search_Click(object sender, EventArgs e)
      {
         SearchClients();
      }


      private void DGV_Clients_SelectionChanged(object sender, EventArgs e)
      {
         int row;

         if (DGV_Clients.CurrentCell != null)
         {

            row = DGV_Clients.CurrentCell.RowIndex;
            LBL_SelectedRow.Text = row.ToString();
            LBL_Client_OP__DOCID.Text = DGV_Clients.Rows[row].Cells[Constants.OP__DOCID_INDEX].Value.ToString();
            LBL_Client_FullName.Text = DGV_Clients.Rows[row].Cells[Constants.FULLNAME_INDEX].Value.ToString();
            Globals.mClientKey = (int)DGV_Clients.Rows[row].Cells[Constants.OP__DOCID_INDEX].Value;

            Image image;
            image = Accessor.GetClientPhoto(Globals.mClientKey);
            if (image != null)
               PB_Client.Image = Accessor.GetClientPhoto(Globals.mClientKey);
            else
            {
               if (DGV_Clients.Rows[row].Cells[Constants.GENDER_INDEX].Value.ToString() == "M")
                  PB_Client.ImageLocation = Constants.DEFAULTMALEIMAGE;
               else
                  PB_Client.ImageLocation = Constants.DEFAULTFEMALEIMAGE;
            }


            List<ProgramClientResult> admissions = new List<ProgramClientResult>(Accessor.GetAdmissionsFromClientKey(Globals.mClientKey));
            DGV_Admissions.DataSource = admissions;
         }
      }

      private void BTN_Print_Click(object sender, EventArgs e)
      {
         //Change the status of the buttons on the UI accordingly
         //The start button is disabled as soon as the background operation is started
         //The Cancel button is enabled so that the user can stop the operation 
         //at any point of time during the execution
         BTN_Print.Enabled = false;
         btnCancel.Enabled = true;
         // Kickoff the worker thread to begin it's DoWork function.
         m_Worker.RunWorkerAsync();

         //Print_Reports();

         //BTN_Print.Enabled = true;
         //btnCancel.Enabled = false;


      }

      private void DGV_Admissions_SelectionChanged(object sender, EventArgs e)
      {
         int row;
         string status;

         row = DGV_Admissions.CurrentCell.RowIndex;
         LBL_AdmissionKey.Text = DGV_Admissions.Rows[row].Cells[Constants.ADMISSIONKEY_INDEX].Value.ToString();
         LBL_Program.Text = DGV_Admissions.Rows[row].Cells[Constants.PROGRAMNAME_INDEX].Value.ToString();
         Globals.mAdmissionKey = (int)DGV_Admissions.Rows[row].Cells[Constants.ADMISSIONKEY_INDEX].Value;
         if (DGV_Admissions.Rows[row].Cells[Constants.STARTDATE_INDEX].Value == null)
            Globals.mStartDate = DateTime.Now;
         else
            Globals.mStartDate = (DateTime)DGV_Admissions.Rows[row].Cells[Constants.STARTDATE_INDEX].Value;

         if (DGV_Admissions.Rows[row].Cells[Constants.ENDDATE_INDEX].Value == null)
            Globals.mEndDate = DateTime.Now;
         else
            Globals.mEndDate = (DateTime)DGV_Admissions.Rows[row].Cells[Constants.ENDDATE_INDEX].Value;

         // Prescription Grid
         List<PrescriptionResult> prescriptions = new List<PrescriptionResult>(Accessor.GetPrescriptions(Globals.mAdmissionKey));
         DGV_Prescriptions.DataSource = prescriptions;
         DGV_Prescriptions.AutoResizeColumns();


         foreach (DataGridViewRow currentrow in DGV_Admissions.Rows)
         {
            status = currentrow.Cells[Constants.STATUS_INDEX].Value.ToString();
            if (status.Contains('A'))
               currentrow.DefaultCellStyle.BackColor = System.Drawing.Color.Green;
            else
               currentrow.DefaultCellStyle.BackColor = System.Drawing.Color.Red;
         }

         DGV_Admissions.AutoResizeColumns();


      }

      /*
      private void SetupLabelObject()
      {
         // clear Printers list
         CB_Printers.Items.Clear();

         if (_label == null)
            return;

         foreach (string objName in _label.ObjectNames)
            if (!string.IsNullOrEmpty(objName))
               CB_Printers.Items.Add(objName);

         if (CB_Printers.Items.Count > 0)
            CB_Printers.SelectedIndex = 0;
      }
      private void SetupLabelWriterSelection()
      {
         // clear all items first
         CB_Printers.Items.Clear();

         foreach (IPrinter printer in Framework.GetPrinters())
            CB_Printers.Items.Add(printer.Name);

         if (CB_Printers.Items.Count > 0)
            CB_Printers.SelectedIndex = 0;

         CB_Printers.Enabled = CB_Printers.Items.Count > 0;
      }
      */


      private void PopulateLabelClass(DataGridViewRow currentrow, ref ILabel _label)
      {
         MedPrescriptionResult prescription;
         int OP__DOCID = (int)currentrow.Cells[Constants.PRESCRIPTION_OP__DOCID].Value;

         prescription = Accessor.GetPrescription(OP__DOCID);

         foreach (DYMOLABEL textObject in Enum.GetValues(typeof(DYMOLABEL)))
         {
            int val = (int)textObject;
            Console.WriteLine(textObject + " " + val.ToString());

            switch (textObject)
            {
               case DYMOLABEL.PATIENT_NAME:
                  _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Patient);
                  break;
               case DYMOLABEL.PATIENT_DOB:
                  if (prescription.Dob == null)
                  {
                     DateTime Dob = DateTime.Now;
                     prescription.Dob = Dob;
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Dob.ToString());
                  }
                  else
                  {
                     DateTime Dob = prescription.Dob.Value;
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), Dob.ToString("MM.dd.yyyy"));
                  }
                  break;
               case DYMOLABEL.PATIENT_ADDRESS:
                  _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Address);
                  break;
               case DYMOLABEL.DATE_PRESCRIBED:
                  if (prescription.DatePrescribed == null)
                  {
                     DateTime DatePrescribed = DateTime.Now;
                     prescription.DatePrescribed = DatePrescribed;
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.DatePrescribed.ToString());
                  }
                  else
                  {
                     DateTime DatePrescribed = prescription.DatePrescribed.Value;
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), DatePrescribed.ToString("MM.dd.yyyy"));
                  }
                  break;
               case DYMOLABEL.TIME_PRESCRIBED:
                  if (prescription.TimePrescribed == null)
                  {
                     DateTime TimePrescribed = DateTime.Now;
                     prescription.TimePrescribed = TimePrescribed;
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.TimePrescribed.ToString());
                  }
                  else
                  {
                     DateTime TimePrescribed = prescription.TimePrescribed.Value;
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), TimePrescribed.ToString("HH:MM"));
                  }
                  break;
               case DYMOLABEL.PRESCRIBER:
                  _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Prescriber);
                  break;
               case DYMOLABEL.NPI:
                  _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.PrescriberNPI);
                  break;
               case DYMOLABEL.DEA:
                  _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.PrescriberDEA);
                  break;
               case DYMOLABEL.SUPERVISOR_LABEL:
                  if (prescription.Supervisor != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), "Supervisor");
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
               case DYMOLABEL.SUPERVISOR:
                  if (prescription.Supervisor != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Supervisor);
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
               case DYMOLABEL.SUPERVISOR_NPI_LABEL:
                  if (prescription.Supervisor != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), "NPI:");
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
               case DYMOLABEL.SUPERVISOR_NPI:
                  if (prescription.Supervisor != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.SupervisorNPI);
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
               case DYMOLABEL.SUPERVISOR_DEA_LABEL:
                  if (prescription.Supervisor != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), "DEA:");
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
               case DYMOLABEL.SUPERVISOR_DEA:
                  if (prescription.Supervisor != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.SupervisorDEA);
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
               case DYMOLABEL.DRUG:
                  if (prescription.Drug != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Drug);
                  break;
               case DYMOLABEL.GENERIC:
                  if (prescription.Generic != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Generic);
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
               case DYMOLABEL.STRENGTH:
                  if (prescription.Strength != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Strength);
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
               case DYMOLABEL.QUANTITY:
                  if (prescription.Quantity != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Quantity.ToString());
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
               case DYMOLABEL.DOSE:
                  if (prescription.Dose != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Dose);
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
               case DYMOLABEL.FREQUENCY:
                  if (prescription.Frequency != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Frequency);
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
               case DYMOLABEL.RATIONALE:
                  if (prescription.Rationale != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Rationale);
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
               case DYMOLABEL.REFILLS:
                  if (prescription.Refills != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Refills);
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
               case DYMOLABEL.INSTRUCTIONS:
                  if (prescription.Instructions != null)
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), prescription.Instructions);
                  else
                     _label.SetObjectText(Translation.GetEnumDescription(textObject), " ");
                  break;
            }

         }
      }

      private void m_Worker_Print_Reports(object sender, DoWorkEventArgs e)
      {
         int i = 0;
         int numReports = 0;
         int errorCode = 0;
         string errorString = "";
         ILabel _label;
         string localPrinter = "";

         if (zedilabs.com.PrinterOffline.GetDymoPrinter())
         {

            foreach (IPrinter localprinterlist in Framework.GetPrinters())
               localPrinter = localprinterlist.Name;

            if (localPrinter != "")
            {
               _label = Framework.Open(Constants.LABEL_PATH);

               UpdateDisplay(0, 0, Constants.STARTING, "");
               numReports = GetNumberReports();

               foreach (DataGridViewRow currentrow in DGV_Prescriptions.Rows)
               {
                  bool selected = (bool)currentrow.Cells[Constants.CHECK_INDEX].Value;
                  if (selected)
                  {
                     PopulateLabelClass(currentrow, ref _label);
                     _label.Print(localPrinter);
                     UpdateDisplay(i++, numReports, errorCode, errorString);
                  }
               }
            }
            else
               UpdateDisplay(0, 0, Constants.NO_PRINTER, "No Printer Detected..");
         }
         else
         {
            UpdateDisplay(0, 0, Constants.NO_PRINTER, "No Printer Detected..");
            e.Result = "No Printer Detected..";
         }
      }
      private int GetProgress(int completed, int total)
      {
         int progress = 0;
         float fProgress;

         if (completed <= 0)
            progress = 0;
         else if (completed >= total)
            progress = 100;
         else
         {
            fProgress = (float)completed / total;
            progress = (int)(fProgress * 100);

         }
         return (progress);
      }

      /*
      private void Print_Reports()
      {
         int i = 0;
         int numReports = 0;
         int errorCode = 0;
         string errorString = "";
         int selectCount = 0;

         IPrinter printer = Framework.GetPrinters()[CB_Printers.Text];
         if (printer is ILabelWriterPrinter)
         {
            ILabelWriterPrintParams printParams = null;
            ILabelWriterPrinter labelWriterPrinter = printer as ILabelWriterPrinter;


            UpdateDisplay(0, 0, "", "", Constants.STARTING, "");
            numReports = GetNumberReports();

            foreach (DataGridViewRow currentrow in DGV_Prescriptions.Rows)
            {
               UpdateDisplay(i++, numReports, "", "", errorCode, errorString);
               bool selected = (bool)currentrow.Cells[Constants.CHECK_INDEX].Value;
               if (selected)
               {
                  //PopulateLabelClass(currentrow, _label);
                  foreach (string objName in _label.ObjectNames)
                     Console.WriteLine(objName.ToString());

                  _label.Print(printer, printParams);

               }
            }
            UpdateDisplay(i, numReports, "", "", errorCode, errorString);
         }
      }
      */

      private void UpdateDisplay(int reportNumber, int totalReports, int returnCode, string errorString)
      {
         string[] workerResult = new string[2];
         int progress;

         if (returnCode == Constants.RETURN_SUCCESS)
         {
            workerResult[0] = string.Format("Report Number {0}  Out of {1} ", reportNumber, totalReports);
            workerResult[1] = string.Format("Smooth Sailing...");
            progress = GetProgress(reportNumber, totalReports);
            m_Worker.ReportProgress(progress, workerResult);

            System.Diagnostics.Debug.WriteLine(string.Format("{0} {1}", workerResult[0], workerResult[1]));
         }
         else if (returnCode == Constants.STARTING)
         {
            workerResult[0] = string.Format("Preparing to Print...");
            workerResult[1] = string.Format("This is gonna be cool!");
            m_Worker.ReportProgress(0, workerResult);

            System.Diagnostics.Debug.WriteLine(string.Format("{0} {1}", workerResult[0], workerResult[1]));

         }
         else if (returnCode == Constants.DEBUGGING)
         {
            workerResult[0] = string.Format("Got this Far...");
            workerResult[1] = string.Format("in the debugging process!");
            m_Worker.ReportProgress(0, workerResult);

            System.Diagnostics.Debug.WriteLine(string.Format("{0} {1}", workerResult[0], workerResult[1]));

         }
         else if (returnCode == Constants.NO_PRINTER)
         {
            workerResult[0] = string.Format("No printer detected...");
            workerResult[1] = string.Format("Are drivers installed? Is USB cable connected?");
            m_Worker.ReportProgress(0, workerResult);

            System.Diagnostics.Debug.WriteLine(string.Format("{0} {1}", workerResult[0], workerResult[1]));

         }
         else
         {
            workerResult[0] = string.Format("Error...{0} ... ", errorString);
            workerResult[1] = string.Format("Run for your lives!");
            progress = GetProgress(reportNumber, totalReports);
            m_Worker.ReportProgress(progress, workerResult);

            System.Diagnostics.Debug.WriteLine(string.Format(" {0} {1}", workerResult[0], workerResult[1]));

            /*
            for (int i = 0; i < 10; i++)
            {
               Thread.Sleep(10);
            }
            */
         }
      }

      private int GetNumberReports()
      {
         int totalNumberOfReports = 0;

         foreach (DataGridViewRow currentrow in DGV_Prescriptions.Rows)
         {
            bool selected = (bool)currentrow.Cells[Constants.CHECK_INDEX].Value;
            if (selected)
            {
               totalNumberOfReports++;
            }
         }
         return (totalNumberOfReports);
      }


      private void TB_LastName_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyData == Keys.Enter)
         {
            SearchClients();
         }
      }

      private void TB_FirstName_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyData == Keys.Enter)
         {
            SearchClients();
         }
      }

      private void TB_LastName_Enter(object sender, EventArgs e)
      {
         this.TB_LastName.SelectionStart = 0;
         this.TB_LastName.SelectionLength = TB_LastName.Text.Length;
      }

      private void TB_FirstName_Enter(object sender, EventArgs e)
      {
         this.TB_FirstName.SelectionStart = 0;
         this.TB_FirstName.SelectionLength = TB_FirstName.Text.Length;
      }

      private void SearchClients()
      {
         String NameL = TB_LastName.Text;
         String NameF = TB_FirstName.Text;
         mClients = new Library.SortableBindingList<ClientResult>(Accessor.GetClientByName(NameL, NameF, CB_Active.Checked));
         DGV_Clients.DataSource = mClients;
         DGV_Clients.AutoResizeColumns();
         LBL_Rows.Text = mClients.Count().ToString();
      }

      /// <summary>
      /// On completed do the appropriate task
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      void m_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         // The background process is complete. We need to inspect
         // our response to see if an error occurred, a cancel was
         // requested or if we completed successfully.  
         if (e.Cancelled)
         {
            lblStatus.Text = "Task Cancelled.";
         }

         // Check to see if an error occurred in the background process.

         else if (e.Result != null)
         {
            //lblStatus.Text = "Error while performing background operation.";
            //lblStatus.Text = e.ToString();
         }
         else
         {
            // Everything completed normally.
            lblStatus.Text = "Task Completed...Wow that was fun!";
         }

         //Change the status of the buttons on the UI accordingly
         BTN_Print.Enabled = true;
         btnCancel.Enabled = false;
      }

      /// <summary>
      /// Notification is performed here to the progress bar
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      void m_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {

         // This function fires on the UI thread so it's safe to edit

         // the UI control directly, no funny business with Control.Invoke :)

         // Update the progressBar with the integer supplied to us from the

         // ReportProgress() function.  

         progressBar1.Value = e.ProgressPercentage;
         if (e.UserState != null)
         {
            string[] results = (string[])e.UserState;
            lblStatus.Text = results[0] + results[1];
         }
      }

      /// <summary>
      /// Time consuming operations go here </br>
      /// i.e. Database operations,Reporting
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      void m_Worker_DoWork(object sender, DoWorkEventArgs e)
      {
         // The sender is the BackgroundWorker object we need it to
         // report progress and check for cancellation.
         //NOTE : Never play with the UI thread here...
         for (int i = 0; i < 100; i++)
         {
            Thread.Sleep(100);

            // Periodically report progress to the main thread so that it can
            // update the UI.  In most cases you'll just need to send an
            // integer that will update a ProgressBar                    
            m_Worker.ReportProgress(i);
            // Periodically check if a cancellation request is pending.
            // If the user clicks cancel the line
            // m_AsyncWorker.CancelAsync(); if ran above.  This
            // sets the CancellationPending to true.
            // You must check this flag in here and react to it.
            // We react to it by setting e.Cancel to true and leaving
            if (m_Worker.CancellationPending)
            {
               // Set the e.Cancel flag so that the WorkerCompleted event
               // knows that the process was cancelled.
               e.Cancel = true;
               m_Worker.ReportProgress(0);
               return;
            }
         }

         //Report 100% completion on operation completed
         m_Worker.ReportProgress(100);
      }

      private void btnCancel_Click(object sender, EventArgs e)
      {
         if (m_Worker.IsBusy)
         {

            // Notify the worker thread that a cancel has been requested.

            // The cancel will not actually happen until the thread in the

            // DoWork checks the m_oWorker.CancellationPending flag. 

            m_Worker.CancelAsync();
         }
      }

      private void btnExit_Click(object sender, EventArgs e)
      {
         this.Close();
         Application.Exit();
      }

      private void DGV_Prescriptions_OnCellValueChanged(object sender, DataGridViewCellEventArgs e)
      {

         if (e.ColumnIndex == 0 && e.RowIndex > -1)
         {
            bool selected = (bool)DGV_Prescriptions[e.ColumnIndex, e.RowIndex].Value;
            DGV_Prescriptions.Rows[e.RowIndex].DefaultCellStyle.BackColor = selected ? Color.LightBlue : Color.White;
         }

         int selectCount = 0;
         foreach (DataGridViewRow currentrow in DGV_Prescriptions.Rows)
         {
            bool selected = (bool)currentrow.Cells[Constants.CHECK_INDEX].Value;
            if (selected)
               selectCount++;
         }

         LBL_Selected.Text = selectCount.ToString();
      }

      private void DGV_Prescriptions_CurrentCellDirtyStateChanged(object sender, EventArgs e)
      {
         DGV_Prescriptions.EndEdit();
      }

      private void BTN_SelectAll_Click(object sender, EventArgs e)
      {
         bool selectall = false;
         int selectCount = 0;

         if (BTN_SelectAll.Text.Contains("UnSelect All"))
         {
            BTN_SelectAll.Text = "Select All";
         }
         else
         {
            selectall = true;
            BTN_SelectAll.Text = "UnSelect All";
         }

         foreach (DataGridViewRow currentrow in DGV_Prescriptions.Rows)
         {
            if (selectall)
            {
               currentrow.Cells[Constants.CHECK_INDEX].Value = true;
               selectCount++;
            }
            else
            {
               currentrow.Cells[Constants.CHECK_INDEX].Value = false;
               selectCount = 0;
            }

            LBL_Selected.Text = selectCount.ToString();
         }
      }

   }
}
