using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryDB
{
    public partial class Form1 : Form
    {
        // Replace with your actual connection string
        private string connectionString = "Data Source=YourServerName;Initial Catalog=YourDatabaseName;User ID=YourUsername;Password=YourPassword;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load data when the form loads
            LoadBooks();
        }

        private void LoadBooks()
        {
            string query = "SELECT * FROM Books";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvBooks.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading books: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Example of querying data
            string authorToSearch = txtauthor.Text.Trim();

            if (string.IsNullOrEmpty(authorToSearch))
            {
                MessageBox.Show("Please enter an author name to search.");
                return;
            }

            string query = "SELECT * FROM Books WHERE Author LIKE @Author";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@Author", "%" + authorToSearch + "%");
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvBooks.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching books: " + ex.Message);
                }
            }
        }

        private void dgvbooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
