using Pet.Repository;

namespace Pet
{
    public partial class AccountForm : Form
    {
        Account account;
        List<Transaction> transactions;
        TransactionManager manager;

        private Transaction setFields(int index)
        {
            Transaction transaction = transactions[index];
            label2.Text = transaction.Date.ToString();
            label5.Text = transaction.Sum.ToString();
            return transaction;
        }

        private void updateListBox(List<Transaction> transactions, ListBox listBox)
        {
            this.transactions = transactions;
            listBox.Items.Clear();
            foreach (Transaction t in transactions) listBox.Items.Add(t.Id);
        }

        public AccountForm(Account acc)
        {
            InitializeComponent();
            account = acc;
            manager = new TransactionManager();
            transactions = new List<Transaction>();
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            label9.Text = account.Id.ToString();
            radioButton1.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {  
            if (radioButton1.Checked)
            {
                listBox2.Hide();
                updateListBox(manager.GetByAddresseeId(account.Id), listBox1);
                listBox1.Show();
            }
            else
            {
                listBox2.Hide();
                updateListBox(manager.GetBySenderId(account.Id), listBox2);
                listBox2.Show();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Transaction transaction = setFields(listBox1.SelectedIndex);
            label6.Text = "from:";
            label7.Text = transaction.FromAccount.ToString();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Transaction transaction = setFields(listBox2.SelectedIndex);
            label6.Text = "to:";
            label7.Text = transaction.ToAccount.ToString();
        }
    }
}
