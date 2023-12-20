using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp
{
    public partial class QuotesHistory : TableForm
    {
        private const string SqlQuotesHistory = "SELECT * FROM quotes_history";
        private const string SqlInvestment = "SELECT * FROM investments";

        public QuotesHistory() : base("quotes_history")
        {
            InitializeComponent();
            dataGridView.DataError += Program.DataGridView_DataError;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                Adapter = new NpgsqlDataAdapter(SqlQuotesHistory, connection);
                var adapterInvestments = new NpgsqlDataAdapter(SqlInvestment, connection);

                DataSet = new DataSet();
                Adapter.Fill(DataSet, TableName);
                adapterInvestments.Fill(DataSet, "investments");

                DataSet.Relations.Add(new DataRelation("relationInvestmentsQuotesHistory",
                    DataSet.Tables["investments"].Columns["id"],
                    DataSet.Tables[TableName].Columns["investment_id"]));

                dataGridView.DataSource = DataSet.Tables[TableName];

                dataGridView.Columns["investment_id"].Visible = false;

                var comboBoxInvestments = new DataGridViewComboBoxColumn();
                comboBoxInvestments.Name = "Инвестиции";
                comboBoxInvestments.DataSource = DataSet.Tables["investments"];
                comboBoxInvestments.ValueMember = "id";
                comboBoxInvestments.DataPropertyName = "investment_id";
                dataGridView.Columns.Insert(2, comboBoxInvestments);
                dataGridView.Columns[2].HeaderText = "Инвестиция";
                dataGridView.Columns["id"].ReadOnly = true;
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["date"].HeaderText = "Дата";
                dataGridView.Columns["cost"].HeaderText = "Стоимость";
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                connection.Close();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            removeButton_Click(dataGridView);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "CALL clean_quotes_history()";
                    command.ExecuteNonQuery();
                }

                Adapter = new NpgsqlDataAdapter(SqlQuotesHistory, connection);
                var adapterInvestments = new NpgsqlDataAdapter(SqlInvestment, connection);

                DataSet = new DataSet();
                Adapter.Fill(DataSet, TableName);
                adapterInvestments.Fill(DataSet, "investments");

                DataSet.Relations.Add(new DataRelation("relationInvestmentsQuotesHistory",
                    DataSet.Tables["investments"].Columns["id"],
                    DataSet.Tables[TableName].Columns["investment_id"]));

                dataGridView.DataSource = DataSet.Tables[TableName];

                dataGridView.Refresh();
                connection.Close();
            }
        }
    }
}