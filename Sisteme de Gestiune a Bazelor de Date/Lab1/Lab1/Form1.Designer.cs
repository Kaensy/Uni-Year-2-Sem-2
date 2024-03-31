namespace Lab1
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
            this.TournamentsDataGrid = new System.Windows.Forms.DataGridView();
            this.OrganizersDataGrid = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TournamentsDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrganizersDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TournamentsDataGrid
            // 
            this.TournamentsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TournamentsDataGrid.Location = new System.Drawing.Point(547, 50);
            this.TournamentsDataGrid.Name = "TournamentsDataGrid";
            this.TournamentsDataGrid.RowHeadersWidth = 62;
            this.TournamentsDataGrid.RowTemplate.Height = 28;
            this.TournamentsDataGrid.Size = new System.Drawing.Size(518, 349);
            this.TournamentsDataGrid.TabIndex = 0;
            // 
            // OrganizersDataGrid
            // 
            this.OrganizersDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OrganizersDataGrid.Location = new System.Drawing.Point(27, 50);
            this.OrganizersDataGrid.Name = "OrganizersDataGrid";
            this.OrganizersDataGrid.RowHeadersVisible = false;
            this.OrganizersDataGrid.RowHeadersWidth = 62;
            this.OrganizersDataGrid.RowTemplate.Height = 28;
            this.OrganizersDataGrid.Size = new System.Drawing.Size(431, 257);
            this.OrganizersDataGrid.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(177, 503);
            this.button1.Name = "Save";
            this.button1.Size = new System.Drawing.Size(102, 52);
            this.button1.TabIndex = 2;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SaveButtonClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(443, 502);
            this.button2.Name = "Update";
            this.button2.Size = new System.Drawing.Size(90, 53);
            this.button2.TabIndex = 3;
            this.button2.Text = "Update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.UpdateButtonClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(724, 501);
            this.button3.Name = "Delete";
            this.button3.Size = new System.Drawing.Size(96, 54);
            this.button3.TabIndex = 4;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.DeleteButtonClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 629);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.OrganizersDataGrid);
            this.Controls.Add(this.TournamentsDataGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TournamentsDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrganizersDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView TournamentsDataGrid;
        private System.Windows.Forms.DataGridView OrganizersDataGrid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

