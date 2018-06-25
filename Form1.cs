using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Deployment.Application;
using System.Drawing.Printing;
using System.Threading;

namespace DPGPP
{

   public partial class Form1 : Form
   {

      PrinterSettings printerSettings = new PrinterSettings();
      BackgroundWorker m_Worker;

      public Form1()
      {
         InitializeComponent();
         if (ApplicationDeployment.IsNetworkDeployed)
         {
            this.Text = string.Format("Daniel's Pretty Good Printing Program - v{0}", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
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

      private void BTN_Tree_Click(object sender, EventArgs e)
      {
         PrepareTree();
      }

      private void BTN_Search_Click(object sender, EventArgs e)
      {
         String NameL = TB_LastName.Text;
         String NameF = TB_FirstName.Text;
         List<FD__CLIENT> clients = new List<FD__CLIENT>(Accessor.GetClientByName(NameL, NameF));
         dataGridView1.DataSource = clients;
         LBL_Rows.Text = clients.Count().ToString();
      }


      private void dataGridView1_SelectionChanged(object sender, EventArgs e)
      {
         int row;

         row = dataGridView1.CurrentCell.RowIndex;
         LBL_SelectedRow.Text = row.ToString();
         LBL_Client_OP__DOCID.Text = dataGridView1.Rows[row].Cells[Constants.OP__DOCID_INDEX].Value.ToString();
         LBL_Client_FullName.Text = dataGridView1.Rows[row].Cells[Constants.FULLNAME_INDEX].Value.ToString();
         Globals.mClientKey = (int)dataGridView1.Rows[row].Cells[Constants.OP__DOCID_INDEX].Value;


         List<ProgramClientResult> admissions = new List<ProgramClientResult>(Accessor.GetAdmissionsFromClientKey(Globals.mClientKey));
         dataGridView2.DataSource = admissions;
      }

      private void BTN_Print_Click(object sender, EventArgs e)
      {
         if (Globals.Printername == Constants.DEFAULT_PRINTER_NAME)
            MessageBox.Show("Invalid Printer Name.. Please select printer or Contact the Great Gazoo");
         else
         {
            //Change the status of the buttons on the UI accordingly
            //The start button is disabled as soon as the background operation is started
            //The Cancel button is enabled so that the user can stop the operation 
            //at any point of time during the execution
            BTN_Print.Enabled = false;
            btnCancel.Enabled = true;
            // Kickoff the worker thread to begin it's DoWork function.
            m_Worker.RunWorkerAsync();
         }
      }

      private void dataGridView2_SelectionChanged(object sender, EventArgs e)
      {
         int row;

         row = dataGridView2.CurrentCell.RowIndex;
         LBL_AdmissionKey.Text = dataGridView2.Rows[row].Cells[Constants.ADMISSIONKEY_INDEX].Value.ToString();
         Globals.mAdmissionKey = (int)dataGridView2.Rows[row].Cells[Constants.ADMISSIONKEY_INDEX].Value;
         if (dataGridView2.Rows[row].Cells[Constants.STARTDATE_INDEX].Value == null)
            Globals.mStartDate = DateTime.Now;
         else
            Globals.mStartDate = (DateTime)dataGridView2.Rows[row].Cells[Constants.STARTDATE_INDEX].Value;

         if (dataGridView2.Rows[row].Cells[Constants.ENDDATE_INDEX].Value == null)
            Globals.mEndDate = DateTime.Now;
         else
            Globals.mEndDate = (DateTime)dataGridView2.Rows[row].Cells[Constants.ENDDATE_INDEX].Value;
         PrepareTree();

      }

      private void PrepareTree()
      {
         TreeNode RootNode = new TreeNode();
         GeneralRpt gp;
         List<Result> resultList;
         // chop the tree down
         treeView1.Nodes.Clear();

         foreach (CRYSTALREPORTS rpt in Enum.GetValues(typeof(CRYSTALREPORTS)))
         {
            int val = (int)rpt;
            Console.WriteLine(rpt + " " + val.ToString());
            string reportPath = Path.Combine(Constants.REPORT_BASE_PATH, ReportTranslation.GetFileName(rpt));

            switch (rpt)
            {
               case CRYSTALREPORTS.ROOT:
                  AddRootNode(rpt.ToString(), "Admission : " + Globals.mAdmissionKey.ToString(), "Admission : " + Globals.mAdmissionKey.ToString(), RootNode);
                  break;
               case CRYSTALREPORTS.FACESHEET:
                  TreeNode parentNode = new TreeNode();
                  gp = GeneralRpt.CreatePrintObject(reportPath, rpt, Constants.NOT_USED, Constants.PARENT_NODE);
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, gp);
                  break;
               case CRYSTALREPORTS.ADMINISTERED_MEDICATION_HISTORY:
                  parentNode = new TreeNode();
                  gp = GeneralRpt.CreatePrintObject(reportPath, rpt, Constants.NOT_USED, Constants.PARENT_NODE);
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, gp);
                  break;
               case CRYSTALREPORTS.HISTORY_PHYSICAL:
                  resultList = new List<Result>(Accessor.GetHistoryAndPhysicals(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.PSYCHIATRIC_EVALUATION:
                  resultList = new List<Result>(Accessor.GetPsychEvals(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.COMPREHENSIVE_PSYCHOSOCIAL:
                  resultList = new List<Result>(Accessor.GetPsychoSocials(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.NURSING_ASSESSMENT:
                  resultList = new List<Result>(Accessor.GetNursingAssessments(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.DISCHARGE_SUMMARY:
                  resultList = new List<Result>(Accessor.GetDischargeSummaries(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.DISCHARGE_AFTERCARE_PLAN:
                  resultList = new List<Result>(Accessor.GetDischargeAftercarePlans(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.PHYSICIAN_DISCHARGE_SUMMARY:
                  resultList = new List<Result>(Accessor.GetPhysicianDischargeSummary(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.NURSING_EVALUATION:
                  resultList = new List<Result>(Accessor.GetNursingEvaluations(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  gp = GeneralRpt.CreatePrintObject(reportPath, rpt, Constants.NOT_USED, Constants.PARENT_NODE);
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, gp);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.INITIAL_CONTACT_NOTE:
                  resultList = new List<Result>(Accessor.GetInitialContactNotes(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.PSYCHIATRIC_PROGRESS_NOTE:
                  resultList = new List<Result>(Accessor.GetPsychiatricProgressNotes(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  gp = GeneralRpt.CreatePrintObject(reportPath, rpt, Constants.NOT_USED, Constants.PARENT_NODE);
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, gp);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.GENERAL_NOTE:
                  resultList = new List<Result>(Accessor.GetGeneralNotes(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  gp = GeneralRpt.CreatePrintObject(reportPath, rpt, Constants.NOT_USED, Constants.PARENT_NODE);
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, gp);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.SOAP_NOTE:
                  resultList = new List<Result>(Accessor.GetSoapNotes(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  gp = GeneralRpt.CreatePrintObject(reportPath, rpt, Constants.NOT_USED, Constants.PARENT_NODE);
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, gp);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.CONTACT_NOTE:
                  resultList = new List<Result>(Accessor.GetContactNotes(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.GENERAL_ORDER:
                  resultList = new List<Result>(Accessor.GetGeneralOrders(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  gp = GeneralRpt.CreatePrintObject(reportPath, rpt, Constants.NOT_USED, Constants.PARENT_NODE);
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, gp);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.MEDICATION_ORDERS_HISTORY:
                  resultList = new List<Result>(Accessor.GetMedicationOrdersHistory(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.UPDATED_COMPREHENSIVE_ASSESSMENT:
                  resultList = new List<Result>(Accessor.GetUpdatedComprehensiveAssessment(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.EVALUATION_OF_RISK:
                  resultList = new List<Result>(Accessor.GetEvaluationOfRisk(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.FALL_RISK_EVALUATION:
                  resultList = new List<Result>(Accessor.GetFallRiskEval(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  gp = GeneralRpt.CreatePrintObject(reportPath, rpt, Constants.NOT_USED, Constants.PARENT_NODE);
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, gp);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.MASTER_TREATMENT_PLAN:
                  try
                  {
                     resultList = new List<Result>(Accessor.GetMasterTreatmentPlan(Globals.mAdmissionKey));
                     parentNode = new TreeNode();
                     AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                     AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  }
                  catch(Exception ex)
                  {
                     Console.WriteLine(ex.ToString());
                  }
                  break;
               case CRYSTALREPORTS.COGNITIVE_ASSESSMENT:
                  resultList = new List<Result>(Accessor.GetCognitiveAssessments(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.BODY_ASSESSMENT_CHECKLIST:
                  resultList = new List<Result>(Accessor.GetBodyAssessmentChecklist(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.ALLERGIES:
                  resultList = new List<Result>(Accessor.GetAllergies(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.PAIN_EVALUATION:
                  resultList = new List<Result>(Accessor.GetPainEvaluations(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.COMPREHENSIVE_MENTAL_STATUS:
                  resultList = new List<Result>(Accessor.GetComprehensiveMentalStatus(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.MINI_MENTAL_STATUS:
                  resultList = new List<Result>(Accessor.GetMiniMentalStatus(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
               case CRYSTALREPORTS.FOLLOWUP_APPOINTMENT:
                  resultList = new List<Result>(Accessor.GetFollowupAppointments(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
                  break;
            }

         }

      }

      private void PrintRecursive(TreeNode treeNode)
      {
         int errorCode;
         string errorString = "";
         GeneralRpt rptObj;
         // Print the node.
         //System.Diagnostics.Debug.WriteLine(treeNode.Text);
         //MessageBox.Show(treeNode.Text);
         // Print each node recursively.
         foreach (TreeNode tn in treeNode.Nodes)
         {
            if(tn.Checked)
            {
               rptObj = (GeneralRpt)tn.Tag;
               if(rptObj != null)
                  errorCode = rptObj.PrintCrystalReport(ref errorString);
            }
            PrintRecursive(tn);
         }
      }

      private void m_Worker_Print_Reports(object sender, DoWorkEventArgs e)
      {
         string rootString;
         TreeNode  rootNode;
         bool printedParent = false;
         GeneralRpt rptObj;
         int i = 0;
         int childNodes = 0;
         int numReports = 0;
         int childIndex = 0;
         int errorCode = 0;
         string errorString = "";
         // Walk through Parent Nodes

         UpdateDisplay(0, 0, "", "", Constants.STARTING, "");

         numReports = GetNumberReports();

         rootString = CRYSTALREPORTS.ROOT.ToString(); 
         rootNode = FindRootNode(rootString, treeView1.Nodes);
         TreeNodeCollection nodes = treeView1.Nodes;
         foreach (TreeNode parentNode in rootNode.Nodes)
         {
            printedParent = false;
            if (parentNode.Checked)
            {
               rptObj = (GeneralRpt)parentNode.Tag;
               if (rptObj != null)
               {
                  errorCode = rptObj.PrintCrystalReport(ref errorString);
                  childNodes = GetChildNodesChecked(parentNode);
                  if (childNodes == 0)
                     i++; // parent report no children like Facesheet
                  else
                     i += childNodes;

                  UpdateDisplay(i, numReports, parentNode.Text, "", errorCode, errorString);

                  printedParent = true;

                  if (m_Worker.CancellationPending)
                  {
                     // Set the e.Cancel flag so that the WorkerCompleted event
                     // knows that the process was cancelled.
                     e.Cancel = true;
                     m_Worker.ReportProgress(0);
                     return;
                  }

               }
            }
            if (!printedParent)
            {
               childIndex = 0;
               foreach (TreeNode childNode in parentNode.Nodes)
               {
                  if (childNode.Checked)
                  {
                     rptObj = (GeneralRpt)childNode.Tag;
                     if (rptObj != null)
                     {
                        childIndex++;
                        errorCode = rptObj.PrintCrystalReport(ref errorString);
                        i++;
                        UpdateDisplay(i, numReports, parentNode.Text, childNode.Text, errorCode, errorString);

                        if (m_Worker.CancellationPending)
                        {
                           // Set the e.Cancel flag so that the WorkerCompleted event
                           // knows that the process was cancelled.
                           e.Cancel = true;
                           m_Worker.ReportProgress(0);
                           return;
                        }
                     }
                  }
               }
            }
         }
         UpdateDisplay(i, numReports, "", "", errorCode, errorString);
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
            progress = (int) (fProgress * 100);

         }
         return (progress);
      }

      private void UpdateDisplay(int reportNumber, int totalReports, string parentName, string childName, int returnCode, string errorString)
      {
         string[] workerResult = new string[2];
         int progress;

         if (returnCode == Constants.RETURN_SUCCESS)
         {
            workerResult[0] = string.Format("Report Number {0}  Out of {1} ", reportNumber, totalReports);
            workerResult[1] = string.Format("Parent: {0}  Child {1} ", parentName, childName);
            progress = GetProgress(reportNumber, totalReports);
            m_Worker.ReportProgress(progress, workerResult);

            System.Diagnostics.Debug.WriteLine(string.Format("{0} {1}", workerResult[0], workerResult[1]));
         }
         else if(returnCode == Constants.STARTING)
         {
            workerResult[0] = string.Format("Preparing to Print...");
            workerResult[1] = string.Format("This is gonna be cool!");
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

            for (int i = 0; i < 1000; i++)
            {
               Thread.Sleep(100);
            }
         }
      }

      private int GetNumberReports()
      {
         string rootString;
         TreeNode rootNode;
         bool checkedParent = false;
         int childNodesChecked = 0;
         int totalNumberOfReports = 0;
         GeneralRpt rptObj;
         // Walk through Parent Nodes

         rootString = CRYSTALREPORTS.ROOT.ToString();
         rootNode = FindRootNode(rootString, treeView1.Nodes);
         TreeNodeCollection nodes = treeView1.Nodes;
         foreach (TreeNode parentNode in rootNode.Nodes)
         {
            checkedParent = false;
            if (parentNode.Checked)
            {
               rptObj = (GeneralRpt)parentNode.Tag;
               if (rptObj != null)
                  checkedParent = true;
            }
            childNodesChecked = 0;
            foreach (TreeNode childNode in parentNode.Nodes)
            {
               if (childNode.Checked)
               {
                  childNodesChecked++;
               }
            }
            if (childNodesChecked > 0)
               totalNumberOfReports += childNodesChecked;
            else if (checkedParent)
               totalNumberOfReports++;
         }
         return (totalNumberOfReports);
      }

      private int GetChildNodesChecked(TreeNode parentNode)
      {
         int childNodesChecked = 0;
       
         foreach (TreeNode childNode in parentNode.Nodes)
         {
            if (childNode.Checked)
            {
               childNodesChecked++;
            }
         }

         return (childNodesChecked);
      }

      private TreeNode FindNode(string search_str, TreeNode tn)
      {
         TreeNode treenode = null;
         foreach (TreeNode node in tn.Nodes)
         {
            if (node.Name.Equals(search_str))
            {
               treenode = node;
            }
            else
               Console.WriteLine("Searching for " + search_str + "...  Found " + node.Name);
         }
         return (treenode);
      }

      private TreeNode FindRootNode(string search_str, TreeNodeCollection tnc)
      {
         TreeNode tn = null;
         foreach (TreeNode node in tnc)
         {
            if (node.Name.Equals(search_str))
            {
               tn = node;
            }
            else
               Console.WriteLine("Searching for " + search_str + "...  Found " + node.Name);
         }
         return (tn);
      }

      private void AddNode(string inName, CRYSTALREPORTS rptEnum, TreeNode parentNode, TreeNode childNode, GeneralRpt gp)
      {
         childNode.Name = inName;
         childNode.Text = inName;
         childNode.Tag = gp;
         childNode.ForeColor = Color.Black;
         childNode.BackColor = Color.White;
         childNode.ImageIndex = 0;
         childNode.SelectedImageIndex = 0;
         parentNode.Nodes.Add(childNode);
      }

      private void AddRootNode(string name, string text, string tag, TreeNode rootNode)
      {
         //Root node just display admissionkey
         rootNode.Name = name;
         rootNode.Text = text;
         rootNode.Tag = tag;
         rootNode.ForeColor = Color.Black;
         rootNode.BackColor = Color.White;
         rootNode.ImageIndex = 0;
         rootNode.SelectedImageIndex = 0;
         treeView1.Nodes.Add(rootNode);           //Root node in TreeView

      }

      private void AddChildNodes(int count, List<Result> resultList, CRYSTALREPORTS rpt, TreeNode parentNode)
      {
         TreeNode childNode = null;
         string nodestr;
         DateTime datestr;
         GeneralRpt gp;
         string reportPath = Path.Combine(Constants.REPORT_BASE_PATH, ReportTranslation.GetFileName(rpt));

         if (count > 0)
         {
            // Each individual OP__DOCID
            foreach (Result result in resultList)
            {
               childNode = new TreeNode();
               if (result.Date_Doc.HasValue)
                  datestr = result.Date_Doc.Value;
               else
                  datestr = new DateTime(1900, 1, 1);

               nodestr = result.OP__DOCID.ToString() + " --- " + datestr.ToShortDateString();

               gp = GeneralRpt.CreatePrintObject(reportPath, rpt, result.OP__DOCID, Constants.CHILD_NODE);
               AddNode(nodestr, rpt, parentNode, childNode, gp);
            }
         }
      }

      private void TB_LastName_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyData == Keys.Enter)
         {
            String NameL = TB_LastName.Text;
            String NameF = TB_FirstName.Text;
            List<FD__CLIENT> clients = new List<FD__CLIENT>(Accessor.GetClientByName(NameL, NameF));
            dataGridView1.DataSource = clients;
            LBL_Rows.Text = clients.Count().ToString();
         }
      }

      private void TB_FirstName_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyData == Keys.Enter)
         {
            String NameL = TB_LastName.Text;
            String NameF = TB_FirstName.Text;
            List<FD__CLIENT> clients = new List<FD__CLIENT>(Accessor.GetClientByName(NameL, NameF));
            dataGridView1.DataSource = clients;
            LBL_Rows.Text = clients.Count().ToString();
         }
      }

      private void printerToolStripMenuItem_Click(object sender, EventArgs e)
      {
         PrintDialog printDialog = new PrintDialog();
         printDialog.PrinterSettings = printerSettings;
         printDialog.AllowPrintToFile = false;
         printDialog.AllowSomePages = true;
         printDialog.UseEXDialog = true;

         DialogResult result = printDialog.ShowDialog();

         if (result == DialogResult.Cancel)
         {
            return;
         }
         else
         {
            Globals.duplex = (PrinterDuplex)printDialog.PrinterSettings.Duplex;
            Globals.Printername = (string)printDialog.PrinterSettings.PrinterName;
         }

      }

      // Updates all child tree nodes recursively.
      private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
      {
         foreach (TreeNode node in treeNode.Nodes)
         {
            node.Checked = nodeChecked;
            if (node.Nodes.Count > 0)
            {
               // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
               this.CheckAllChildNodes(node, nodeChecked);
            }
         }
      }

      private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
      {
         // The code only executes if the user caused the checked state to change.
         if (e.Action != TreeViewAction.Unknown)
         {
            if (e.Node.Nodes.Count > 0)
            {
               /* Calls the CheckAllChildNodes method, passing in the current 
               Checked value of the TreeNode whose checked state changed. */
               this.CheckAllChildNodes(e.Node, e.Node.Checked);
            }
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

         else if (e.Error != null)
         {
            lblStatus.Text = "Error while performing background operation.";
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

   }
}
