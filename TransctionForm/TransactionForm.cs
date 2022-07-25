namespace Pet
{
    public partial class TransactionForm : Form
    {
        Account from_account;
        public TransactionForm(Account account)
        {
            InitializeComponent();
            from_account = account;
        }

        private bool do_transaction(double sum, int id)
        {
            AccountManager accountManager = new AccountManager();
            Account? to_account = accountManager.GetById(id);
            if (to_account != null)
            {
                accountManager.Transact(from_account, (Account)to_account, sum);
                return true;
            }
            return false;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!Char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back) e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double sum = Convert.ToDouble(textBox2.Text);
                int to_acc = Convert.ToInt32(textBox1.Text);
                if (sum > from_account.Sum || sum == 0 || !do_transaction(sum, to_acc)) throw new Exception("transaction error");
                MessageBox.Show("transaction accepted");
                Close();
            }
            catch
            {
                MessageBox.Show("attempt to transact failed");
            }
        }
    }
}
