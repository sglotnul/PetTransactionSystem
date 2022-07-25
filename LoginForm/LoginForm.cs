namespace Pet
{
    public partial class LoginForm : Form
    {
        private UserManager manager;
        private void ResetFields()
        {
            textBox1.Text = null;
            textBox2.Text = null;
        }

        private void ShowForm(Form f)
        {
            f.Location = Location;
            f.StartPosition = FormStartPosition.Manual;
            f.FormClosing += delegate { Show(); ResetFields(); };
            f.Show();
            Hide();
        }

        private void F_FormClosing(object? sender, FormClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        public LoginForm()
        {
            InitializeComponent();
            manager = new();
            ActiveControl = textBox1;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistryForm f = new();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                User? user = manager.GetByName(textBox1.Text);
                if (user is User u && BCrypt.Net.BCrypt.Verify(string.Join(null, textBox2.Text), u.Password)) 
                {
                    MessageBox.Show($"Welcome {u.Name}");
                    ShowForm(new MainForm(u));
                }
                else throw new Exception();
            }
            catch
            {
                ResetFields();
                MessageBox.Show("invalid username or password");
            }
        }
    }
}
