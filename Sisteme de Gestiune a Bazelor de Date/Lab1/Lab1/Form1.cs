using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        private const string connectionString = "Server=localhost;Database=Tournaments;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Organizers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);

                    OrganizersDataGrid.DataSource = dataTable;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            LoadTournamentsWithCurrentOrganizerSelected();
        }

        public void LoadTournamentsWithCurrentOrganizerSelected()
        {

            string query = "SELECT TournamentID, TournamentName, StartDate, TournamentLocation, OrganizerID FROM Tournaments where OrganizerID=@org_id";

            int rowIndex = OrganizersDataGrid.CurrentCell.RowIndex;
            var idOrganizerCurent = Convert.ToInt32(OrganizersDataGrid.Rows[rowIndex].Cells["OrganizerID"].Value.ToString());


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@org_id", idOrganizerCurent);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    TournamentsDataGrid.DataSource = dataTable;

                    TournamentsDataGrid.Columns["TournamentID"].ReadOnly = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void DeleteButtonClick(object sender, EventArgs e)
        {
            DeleteTournament();

            LoadTournamentsWithCurrentOrganizerSelected();
        }

        private void DeleteTournament()
        {
            string query = "Delete from Tournaments where TournamentID = @id";

            int rowIndex = TournamentsDataGrid.CurrentCell.RowIndex;
            int idTournamentCurent = Convert.ToInt32(TournamentsDataGrid.Rows[rowIndex].Cells["TournamentID"].Value);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@id", idTournamentCurent);

                try
                {
                    connection.Open();
                    int rowsDeleted = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void UpdateButtonClick(object sender, EventArgs e)
        {
            UpdateTournament();

            LoadTournamentsWithCurrentOrganizerSelected();
        }

        private void UpdateTournament()
        {
            string query = "update Tournaments set TournamentName=@new_name, TournamentLocation=@new_location, OrganizerID=@new_organizer, StartDate=@new_date where TournamentID=@id";

            int rowIndex = TournamentsDataGrid.CurrentCell.RowIndex;
            int idTournament = Convert.ToInt32(TournamentsDataGrid.Rows[rowIndex].Cells["TournamentID"].Value);
            String newName = TournamentsDataGrid.Rows[rowIndex].Cells["TournamentName"].Value.ToString();
            String newLocation = TournamentsDataGrid.Rows[rowIndex].Cells["TournamentLocation"].Value.ToString();
            int newOrganizerID = Convert.ToInt32(TournamentsDataGrid.Rows[rowIndex].Cells["OrganizerID"].Value);
            DateTime newStartDate = Convert.ToDateTime(TournamentsDataGrid.Rows[rowIndex].Cells["StartDate"].Value);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@new_name", newName);
                sqlCommand.Parameters.AddWithValue("@new_location", newLocation);
                sqlCommand.Parameters.AddWithValue("@new_organizer", newOrganizerID);
                sqlCommand.Parameters.AddWithValue("@new_date", newStartDate);
                sqlCommand.Parameters.AddWithValue("@id", idTournament);

                try
                {
                    connection.Open();
                    int rowsUpdated = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }



        private void SaveTournament()
        {
            string query = "insert into Tournaments(TournamentName, TournamentLocation, OrganizerID, StartDate) " +
                "values (@name, @location, @organizer_ID, @start_date)";

            int rowIndex = TournamentsDataGrid.CurrentCell.RowIndex;
            String name = TournamentsDataGrid.Rows[rowIndex].Cells["TournamentName"].Value.ToString();
            String location = TournamentsDataGrid.Rows[rowIndex].Cells["TournamentLocation"].Value.ToString();
            int orgRowIndex = OrganizersDataGrid.CurrentCell.RowIndex;
            int organizerID = Convert.ToInt32(OrganizersDataGrid.Rows[orgRowIndex].Cells["OrganizerID"].Value);
            DateTime startDate = DateTime.ParseExact(TournamentsDataGrid.Rows[rowIndex].Cells["StartDate"].Value.ToString(),
        "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@name", name);
                sqlCommand.Parameters.AddWithValue("@location", location);
                sqlCommand.Parameters.AddWithValue("@organizer_ID", organizerID);
                sqlCommand.Parameters.AddWithValue("@start_date", startDate);

                try
                {
                    connection.Open();
                    int insertedRows = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            SaveTournament();

            LoadTournamentsWithCurrentOrganizerSelected();
        }

        private void LoadTournamentsWithCurrentOrganizerSelected(object sender, DataGridViewCellEventArgs e)
        {
            LoadTournamentsWithCurrentOrganizerSelected();
        }

    }
}

