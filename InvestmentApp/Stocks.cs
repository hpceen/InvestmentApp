using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp {
    public partial class Stocks : Form {
        private const string ConnectionString =
                "Server=localhost; Port=5432; User Id=postgres; Password=zxcvf1km2msbnm; Database=postgres;";

        private const string Sql = "SELECT * FROM stocks ORDER BY id";

        private readonly DataSet _dataSet;
        private NpgsqlDataAdapter _adapter;


        public Stocks() {
            InitializeComponent();
            dataGridView.DataError += Program.DataGridView_DataError;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                _adapter = new NpgsqlDataAdapter(Sql, connection);

                _dataSet = new DataSet();
                _adapter.Fill(_dataSet);
                dataGridView.DataSource = _dataSet.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView.Columns["id"].ReadOnly = true;
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["minimum_transaction_amount"].HeaderText = "Минимальная сумма транзакции";
                dataGridView.Columns["rating"].HeaderText = "Рейтинг";
                dataGridView.Columns["yield"].HeaderText = "Доходность";
                dataGridView.Columns["additional_info"].HeaderText = "Дополнительная информация";
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void backButton_Click(object sender, EventArgs e) {
            Form f1 = new MainForm();
            f1.Show();
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
                    _adapter = new NpgsqlDataAdapter(Sql, connection);
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

        private void Clients_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}