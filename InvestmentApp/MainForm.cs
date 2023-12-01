using System;
using System.Windows.Forms;

namespace InvestmentApp {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }


        private void banksButton_Click(object sender, EventArgs e) {
            Form banksForm = new Banks();
            banksForm.Show();
            Hide();
        }

        private void clientsButton_Click(object sender, EventArgs e) {
            Form clientsForm= new Clients();
            clientsForm.Show();
            Hide();
        }

        private void depositsButton_Click(object sender, EventArgs e) {
            Form depositsForm = new Deposits();
            depositsForm.Show();
            Hide();
        }

        private void investmentButton_Click(object sender, EventArgs e) {
            Form f6 = new Form6();
            f6.Show();
            Hide();
        }

        private void quotesHistoryButton_Click(object sender, EventArgs e) {
            Form f7 = new Form7();
            f7.Show();
            Hide();
        }

        private void stocksButton_Click(object sender, EventArgs e) {
            Form f8 = new Form8();
            f8.Show();
            Hide();
        }

        private void depositsViewButton_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void investmentViewButton_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}