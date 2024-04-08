namespace MPP_CSharp
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.LogInbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(215, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username\r\n";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(215, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password\r\n";
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.Location = new System.Drawing.Point(349, 160);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(100, 22);
            this.UsernameTextBox.TabIndex = 2;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(349, 217);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(100, 22);
            this.PasswordTextBox.TabIndex = 3;
            // 
            // LogInbutton
            // 
            this.LogInbutton.Location = new System.Drawing.Point(282, 289);
            this.LogInbutton.Name = "LogInbutton";
            this.LogInbutton.Size = new System.Drawing.Size(75, 23);
            this.LogInbutton.TabIndex = 4;
            this.LogInbutton.Text = "Log In";
            this.LogInbutton.UseVisualStyleBackColor = true;
            this.LogInbutton.Click += new System.EventHandler(this.LogInbutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LogInbutton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.UsernameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button LogInbutton;

        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}