using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practic2
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Server=localhost;Database=Tournaments;Integrated Security=True;";

        private readonly DataSet _dataSet = new DataSet();

        public Form1()
        {
            InitializeComponent();
        }
        
        // dont forget to add this : this.Load += new System.EventHandler(this.Form1_Load);
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var parentAdapter = new SqlDataAdapter("select * from Organizers", connection);
                    var childAdapter =
                        new SqlDataAdapter("SELECT TournamentID, TournamentName, TournamentLocation, OrganizerID, StartDate FROM Tournaments",
                            connection);

                    parentAdapter.Fill(_dataSet, "Organizers");
                    childAdapter.Fill(_dataSet, "Tournaments");

                    var parentBs = new BindingSource();
                    var childBs = new BindingSource();

                    parentBs.DataSource = _dataSet.Tables["Organizers"];
                    parentGridView.DataSource = parentBs;

                    var parentPk = _dataSet.Tables["Organizers"].Columns["OrganizerID"];
                    var childFk = _dataSet.Tables["Tournaments"].Columns["OrganizerID"];
                    var relation = new DataRelation("fk_parent_child", parentPk, childFk);
                    _dataSet.Relations.Add(relation);

                    childBs.DataSource = parentBs;
                    childBs.DataMember = "fk_parent_child";
                    childGridView.DataSource = childBs;
                    childGridView.Columns["TournamentID"]!.ReadOnly = true;
                    childGridView.Columns["OrganizerID"]!.ReadOnly = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void RefreshData()
        {
            _dataSet.Tables["Tournaments"].Clear();
            using var connection = new SqlConnection(ConnectionString);
            var adapter = new SqlDataAdapter("SELECT TournamentID, TournamentName, TournamentLocation, OrganizerID, StartDate FROM Tournaments",
                connection);
            adapter.Fill(_dataSet, "Tournaments");
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var row = childGridView.CurrentCell.RowIndex;
            var name = childGridView.Rows[row].Cells["TournamentName"].Value.ToString();
            var location = childGridView.Rows[row].Cells["TournamentLocation"].Value.ToString();
            var startDate = childGridView.Rows[row].Cells["StartDate"].Value.ToString();
            var rowParent = parentGridView.CurrentCell.RowIndex;
            var idOrganizer = parentGridView.Rows[rowParent].Cells["OrganizerID"].Value.ToString();
            

            using var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("INSERT into Tournaments(TournamentName, TournamentLocation, OrganizerID, StartDate) values (@param1, @param2, @param3, @param4)",
                connection);
            command.Parameters.AddWithValue("@param1", name);
            command.Parameters.AddWithValue("@param2", location);
            command.Parameters.AddWithValue("@param3", idOrganizer);
            command.Parameters.AddWithValue("@param4", startDate);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                RefreshData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {   
            var row = childGridView.CurrentCell.RowIndex;
            var id = Convert.ToInt32(childGridView.Rows[row].Cells["TournamentID"].Value.ToString());
            var name = childGridView.Rows[row].Cells["TournamentName"].Value.ToString();
            var location = childGridView.Rows[row].Cells["TournamentLocation"].Value.ToString();
            var idOrganizer = childGridView.Rows[row].Cells["OrganizerID"].Value.ToString();
            var startDate = childGridView.Rows[row].Cells["StartDate"].Value.ToString();

            using var connection = new SqlConnection(ConnectionString);
            var command =
                new SqlCommand(
                    "UPDATE Tournaments SET TournamentName = @param1, TournamentLocation = @param2, OrganizerID = @param3, StartDate = @param4 WHERE TournamentID = @id",
                    connection);
            command.Parameters.AddWithValue("@param1", name);
            command.Parameters.AddWithValue("@param2", location);
            command.Parameters.AddWithValue("@param3", idOrganizer);
            command.Parameters.AddWithValue("@param4", startDate);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                RefreshData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var row = childGridView.CurrentCell.RowIndex;
            var id = Convert.ToInt32(childGridView.Rows[row].Cells["TournamentID"].Value.ToString());

            using var connection = new SqlConnection(ConnectionString);
            var sqlCommand = new SqlCommand("delete from Tournaments where TournamentID = @id", connection);
            sqlCommand.Parameters.AddWithValue("id", id);
            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                RefreshData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
/*

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practic
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Server=localhost;Database=Practic;Integrated Security=True;";

        private readonly DataSet _dataSet = new DataSet();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var parentAdapter = new SqlDataAdapter("select * from Tipuri", connection);
                    var childAdapter = new SqlDataAdapter("select * from Jucarii", connection);

                    parentAdapter.Fill(_dataSet, "Tipuri");
                    childAdapter.Fill(_dataSet, "Jucarii");

                    var parentBs = new BindingSource();
                    var childBs = new BindingSource();

                    parentBs.DataSource = _dataSet.Tables["Tipuri"];
                    parentGridView.DataSource = parentBs;

                    var parentPk = _dataSet.Tables["Tipuri"].Columns["id"];
                    var childFk = _dataSet.Tables["Jucarii"].Columns["id_tip"];
                    var relation = new DataRelation("fk_tip_jucarii", parentPk, childFk);
                    _dataSet.Relations.Add(relation);

                    childBs.DataSource = parentBs;
                    childBs.DataMember = "fk_tip_jucarii";
                    childGridView.DataSource = childBs;
                    childGridView.Columns["id"]!.ReadOnly = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void RefreshData()
        {
            _dataSet.Tables["Jucarii"].Clear();
            using var connection = new SqlConnection(ConnectionString);
            var adapter = new SqlDataAdapter("select * from Jucarii", connection);
            adapter.Fill(_dataSet, "Jucarii");
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var row = childGridView.CurrentCell.RowIndex;
            var name = childGridView.Rows[row].Cells["name"].Value.ToString();
            var rowParent = parentGridView.CurrentCell.RowIndex;
            var idTip = parentGridView.Rows[rowParent].Cells["id"].Value.ToString();

            using var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("insert into Jucarii(name, id_tip) values(@name, @id_tip)", connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@id_tip", idTip);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                RefreshData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            var row = childGridView.CurrentCell.RowIndex;
            var id = childGridView.Rows[row].Cells["id"].Value.ToString();
            var name = childGridView.Rows[row].Cells["name"].Value.ToString();
            var idTip = childGridView.Rows[row].Cells["id_tip"].Value.ToString();

            using var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("update Jucarii set name = @name, id_tip = @id_tip where id = @id", connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@id_tip", idTip);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                RefreshData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var row = childGridView.CurrentCell.RowIndex;
            var id = childGridView.Rows[row].Cells["id"].Value.ToString();

            using var connection = new SqlConnection(ConnectionString);
            var sqlCommand = new SqlCommand("delete from Jucarii where id = @id", connection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                RefreshData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

*/