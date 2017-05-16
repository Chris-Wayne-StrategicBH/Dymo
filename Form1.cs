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

namespace DPGPP
{

   public partial class Form1 : Form
   {

      PrinterSettings printerSettings = new PrinterSettings();

      public Form1()
      {
         InitializeComponent();
         if (ApplicationDeployment.IsNetworkDeployed)
         {
            this.Text = string.Format("Daniel's Pretty Good Printing Program - v{0}", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
         }
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
            Print_Reports();
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
                  resultList = new List<Result>(Accessor.GetMasterTreatmentPlan(Globals.mAdmissionKey));
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt, RootNode, parentNode, null);
                  AddChildNodes(resultList.Count, resultList, rpt, parentNode);
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
                  rptObj.PrintCrystalReport();
            }
            PrintRecursive(tn);
         }
      }

      private void Print_Reports()
      {
         string rootString;
         TreeNode  rootNode;
         bool printedParent = false;
         GeneralRpt rptObj;
         // Walk through Parent Nodes

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
                  rptObj.PrintCrystalReport();
                  System.Diagnostics.Debug.WriteLine(parentNode.Text);
                  printedParent = true;
               }
            }
            if (!printedParent)
            {
               foreach (TreeNode childNode in parentNode.Nodes)
               {
                  if (childNode.Checked)
                  {
                     rptObj = (GeneralRpt)childNode.Tag;
                     if (rptObj != null)
                     {
                        rptObj.PrintCrystalReport();
                        System.Diagnostics.Debug.WriteLine(childNode.Text);
                     }
                  }
               }
            }
         }
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
   }
}
