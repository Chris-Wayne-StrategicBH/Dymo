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

      public int mClientKey;
      public int mAdmissionKey;
      public DateTime mStartDate, mEndDate;
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
         mClientKey = (int)dataGridView1.Rows[row].Cells[Constants.OP__DOCID_INDEX].Value;


         List<ProgramClientResult> admissions = new List<ProgramClientResult>(Accessor.GetAdmissionsFromClientKey(Convert.ToInt32(LBL_Client_OP__DOCID.Text)));
         dataGridView2.DataSource = admissions;
      }

      private void BTN_Print_Click(object sender, EventArgs e)
      {
         Print_Reports();
      }

      private void dataGridView2_SelectionChanged(object sender, EventArgs e)
      {
         int row;

         row = dataGridView2.CurrentCell.RowIndex;
         LBL_AdmissionKey.Text = dataGridView2.Rows[row].Cells[Constants.ADMISSIONKEY_INDEX].Value.ToString();
         mAdmissionKey = (int)dataGridView2.Rows[row].Cells[Constants.ADMISSIONKEY_INDEX].Value;
         if (dataGridView2.Rows[row].Cells[Constants.STARTDATE_INDEX].Value == null)
            mStartDate = DateTime.Now;
         else
            mStartDate = (DateTime)dataGridView2.Rows[row].Cells[Constants.STARTDATE_INDEX].Value;

         if (dataGridView2.Rows[row].Cells[Constants.ENDDATE_INDEX].Value == null)
            mEndDate = DateTime.Now;
         else
            mEndDate = (DateTime)dataGridView2.Rows[row].Cells[Constants.ENDDATE_INDEX].Value;
         PrepareTree();

      }

      private void PrintReport(RptInterface rpt)
      {
         Tables CrTables;
         ReportDocument cryRpt = new ReportDocument();
         ConnectionInfo crConnectionInfo = new ConnectionInfo();
         TableLogOnInfo crtablelogoninfo = new TableLogOnInfo();

         crConnectionInfo.ServerName = "srvcostier";
         crConnectionInfo.DatabaseName = "TIER_PVBH";
         crConnectionInfo.UserID = "TIER";
         crConnectionInfo.Password = "38$bH125";

         cryRpt.Load(rpt.Path);
         switch (rpt.reportType)
         {
            case CRYSTALREPORTTYPES.OP__DOCID:
               cryRpt.SetParameterValue("OP__DOCID", rpt.Get_OP__DOCID());
               break;
            case CRYSTALREPORTTYPES.CLIENTKEY_ADMISSIONKEY:
               cryRpt.SetParameterValue("ClientKey", rpt.Get_ClientKey());
               cryRpt.SetParameterValue("AdmissionKey", rpt.Get_AdmissionKey());
               break;
            case CRYSTALREPORTTYPES.OP__DOCID_ADMISSIONKEY0:
               cryRpt.SetParameterValue("OP__DOCID", rpt.Get_OP__DOCID());
               cryRpt.SetParameterValue("AdmissionKey", 0);
               break;
            case CRYSTALREPORTTYPES.STARTDATE_ENDDATE:
               cryRpt.SetParameterValue("AdmissionKey", rpt.Get_AdmissionKey());
               cryRpt.SetParameterValue("StartDate", rpt.Get_StartDate());
               cryRpt.SetParameterValue("EndDate", rpt.Get_EndDate());
               break;
         }
         
         CrTables = cryRpt.Database.Tables;

         foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
         {
            crtablelogoninfo = CrTable.LogOnInfo;
            crtablelogoninfo.ConnectionInfo = crConnectionInfo;
            CrTable.ApplyLogOnInfo(crtablelogoninfo);
         }

         // Select the printer and print
         cryRpt.PrintOptions.PrinterDuplex = (PrinterDuplex) printerSettings.Duplex; // 1 sided ?
         cryRpt.PrintOptions.PrinterName = printerSettings.PrinterName;
         cryRpt.PrintToPrinter(1, false, 0, 0);

         cryRpt.Close();
         cryRpt.Clone();
         cryRpt.Dispose();
         cryRpt = null;
         GC.Collect();
         GC.WaitForPendingFinalizers();

         //crystalReportViewer1.ReportSource = cryRpt;
         //crystalReportViewer1.Refresh();
         //crystalReportViewer1.PrintReport();

      }
      private void PrepareTree()
      {
         TreeNode RootNode = new TreeNode();
         List<Result> resultList;
         // chop the tree down
         treeView1.Nodes.Clear();

         foreach (CRYSTALREPORTS rpt in Enum.GetValues(typeof(CRYSTALREPORTS)))
         {
            int val = (int)rpt;
            Console.WriteLine(rpt + " " + val.ToString());

            switch(rpt)
            {
               case CRYSTALREPORTS.ROOT:
                  //Root node just display admissionkey
                  AddRootNode(rpt.ToString(), "Admission : " + mAdmissionKey.ToString(), "Admission : " + mAdmissionKey.ToString(), RootNode);
                  break;
               case CRYSTALREPORTS.FACESHEET:
                  // FaceSheet Report.. already have ClientKey and AdmissionKey.. should work
                  TreeNode parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt.ToString(), rpt.ToString(), RootNode, parentNode);
                  break;
               case CRYSTALREPORTS.HISTORY_PHYSICAL:
                  // History And Physical report.. Are there any?
                  resultList = new List<Result>(Accessor.GetHistoryAndPhysicals(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.PSYCHIATRIC_EVALUATION:
                  // Psych Eval report.. Are there any?
                  resultList = new List<Result>(Accessor.GetPsychEvals(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.COMPREHENSIVE_PSYCHOSOCIAL:
                  resultList = new List<Result>(Accessor.GetPsychoSocials(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.NURSING_ASSESSMENT:
                  resultList = new List<Result>(Accessor.GetNursingAssessments(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.DISCHARGE_SUMMARY:
                  resultList = new List<Result>(Accessor.GetDischargeSummaries(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.DISCHARGE_AFTERCARE_PLAN:
                  resultList = new List<Result>(Accessor.GetDischargeAftercarePlans(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.PHYSICIAN_DISCHARGE_SUMMARY:
                  resultList = new List<Result>(Accessor.GetPhysicianDischargeSummary(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.NURSING_EVALUATION:
                  resultList = new List<Result>(Accessor.GetNursingEvaluations(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.INITIAL_CONTACT_NOTE:
                  resultList = new List<Result>(Accessor.GetInitialContactNotes(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.PSYCHIATRIC_PROGRESS_NOTE:
                  resultList = new List<Result>(Accessor.GetPsychiatricProgressNotes(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.GENERAL_NOTE:
                  resultList = new List<Result>(Accessor.GetGeneralNotes(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.SOAP_NOTE:
                  resultList = new List<Result>(Accessor.GetSoapNotes(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.CONTACT_NOTE:
                  resultList = new List<Result>(Accessor.GetContactNotes(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.GENERAL_ORDER:
                  resultList = new List<Result>(Accessor.GetGeneralOrders(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.MEDICATION_ORDERS_HISTORY:
                  resultList = new List<Result>(Accessor.GetMedicationOrdersHistory(mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
               case CRYSTALREPORTS.ADMINISTERED_MEDICATION_HISTORY:
                  parentNode = new TreeNode();
                  AddNode(rpt.ToString(), rpt.ToString(), rpt.ToString(), RootNode, parentNode);
                  break;
               case CRYSTALREPORTS.UPDATED_COMPREHENSIVE_ASSESSMENT:
                  resultList = new List<Result>(Accessor.GetUpdatedComprehensiveAssessment (mAdmissionKey));
                  AddNodes(resultList.Count, resultList, rpt, RootNode);
                  break;
            }

         }
         treeView1.ExpandAll();

      }

      private void Print_Reports()
      {
         TreeNode parentNode, rootNode = null;
         RptInterface rptObj;

         // Find Root Node
         rootNode = FindRootNode(CRYSTALREPORTS.ROOT.ToString(), treeView1.Nodes);
         if (rootNode == null)
         {
            Console.WriteLine("Root Node not found.. See The Great Gazoo!");
         }
         else
         {
            foreach (CRYSTALREPORTS rpt in Enum.GetValues(typeof(CRYSTALREPORTS)))
            {
               int val = (int)rpt;
               Console.WriteLine(rpt + " " + val.ToString());
               string reportPath = Path.Combine(Constants.REPORT_BASE_PATH, ReportInterface.GetFileName(rpt));
               switch (rpt)
               {
                  case CRYSTALREPORTS.ROOT:
                     //Root node just display admissionkey
                     // Nothing to print
                     break;
                  case CRYSTALREPORTS.FACESHEET:
                     // FaceSheet Report.. already have ClientKey and AdmissionKey.. should work
                     // Find Parent Node
                     parentNode = FindNode(rpt.ToString(), rootNode);
                     if (parentNode == null)
                        Console.WriteLine("Node " + rpt.ToString() + " not found");
                     else
                     {
                        if (parentNode.Checked)
                        {
                           rptObj = new RptInterface(reportPath, mClientKey, mAdmissionKey);
                           //MessageBox.Show("Printing Facesheet......");
                           PrintReport(rptObj);
                        }
                        else
                           Console.WriteLine(rpt.ToString() + parentNode.Text + "Not Checked.....");
                     }
                     break;

                  case CRYSTALREPORTS.NURSING_EVALUATION:
                  case CRYSTALREPORTS.PSYCHIATRIC_PROGRESS_NOTE:
                  case CRYSTALREPORTS.GENERAL_NOTE:
                  case CRYSTALREPORTS.SOAP_NOTE:
                  case CRYSTALREPORTS.CONTACT_NOTE:
                  case CRYSTALREPORTS.GENERAL_ORDER:
                     // Find Parent Node
                     parentNode = FindNode(rpt.ToString(), rootNode);
                     if (parentNode == null)
                        Console.WriteLine("Node " + rpt.ToString() + " not found");
                     else
                     {
                        foreach (TreeNode childnode in parentNode.Nodes)
                        {
                           if (childnode.Checked)
                           {
                              rptObj = new RptInterface(reportPath, Convert.ToInt32(childnode.Tag), "Dummy");
                              PrintReport(rptObj);
                           }
                           else
                              Console.WriteLine(rpt.ToString() + childnode.Text + "Not Checked.....");
                        }
                     }
                     break;
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
                     // Find Parent Node
                     parentNode = FindNode(rpt.ToString(), rootNode);
                     if (parentNode == null)
                        Console.WriteLine("Node " + rpt.ToString() + " not found");
                     else
                     {
                        foreach (TreeNode childnode in parentNode.Nodes)
                        {
                           if (childnode.Checked)
                           {
                              rptObj = new RptInterface(reportPath, Convert.ToInt32(childnode.Tag));
                              PrintReport(rptObj);
                           }
                           else
                              Console.WriteLine(rpt.ToString() + childnode.Text + "Not Checked.....");
                        }
                     }
                     break;
                  case CRYSTALREPORTS.ADMINISTERED_MEDICATION_HISTORY:
                     // Administered Medication Report.. already have AdmissionKey Startdate and End date
                     // Find Parent Node
                     parentNode = FindNode(rpt.ToString(), rootNode);
                     if (parentNode == null)
                        Console.WriteLine("Node " + rpt.ToString() + " not found");
                     else
                     {
                        if (parentNode.Checked)
                        {
                           rptObj = new RptInterface(reportPath, mAdmissionKey, mStartDate, mEndDate);
                           //MessageBox.Show("Printing Administered Meds......");
                           PrintReport(rptObj);
                        }
                        else
                           Console.WriteLine(rpt.ToString() + parentNode.Text + "Not Checked.....");
                     }
                     break;
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
      private void AddNode(string name, string text, string tag, TreeNode parentNode, TreeNode childNode)
      {
         childNode.Name = name;
         childNode.Text = text;
         childNode.Tag = tag;
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

      private void AddNodes(int count, List<Result> resultList, CRYSTALREPORTS rpt, TreeNode rootNode)
      {
         TreeNode parentNode, childNode = null;
         string nodestr;
         DateTime datestr;

         if (count > 0)
         {
            parentNode = new TreeNode();
            AddNode(rpt.ToString(), rpt.ToString(), rpt.ToString(), rootNode, parentNode);

            // Each individual OP__DOCID
            foreach (Result result in resultList)
            {
               childNode = new TreeNode();
               if (result.Date_Doc.HasValue)
                  datestr = result.Date_Doc.Value;
               else
                  datestr = new DateTime(1900, 1, 1);

               nodestr = result.OP__DOCID.ToString() + " --- " + datestr.ToShortDateString();
               AddNode(result.OP__DOCID.ToString(), nodestr, result.OP__DOCID.ToString(), parentNode, childNode);
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

      }

      private void Form1_Load(object sender, EventArgs e)
      {
         PrinterSettings printerSettings = new PrinterSettings();
      }
   }
}
