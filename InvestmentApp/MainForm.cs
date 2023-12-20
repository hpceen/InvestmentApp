using System;
using System.Windows.Forms;

namespace InvestmentApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void banksButton_Click(object sender, EventArgs e)
        {
            Form banksForm = new Banks();
            banksForm.Show();
            Hide();
        }

        private void clientsButton_Click(object sender, EventArgs e)
        {
            Form clientsForm = new Clients();
            clientsForm.Show();
            Hide();
        }

        private void depositsButton_Click(object sender, EventArgs e)
        {
            Form depositsForm = new Deposits();
            depositsForm.Show();
            Hide();
        }

        private void investmentButton_Click(object sender, EventArgs e)
        {
            Form investmentsForm = new Investments();
            investmentsForm.Show();
            Hide();
        }

        private void quotesHistoryButton_Click(object sender, EventArgs e)
        {
            Form quotesHistoryForm = new QuotesHistory();
            quotesHistoryForm.Show();
            Hide();
        }

        private void stocksButton_Click(object sender, EventArgs e)
        {
            Form stocksForm = new Stocks();
            stocksForm.Show();
            Hide();
        }

        private void depositsViewButton_Click(object sender, EventArgs e)
        {
            Form depositsViewForm = new DepositsView();
            depositsViewForm.Show();
            Hide();
        }

        private void investmentViewButton_Click(object sender, EventArgs e)
        {
            Form investmentsViewForm = new InvestmentsView();
            investmentsViewForm.Show();
            Hide();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void earnedAmountButton_Click(object sender, EventArgs e)
        {
            Form earnedAmountView = new EarnedAmountView();
            earnedAmountView.Show();
            Hide();
        }
    }
}