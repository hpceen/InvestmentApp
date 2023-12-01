using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace InvestmentApp {
    public partial class Form6 : Form {
        private SqlDataAdapter adapter;

        private SqlCommandBuilder commandBuilder;

        //string connectionString = "Server=server46;Database=Valiullin_VT-31;User Id=stud;Password=stud;";
        private readonly string connectionString = "Data Source=COMPUTER;Initial Catalog=master;" +
                                                   "Integrated Security=true;";

        private readonly DataSet ds;

        private readonly string sql = "SELECT * FROM dbo.Routes ORDER BY Id";


        public Form6() {
            InitializeComponent();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (var connection = new SqlConnection(connectionString)) {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                //ds.Relations.Add(new DataRelation("Hot", ds.Tables["dbo.Hotels"].Columns["Id"], ds.Tables["dbo.Routes"].Columns["HotelId"]));
                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["Id"].ReadOnly = true;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["HotelId"].HeaderText = "Номер отеля";
                dataGridView1.Columns["Duration"].HeaderText = "Длительность";
                dataGridView1.Columns["Cost"].HeaderText = "Стоимость";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //var hotel = new DataGridViewComboBoxColumn(); // добавить новую колонку
                //hotel.Name = "Отель";
                //hotel.DataSource = ds.Tables["Hotels"];
                //hotel.DisplayMember = "Name"; 
                //hotel.ValueMember = "Id";
                //hotel.DataPropertyName = "HotelId"; // Для связи с Works
                //hotel.MaxDropDownItems = 5;
                //hotel.FlatStyle = FlatStyle.Flat;
                //dataGridView1.Columns.Insert(1, hotel);
                //dataGridView1.Columns["HotelId"].Visible = false;
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
                        if (Convert.ToInt32(row.Cells[1].Value) > 10) {
                            MessageBox.Show("Номер отеля не превышает цифры 10!!!");
                            row.Cells[1].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch {
                        row.Cells[1].Style.BackColor = Color.White;
                    }

                    try {
                        if (Convert.ToInt32(row.Cells[1].Value) < 0) {
                            MessageBox.Show("Номер отеля не может быть отрицательным числом!!!");
                            row.Cells[1].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch {
                        row.Cells[1].Style.BackColor = Color.White;
                    }

                    try {
                        if (Convert.ToInt32(row.Cells[2].Value) > 10) {
                            MessageBox.Show("Длительность поездки не превышает 10 недель!!!");
                            row.Cells[2].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch {
                        row.Cells[2].Style.BackColor = Color.White;
                    }

                    try {
                        if (Convert.ToInt32(row.Cells[2].Value) < 0) {
                            MessageBox.Show("Длительность поездки не может быть отрицательным числом!!!");
                            row.Cells[2].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch {
                        row.Cells[2].Style.BackColor = Color.White;
                    }

                    try {
                        if (Convert.ToInt32(row.Cells[3].Value) < 20000) {
                            MessageBox.Show("Стоимость поездки не может быть меньше 20000 рублей!!!!!!");
                            row.Cells[3].Style.BackColor = Color.Red;
                            return;
                        }
                    }
                    catch {
                        row.Cells[3].Style.BackColor = Color.White;
                    }
                }

            try {
                using (var connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    adapter = new SqlDataAdapter(sql, connection);
                    commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.InsertCommand = new SqlCommand("sp_CreateRoute", connection);
                    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@hotelid", SqlDbType.Int, 0, "HotelId"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@duration", SqlDbType.Int, 0, "Duration"));
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@cost", SqlDbType.Int, 0, "Cost"));

                    var parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                    parameter.Direction = ParameterDirection.Output;

                    adapter.Update(ds);
                }

                MessageBox.Show("Сохранение успешно выполнено.");
            }

            catch {
                MessageBox.Show("Вы не можете удалить данную строку, так как она используется в других таблицах!!!");
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) dataGridView1.Rows.Remove(row);
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            MessageBox.Show("Ошибка ввода данных!!!");
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}