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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PasswordPage = new System.Windows.Forms.TabPage();
            this.InterfacePage = new System.Windows.Forms.TabPage();
            this.RootDirectoryPage = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.PasswordPage);
            this.tabControl1.Controls.Add(this.InterfacePage);
            this.tabControl1.Controls.Add(this.RootDirectoryPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(582, 276);
            this.tabControl1.TabIndex = 0;
            // 
            // PasswordPage
            // 
            this.PasswordPage.Location = new System.Drawing.Point(4, 22);
            this.PasswordPage.Name = "PasswordPage";
            this.PasswordPage.Padding = new System.Windows.Forms.Padding(3);
            this.PasswordPage.Size = new System.Drawing.Size(574, 250);
            this.PasswordPage.TabIndex = 0;
            this.PasswordPage.Text = "Password";
            this.PasswordPage.UseVisualStyleBackColor = true;
            // 
            // InterfacePage
            // 
            this.InterfacePage.Location = new System.Drawing.Point(4, 22);
            this.InterfacePage.Name = "InterfacePage";
            this.InterfacePage.Padding = new System.Windows.Forms.Padding(3);
            this.InterfacePage.Size = new System.Drawing.Size(574, 250);
            this.InterfacePage.TabIndex = 1;
            this.InterfacePage.Text = "Interface";
            this.InterfacePage.UseVisualStyleBackColor = true;
            // 
            // RootDirectoryPage
            // 
            this.RootDirectoryPage.Location = new System.Drawing.Point(4, 22);
            this.RootDirectoryPage.Name = "RootDirectoryPage";
            this.RootDirectoryPage.Size = new System.Drawing.Size(574, 250);
            this.RootDirectoryPage.TabIndex = 2;
            this.RootDirectoryPage.Text = "RootDirectory";
            this.RootDirectoryPage.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 300);
            this.Controls.Add(this.tabControl1);
            this.Name = "SettingForm";
            this.Text = "SettingForm";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage PasswordPage;
        private System.Windows.Forms.TabPage InterfacePage;
        private System.Windows.Forms.TabPage RootDirectoryPage;
    }
}