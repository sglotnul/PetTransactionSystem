namespace Pet
{
    public partial class RegistryForm : Form
    {
        private UserManager manager;
        public RegistryForm()
        {
            InitializeComponent();
            manager = new UserManager();
            ActiveControl = textBox1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(string.Join(null, textBox2.Text));
                manager.Insert(new User(textBox1.Text, hashedPassword));
                MessageBox.Show("registration completed successfully");
                Close();
            }
            catch
            {
                textBox1.Text = null;
                textBox2.Text = null;
                MessageBox.Show("invalid username or password");
            }
        }
    }
}
