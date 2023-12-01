﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace InvestmentApp {
    public partial class Banks : Form {
        private NpgsqlDataAdapter _adapter;

        private const string ConnectionString =
                "Server=localhost; Port=5432; User Id=postgres; Password=zxcvf1km2msbnm; Database=postgres;";

        private readonly DataSet _dataSet;

        private const string Sql = "SELECT * FROM banks ORDER BY id";


        public Banks() {
            InitializeComponent();
            dataGridView.DataError += Program.DataGridView_DataError;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            using (var connection = new NpgsqlConnection(ConnectionString)) {
                connection.Open();
                _adapter = new NpgsqlDataAdapter(Sql, connection);

                _dataSet = new DataSet();
                _adapter.Fill(_dataSet);
                dataGridView.DataSource = _dataSet.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView.Columns["id"].ReadOnly = true;
                dataGridView.Columns["id"].Visible = false;
                dataGridView.Columns["name"].HeaderText = "Название";
                dataGridView.Columns["percent"].HeaderText = "Процент по депозиту";
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void backButton_Click(object sender, EventArgs e) {
            Form f1 = new MainForm();
            f1.Show();
            Hide();
        }

        private void addButton_Click(object sender, EventArgs e) // add
        {
            var row = _dataSet.Tables[0].NewRow(); // добавляем новую строку в DataTable
            _dataSet.Tables[0].Rows.Add(row);
        }

        private void saveButton_Click(object sender, EventArgs e) {
            try {
                using (var connection = new NpgsqlConnection(ConnectionString)) {
                    connection.Open();
                    _adapter = new NpgsqlDataAdapter(Sql, connection);
                    _adapter.UpdateCommand = new NpgsqlCommandBuilder(_adapter).GetUpdateCommand();
                    _adapter.Update(_dataSet);
                }

                MessageBox.Show(@"Сохранение успешно.");
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show($@"{exception.Data["MessageText"]}");
            }
        }

        private void removeButton_Click(object sender, EventArgs e) // delete
        {
            foreach (DataGridViewRow row in dataGridView.SelectedRows) dataGridView.Rows.Remove(row);
        }

        private void Banks_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}