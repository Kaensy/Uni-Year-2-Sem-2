using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_SGBD.repository
{
    
    public class Repository
    {
        private readonly String connectionString;
        public Repository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable GetAllOrganizers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Organizers";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public DataTable GetTournamentsByOrganizerID(int parentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TournamentName, StartDate, EndDate, TournamentLocation, PrizePool, GameID FROM Tournaments WHERE OrganizerID = @parentId";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@parentId", parentId);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public void AddTournament(String TournamentName, DateTime StartDate, DateTime EndDate, String TournamentLocation, float PrizePool, int GameID, int OrganizerID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Filme (TournamentName, StartDate, EndDate, TournamentLocation, PrizePool, GameID, OrganizerID) VALUES (@TournamentName, @StartDate, @EndDate, @TournamentLocation, @PrizePool, @GameID, @OrganizerID)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TournamentName", TournamentName);
                command.Parameters.AddWithValue("@StartDate", StartDate);
                command.Parameters.AddWithValue("@EndDate", EndDate);
                command.Parameters.AddWithValue("@TournamentLocation", TournamentLocation);
                command.Parameters.AddWithValue("@PrizePool", PrizePool);
                command.Parameters.AddWithValue("@GameID", GameID);
                command.Parameters.AddWithValue("@OrganizerID", OrganizerID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateTournament(String TournamentName, DateTime StartDate, DateTime EndDate, String TournamentLocation, float PrizePool, int GameID, int OrganizerID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Tournaments SET StartDate = @StartDate, EndDate = @EndDate, TournamentLocation = @TournamentLocation, PrizePool = @PrizePool, GameID = @GameID WHERE TournamentName = @TournamentName AND OrganizerID = @OrganizerID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TournamentName", TournamentName);
                command.Parameters.AddWithValue("@StartDate", StartDate);
                command.Parameters.AddWithValue("@EndDate", EndDate);
                command.Parameters.AddWithValue("@TournamentLocation", TournamentLocation);
                command.Parameters.AddWithValue("@PrizePool", PrizePool);
                command.Parameters.AddWithValue("@GameID", GameID);
                command.Parameters.AddWithValue("@OrganizerID", OrganizerID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteTournament(String TournamentName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Tournaments WHERE TournamentName = @TournamentName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TournamentName", TournamentName);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
