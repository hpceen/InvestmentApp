using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp {
    public partial class DepositsView : Form {
        private const string ConnectionString =
                "Server=localhost; Port=5432; User Id=postgres; Password=zxcvf1km2msbnm; Database=postgres;";

        private const string Sql = "SELECT * FROM deposits_view";

        private readonly DataSet _dataSet;
        private NpgsqlDataAdapter _adapter;


        public DepositsView() {
            InitializeComponent();
            dataGridView.DataError += Program.DataGridView_DataError;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                _adapter = new NpgsqlDataAdapter(Sql, connection);

                _dataSet = new DataSet();
                _adapter.Fill(_dataSet, "deposits_view");
                dataGridView.DataSource = _dataSet.Tables["deposits_view"];
                dataGridView.ReadOnly = true;
                dataGridView.Columns["client_name"].HeaderText = "Имя Клиента";
                dataGridView.Columns["bank_name"].HeaderText = "Процент по депозиту";
                dataGridView.Columns["deposit_duration"].HeaderText = "Длительность депозта";
                dataGridView.Columns["date"].HeaderText = "Дата";
                dataGridView.Columns["deposit_percent"].HeaderText = "Процент депозита";
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void backButton_Click(object sender, EventArgs e) {
            Form mainForm = new MainForm();
            mainForm.Show();
            Hide();
        }

        private void DepositsView_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}