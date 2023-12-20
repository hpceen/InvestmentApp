using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace InvestmentApp
{
    public abstract class TableForm : Form
    {
        protected const string ConnectionString =
            "###########################";

        protected readonly string Sql;

        protected readonly string TableName;
        protected NpgsqlDataAdapter Adapter;
        protected DataSet DataSet;

        protected TableForm(string tableName)
        {
            TableName = tableName;
            Sql = $"SELECT * FROM {TableName}";
        }

        protected void backButton_Click(object sender, EventArgs e)
        {
            Form mainForm = new MainForm();
            mainForm.Show();
            Hide();
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            DataSet.Tables[TableName].Rows.Add(DataSet.Tables[TableName].NewRow());
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();
                    Adapter = new NpgsqlDataAdapter(Sql, connection);
                    Adapter.UpdateCommand = new NpgsqlCommandBuilder(Adapter).GetUpdateCommand();
                    Adapter.Update(DataSet, TableName);
                    connection.Close();
                }

                MessageBox.Show(@"Сохранение успешно выполнено.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show($@"{exception.Data["MessageText"]}");
            }
        }

        protected void removeButton_Click(DataGridView dataGridView)
        {
            foreach (DataGridViewRow row in dataGridView.SelectedRows) dataGridView.Rows.Remove(row);
        }

        protected void ThisFormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}