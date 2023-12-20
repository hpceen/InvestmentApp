using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp
{
    public partial class EarnedAmountView : TableForm
    {
        public EarnedAmountView() : base("earned_amount_view")
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
                dataGridView.Columns["client_name"].HeaderText = "Имя клиента";
                dataGridView.Columns["bank_name"].HeaderText = "Название банка";
                dataGridView.Columns["earned"].HeaderText = "Заработок с депозитов";
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                connection.Close();
            }
        }
    }
}