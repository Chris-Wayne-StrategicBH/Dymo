namespace RPGPP
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
         this.DGV_Clients = new System.Windows.Forms.DataGridView();
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
         this.DGV_Admissions = new System.Windows.Forms.DataGridView();
         this.LBL_AdmissionKey = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.lblStatus = new System.Windows.Forms.Label();
         this.btnCancel = new System.Windows.Forms.Button();
         this.progressBar1 = new System.Windows.Forms.ProgressBar();
         this.btnExit = new System.Windows.Forms.Button();
         this.DGV_Prescriptions = new System.Windows.Forms.DataGridView();
         this.P_Clients = new System.Windows.Forms.Panel();
         this.CB_Active = new System.Windows.Forms.CheckBox();
         this.PB_Client = new System.Windows.Forms.PictureBox();
         this.P_Admissions = new System.Windows.Forms.Panel();
         this.LBL_Program = new System.Windows.Forms.Label();
         this.P_Prescription = new System.Windows.Forms.Panel();
         this.P_Button = new System.Windows.Forms.Panel();
         this.BTN_SelectAll = new System.Windows.Forms.Button();
         this.LBL_Selected = new System.Windows.Forms.Label();
         this.label9 = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.DGV_Clients)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.DGV_Admissions)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.DGV_Prescriptions)).BeginInit();
         this.P_Clients.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.PB_Client)).BeginInit();
         this.P_Admissions.SuspendLayout();
         this.P_Prescription.SuspendLayout();
         this.P_Button.SuspendLayout();
         this.SuspendLayout();
         // 
         // DGV_Clients
         // 
         this.DGV_Clients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.DGV_Clients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.DGV_Clients.Location = new System.Drawing.Point(11, 53);
         this.DGV_Clients.Name = "DGV_Clients";
         this.DGV_Clients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
         this.DGV_Clients.Size = new System.Drawing.Size(538, 197);
         this.DGV_Clients.TabIndex = 1;
         this.DGV_Clients.SelectionChanged += new System.EventHandler(this.DGV_Clients_SelectionChanged);
         // 
         // TB_LastName
         // 
         this.TB_LastName.Location = new System.Drawing.Point(89, 23);
         this.TB_LastName.Name = "TB_LastName";
         this.TB_LastName.Size = new System.Drawing.Size(221, 20);
         this.TB_LastName.TabIndex = 3;
         this.TB_LastName.Enter += new System.EventHandler(this.TB_LastName_Enter);
         this.TB_LastName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_LastName_KeyDown);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label1.Location = new System.Drawing.Point(4, 23);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(86, 20);
         this.label1.TabIndex = 4;
         this.label1.Text = "Last Name";
         // 
         // TB_FirstName
         // 
         this.TB_FirstName.Location = new System.Drawing.Point(410, 23);
         this.TB_FirstName.Name = "TB_FirstName";
         this.TB_FirstName.Size = new System.Drawing.Size(221, 20);
         this.TB_FirstName.TabIndex = 5;
         this.TB_FirstName.Enter += new System.EventHandler(this.TB_FirstName_Enter);
         this.TB_FirstName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_FirstName_KeyDown);
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label2.Location = new System.Drawing.Point(320, 23);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(86, 20);
         this.label2.TabIndex = 6;
         this.label2.Text = "First Name";
         // 
         // label3
         // 
         this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(8, 267);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(64, 13);
         this.label3.TabIndex = 7;
         this.label3.Text = "Total Rows:";
         // 
         // LBL_Rows
         // 
         this.LBL_Rows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.LBL_Rows.AutoSize = true;
         this.LBL_Rows.Location = new System.Drawing.Point(75, 267);
         this.LBL_Rows.Name = "LBL_Rows";
         this.LBL_Rows.Size = new System.Drawing.Size(13, 13);
         this.LBL_Rows.TabIndex = 8;
         this.LBL_Rows.Text = "0";
         // 
         // label4
         // 
         this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(108, 267);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(77, 13);
         this.label4.TabIndex = 9;
         this.label4.Text = "Selected Row:";
         // 
         // LBL_SelectedRow
         // 
         this.LBL_SelectedRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.LBL_SelectedRow.AutoSize = true;
         this.LBL_SelectedRow.Location = new System.Drawing.Point(191, 267);
         this.LBL_SelectedRow.Name = "LBL_SelectedRow";
         this.LBL_SelectedRow.Size = new System.Drawing.Size(13, 13);
         this.LBL_SelectedRow.TabIndex = 10;
         this.LBL_SelectedRow.Text = "0";
         // 
         // label5
         // 
         this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(214, 267);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(81, 13);
         this.label5.TabIndex = 11;
         this.label5.Text = "Selected Client:";
         // 
         // LBL_Client_OP__DOCID
         // 
         this.LBL_Client_OP__DOCID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.LBL_Client_OP__DOCID.AutoSize = true;
         this.LBL_Client_OP__DOCID.Location = new System.Drawing.Point(296, 267);
         this.LBL_Client_OP__DOCID.Name = "LBL_Client_OP__DOCID";
         this.LBL_Client_OP__DOCID.Size = new System.Drawing.Size(13, 13);
         this.LBL_Client_OP__DOCID.TabIndex = 12;
         this.LBL_Client_OP__DOCID.Text = "0";
         // 
         // LBL_Client_FullName
         // 
         this.LBL_Client_FullName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.LBL_Client_FullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.LBL_Client_FullName.ForeColor = System.Drawing.Color.Purple;
         this.LBL_Client_FullName.Location = new System.Drawing.Point(492, 248);
         this.LBL_Client_FullName.Name = "LBL_Client_FullName";
         this.LBL_Client_FullName.Size = new System.Drawing.Size(217, 33);
         this.LBL_Client_FullName.TabIndex = 13;
         this.LBL_Client_FullName.Text = "Shemp";
         this.LBL_Client_FullName.TextAlign = System.Drawing.ContentAlignment.BottomRight;
         // 
         // BTN_Print
         // 
         this.BTN_Print.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.BTN_Print.Location = new System.Drawing.Point(44, 70);
         this.BTN_Print.Name = "BTN_Print";
         this.BTN_Print.Size = new System.Drawing.Size(115, 47);
         this.BTN_Print.TabIndex = 14;
         this.BTN_Print.Text = "Print";
         this.BTN_Print.UseVisualStyleBackColor = true;
         this.BTN_Print.Click += new System.EventHandler(this.BTN_Print_Click);
         // 
         // DGV_Admissions
         // 
         this.DGV_Admissions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.DGV_Admissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.DGV_Admissions.Location = new System.Drawing.Point(25, 29);
         this.DGV_Admissions.Name = "DGV_Admissions";
         this.DGV_Admissions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
         this.DGV_Admissions.Size = new System.Drawing.Size(448, 221);
         this.DGV_Admissions.TabIndex = 17;
         this.DGV_Admissions.SelectionChanged += new System.EventHandler(this.DGV_Admissions_SelectionChanged);
         // 
         // LBL_AdmissionKey
         // 
         this.LBL_AdmissionKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.LBL_AdmissionKey.AutoSize = true;
         this.LBL_AdmissionKey.ForeColor = System.Drawing.Color.Purple;
         this.LBL_AdmissionKey.Location = new System.Drawing.Point(89, 265);
         this.LBL_AdmissionKey.Name = "LBL_AdmissionKey";
         this.LBL_AdmissionKey.Size = new System.Drawing.Size(61, 13);
         this.LBL_AdmissionKey.TabIndex = 18;
         this.LBL_AdmissionKey.Text = "123456789";
         // 
         // label6
         // 
         this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label6.AutoSize = true;
         this.label6.ForeColor = System.Drawing.Color.Purple;
         this.label6.Location = new System.Drawing.Point(5, 265);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(78, 13);
         this.label6.TabIndex = 19;
         this.label6.Text = "Admission Key:";
         // 
         // lblStatus
         // 
         this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.lblStatus.AutoSize = true;
         this.lblStatus.Location = new System.Drawing.Point(29, 638);
         this.lblStatus.Name = "lblStatus";
         this.lblStatus.Size = new System.Drawing.Size(37, 13);
         this.lblStatus.TabIndex = 24;
         this.lblStatus.Text = "Status";
         // 
         // btnCancel
         // 
         this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.btnCancel.Location = new System.Drawing.Point(44, 135);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size(115, 35);
         this.btnCancel.TabIndex = 25;
         this.btnCancel.Text = "Cancel";
         this.btnCancel.UseVisualStyleBackColor = true;
         this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
         // 
         // progressBar1
         // 
         this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.progressBar1.Location = new System.Drawing.Point(13, 273);
         this.progressBar1.Name = "progressBar1";
         this.progressBar1.Size = new System.Drawing.Size(183, 23);
         this.progressBar1.TabIndex = 26;
         // 
         // btnExit
         // 
         this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.btnExit.Location = new System.Drawing.Point(44, 206);
         this.btnExit.Name = "btnExit";
         this.btnExit.Size = new System.Drawing.Size(115, 39);
         this.btnExit.TabIndex = 27;
         this.btnExit.Text = "Exit";
         this.btnExit.UseVisualStyleBackColor = true;
         this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
         // 
         // DGV_Prescriptions
         // 
         this.DGV_Prescriptions.AllowUserToOrderColumns = true;
         this.DGV_Prescriptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.DGV_Prescriptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.DGV_Prescriptions.Location = new System.Drawing.Point(11, 16);
         this.DGV_Prescriptions.Name = "DGV_Prescriptions";
         this.DGV_Prescriptions.Size = new System.Drawing.Size(979, 298);
         this.DGV_Prescriptions.TabIndex = 28;
         this.DGV_Prescriptions.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_Prescriptions_OnCellValueChanged);
         this.DGV_Prescriptions.CurrentCellDirtyStateChanged += new System.EventHandler(this.DGV_Prescriptions_CurrentCellDirtyStateChanged);
         // 
         // P_Clients
         // 
         this.P_Clients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.P_Clients.BackColor = System.Drawing.SystemColors.ActiveCaption;
         this.P_Clients.Controls.Add(this.CB_Active);
         this.P_Clients.Controls.Add(this.PB_Client);
         this.P_Clients.Controls.Add(this.label1);
         this.P_Clients.Controls.Add(this.TB_LastName);
         this.P_Clients.Controls.Add(this.label2);
         this.P_Clients.Controls.Add(this.TB_FirstName);
         this.P_Clients.Controls.Add(this.DGV_Clients);
         this.P_Clients.Controls.Add(this.label3);
         this.P_Clients.Controls.Add(this.LBL_Rows);
         this.P_Clients.Controls.Add(this.label4);
         this.P_Clients.Controls.Add(this.LBL_SelectedRow);
         this.P_Clients.Controls.Add(this.label5);
         this.P_Clients.Controls.Add(this.LBL_Client_OP__DOCID);
         this.P_Clients.Controls.Add(this.LBL_Client_FullName);
         this.P_Clients.Location = new System.Drawing.Point(12, 12);
         this.P_Clients.Name = "P_Clients";
         this.P_Clients.Size = new System.Drawing.Size(713, 284);
         this.P_Clients.TabIndex = 33;
         // 
         // CB_Active
         // 
         this.CB_Active.AutoSize = true;
         this.CB_Active.Location = new System.Drawing.Point(178, 4);
         this.CB_Active.Name = "CB_Active";
         this.CB_Active.Size = new System.Drawing.Size(56, 17);
         this.CB_Active.TabIndex = 16;
         this.CB_Active.Text = "Active";
         this.CB_Active.UseVisualStyleBackColor = true;
         // 
         // PB_Client
         // 
         this.PB_Client.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.PB_Client.Location = new System.Drawing.Point(555, 53);
         this.PB_Client.Name = "PB_Client";
         this.PB_Client.Size = new System.Drawing.Size(155, 196);
         this.PB_Client.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.PB_Client.TabIndex = 14;
         this.PB_Client.TabStop = false;
         // 
         // P_Admissions
         // 
         this.P_Admissions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.P_Admissions.BackColor = System.Drawing.Color.Thistle;
         this.P_Admissions.Controls.Add(this.LBL_Program);
         this.P_Admissions.Controls.Add(this.DGV_Admissions);
         this.P_Admissions.Controls.Add(this.label6);
         this.P_Admissions.Controls.Add(this.LBL_AdmissionKey);
         this.P_Admissions.Location = new System.Drawing.Point(731, 12);
         this.P_Admissions.Name = "P_Admissions";
         this.P_Admissions.Size = new System.Drawing.Size(505, 284);
         this.P_Admissions.TabIndex = 34;
         // 
         // LBL_Program
         // 
         this.LBL_Program.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.LBL_Program.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.LBL_Program.ForeColor = System.Drawing.Color.Purple;
         this.LBL_Program.Location = new System.Drawing.Point(204, 254);
         this.LBL_Program.Name = "LBL_Program";
         this.LBL_Program.Size = new System.Drawing.Size(297, 26);
         this.LBL_Program.TabIndex = 20;
         this.LBL_Program.Text = "123456789";
         this.LBL_Program.TextAlign = System.Drawing.ContentAlignment.BottomRight;
         // 
         // P_Prescription
         // 
         this.P_Prescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.P_Prescription.BackColor = System.Drawing.Color.SlateGray;
         this.P_Prescription.Controls.Add(this.P_Button);
         this.P_Prescription.Controls.Add(this.LBL_Selected);
         this.P_Prescription.Controls.Add(this.label9);
         this.P_Prescription.Controls.Add(this.DGV_Prescriptions);
         this.P_Prescription.Location = new System.Drawing.Point(12, 300);
         this.P_Prescription.Name = "P_Prescription";
         this.P_Prescription.Size = new System.Drawing.Size(1224, 334);
         this.P_Prescription.TabIndex = 35;
         // 
         // P_Button
         // 
         this.P_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.P_Button.BackColor = System.Drawing.Color.LightSlateGray;
         this.P_Button.Controls.Add(this.BTN_SelectAll);
         this.P_Button.Controls.Add(this.BTN_Print);
         this.P_Button.Controls.Add(this.btnCancel);
         this.P_Button.Controls.Add(this.btnExit);
         this.P_Button.Controls.Add(this.progressBar1);
         this.P_Button.Location = new System.Drawing.Point(1008, 16);
         this.P_Button.Name = "P_Button";
         this.P_Button.Size = new System.Drawing.Size(200, 298);
         this.P_Button.TabIndex = 31;
         // 
         // BTN_SelectAll
         // 
         this.BTN_SelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.BTN_SelectAll.Location = new System.Drawing.Point(44, 15);
         this.BTN_SelectAll.Name = "BTN_SelectAll";
         this.BTN_SelectAll.Size = new System.Drawing.Size(115, 38);
         this.BTN_SelectAll.TabIndex = 28;
         this.BTN_SelectAll.Text = "Select All";
         this.BTN_SelectAll.UseVisualStyleBackColor = true;
         this.BTN_SelectAll.Click += new System.EventHandler(this.BTN_SelectAll_Click);
         // 
         // LBL_Selected
         // 
         this.LBL_Selected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.LBL_Selected.AutoSize = true;
         this.LBL_Selected.Location = new System.Drawing.Point(102, 317);
         this.LBL_Selected.Name = "LBL_Selected";
         this.LBL_Selected.Size = new System.Drawing.Size(13, 13);
         this.LBL_Selected.TabIndex = 30;
         this.LBL_Selected.Text = "0";
         // 
         // label9
         // 
         this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label9.AutoSize = true;
         this.label9.Location = new System.Drawing.Point(46, 317);
         this.label9.Name = "label9";
         this.label9.Size = new System.Drawing.Size(49, 13);
         this.label9.TabIndex = 29;
         this.label9.Text = "Selected";
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.SteelBlue;
         this.ClientSize = new System.Drawing.Size(1248, 656);
         this.Controls.Add(this.P_Prescription);
         this.Controls.Add(this.P_Admissions);
         this.Controls.Add(this.P_Clients);
         this.Controls.Add(this.lblStatus);
         this.ForeColor = System.Drawing.SystemColors.WindowText;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "Form1";
         this.Text = "Regime Prescription Printing";
         ((System.ComponentModel.ISupportInitialize)(this.DGV_Clients)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.DGV_Admissions)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.DGV_Prescriptions)).EndInit();
         this.P_Clients.ResumeLayout(false);
         this.P_Clients.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.PB_Client)).EndInit();
         this.P_Admissions.ResumeLayout(false);
         this.P_Admissions.PerformLayout();
         this.P_Prescription.ResumeLayout(false);
         this.P_Prescription.PerformLayout();
         this.P_Button.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.DataGridView DGV_Clients;
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
      private System.Windows.Forms.DataGridView DGV_Admissions;
      private System.Windows.Forms.Label LBL_AdmissionKey;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label lblStatus;
      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.ProgressBar progressBar1;
      private System.Windows.Forms.Button btnExit;
      private System.Windows.Forms.DataGridView DGV_Prescriptions;
      private System.Windows.Forms.Panel P_Clients;
      private System.Windows.Forms.PictureBox PB_Client;
      private System.Windows.Forms.Panel P_Admissions;
      private System.Windows.Forms.Label LBL_Program;
      private System.Windows.Forms.Panel P_Prescription;
      private System.Windows.Forms.Label LBL_Selected;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.Panel P_Button;
      private System.Windows.Forms.Button BTN_SelectAll;
      private System.Windows.Forms.CheckBox CB_Active;
   }
}

