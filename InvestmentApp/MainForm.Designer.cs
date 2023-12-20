namespace InvestmentApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.banksButton = new System.Windows.Forms.Button();
            this.clientsButton = new System.Windows.Forms.Button();
            this.depositsButton = new System.Windows.Forms.Button();
            this.investmentButton = new System.Windows.Forms.Button();
            this.quotesHistoryButton = new System.Windows.Forms.Button();
            this.stocksButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.depositsViewButton = new System.Windows.Forms.Button();
            this.investmentViewButton = new System.Windows.Forms.Button();
            this.earnedAmountButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // banksButton
            // 
            this.banksButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.banksButton.BackColor = System.Drawing.Color.Bisque;
            this.banksButton.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.banksButton.Location = new System.Drawing.Point(15, 80);
            this.banksButton.Margin = new System.Windows.Forms.Padding(0);
            this.banksButton.Name = "banksButton";
            this.banksButton.Size = new System.Drawing.Size(950, 80);
            this.banksButton.TabIndex = 2;
            this.banksButton.Text = "Банки";
            this.banksButton.UseVisualStyleBackColor = false;
            this.banksButton.Click += new System.EventHandler(this.banksButton_Click);
            // 
            // clientsButton
            // 
            this.clientsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.clientsButton.BackColor = System.Drawing.Color.Bisque;
            this.clientsButton.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientsButton.Location = new System.Drawing.Point(15, 170);
            this.clientsButton.Margin = new System.Windows.Forms.Padding(0);
            this.clientsButton.Name = "clientsButton";
            this.clientsButton.Size = new System.Drawing.Size(950, 80);
            this.clientsButton.TabIndex = 3;
            this.clientsButton.Text = "Клиенты";
            this.clientsButton.UseVisualStyleBackColor = false;
            this.clientsButton.Click += new System.EventHandler(this.clientsButton_Click);
            // 
            // depositsButton
            // 
            this.depositsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.depositsButton.BackColor = System.Drawing.Color.Bisque;
            this.depositsButton.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.depositsButton.Location = new System.Drawing.Point(15, 260);
            this.depositsButton.Margin = new System.Windows.Forms.Padding(0);
            this.depositsButton.Name = "depositsButton";
            this.depositsButton.Size = new System.Drawing.Size(950, 80);
            this.depositsButton.TabIndex = 4;
            this.depositsButton.Text = "Депозиты";
            this.depositsButton.UseVisualStyleBackColor = false;
            this.depositsButton.Click += new System.EventHandler(this.depositsButton_Click);
            // 
            // investmentButton
            // 
            this.investmentButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.investmentButton.BackColor = System.Drawing.Color.Bisque;
            this.investmentButton.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.investmentButton.Location = new System.Drawing.Point(15, 350);
            this.investmentButton.Margin = new System.Windows.Forms.Padding(0);
            this.investmentButton.Name = "investmentButton";
            this.investmentButton.Size = new System.Drawing.Size(950, 80);
            this.investmentButton.TabIndex = 5;
            this.investmentButton.Text = "Инвестиции";
            this.investmentButton.UseVisualStyleBackColor = false;
            this.investmentButton.Click += new System.EventHandler(this.investmentButton_Click);
            // 
            // quotesHistoryButton
            // 
            this.quotesHistoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.quotesHistoryButton.BackColor = System.Drawing.Color.Bisque;
            this.quotesHistoryButton.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.quotesHistoryButton.Location = new System.Drawing.Point(15, 440);
            this.quotesHistoryButton.Margin = new System.Windows.Forms.Padding(0);
            this.quotesHistoryButton.Name = "quotesHistoryButton";
            this.quotesHistoryButton.Size = new System.Drawing.Size(950, 80);
            this.quotesHistoryButton.TabIndex = 6;
            this.quotesHistoryButton.Text = "История котировок";
            this.quotesHistoryButton.UseVisualStyleBackColor = false;
            this.quotesHistoryButton.Click += new System.EventHandler(this.quotesHistoryButton_Click);
            // 
            // stocksButton
            // 
            this.stocksButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.stocksButton.BackColor = System.Drawing.Color.Bisque;
            this.stocksButton.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stocksButton.Location = new System.Drawing.Point(15, 530);
            this.stocksButton.Margin = new System.Windows.Forms.Padding(0);
            this.stocksButton.Name = "stocksButton";
            this.stocksButton.Size = new System.Drawing.Size(950, 80);
            this.stocksButton.TabIndex = 7;
            this.stocksButton.Text = "Акции";
            this.stocksButton.UseVisualStyleBackColor = false;
            this.stocksButton.Click += new System.EventHandler(this.stocksButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Bookman Old Style", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.titleLabel.Location = new System.Drawing.Point(15, 0);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(951, 80);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Таблицы и представления\r\n";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // depositsViewButton
            // 
            this.depositsViewButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.depositsViewButton.BackColor = System.Drawing.Color.Bisque;
            this.depositsViewButton.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.depositsViewButton.Location = new System.Drawing.Point(19, 620);
            this.depositsViewButton.Margin = new System.Windows.Forms.Padding(0);
            this.depositsViewButton.Name = "depositsViewButton";
            this.depositsViewButton.Size = new System.Drawing.Size(950, 80);
            this.depositsViewButton.TabIndex = 8;
            this.depositsViewButton.Text = "Представление депозитов";
            this.depositsViewButton.UseVisualStyleBackColor = false;
            this.depositsViewButton.Click += new System.EventHandler(this.depositsViewButton_Click);
            // 
            // investmentViewButton
            // 
            this.investmentViewButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.investmentViewButton.BackColor = System.Drawing.Color.Bisque;
            this.investmentViewButton.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.investmentViewButton.Location = new System.Drawing.Point(15, 710);
            this.investmentViewButton.Margin = new System.Windows.Forms.Padding(0);
            this.investmentViewButton.Name = "investmentViewButton";
            this.investmentViewButton.Size = new System.Drawing.Size(950, 80);
            this.investmentViewButton.TabIndex = 9;
            this.investmentViewButton.Text = "Представление инвестиций\r\n";
            this.investmentViewButton.UseVisualStyleBackColor = false;
            this.investmentViewButton.Click += new System.EventHandler(this.investmentViewButton_Click);
            // 
            // earnedAmountButton
            // 
            this.earnedAmountButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.earnedAmountButton.BackColor = System.Drawing.Color.Bisque;
            this.earnedAmountButton.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.earnedAmountButton.Location = new System.Drawing.Point(15, 800);
            this.earnedAmountButton.Margin = new System.Windows.Forms.Padding(0);
            this.earnedAmountButton.Name = "earnedAmountButton";
            this.earnedAmountButton.Size = new System.Drawing.Size(950, 80);
            this.earnedAmountButton.TabIndex = 10;
            this.earnedAmountButton.Text = "Заработано Клиентами";
            this.earnedAmountButton.UseVisualStyleBackColor = false;
            this.earnedAmountButton.Click += new System.EventHandler(this.earnedAmountButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 966);
            this.Controls.Add(this.earnedAmountButton);
            this.Controls.Add(this.investmentViewButton);
            this.Controls.Add(this.depositsViewButton);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.stocksButton);
            this.Controls.Add(this.quotesHistoryButton);
            this.Controls.Add(this.investmentButton);
            this.Controls.Add(this.depositsButton);
            this.Controls.Add(this.clientsButton);
            this.Controls.Add(this.banksButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Инвестиции";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);
        }

        public System.Windows.Forms.Button earnedAmountButton;

        #endregion
        public System.Windows.Forms.Button banksButton;
        public System.Windows.Forms.Button clientsButton;
        public System.Windows.Forms.Button depositsButton;
        public System.Windows.Forms.Button investmentButton;
        public System.Windows.Forms.Button quotesHistoryButton;
        public System.Windows.Forms.Button stocksButton;
        public System.Windows.Forms.Label titleLabel;
        public System.Windows.Forms.Button depositsViewButton;
        public System.Windows.Forms.Button investmentViewButton;
    }
}