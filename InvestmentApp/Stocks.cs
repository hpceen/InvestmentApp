using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp
{
    public partial class Stocks : TableForm
    {
        public Stocks() : base("stocks")
        {
            InitializeComponent();
            dataGridView.DataError += Program.DataGridView_DataError;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                Adapter = new NpgsqlDataAdapter(Sql, connection);

                DataSet = new DataSet();
                Adapter.Fill(DataSet);
                dataGridView.DataSource = DataSet.Tables[0];
                dataGridView.Columns["id"].ReadOnly = true;
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["minimum_transaction_amount"].HeaderText = "Минимальная сумма транзакции";
                dataGridView.Columns["rating"].HeaderText = "Рейтинг";
                dataGridView.Columns["yield"].HeaderText = "Доходность";
                dataGridView.Columns["additional_info"].HeaderText = "Дополнительная информация";
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                connection.Close();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            removeButton_Click(dataGridView);
        }
    }
}