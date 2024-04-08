using System.ComponentModel;

namespace MPP_CSharp;

partial class MainWindow
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        this.NameTextBox = new System.Windows.Forms.TextBox();
        this.AgeTextBox = new System.Windows.Forms.TextBox();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.dataGridViewChildren = new System.Windows.Forms.DataGridView();
        this.dataGridViewAges = new System.Windows.Forms.DataGridView();
        this.dataGridViewTracks = new System.Windows.Forms.DataGridView();
        this.buttonExit = new System.Windows.Forms.Button();
        this.buttonAdd = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChildren)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAges)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTracks)).BeginInit();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.Location = new System.Drawing.Point(22, 50);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(100, 23);
        this.label1.TabIndex = 0;
        this.label1.Text = "Add Child";
        // 
        // NameTextBox
        // 
        this.NameTextBox.Location = new System.Drawing.Point(22, 87);
        this.NameTextBox.Name = "NameTextBox";
        this.NameTextBox.Size = new System.Drawing.Size(100, 22);
        this.NameTextBox.TabIndex = 1;
        // 
        // AgeTextBox
        // 
        this.AgeTextBox.Location = new System.Drawing.Point(22, 125);
        this.AgeTextBox.Name = "AgeTextBox";
        this.AgeTextBox.Size = new System.Drawing.Size(100, 22);
        this.AgeTextBox.TabIndex = 2;
        // 
        // textBox1
        // 
        this.textBox1.Location = new System.Drawing.Point(22, 165);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(100, 22);
        this.textBox1.TabIndex = 3;
        // 
        // dataGridViewChildren
        // 
        this.dataGridViewChildren.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewChildren.Location = new System.Drawing.Point(152, 87);
        this.dataGridViewChildren.Name = "dataGridViewChildren";
        this.dataGridViewChildren.RowTemplate.Height = 24;
        this.dataGridViewChildren.Size = new System.Drawing.Size(431, 352);
        this.dataGridViewChildren.TabIndex = 4;
        // 
        // dataGridViewAges
        // 
        this.dataGridViewAges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewAges.Location = new System.Drawing.Point(634, 87);
        this.dataGridViewAges.Name = "dataGridViewAges";
        this.dataGridViewAges.RowTemplate.Height = 24;
        this.dataGridViewAges.Size = new System.Drawing.Size(182, 352);
        this.dataGridViewAges.TabIndex = 5;
        this.dataGridViewAges.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAges_CellContentClick);
        // 
        // dataGridViewTracks
        // 
        this.dataGridViewTracks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewTracks.Location = new System.Drawing.Point(867, 87);
        this.dataGridViewTracks.Name = "dataGridViewTracks";
        this.dataGridViewTracks.RowTemplate.Height = 24;
        this.dataGridViewTracks.Size = new System.Drawing.Size(346, 352);
        this.dataGridViewTracks.TabIndex = 6;
        this.dataGridViewTracks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTracks_CellContentClick);
        // 
        // buttonExit
        // 
        this.buttonExit.Location = new System.Drawing.Point(550, 507);
        this.buttonExit.Name = "buttonExit";
        this.buttonExit.Size = new System.Drawing.Size(150, 52);
        this.buttonExit.TabIndex = 7;
        this.buttonExit.Text = "Exit";
        this.buttonExit.UseVisualStyleBackColor = true;
        this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
        // 
        // buttonAdd
        // 
        this.buttonAdd.Location = new System.Drawing.Point(35, 229);
        this.buttonAdd.Name = "buttonAdd";
        this.buttonAdd.Size = new System.Drawing.Size(75, 23);
        this.buttonAdd.TabIndex = 8;
        this.buttonAdd.Text = "Add";
        this.buttonAdd.UseVisualStyleBackColor = true;
        this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
        // 
        // MainWindow
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1236, 632);
        this.Controls.Add(this.buttonAdd);
        this.Controls.Add(this.buttonExit);
        this.Controls.Add(this.dataGridViewTracks);
        this.Controls.Add(this.dataGridViewAges);
        this.Controls.Add(this.dataGridViewChildren);
        this.Controls.Add(this.textBox1);
        this.Controls.Add(this.AgeTextBox);
        this.Controls.Add(this.NameTextBox);
        this.Controls.Add(this.label1);
        this.Name = "MainWindow";
        this.Text = "MainWindow";
        this.Load += new System.EventHandler(this.MainWindow_Load);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChildren)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAges)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTracks)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.DataGridView dataGridViewAges;
    private System.Windows.Forms.Button buttonExit;
    private System.Windows.Forms.Button buttonAdd;

    private System.Windows.Forms.DataGridView dataGridViewTracks;

    private System.Windows.Forms.DataGridView dataGridViewChildren;

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox NameTextBox;
    private System.Windows.Forms.TextBox AgeTextBox;
    private System.Windows.Forms.TextBox textBox1;

    #endregion
}