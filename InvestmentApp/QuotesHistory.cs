using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp {
    public partial class QuotesHistory : Form {
        private const string ConnectionString =
                "Server=localhost; Port=5432; User Id=postgres; Password=zxcvf1km2msbnm; Database=postgres;";

        private const string SqlQuotesHistory = "SELECT * FROM quotes_history ORDER BY id";
        private const string SqlInvestment = "SELECT * FROM investments ORDER BY purchase_date";

        private readonly DataSet _dataSet;
        private NpgsqlDataAdapter _adapter;


        public QuotesHistory() {
            InitializeComponent();
            dataGridView.DataError += Program.DataGridView_DataError;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                _adapter = new NpgsqlDataAdapter(SqlQuotesHistory, connection);
                var adapterInvestments = new NpgsqlDataAdapter(SqlInvestment, connection);

                _dataSet = new DataSet();
                _adapter.Fill(_dataSet, "quotes_history");
                adapterInvestments.Fill(_dataSet, "investments");

                _dataSet.Relations.Add(new DataRelation("relationInvestmentsQuotesHistory",
                        _dataSet.Tables["investments"].Columns["id"],
                        _dataSet.Tables["quotes_history"].Columns["investment_id"]));

                dataGridView.DataSource = _dataSet.Tables["quotes_history"];

                dataGridView.Columns["investment_id"].Visible = false;

                var comboBoxInvestments = new DataGridViewComboBoxColumn();
                comboBoxInvestments.Name = "Инвестиции";
                comboBoxInvestments.DataSource = _dataSet.Tables["investments"];
                comboBoxInvestments.ValueMember = "id";
                comboBoxInvestments.DataPropertyName = "investment_id";
                dataGridView.Columns.Insert(2, comboBoxInvestments);
                dataGridView.Columns[2].HeaderText = "Инвестиция";

                // делаем недоступным столбец id для изменения
                dataGridView.Columns["id"].ReadOnly = true;
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["date"].HeaderText = "Дата";
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
            var row = _dataSet.Tables["quotes_history"].NewRow(); // добавляем новую строку в DataTable
            _dataSet.Tables["quotes_history"].Rows.Add(row);
        }

        private void saveButton_Click(object sender, EventArgs e) {
            try {
                using (var connection = new NpgsqlConnection(ConnectionString)) {
                    connection.Open();
                    _adapter = new NpgsqlDataAdapter(SqlQuotesHistory, connection);
                    _adapter.UpdateCommand = new NpgsqlCommandBuilder(_adapter).GetUpdateCommand();
                    _adapter.Update(_dataSet, "quotes_history");
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

        private void QuotesHistory_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}