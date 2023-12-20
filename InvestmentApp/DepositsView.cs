using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp
{
    public partial class DepositsView : TableForm
    {
        public DepositsView() : base("deposits_view")
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
                dataGridView.ReadOnly = true;
                dataGridView.Columns["client_name"].HeaderText = "Имя Клиента";
                dataGridView.Columns["bank_name"].HeaderText = "Процент по депозиту";
                dataGridView.Columns["deposit_duration"].HeaderText = "Длительность депозта";
                dataGridView.Columns["date"].HeaderText = "Дата";
                dataGridView.Columns["deposit_percent"].HeaderText = "Процент депозита";
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                connection.Close();
            }
        }
    }
}