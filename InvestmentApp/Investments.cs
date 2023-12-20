using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp
{
    public partial class Investments : TableForm
    {
        private const string SqlClients = "SELECT * FROM clients";
        private const string SqlStocks = "SELECT * FROM stocks";

        public Investments() : base("investments")
        {
            InitializeComponent();
            dataGridView.DataError += Program.DataGridView_DataError;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                Adapter = new NpgsqlDataAdapter(Sql, connection);
                var adapterClients = new NpgsqlDataAdapter(SqlClients, connection);
                var adapterStocks = new NpgsqlDataAdapter(SqlStocks, connection);

                DataSet = new DataSet();
                Adapter.Fill(DataSet, TableName);
                adapterClients.Fill(DataSet, "clients");
                adapterStocks.Fill(DataSet, "stocks");

                DataSet.Relations.Add(new DataRelation("relationClientsInvestments",
                    DataSet.Tables["clients"].Columns["id"],
                    DataSet.Tables[TableName].Columns["client_id"]));

                DataSet.Relations.Add(new DataRelation("relationStocksInvestments",
                    DataSet.Tables["stocks"].Columns["id"],
                    DataSet.Tables[TableName].Columns["stock_id"]));


                dataGridView.DataSource = DataSet.Tables[TableName];

                dataGridView.Columns["client_id"].Visible = false;
                dataGridView.Columns["stock_id"].Visible = false;

                var comboBoxStocks = new DataGridViewComboBoxColumn();
                comboBoxStocks.Name = "Акции";
                comboBoxStocks.DataSource = DataSet.Tables["stocks"];
                comboBoxStocks.ValueMember = "id";
                comboBoxStocks.DataPropertyName = "stock_id";
                dataGridView.Columns.Insert(2, comboBoxStocks);
                dataGridView.Columns[2].HeaderText = "Акция";

                var comboBoxClients = new DataGridViewComboBoxColumn();
                comboBoxClients.Name = "Клиенты";
                comboBoxClients.DataSource = DataSet.Tables["clients"];
                comboBoxClients.DisplayMember = "name";
                comboBoxClients.ValueMember = "id";
                comboBoxClients.DataPropertyName = "client_id";
                dataGridView.Columns.Insert(3, comboBoxClients);
                dataGridView.Columns[3].HeaderText = "Имя Клиента";
                dataGridView.Columns["id"].ReadOnly = true;
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["purchase_date"].HeaderText = "Дата покупки";
                dataGridView.Columns["sale_date"].HeaderText = "Дата продажи";
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