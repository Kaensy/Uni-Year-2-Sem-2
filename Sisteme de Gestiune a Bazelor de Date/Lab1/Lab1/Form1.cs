using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
        private DataSet _dataSet = new DataSet();
        
        
        private const string connectionString = "Server=localhost;Database=Tournaments;Integrated Security=True;";
        private string formTitle;
        private string formWidth;
        private string formHeight;
        private string tableParentName;
        private string tableChildName;
        private string SelectCommandParent;
        private string SelectCommandChild;
        private string InsertCommandChild;
        private string UpdateCommandChild;
        private string DeleteCommandChild;
        private string tableParentPK;
        private string tableChildPK;
        private string tableChildFK;
        
        public Form1()
        {
            InitializeComponent();
            tableParentName = ConfigurationSettings.AppSettings["tableParentName"];
            tableChildName = ConfigurationSettings.AppSettings["tableChildName"];
            SelectCommandParent = ConfigurationSettings.AppSettings["SelectCommandParent"];
            SelectCommandChild = ConfigurationSettings.AppSettings["SelectCommandChild"];
            InsertCommandChild = ConfigurationSettings.AppSettings["InsertCommandChild"];
            UpdateCommandChild = ConfigurationSettings.AppSettings["UpdateCommandChild"];
            DeleteCommandChild = ConfigurationSettings.AppSettings["DeleteCommandChild"];
            tableParentPK = ConfigurationSettings.AppSettings["tableParentPK"];
            tableChildPK = ConfigurationSettings.AppSettings["tableChildPK"];
            tableChildFK = ConfigurationSettings.AppSettings["tableChildFK"];
            
            formTitle = ConfigurationSettings.AppSettings["formTitle"];
            this.Text = formTitle;
            formWidth = ConfigurationSettings.AppSettings["formWidth"];
            this.Width = Convert.ToInt32(formWidth);
            formHeight = ConfigurationSettings.AppSettings["formHeight"];
            this.Height = Convert.ToInt32(formHeight);
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var adapterParent = new SqlDataAdapter(SelectCommandParent, connection);
                    var adapterChild = new SqlDataAdapter(SelectCommandChild, connection);

                    adapterParent.Fill(_dataSet, tableParentName);
                    adapterChild.Fill(_dataSet, tableChildName);

                    var parentBs = new BindingSource();
                    var childBs = new BindingSource();

                    parentBs.DataSource = _dataSet.Tables[tableParentName];

                    OrganizersDataGrid.DataSource = parentBs;

                    var parentPk = _dataSet.Tables[tableParentName].Columns[tableParentPK];
                    var childFk = _dataSet.Tables[tableChildName].Columns[tableChildFK];
                    var relation = new DataRelation("fk_parent_child", parentPk, childFk);
                    _dataSet.Relations.Add(relation);

                    childBs.DataSource = parentBs;
                    childBs.DataMember = "fk_parent_child";
                    TournamentsDataGrid.DataSource = childBs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error: "+ ex.Message);
            }
        }

        private void DeleteButtonClick(object sender, EventArgs e)
        {
            DeleteTournament();
        }

        private void DeleteTournament()
        {
            var rowIndex = TournamentsDataGrid.CurrentCell.RowIndex;
            var idCurent = Convert.ToInt32(TournamentsDataGrid.Rows[rowIndex].Cells[tableChildPK].Value);
            
            using (var connection = new SqlConnection(connectionString))
            {
                var sqlCommand = new SqlCommand(DeleteCommandChild, connection);
                sqlCommand.Parameters.AddWithValue("@id", idCurent);

                try
                {
                    connection.Open();
                    var rowsDeleted = sqlCommand.ExecuteNonQuery();
                    
                    RefreshDate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Error: " + ex.Message);
                }
            }
        }


        private void UpdateButtonClick(object sender, EventArgs e)
        {
            UpdateTournament();
            
        }
        
        private void UpdateTournament()
        {
            var rowIndex = TournamentsDataGrid.CurrentCell.RowIndex;
            var idCurent = Convert.ToInt32(TournamentsDataGrid.Rows[rowIndex].Cells[tableChildPK].Value);
            
            using (var connection = new SqlConnection(connectionString))
            {
                var sqlCommand = new SqlCommand(UpdateCommandChild, connection);

                for (var i = 0; i < TournamentsDataGrid.Columns.Count; i++)
                {
                    if (TournamentsDataGrid.Columns[i].Name != tableChildPK)
                    {
                        sqlCommand.Parameters.AddWithValue($"@param{i}", TournamentsDataGrid.Rows[rowIndex].Cells[i].Value.ToString());
                    }
                }
                
                sqlCommand.Parameters.AddWithValue("@id", idCurent);

                try {
                    connection.Open();
                    var rowsUpdated = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Error: " + ex.Message);
                }
            }
        }

        private void SaveTournament()
        {
            var rowIndex = TournamentsDataGrid.CurrentCell.RowIndex;
            using (var connection = new SqlConnection(connectionString))
            {
                var sqlCommand = new SqlCommand(InsertCommandChild, connection);
                
                for (var i = 0; i < TournamentsDataGrid.ColumnCount; i++)
                {
                    if (TournamentsDataGrid.Columns[i].Name != tableChildPK)
                    {
                        sqlCommand.Parameters.AddWithValue($"@param{i}", TournamentsDataGrid.Rows[rowIndex].Cells[i].Value.ToString());
                    }
                }

                try
                {
                    connection.Open();
                    var rowsInserted = sqlCommand.ExecuteNonQuery();

                    RefreshDate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Error: " + ex.Message);
                }
            }
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            SaveTournament();
        }

        private void RefreshDate()
        {
            _dataSet.Tables[tableChildName].Clear();
            using (var connection = new SqlConnection(connectionString))
            {
                var adapter = new SqlDataAdapter(SelectCommandChild, connection);
                adapter.Fill(_dataSet, tableChildName);
            }
        }
        
    }
}

