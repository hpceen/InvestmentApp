using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp
{
    public partial class InvestmentsView : TableForm
    {
        public InvestmentsView() : base("investments_view")
        {
            InitializeComponent();
            dataGridView.DataError += Program.DataGridView_DataError;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                var adapter = new NpgsqlDataAdapter(Sql, connection);

                var dataSet = new DataSet();
                adapter.Fill(dataSet, TableName);
                dataGridView.DataSource = dataSet.Tables[TableName];
                dataGridView.ReadOnly = true;
                dataGridView.Columns["client_name"].HeaderText = "Имя клиента";
                dataGridView.Columns["stock_rating"].HeaderText = "Рейтинг акции";
                dataGridView.Columns["additional_stock_info"].HeaderText = "Дополнительная информация по акции";
                dataGridView.Columns["purchase_date"].HeaderText = "Дата покупки";
                dataGridView.Columns["sale_date"].HeaderText = "Дата продажи";
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                connection.Close();
            }
        }
    }
}