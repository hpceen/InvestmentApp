using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp
{
    public partial class Clients : TableForm
    {
        public Clients() : base("clients")
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
                Adapter.Fill(DataSet, TableName);
                dataGridView.DataSource = DataSet.Tables[TableName];
                dataGridView.Columns["id"].ReadOnly = true;
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["name"].HeaderText = "Имя клиента";
                dataGridView.Columns["property_type"].HeaderText = "Тип собственности";
                dataGridView.Columns["address"].HeaderText = "Адрес";
                dataGridView.Columns["phone"].HeaderText = "Телефон";
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