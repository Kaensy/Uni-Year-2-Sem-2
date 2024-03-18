using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

using Lab1_SGBD.service;
using Lab1_SGBD.repository;
using Lab1_SGBD.validator;
using System.Data.SqlClient;
using System.Drawing;

namespace Lab1_SGBD.ui
{
    public class UI: Form
    {
        string connectionString = @"Server=DESKTOP-AU6TF80; Database=Tournaments; Integrated Security=true;";
        private readonly Service service;
        private readonly Repository repository;
        private readonly Validator validator;

        private DataGridView organizersDataGridView;
        private DataGridView tournamentsDataGridView;

        private TextBox tournamentNameTextBox;
        private DateTimePicker startDateDateTimePicker;
        private DateTimePicker endDateDateTimePicker;
        private TextBox tournamentLocationTextBox;
        private TextBox prizePoolTextBox;
        private TextBox organizerTextBox;
        private TextBox gameTextBox;

        public UI()
        {
            InitializeComponent();
            this.repository = new Repository(connectionString);
            this.service = new Service(repository);
            this.validator = new Validator();
            this.CenterToScreen();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        /* This function is an event that is triggered when the form is loaded. */
        private void UI_Load(object sender, EventArgs e)
        {
            DataTable organizersData = service.GetAllOrganizers();
            if (organizersData != null)
            {
                organizersDataGridView.DataSource = organizersData;
                if (organizersDataGridView.Rows.Count > 0)
                    organizersDataGridView.Rows[0].Selected = true;
            }
            else
            {
                MessageBox.Show("Failed to retrieve data from the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* This function is an event that is triggered when a row is selected in the parent table. */
        private void OrganizersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (organizersDataGridView.SelectedRows.Count > 0)
            {
                int selectedOrganizerId = (int)organizersDataGridView.SelectedRows[0].Cells["OrganizerID"].Value;
                DataTable relatedMovies = service.GetTournamentsByOrganizerID(selectedOrganizerId);
                tournamentsDataGridView.DataSource = relatedMovies;
            }
        }

        /* This function is an event that is triggered when the form is loaded to set up all the controls. */
        private void InitializeComponent()
        {
            this.ClientSize = new System.Drawing.Size(1520, 800);
            this.BackColor = System.Drawing.Color.LightBlue;
            this.WindowState = FormWindowState.Maximized;
            this.Name = "UI";

            this.organizersDataGridView = new DataGridView();
            this.tournamentsDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.organizersDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tournamentsDataGridView)).BeginInit();
            this.SuspendLayout();

            this.organizersDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.organizersDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.organizersDataGridView.Location = new System.Drawing.Point(30, 250);
            this.organizersDataGridView.Width = 700;
            this.organizersDataGridView.Height = 200;
            this.organizersDataGridView.Name = "organizersDataGridView";
            this.organizersDataGridView.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            this.organizersDataGridView.RowTemplate.Height = 24;
            this.organizersDataGridView.TabIndex = 0;
            // Setam modul de selectie a unei linii din organizersDataGridView
            this.organizersDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // Atribuim Eventul de selectie a unei linii din organizersDataGridView pentru popularea filmeDataGridView
            this.organizersDataGridView.SelectionChanged += OrganizersDataGridView_SelectionChanged;

            this.tournamentsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.tournamentsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tournamentsDataGridView.Location = new System.Drawing.Point(30, 500);
            this.tournamentsDataGridView.Width = 1460;
            this.tournamentsDataGridView.Height = 250;
            this.tournamentsDataGridView.Name = "tournamentsDataGridView";
            this.tournamentsDataGridView.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            this.tournamentsDataGridView.RowTemplate.Height = 24;
            this.tournamentsDataGridView.TabIndex = 1;
            // Setam modul de selectie a unei linii din tournamentsDataGridView
            this.tournamentsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.Controls.Add(this.organizersDataGridView);
            this.Controls.Add(this.tournamentsDataGridView);
            this.Load += new System.EventHandler(this.UI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.organizersDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tournamentsDataGridView)).EndInit();

            addButtons();
            addLabels();
            addTextBoxes();

            this.ResumeLayout(false);
        }

        /* Function for Button management */
        private void addButtons()
        {
            Button addButton = new Button();
            addButton.Location = new System.Drawing.Point(900, 400);
            addButton.Width = 100;
            addButton.Height = 50;
            addButton.Name = "addButton";
            addButton.TabIndex = 2;
            addButton.Text = "Add";
            addButton.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += AddButton_Click;
            this.Controls.Add(addButton);

            Button removeButton = new Button();
            removeButton.Location = new System.Drawing.Point(1100, 400);
            removeButton.Width = 100;
            removeButton.Height = 50;
            removeButton.Name = "removeButton";
            removeButton.TabIndex = 3;
            removeButton.Text = "Remove";
            removeButton.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            removeButton.UseVisualStyleBackColor = true;
            removeButton.Click += RemoveButton_Click;
            this.Controls.Add(removeButton);

            Button updateButton = new Button();
            updateButton.Location = new System.Drawing.Point(1300, 400);
            updateButton.Width = 100;
            updateButton.Height = 50;
            updateButton.Name = "updateButton";
            updateButton.TabIndex = 4;
            updateButton.Text = "Update";
            updateButton.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += UpdateButton_Click;
            this.Controls.Add(updateButton);

            Button exitButton = new Button();
            exitButton.Location = new System.Drawing.Point(1350, 750);
            exitButton.Width = 100;
            exitButton.Height = 50;
            exitButton.Name = "exitButton";
            exitButton.TabIndex = 5;
            exitButton.Text = "Exit";
            exitButton.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += ExitButton_Click;
            this.Controls.Add(exitButton);
        }

        /* Function for Label management */
        private void addLabels()
        {
            Label titluFereastra = new Label();
            titluFereastra.Location = new System.Drawing.Point(660, 20);
            titluFereastra.Width = 200;
            titluFereastra.Height = 30;
            titluFereastra.Name = "titluFereastra";
            titluFereastra.TabIndex = 1;
            titluFereastra.Text = "Aplicație Tournaments";
            titluFereastra.Font = new Font("Comic Sans MS", 18, FontStyle.Regular);
            this.Controls.Add(titluFereastra);

            Label organizersLabel = new Label();
            organizersLabel.Location = new System.Drawing.Point(30, 220);
            organizersLabel.Width = 150;
            organizersLabel.Height = 30;
            organizersLabel.Name = "organizersLabel";
            organizersLabel.TabIndex = 5;
            organizersLabel.Text = "Organizers";
            organizersLabel.Font = new Font("Comic Sans MS", 18, FontStyle.Regular);
            this.Controls.Add(organizersLabel);

            Label tournamentsLabel = new Label();
            tournamentsLabel.Location = new System.Drawing.Point(30, 470);
            tournamentsLabel.Width = 150;
            tournamentsLabel.Height = 30;
            tournamentsLabel.Name = "tournamentsLabel";
            tournamentsLabel.TabIndex = 6;
            tournamentsLabel.Text = "Tournaments";
            tournamentsLabel.Font = new Font("Comic Sans MS", 18, FontStyle.Regular);
            this.Controls.Add(tournamentsLabel);

            Label startDateLabel = new Label();
            startDateLabel.Location = new System.Drawing.Point(242, 120);
            startDateLabel.Width = 200;
            startDateLabel.Height = 30;
            startDateLabel.Name = "Start Date";
            startDateLabel.TabIndex = 15;
            startDateLabel.Text = "Start Date             ↓";
            startDateLabel.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            this.Controls.Add(startDateLabel);

            Label functionalitatiLabel = new Label();
            functionalitatiLabel.Location = new System.Drawing.Point(950, 250);
            functionalitatiLabel.Width = 400;
            functionalitatiLabel.Height = 50;
            functionalitatiLabel.TextAlign = ContentAlignment.BottomCenter;
            functionalitatiLabel.Name = "functionalitatiLabel";
            functionalitatiLabel.TabIndex = 16;
            functionalitatiLabel.Text = "Funcționalități";
            functionalitatiLabel.Font = new Font("Comic Sans MS", 24, FontStyle.Regular);
            this.Controls.Add(functionalitatiLabel);
        }

        /* Function for TextBox management*/
        private void addTextBoxes()
        {
            tournamentNameTextBox = new TextBox();
            tournamentNameTextBox.Location = new System.Drawing.Point(30, 150);
            tournamentNameTextBox.Width = 187;
            tournamentNameTextBox.Height = 30;
            tournamentNameTextBox.Name = "tournamentNameTextBox";
            tournamentNameTextBox.TabIndex = 7;
            tournamentNameTextBox.Text = "Enter Name...";
            tournamentNameTextBox.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            tournamentNameTextBox.Tag = "Enter Name...";
            tournamentNameTextBox.ForeColor = SystemColors.GrayText;
            tournamentNameTextBox.GotFocus += TextBox_GotFocus;
            tournamentNameTextBox.LostFocus += TextBox_LostFocus;
            this.Controls.Add(tournamentNameTextBox);

            startDateDateTimePicker = new DateTimePicker();
            startDateDateTimePicker.Location = new System.Drawing.Point(242, 150);
            startDateDateTimePicker.Width = 187;
            startDateDateTimePicker.Height = 30;
            startDateDateTimePicker.Name = "startDateDateTimePicker";
            startDateDateTimePicker.Format = DateTimePickerFormat.Short;
            startDateDateTimePicker.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            startDateDateTimePicker.TabIndex = 8;
            this.Controls.Add(startDateDateTimePicker);

            tournamentLocationTextBox = new TextBox();
            tournamentLocationTextBox.Location = new System.Drawing.Point(454, 150);
            tournamentLocationTextBox.Width = 187;
            tournamentLocationTextBox.Height = 30;
            tournamentLocationTextBox.Name = "tournamentLocationTextBox";
            tournamentLocationTextBox.TabIndex = 9;
            tournamentLocationTextBox.Text = "Enter Location...";
            tournamentLocationTextBox.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            tournamentLocationTextBox.Tag = "Enter Location...";
            tournamentLocationTextBox.ForeColor = SystemColors.GrayText;
            tournamentLocationTextBox.GotFocus += TextBox_GotFocus;
            tournamentLocationTextBox.LostFocus += TextBox_LostFocus;
            this.Controls.Add(tournamentLocationTextBox);

            prizePoolTextBox = new TextBox();
            prizePoolTextBox.Location = new System.Drawing.Point(666, 150);
            prizePoolTextBox.Width = 187;
            prizePoolTextBox.Height = 30;
            prizePoolTextBox.Name = "PrizePool";
            prizePoolTextBox.TabIndex = 10;
            prizePoolTextBox.Text = "Enter Prize Pool...";
            prizePoolTextBox.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            prizePoolTextBox.Tag = "Enter Prize Pool...";
            prizePoolTextBox.ForeColor = SystemColors.GrayText;
            prizePoolTextBox.GotFocus += TextBox_GotFocus;
            prizePoolTextBox.LostFocus += TextBox_LostFocus;
            this.Controls.Add(prizePoolTextBox);

            organizerTextBox = new TextBox();
            organizerTextBox.Location = new System.Drawing.Point(878, 150);
            organizerTextBox.Width = 187;
            organizerTextBox.Height = 30;
            organizerTextBox.Name = "Organizer";
            organizerTextBox.TabIndex = 11;
            organizerTextBox.Text = "Enter Organizer...";
            organizerTextBox.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            organizerTextBox.Tag = "Enter Organizer...";
            organizerTextBox.ForeColor = SystemColors.GrayText;
            organizerTextBox.GotFocus += TextBox_GotFocus;
            organizerTextBox.LostFocus += TextBox_LostFocus;
            this.Controls.Add(organizerTextBox);

            gameTextBox = new TextBox();
            gameTextBox.Location = new System.Drawing.Point(1090, 150);
            gameTextBox.Width = 187;
            gameTextBox.Height = 30;
            gameTextBox.Name = "Game";
            gameTextBox.TabIndex = 13;
            gameTextBox.Text = "Enter Game...";
            gameTextBox.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            gameTextBox.Tag = "Enter Game...";
            gameTextBox.ForeColor = SystemColors.GrayText;
            gameTextBox.GotFocus += TextBox_GotFocus;
            gameTextBox.LostFocus += TextBox_LostFocus;
            this.Controls.Add(gameTextBox);

            /*boxOfficeTextBox = new TextBox();
            boxOfficeTextBox.Location = new System.Drawing.Point(1302, 150);
            boxOfficeTextBox.Width = 187;
            boxOfficeTextBox.Height = 30;
            boxOfficeTextBox.Name = "Box Office";
            boxOfficeTextBox.TabIndex = 14;
            boxOfficeTextBox.Text = "Enter Box Office...";
            boxOfficeTextBox.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            boxOfficeTextBox.Tag = "Enter Box Office...";
            boxOfficeTextBox.ForeColor = SystemColors.GrayText;
            boxOfficeTextBox.GotFocus += TextBox_GotFocus;
            boxOfficeTextBox.LostFocus += TextBox_LostFocus;
            this.Controls.Add(boxOfficeTextBox);*/
        }

        /* Event for adding a movie in the child table when button Add is pressed */
        private void AddButton_Click(object sender, EventArgs e)
        {
            String name = this.tournamentNameTextBox.Text;
            DateTime startDate = this.startDateDateTimePicker.Value;
            DateTime endDate = this.endDateDateTimePicker.Value;
            String location = this.tournamentLocationTextBox.Text;
            String game = this.gameTextBox.Text;
            String organizer = this.organizerTextBox.Text;
            float prizePool = float.Parse(this.prizePoolTextBox.Text);

            if (organizersDataGridView.SelectedRows.Count > 0 && validator.ValidateInputs(name, startDate, endDate, game, organizer, prizePool))
            {
                int selectedOrganizerId = (int)organizersDataGridView.SelectedRows[0].Cells["OrganizerID"].Value;
                service.AddFilm(name, startDate, endDate, location, prizePool, game, selectedOrganizerId);
                tournamentsDataGridView.DataSource = service.GetTournamentsByOrganizerID(selectedOrganizerId);
            }
            else
            {
                MessageBox.Show("Please select a director from the parent table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Event for removing a movie from the child table by its id when button Remove is pressed */
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            String selectedTournamentName = (String)tournamentsDataGridView.SelectedRows[0].Cells["TournamentName"].Value;
            service.DeleteTournament(selectedTournamentName);
            tournamentsDataGridView.DataSource = service.GetTournamentsByOrganizerID((int)organizersDataGridView.SelectedRows[0].Cells["OrganizerID"].Value);
        }

        /* Event for updating a movie from the child table when button Update is pressed */
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            String name = (String)tournamentsDataGridView.SelectedRows[0].Cells["TournamentName"].Value;
            DateTime startDate = (DateTime)tournamentsDataGridView.SelectedRows[0].Cells["StartDate"].Value;
            DateTime endDate = (DateTime)tournamentsDataGridView.SelectedRows[0].Cells["EndDate"].Value;
            String location = (String)tournamentsDataGridView.SelectedRows[0].Cells["TournamentLocation"].Value;
            float prizePool;
            float.TryParse(tournamentsDataGridView.SelectedRows[0].Cells["PrizePool"].Value?.ToString(), out prizePool);
            String game = (String)tournamentsDataGridView.SelectedRows[0].Cells["GameID"].Value;
            String organizer = (String)tournamentsDataGridView.SelectedRows[0].Cells["OrganizerID"].Value;



            if (organizersDataGridView.SelectedRows.Count > 0 && validator.ValidateInputs(name, startDate, endDate, location, prizePool, game, organizer))
            {
                int selectedOrganizerID = (int)organizersDataGridView.SelectedRows[0].Cells["OrganizerID"].Value;
                service.UpdateFilm(name, startDate, endDate, location,prizePool, game, selectedOrganizerID);
                tournamentsDataGridView.DataSource = service.GetTournamentsByOrganizerID(selectedOrganizerID);
            }
            else
            {
                MessageBox.Show("Please select a director from the parent table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Event for deleting the placeholder text when the TextBox is pressed */
        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == textBox.Tag as string)
            {
                textBox.Text = "";
                textBox.ForeColor = SystemColors.WindowText;
            }
        }

        /* Event for making a placeholder text that shows while the TextBox is not pressed */
        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag as string;
                textBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
