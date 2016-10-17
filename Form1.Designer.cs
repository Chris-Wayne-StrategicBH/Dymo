namespace DPGPP
{
   partial class Form1
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
         this.BTN_Tree = new System.Windows.Forms.Button();
         this.dataGridView1 = new System.Windows.Forms.DataGridView();
         this.BTN_Search = new System.Windows.Forms.Button();
         this.TB_LastName = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.TB_FirstName = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.LBL_Rows = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.LBL_SelectedRow = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.LBL_Client_OP__DOCID = new System.Windows.Forms.Label();
         this.LBL_Client_FullName = new System.Windows.Forms.Label();
         this.BTN_Print = new System.Windows.Forms.Button();
         this.dataGridView2 = new System.Windows.Forms.DataGridView();
         this.LBL_AdmissionKey = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.treeView1 = new System.Windows.Forms.TreeView();
         this.pictureBox2 = new System.Windows.Forms.PictureBox();
         this.pictureBox1 = new System.Windows.Forms.PictureBox();
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.printerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         this.menuStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // BTN_Tree
         // 
         this.BTN_Tree.Location = new System.Drawing.Point(1114, 129);
         this.BTN_Tree.Name = "BTN_Tree";
         this.BTN_Tree.Size = new System.Drawing.Size(115, 47);
         this.BTN_Tree.TabIndex = 0;
         this.BTN_Tree.Text = "Build Tree";
         this.BTN_Tree.UseVisualStyleBackColor = true;
         this.BTN_Tree.Click += new System.EventHandler(this.BTN_Tree_Click);
         // 
         // dataGridView1
         // 
         this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.dataGridView1.Location = new System.Drawing.Point(42, 76);
         this.dataGridView1.Name = "dataGridView1";
         this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
         this.dataGridView1.Size = new System.Drawing.Size(637, 150);
         this.dataGridView1.TabIndex = 1;
         this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
         // 
         // BTN_Search
         // 
         this.BTN_Search.Location = new System.Drawing.Point(1114, 76);
         this.BTN_Search.Name = "BTN_Search";
         this.BTN_Search.Size = new System.Drawing.Size(115, 47);
         this.BTN_Search.TabIndex = 2;
         this.BTN_Search.Text = "Search";
         this.BTN_Search.UseVisualStyleBackColor = true;
         this.BTN_Search.Click += new System.EventHandler(this.BTN_Search_Click);
         // 
         // TB_LastName
         // 
         this.TB_LastName.Location = new System.Drawing.Point(103, 36);
         this.TB_LastName.Name = "TB_LastName";
         this.TB_LastName.Size = new System.Drawing.Size(221, 20);
         this.TB_LastName.TabIndex = 3;
         this.TB_LastName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_LastName_KeyDown);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(39, 39);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(58, 13);
         this.label1.TabIndex = 4;
         this.label1.Text = "Last Name";
         // 
         // TB_FirstName
         // 
         this.TB_FirstName.Location = new System.Drawing.Point(458, 36);
         this.TB_FirstName.Name = "TB_FirstName";
         this.TB_FirstName.Size = new System.Drawing.Size(221, 20);
         this.TB_FirstName.TabIndex = 5;
         this.TB_FirstName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_FirstName_KeyDown);
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(394, 39);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(57, 13);
         this.label2.TabIndex = 6;
         this.label2.Text = "First Name";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(104, 243);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(64, 13);
         this.label3.TabIndex = 7;
         this.label3.Text = "Total Rows:";
         // 
         // LBL_Rows
         // 
         this.LBL_Rows.AutoSize = true;
         this.LBL_Rows.Location = new System.Drawing.Point(169, 242);
         this.LBL_Rows.Name = "LBL_Rows";
         this.LBL_Rows.Size = new System.Drawing.Size(13, 13);
         this.LBL_Rows.TabIndex = 8;
         this.LBL_Rows.Text = "0";
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(260, 242);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(77, 13);
         this.label4.TabIndex = 9;
         this.label4.Text = "Selected Row:";
         // 
         // LBL_SelectedRow
         // 
         this.LBL_SelectedRow.AutoSize = true;
         this.LBL_SelectedRow.Location = new System.Drawing.Point(343, 243);
         this.LBL_SelectedRow.Name = "LBL_SelectedRow";
         this.LBL_SelectedRow.Size = new System.Drawing.Size(13, 13);
         this.LBL_SelectedRow.TabIndex = 10;
         this.LBL_SelectedRow.Text = "0";
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(486, 243);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(81, 13);
         this.label5.TabIndex = 11;
         this.label5.Text = "Selected Client:";
         // 
         // LBL_Client_OP__DOCID
         // 
         this.LBL_Client_OP__DOCID.AutoSize = true;
         this.LBL_Client_OP__DOCID.Location = new System.Drawing.Point(573, 243);
         this.LBL_Client_OP__DOCID.Name = "LBL_Client_OP__DOCID";
         this.LBL_Client_OP__DOCID.Size = new System.Drawing.Size(13, 13);
         this.LBL_Client_OP__DOCID.TabIndex = 12;
         this.LBL_Client_OP__DOCID.Text = "0";
         // 
         // LBL_Client_FullName
         // 
         this.LBL_Client_FullName.AutoSize = true;
         this.LBL_Client_FullName.ForeColor = System.Drawing.Color.Purple;
         this.LBL_Client_FullName.Location = new System.Drawing.Point(639, 242);
         this.LBL_Client_FullName.Name = "LBL_Client_FullName";
         this.LBL_Client_FullName.Size = new System.Drawing.Size(40, 13);
         this.LBL_Client_FullName.TabIndex = 13;
         this.LBL_Client_FullName.Text = "Shemp";
         // 
         // BTN_Print
         // 
         this.BTN_Print.Location = new System.Drawing.Point(1114, 182);
         this.BTN_Print.Name = "BTN_Print";
         this.BTN_Print.Size = new System.Drawing.Size(115, 47);
         this.BTN_Print.TabIndex = 14;
         this.BTN_Print.Text = "Print";
         this.BTN_Print.UseVisualStyleBackColor = true;
         this.BTN_Print.Click += new System.EventHandler(this.BTN_Print_Click);
         // 
         // dataGridView2
         // 
         this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.dataGridView2.Location = new System.Drawing.Point(699, 76);
         this.dataGridView2.Name = "dataGridView2";
         this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
         this.dataGridView2.Size = new System.Drawing.Size(392, 150);
         this.dataGridView2.TabIndex = 17;
         this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
         // 
         // LBL_AdmissionKey
         // 
         this.LBL_AdmissionKey.AutoSize = true;
         this.LBL_AdmissionKey.ForeColor = System.Drawing.Color.Purple;
         this.LBL_AdmissionKey.Location = new System.Drawing.Point(874, 242);
         this.LBL_AdmissionKey.Name = "LBL_AdmissionKey";
         this.LBL_AdmissionKey.Size = new System.Drawing.Size(61, 13);
         this.LBL_AdmissionKey.TabIndex = 18;
         this.LBL_AdmissionKey.Text = "123456789";
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.ForeColor = System.Drawing.Color.Purple;
         this.label6.Location = new System.Drawing.Point(790, 243);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(78, 13);
         this.label6.TabIndex = 19;
         this.label6.Text = "Admission Key:";
         // 
         // treeView1
         // 
         this.treeView1.CheckBoxes = true;
         this.treeView1.Location = new System.Drawing.Point(38, 259);
         this.treeView1.Name = "treeView1";
         this.treeView1.Size = new System.Drawing.Size(295, 445);
         this.treeView1.TabIndex = 20;
         // 
         // pictureBox2
         // 
         this.pictureBox2.Image = global::DPGPP.Properties.Resources.printer;
         this.pictureBox2.Location = new System.Drawing.Point(674, 518);
         this.pictureBox2.Name = "pictureBox2";
         this.pictureBox2.Size = new System.Drawing.Size(288, 177);
         this.pictureBox2.TabIndex = 22;
         this.pictureBox2.TabStop = false;
         // 
         // pictureBox1
         // 
         this.pictureBox1.Image = global::DPGPP.Properties.Resources.The_Great_Gazoo;
         this.pictureBox1.Location = new System.Drawing.Point(959, 258);
         this.pictureBox1.Name = "pictureBox1";
         this.pictureBox1.Size = new System.Drawing.Size(289, 263);
         this.pictureBox1.TabIndex = 21;
         this.pictureBox1.TabStop = false;
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(1248, 24);
         this.menuStrip1.TabIndex = 23;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printerToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileToolStripMenuItem.Text = "File";
         // 
         // printerToolStripMenuItem
         // 
         this.printerToolStripMenuItem.Name = "printerToolStripMenuItem";
         this.printerToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
         this.printerToolStripMenuItem.Text = "Printer";
         this.printerToolStripMenuItem.Click += new System.EventHandler(this.printerToolStripMenuItem_Click);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1248, 730);
         this.Controls.Add(this.pictureBox2);
         this.Controls.Add(this.pictureBox1);
         this.Controls.Add(this.treeView1);
         this.Controls.Add(this.label6);
         this.Controls.Add(this.LBL_AdmissionKey);
         this.Controls.Add(this.dataGridView2);
         this.Controls.Add(this.BTN_Print);
         this.Controls.Add(this.LBL_Client_FullName);
         this.Controls.Add(this.LBL_Client_OP__DOCID);
         this.Controls.Add(this.label5);
         this.Controls.Add(this.LBL_SelectedRow);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.LBL_Rows);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.TB_FirstName);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.TB_LastName);
         this.Controls.Add(this.BTN_Search);
         this.Controls.Add(this.dataGridView1);
         this.Controls.Add(this.BTN_Tree);
         this.Controls.Add(this.menuStrip1);
         this.ForeColor = System.Drawing.SystemColors.WindowText;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MainMenuStrip = this.menuStrip1;
         this.Name = "Form1";
         this.Text = "Daniel\'s Pretty Good Printing Program";
         this.Load += new System.EventHandler(this.Form1_Load);
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button BTN_Tree;
      private System.Windows.Forms.DataGridView dataGridView1;
      private System.Windows.Forms.Button BTN_Search;
      private System.Windows.Forms.TextBox TB_LastName;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox TB_FirstName;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label LBL_Rows;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label LBL_SelectedRow;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label LBL_Client_OP__DOCID;
      private System.Windows.Forms.Label LBL_Client_FullName;
      private System.Windows.Forms.Button BTN_Print;
      private System.Windows.Forms.DataGridView dataGridView2;
      private System.Windows.Forms.Label LBL_AdmissionKey;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.TreeView treeView1;
      private System.Windows.Forms.PictureBox pictureBox1;
      private System.Windows.Forms.PictureBox pictureBox2;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem printerToolStripMenuItem;
   }
}

