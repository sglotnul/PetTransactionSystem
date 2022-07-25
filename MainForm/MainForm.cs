namespace Pet
{
    public partial class MainForm : Form
    {
        User user;
        List<Account> accounts;

        AccountManager accountManager;
        public MainForm(User user)
        {
            InitializeComponent();
            this.user = user;
            accountManager = new AccountManager();
            accounts = new List<Account>();
        }

        private void UpdateAccountList()
        {
            comboBox1.Items.Clear();
            accounts = accountManager.GetByUserId(user.Id);
            linkLabel1.Text = user.Name;
            foreach (Account account in accounts)
            {
                comboBox1.Items.Add(account.Id);
                comboBox1.SelectedIndex = 0;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateAccountList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Account acc = accountManager.Insert(new Account(user.Id, 0));
                accounts.Add(acc);
                comboBox1.Items.Add(acc.Id);
                MessageBox.Show($"account {acc.Id} created");
            }
            catch 
            {
                MessageBox.Show("attempt to create account failed");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = accounts[comboBox1.SelectedIndex].Sum.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TransactionForm f = new TransactionForm(accounts[comboBox1.SelectedIndex]);
            f.FormClosed += delegate { UpdateAccountList(); };
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
