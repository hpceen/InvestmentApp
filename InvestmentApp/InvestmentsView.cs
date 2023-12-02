using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp {
    public partial class InvestmentsView : Form {
        private const string ConnectionString =
                "Server=localhost; Port=5432; User Id=postgres; Password=zxcvf1km2msbnm; Database=postgres;";

        private const string Sql = "SELECT * FROM deposits_view";

        private readonly DataSet _dataSet;
        private NpgsqlDataAdapter _adapter;


        public InvestmentsView() {
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
                dataGridView.Columns["client_name"].HeaderText = "Имя клиента";
                dataGridView.Columns["stock_rating"].HeaderText = "Рейтинг акции";
                dataGridView.Columns["additional_stock_info"].HeaderText = "Дополнительная информация по акции";
                dataGridView.Columns["purchase_date"].HeaderText = "Дата покупки";
                dataGridView.Columns["sell_date"].HeaderText = "Дата продажи";
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