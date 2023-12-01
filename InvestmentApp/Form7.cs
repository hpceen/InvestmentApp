using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace InvestmentApp {
    public partial class Form7 : Form {
        private SqlDataAdapter adapter;

        private SqlCommandBuilder commandBuilder;

        //string connectionString = "Server=server46;Database=Valiullin_VT-31;User Id=stud;Password=stud;";
        private readonly string connectionString = "Data Source=COMPUTER;Initial Catalog=master;" +
                                                   "Integrated Security=true;";

        private readonly DataSet ds;

        private readonly string sql = "SELECT * FROM dbo.Vouchers ORDER BY Id";

        public Form7() {
            InitializeComponent();


            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (var connection = new SqlConnection(connectionString)) {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["Id"].ReadOnly = true;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["RouteId"].HeaderText = "Номер маршрута";
                dataGridView1.Columns["ClientId"].HeaderText = "Номер клиента";
                dataGridView1.Columns["Date"].HeaderText = "Дата отправки";
                dataGridView1.Columns["Count"].HeaderText = "Количество билетов";
                dataGridView1.Columns["Discount"].HeaderText = "Скидка";
                dataGridView1.Columns["FinalCost"].HeaderText = "Финальная стоимость";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }


        private void button8_Click_1(object sender, EventArgs e) {
            Form f1 = new MainForm();
            f1.Show();
            Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
        }

        private void button2_Click(object sender, EventArgs e) {
            var row = ds.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds.Tables[0].Rows.Add(row);
        }

        private void button3_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                for (var i = 1; i < row.Cells.Count; i++) {
                    //Проверка на пустоту ячеек
                    if (row.Cells[i].Value == null || row.Cells[i].Value == DBNull.Value) {
                        MessageBox.Show("Заполните пустые ячейки!!!");
                        row.Cells[i].Style.BackColor = Color.Red;
                        return;
                    }

                    row.Cells[i].Style.BackColor = Color.White;

                    try {
                        if (Convert.ToInt32(row.Cells[1].Value) > 15) {
                            MessageBox.Show("Номер маршрута не превышает цифры 15!!!");
                            row.Cells[1].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch {
                        row.Cells[1].Style.BackColor = Color.White;
                    }

                    try {
                        if (Convert.ToInt32(row.Cells[1].Value) < 0) {
                            MessageBox.Show("Номер маршрута не может быть отрицательным числом!!!");
                            row.Cells[1].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch {
                        row.Cells[1].Style.BackColor = Color.White;
                    }

                    try {
                        if (Convert.ToInt32(row.Cells[2].Value) > 15) {
                            MessageBox.Show("Номер клиента не превышает значения 15!!!");
                            row.Cells[2].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch {
                        row.Cells[2].Style.BackColor = Color.White;
                    }

                    try {
                        if (Convert.ToInt32(row.Cells[2].Value) < 0) {
                            MessageBox.Show("Номер клиента не может быть отрицательным числом!!!");
                            row.Cells[2].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch {
                        row.Cells[2].Style.BackColor = Color.White;
                    }

                    try {
                        if (Convert.ToInt32(row.Cells[5].Value.ToString().Replace("%", "")) <= 100) {
                            row.Cells[5].Style.BackColor = Color.White;
                        }

                        else {
                            MessageBox.Show("Введите корректное число, не превыщающее 100%!!!");
                            row.Cells[5].Style.BackColor = Color.Red;
                            return;
                        }

                        if (Convert.ToInt32(row.Cells[5].Value.ToString().Replace("%", "")) < 0) {
                            MessageBox.Show("Проценты не могут быть отрицательным числом!!!");
                            row.Cells[5].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch {
                        MessageBox.Show("Введите корректное число, без лишних символов!!!");
                        row.Cells[5].Style.BackColor = Color.Red;
                        return;
                    }

                    if (row.Cells[5].Value.ToString().IndexOf("%") == -1) row.Cells[5].Value += "%";
                    try {
                        if (Convert.ToInt32(row.Cells[6].Value) < 5000) {
                            MessageBox.Show(
                                    "Стоимость путёвки с учётом всех скидок не может быть меньше 5000 рублей!!!!!!");
                            row.Cells[6].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch {
                        row.Cells[6].Style.BackColor = Color.White;
                    }
                }

            using (var connection = new SqlConnection(connectionString)) {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateVoucher", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@routeid", SqlDbType.Int, 0, "RouteId"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@clientid", SqlDbType.Int, 0, "ClientId"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@date", SqlDbType.Date, 50, "date"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@count", SqlDbType.Int, 0, "Count"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@discount", SqlDbType.NVarChar, 50, "Discount"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@finalcost", SqlDbType.Int, 0, "FinalCost"));

                var parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds);
            }

            MessageBox.Show("Сохранение успешно выполнено.");
        }

        private void button4_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) dataGridView1.Rows.Remove(row);
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            MessageBox.Show("Ошибка ввода!!!");
        }

        private void Form7_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}