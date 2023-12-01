using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp {
    public partial class Investments : Form {
        private const string ConnectionString =
                "Server=localhost; Port=5432; User Id=postgres; Password=zxcvf1km2msbnm; Database=postgres;";

        private const string SqlInvestments = "SELECT * FROM investments ORDER BY id";
        private const string SqlClients = "SELECT * FROM clients ORDER BY name";
        private const string SqlStocks = "SELECT * FROM stocks";
        private readonly NpgsqlDataAdapter _adapterClients;
        private readonly NpgsqlDataAdapter _adapterStocks;

        private readonly DataSet _dataSet;
        private NpgsqlDataAdapter _adapter;


        public Investments() {
            InitializeComponent();
            dataGridView.DataError += Program.DataGridView_DataError;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                _adapter = new NpgsqlDataAdapter(SqlInvestments, connection);
                _adapterClients = new NpgsqlDataAdapter(SqlClients, connection);
                _adapterStocks = new NpgsqlDataAdapter(SqlStocks, connection);

                _dataSet = new DataSet();
                _adapter.Fill(_dataSet, "investments");
                _adapterClients.Fill(_dataSet, "clients");
                _adapterStocks.Fill(_dataSet, "stocks");

                _dataSet.Relations.Add(new DataRelation("relationClientsInvestments",
                        _dataSet.Tables["clients"].Columns["id"],
                        _dataSet.Tables["investments"].Columns["client_id"]));

                _dataSet.Relations.Add(new DataRelation("relationStocksInvestments",
                        _dataSet.Tables["stocks"].Columns["id"],
                        _dataSet.Tables["investments"].Columns["stock_id"]));


                dataGridView.DataSource = _dataSet.Tables["investments"];

                dataGridView.Columns["client_id"].Visible = false;
                dataGridView.Columns["stock_id"].Visible = false;

                var comboBoxStocks = new DataGridViewComboBoxColumn();
                comboBoxStocks.Name = "Акции";
                comboBoxStocks.DataSource = _dataSet.Tables["stocks"];
                comboBoxStocks.ValueMember = "id";
                comboBoxStocks.DataPropertyName = "stock_id";
                dataGridView.Columns.Insert(2, comboBoxStocks);
                dataGridView.Columns[2].HeaderText = "Акция";

                var comboBoxClients = new DataGridViewComboBoxColumn();
                comboBoxClients.Name = "Клиенты";
                comboBoxClients.DataSource = _dataSet.Tables["clients"];
                comboBoxClients.DisplayMember = "name";
                comboBoxClients.ValueMember = "id";
                comboBoxClients.DataPropertyName = "client_id";
                dataGridView.Columns.Insert(3, comboBoxClients);
                dataGridView.Columns[3].HeaderText = "Имя Клиента";

                // делаем недоступным столбец id для изменения
                dataGridView.Columns["id"].ReadOnly = true;
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["purchase_date"].HeaderText = "Дата покупки";
                dataGridView.Columns["sale_date"].HeaderText = "Дата продажи";
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void backButton_Click(object sender, EventArgs e) {
            Form mainForm = new MainForm();
            mainForm.Show();
            Hide();
        }

        private void addButton_Click(object sender, EventArgs e) // add
        {
            var row = _dataSet.Tables["investments"].NewRow(); // добавляем новую строку в DataTable
            _dataSet.Tables["investments"].Rows.Add(row);
        }

        private void saveButton_Click(object sender, EventArgs e) {
            try {
                using (var connection = new NpgsqlConnection(ConnectionString)) {
                    connection.Open();
                    _adapter = new NpgsqlDataAdapter(SqlInvestments, connection);
                    _adapter.UpdateCommand = new NpgsqlCommandBuilder(_adapter).GetUpdateCommand();
                    _adapter.Update(_dataSet, "investments");
                }

                MessageBox.Show(@"Сохранение успешно выполнено.");
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show($@"{exception.Data["MessageText"]}");
            }
        }

        private void removeButton_Click(object sender, EventArgs e) // delete
        {
            foreach (DataGridViewRow row in dataGridView.SelectedRows) dataGridView.Rows.Remove(row);
        }

        private void Investments_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}