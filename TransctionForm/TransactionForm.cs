using Pet.Repository;

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
            AccountManager accountManager = new AccountManager();
            try
            {
                double sum = Math.Round(Convert.ToDouble(textBox2.Text), 2);
                int to_acc = Convert.ToInt32(textBox1.Text);
                if (to_acc == from_account.Id || sum > from_account.Sum || sum == 0) throw new Exception("transaction error");
                accountManager.Transact(from_account.Id, to_acc, sum);
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
