using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace InvestmentApp {
    public partial class Deposits : Form {
        private NpgsqlDataAdapter _adapter;
        private NpgsqlDataAdapter _adapterClients;
        private NpgsqlDataAdapter _adapterBanks;

        private const string ConnectionString =
                "Server=localhost; Port=5432; User Id=postgres; Password=zxcvf1km2msbnm; Database=postgres;";

        private readonly DataSet _dataSet;

        private const string SqlDeposits = "SELECT * FROM deposits ORDER BY id";
        private const string SqlClients = "SELECT * FROM clients ORDER BY name";
        private const string SqlBanks = "SELECT * FROM banks ORDER BY name";


        public Deposits() {
            InitializeComponent();
            dataGridView.DataError += Program.DataGridView_DataError;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                _adapter = new NpgsqlDataAdapter(SqlDeposits, connection);
                _adapterClients = new NpgsqlDataAdapter(SqlClients, connection);
                _adapterBanks = new NpgsqlDataAdapter(SqlBanks, connection);

                _dataSet = new DataSet();
                _adapter.Fill(_dataSet, "deposits");
                _adapterClients.Fill(_dataSet, "clients");
                _adapterBanks.Fill(_dataSet, "banks");

                _dataSet.Relations.Add(new DataRelation("relationClientsDeposits",
                        _dataSet.Tables["clients"].Columns["id"],
                        _dataSet.Tables["deposits"].Columns["client_id"]));

                _dataSet.Relations.Add(new DataRelation("relationBanksDeposits",
                        _dataSet.Tables["banks"].Columns["id"],
                        _dataSet.Tables["deposits"].Columns["bank_id"]));


                dataGridView.DataSource = _dataSet.Tables["deposits"];

                dataGridView.Columns["client_id"].Visible = false;
                dataGridView.Columns["bank_id"].Visible = false;

                var comboBoxClients = new DataGridViewComboBoxColumn();
                comboBoxClients.Name = "Клиенты";
                comboBoxClients.DataSource = _dataSet.Tables["clients"];
                comboBoxClients.DisplayMember = "name";
                comboBoxClients.ValueMember = "id";
                comboBoxClients.DataPropertyName = "client_id";
                dataGridView.Columns.Insert(2, comboBoxClients);
                dataGridView.Columns[2].HeaderText = "Имя Клиента";

                var comboBoxBanks = new DataGridViewComboBoxColumn();
                comboBoxBanks.Name = "Банки";
                comboBoxBanks.DataSource = _dataSet.Tables["banks"];
                comboBoxBanks.DisplayMember = "name";
                comboBoxBanks.ValueMember = "id";
                comboBoxBanks.DataPropertyName = "bank_id";
                dataGridView.Columns.Insert(3, comboBoxBanks);
                dataGridView.Columns[3].HeaderText = "Имя Банка";

                // делаем недоступным столбец id для изменения
                dataGridView.Columns["id"].ReadOnly = true;
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["bank_id"].HeaderText = "ID Банка";
                dataGridView.Columns["deposit_duration"].HeaderText = "Длительность депозита";
                dataGridView.Columns["date"].HeaderText = "Дата";
                dataGridView.Columns["amount"].HeaderText = "Сумма";
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                _adapter = new NpgsqlDataAdapter(SqlDeposits, connection);

                _dataSet = new DataSet();
                _adapter.Fill(_dataSet);
                dataGridView.DataSource = _dataSet.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView.Columns["id"].ReadOnly = true;
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["client_id"].HeaderText = "ID Клиента";
                dataGridView.Columns["bank_id"].HeaderText = "ID Банка";
                dataGridView.Columns["deposit_duration"].HeaderText = "Длительность депозита";
                dataGridView.Columns["date"].HeaderText = "Дата";
                dataGridView.Columns["amount"].HeaderText = "Сумма";
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
            var row = _dataSet.Tables[0].NewRow(); // добавляем новую строку в DataTable
            _dataSet.Tables[0].Rows.Add(row);
        }

        private void saveButton_Click(object sender, EventArgs e) {
            try {
                using (var connection = new NpgsqlConnection(ConnectionString)) {
                    connection.Open();
                    _adapter = new NpgsqlDataAdapter(SqlDeposits, connection);
                    _adapter.UpdateCommand = new NpgsqlCommandBuilder(_adapter).GetUpdateCommand();
                    _adapter.Update(_dataSet);
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

        private void Deposits_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}