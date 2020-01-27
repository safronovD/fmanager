namespace FileManagerWithProfiles
{
    partial class AutentificationForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonIn = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonGuest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(132, 65);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(156, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(132, 91);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(156, 20);
            this.textBox2.TabIndex = 1;
            // 
            // buttonIn
            // 
            this.buttonIn.Location = new System.Drawing.Point(132, 117);
            this.buttonIn.Name = "buttonIn";
            this.buttonIn.Size = new System.Drawing.Size(75, 23);
            this.buttonIn.TabIndex = 2;
            this.buttonIn.Text = "Sign In";
            this.buttonIn.UseVisualStyleBackColor = true;
            this.buttonIn.Click += new System.EventHandler(this.buttonIn_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Location = new System.Drawing.Point(213, 117);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(75, 23);
            this.buttonUp.TabIndex = 3;
            this.buttonUp.Text = "Sign Up";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonGuest
            // 
            this.buttonGuest.Location = new System.Drawing.Point(132, 170);
            this.buttonGuest.Name = "buttonGuest";
            this.buttonGuest.Size = new System.Drawing.Size(156, 23);
            this.buttonGuest.TabIndex = 4;
            this.buttonGuest.Text = "Guest Mode";
            this.buttonGuest.UseVisualStyleBackColor = true;
            this.buttonGuest.Click += new System.EventHandler(this.buttonGuest_Click);
            // 
            // AutentificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 228);
            this.Controls.Add(this.buttonGuest);
            this.Controls.Add(this.buttonUp);
            this.Controls.Add(this.buttonIn);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "AutentificationForm";
            this.Text = "Autentification";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonIn;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonGuest;
    }
}

