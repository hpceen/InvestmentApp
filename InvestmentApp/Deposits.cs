using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp
{
    public partial class Deposits : TableForm
    {
        private const string SqlClients = "SELECT * FROM clients";
        private const string SqlBanks = "SELECT * FROM banks";

        public Deposits() : base("deposits")
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
                var adapterBanks = new NpgsqlDataAdapter(SqlBanks, connection);

                DataSet = new DataSet();
                Adapter.Fill(DataSet, TableName);
                adapterClients.Fill(DataSet, "clients");
                adapterBanks.Fill(DataSet, "banks");

                DataSet.Relations.Add(new DataRelation("relationClientsDeposits",
                    DataSet.Tables["clients"].Columns["id"],
                    DataSet.Tables[TableName].Columns["client_id"]));

                DataSet.Relations.Add(new DataRelation("relationBanksDeposits",
                    DataSet.Tables["banks"].Columns["id"],
                    DataSet.Tables[TableName].Columns["bank_id"]));


                dataGridView.DataSource = DataSet.Tables[TableName];

                dataGridView.Columns["client_id"].Visible = false;
                dataGridView.Columns["bank_id"].Visible = false;

                var comboBoxClients = new DataGridViewComboBoxColumn();
                comboBoxClients.Name = "Клиенты";
                comboBoxClients.DataSource = DataSet.Tables["clients"];
                comboBoxClients.DisplayMember = "name";
                comboBoxClients.ValueMember = "id";
                comboBoxClients.DataPropertyName = "client_id";
                dataGridView.Columns.Insert(2, comboBoxClients);
                dataGridView.Columns[2].HeaderText = "Имя Клиента";

                var comboBoxBanks = new DataGridViewComboBoxColumn();
                comboBoxBanks.Name = "Банки";
                comboBoxBanks.DataSource = DataSet.Tables["banks"];
                comboBoxBanks.DisplayMember = "name";
                comboBoxBanks.ValueMember = "id";
                comboBoxBanks.DataPropertyName = "bank_id";
                dataGridView.Columns.Insert(3, comboBoxBanks);
                dataGridView.Columns[3].HeaderText = "Имя Банка";

                dataGridView.Columns["id"].ReadOnly = true;
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["bank_id"].HeaderText = "ID Банка";
                dataGridView.Columns["deposit_duration"].HeaderText = "Длительность депозита";
                dataGridView.Columns["date"].HeaderText = "Дата";
                dataGridView.Columns["amount"].HeaderText = "Сумма";
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