namespace FileManagerWithProfiles
{
    partial class SettingForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.PasswordPage = new System.Windows.Forms.TabPage();
            this.labelTitlePass = new System.Windows.Forms.Label();
            this.buttonChangePass = new System.Windows.Forms.Button();
            this.newPasLabel = new System.Windows.Forms.Label();
            this.curPasLabel = new System.Windows.Forms.Label();
            this.textBoxNewPass = new System.Windows.Forms.TextBox();
            this.textBoxCurPass = new System.Windows.Forms.TextBox();
            this.InterfacePage = new System.Windows.Forms.TabPage();
            this.labelFontColor = new System.Windows.Forms.Label();
            this.comboBoxFontColors = new System.Windows.Forms.ComboBox();
            this.labelInterfase = new System.Windows.Forms.Label();
            this.RootDirectoryPage = new System.Windows.Forms.TabPage();
            this.labelBackColor = new System.Windows.Forms.Label();
            this.comboBoxBackColor = new System.Windows.Forms.ComboBox();
            this.buttonColorsSave = new System.Windows.Forms.Button();
            this.labelRootDirectory = new System.Windows.Forms.Label();
            this.buttonChangeRootDirectory = new System.Windows.Forms.Button();
            this.textBoxRootDirectory = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.PasswordPage.SuspendLayout();
            this.InterfacePage.SuspendLayout();
            this.RootDirectoryPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.PasswordPage);
            this.tabControl.Controls.Add(this.InterfacePage);
            this.tabControl.Controls.Add(this.RootDirectoryPage);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(0, 0);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(582, 276);
            this.tabControl.TabIndex = 0;
            // 
            // PasswordPage
            // 
            this.PasswordPage.BackColor = System.Drawing.Color.White;
            this.PasswordPage.Controls.Add(this.labelTitlePass);
            this.PasswordPage.Controls.Add(this.buttonChangePass);
            this.PasswordPage.Controls.Add(this.newPasLabel);
            this.PasswordPage.Controls.Add(this.curPasLabel);
            this.PasswordPage.Controls.Add(this.textBoxNewPass);
            this.PasswordPage.Controls.Add(this.textBoxCurPass);
            this.PasswordPage.Location = new System.Drawing.Point(4, 22);
            this.PasswordPage.Name = "PasswordPage";
            this.PasswordPage.Padding = new System.Windows.Forms.Padding(3);
            this.PasswordPage.Size = new System.Drawing.Size(574, 250);
            this.PasswordPage.TabIndex = 0;
            this.PasswordPage.Text = "Password";
            // 
            // labelTitlePass
            // 
            this.labelTitlePass.AutoSize = true;
            this.labelTitlePass.Font = new System.Drawing.Font("Malgun Gothic Semilight", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitlePass.Location = new System.Drawing.Point(28, 23);
            this.labelTitlePass.Name = "labelTitlePass";
            this.labelTitlePass.Size = new System.Drawing.Size(215, 13);
            this.labelTitlePass.TabIndex = 6;
            this.labelTitlePass.Text = "Here you can change your password";
            // 
            // buttonChangePass
            // 
            this.buttonChangePass.Location = new System.Drawing.Point(28, 141);
            this.buttonChangePass.Name = "buttonChangePass";
            this.buttonChangePass.Size = new System.Drawing.Size(75, 23);
            this.buttonChangePass.TabIndex = 5;
            this.buttonChangePass.Text = "Save";
            this.buttonChangePass.UseVisualStyleBackColor = true;
            this.buttonChangePass.Click += new System.EventHandler(this.buttonChangePass_Click);
            // 
            // newPasLabel
            // 
            this.newPasLabel.AutoSize = true;
            this.newPasLabel.Location = new System.Drawing.Point(28, 99);
            this.newPasLabel.Name = "newPasLabel";
            this.newPasLabel.Size = new System.Drawing.Size(78, 13);
            this.newPasLabel.TabIndex = 4;
            this.newPasLabel.Text = "New Password";
            // 
            // curPasLabel
            // 
            this.curPasLabel.AutoSize = true;
            this.curPasLabel.Location = new System.Drawing.Point(28, 57);
            this.curPasLabel.Name = "curPasLabel";
            this.curPasLabel.Size = new System.Drawing.Size(89, 13);
            this.curPasLabel.TabIndex = 3;
            this.curPasLabel.Text = "Current password";
            // 
            // textBoxNewPass
            // 
            this.textBoxNewPass.Location = new System.Drawing.Point(28, 115);
            this.textBoxNewPass.Name = "textBoxNewPass";
            this.textBoxNewPass.PasswordChar = '*';
            this.textBoxNewPass.Size = new System.Drawing.Size(156, 20);
            this.textBoxNewPass.TabIndex = 2;
            // 
            // textBoxCurPass
            // 
            this.textBoxCurPass.ForeColor = System.Drawing.Color.Black;
            this.textBoxCurPass.Location = new System.Drawing.Point(28, 76);
            this.textBoxCurPass.Name = "textBoxCurPass";
            this.textBoxCurPass.PasswordChar = '*';
            this.textBoxCurPass.Size = new System.Drawing.Size(156, 20);
            this.textBoxCurPass.TabIndex = 1;
            // 
            // InterfacePage
            // 
            this.InterfacePage.Controls.Add(this.buttonColorsSave);
            this.InterfacePage.Controls.Add(this.labelBackColor);
            this.InterfacePage.Controls.Add(this.comboBoxBackColor);
            this.InterfacePage.Controls.Add(this.labelFontColor);
            this.InterfacePage.Controls.Add(this.comboBoxFontColors);
            this.InterfacePage.Controls.Add(this.labelInterfase);
            this.InterfacePage.Location = new System.Drawing.Point(4, 22);
            this.InterfacePage.Name = "InterfacePage";
            this.InterfacePage.Padding = new System.Windows.Forms.Padding(3);
            this.InterfacePage.Size = new System.Drawing.Size(574, 250);
            this.InterfacePage.TabIndex = 1;
            this.InterfacePage.Text = "Interface";
            this.InterfacePage.UseVisualStyleBackColor = true;
            // 
            // labelFontColor
            // 
            this.labelFontColor.AutoSize = true;
            this.labelFontColor.Font = new System.Drawing.Font("Malgun Gothic Semilight", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFontColor.Location = new System.Drawing.Point(28, 65);
            this.labelFontColor.Name = "labelFontColor";
            this.labelFontColor.Size = new System.Drawing.Size(67, 13);
            this.labelFontColor.TabIndex = 9;
            this.labelFontColor.Text = "Font color:";
            // 
            // comboBoxFontColors
            // 
            this.comboBoxFontColors.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBoxFontColors.FormattingEnabled = true;
            this.comboBoxFontColors.Location = new System.Drawing.Point(31, 81);
            this.comboBoxFontColors.Name = "comboBoxFontColors";
            this.comboBoxFontColors.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFontColors.TabIndex = 8;
            this.comboBoxFontColors.SelectedIndexChanged += new System.EventHandler(this.comboBoxFontColors_SelectedIndexChanged);
            // 
            // labelInterfase
            // 
            this.labelInterfase.AutoSize = true;
            this.labelInterfase.Font = new System.Drawing.Font("Malgun Gothic Semilight", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInterfase.Location = new System.Drawing.Point(28, 23);
            this.labelInterfase.Name = "labelInterfase";
            this.labelInterfase.Size = new System.Drawing.Size(142, 13);
            this.labelInterfase.TabIndex = 7;
            this.labelInterfase.Text = "Chouse favourite colors";
            // 
            // RootDirectoryPage
            // 
            this.RootDirectoryPage.Controls.Add(this.labelRootDirectory);
            this.RootDirectoryPage.Controls.Add(this.buttonChangeRootDirectory);
            this.RootDirectoryPage.Controls.Add(this.textBoxRootDirectory);
            this.RootDirectoryPage.Location = new System.Drawing.Point(4, 22);
            this.RootDirectoryPage.Name = "RootDirectoryPage";
            this.RootDirectoryPage.Size = new System.Drawing.Size(574, 250);
            this.RootDirectoryPage.TabIndex = 2;
            this.RootDirectoryPage.Text = "RootDirectory";
            this.RootDirectoryPage.UseVisualStyleBackColor = true;
            // 
            // labelBackColor
            // 
            this.labelBackColor.AutoSize = true;
            this.labelBackColor.Font = new System.Drawing.Font("Malgun Gothic Semilight", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBackColor.Location = new System.Drawing.Point(28, 105);
            this.labelBackColor.Name = "labelBackColor";
            this.labelBackColor.Size = new System.Drawing.Size(68, 13);
            this.labelBackColor.TabIndex = 11;
            this.labelBackColor.Text = "Back color:";
            // 
            // comboBoxBackColor
            // 
            this.comboBoxBackColor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBoxBackColor.FormattingEnabled = true;
            this.comboBoxBackColor.Location = new System.Drawing.Point(31, 121);
            this.comboBoxBackColor.Name = "comboBoxBackColor";
            this.comboBoxBackColor.Size = new System.Drawing.Size(121, 21);
            this.comboBoxBackColor.TabIndex = 10;
            this.comboBoxBackColor.SelectedIndexChanged += new System.EventHandler(this.comboBoxBackColor_SelectedIndexChanged);
            // 
            // buttonColorsSave
            // 
            this.buttonColorsSave.Location = new System.Drawing.Point(31, 148);
            this.buttonColorsSave.Name = "buttonColorsSave";
            this.buttonColorsSave.Size = new System.Drawing.Size(75, 23);
            this.buttonColorsSave.TabIndex = 12;
            this.buttonColorsSave.Text = "Save";
            this.buttonColorsSave.UseVisualStyleBackColor = true;
            this.buttonColorsSave.Click += new System.EventHandler(this.buttonColorsSave_Click);
            // 
            // labelRootDirectory
            // 
            this.labelRootDirectory.AutoSize = true;
            this.labelRootDirectory.Font = new System.Drawing.Font("Malgun Gothic Semilight", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRootDirectory.Location = new System.Drawing.Point(28, 23);
            this.labelRootDirectory.Name = "labelRootDirectory";
            this.labelRootDirectory.Size = new System.Drawing.Size(133, 13);
            this.labelRootDirectory.TabIndex = 12;
            this.labelRootDirectory.Text = "Change root directory";
            // 
            // buttonChangeRootDirectory
            // 
            this.buttonChangeRootDirectory.Location = new System.Drawing.Point(31, 113);
            this.buttonChangeRootDirectory.Name = "buttonChangeRootDirectory";
            this.buttonChangeRootDirectory.Size = new System.Drawing.Size(75, 23);
            this.buttonChangeRootDirectory.TabIndex = 11;
            this.buttonChangeRootDirectory.Text = "Save";
            this.buttonChangeRootDirectory.UseVisualStyleBackColor = true;
            this.buttonChangeRootDirectory.Click += new System.EventHandler(this.buttonChangeRootDirectory_Click);
            // 
            // textBoxRootDirectory
            // 
            this.textBoxRootDirectory.Location = new System.Drawing.Point(31, 87);
            this.textBoxRootDirectory.Name = "textBoxRootDirectory";
            this.textBoxRootDirectory.Size = new System.Drawing.Size(156, 20);
            this.textBoxRootDirectory.TabIndex = 8;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(606, 300);
            this.Controls.Add(this.tabControl);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "SettingForm";
            this.Text = "SettingForm";
            this.tabControl.ResumeLayout(false);
            this.PasswordPage.ResumeLayout(false);
            this.PasswordPage.PerformLayout();
            this.InterfacePage.ResumeLayout(false);
            this.InterfacePage.PerformLayout();
            this.RootDirectoryPage.ResumeLayout(false);
            this.RootDirectoryPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage PasswordPage;
        private System.Windows.Forms.TabPage InterfacePage;
        private System.Windows.Forms.TabPage RootDirectoryPage;
        private System.Windows.Forms.Label labelTitlePass;
        private System.Windows.Forms.Button buttonChangePass;
        private System.Windows.Forms.Label newPasLabel;
        private System.Windows.Forms.Label curPasLabel;
        private System.Windows.Forms.TextBox textBoxNewPass;
        private System.Windows.Forms.TextBox textBoxCurPass;
        private System.Windows.Forms.Label labelInterfase;
        private System.Windows.Forms.ComboBox comboBoxFontColors;
        private System.Windows.Forms.Label labelFontColor;
        private System.Windows.Forms.Label labelBackColor;
        private System.Windows.Forms.ComboBox comboBoxBackColor;
        private System.Windows.Forms.Button buttonColorsSave;
        private System.Windows.Forms.Label labelRootDirectory;
        private System.Windows.Forms.Button buttonChangeRootDirectory;
        private System.Windows.Forms.TextBox textBoxRootDirectory;
    }
}