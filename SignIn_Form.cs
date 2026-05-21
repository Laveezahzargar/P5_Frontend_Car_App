using P5_Frontend_Car_App.DTOs.User;
using P5_Frontend_Car_App.Interfaces;
using Serilog;
using System.Drawing.Drawing2D;


namespace P5_Frontend_Car_App
{
    public partial class SignIn_Form : Form
    {
        private readonly IApiService _api;
        public SignIn_Form(IApiService apiService)
        {
            InitializeComponent();

            _api = apiService;
        }
        private void SignIn_Form_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            ApplyResponsiveLayout();

            StyleButton(btnLogin);

            StyleTextBox(txtUsername);
            StyleTextBox(txtPassword);

            txtPassword.PasswordChar = '*';
            chkShowPassword.Checked = false;

            txtUsername.Focus();
        }

        void ApplyTheme()
        {
            this.BackColor = Color.FromArgb(245, 247, 250);

            this.Font = new Font("Segoe UI", 10);

            btnLogin.BackColor = Color.FromArgb(37, 99, 235);

            btnLogin.ForeColor = Color.White;
        }

        void ApplyResponsiveLayout()
        {
            txtUsername.Anchor =
                AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            txtPassword.Anchor =
                AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            btnLogin.Anchor =
                AnchorStyles.Top | AnchorStyles.Right;
        }

        void StyleButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(37, 99, 235);
            btn.ForeColor = Color.White;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.Height = 42;

            btn.Cursor = Cursors.Hand;

            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            GraphicsPath path = new GraphicsPath();

            path.AddArc(0, 0, 20, 20, 180, 90);
            path.AddArc(btn.Width - 20, 0, 20, 20, 270, 90);
            path.AddArc(btn.Width - 20, btn.Height - 20, 20, 20, 0, 90);
            path.AddArc(0, btn.Height - 20, 20, 20, 90, 90);

            btn.Region = new Region(path);
        }

        void StyleTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.FixedSingle;

            txt.Font = new Font("Segoe UI", 10);

            txt.BackColor = Color.White;
            txt.ForeColor = Color.Black;

            txt.Height = 30;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please fill all fields");
                    return;
                }

                btnLogin.Enabled = false;
                btnLogin.Text = "Please wait...";

                var result = await _api.PostAsync<LoginResponseDto>(
                    "user/login",
                    new LoginRequestDto
                    {
                        Username = txtUsername.Text.Trim(),
                        Password = txtPassword.Text
                    });

                if (result == null)
                {
                    MessageBox.Show("Invalid login response");
                    return;
                }

                MessageBox.Show("Login successful!");

                Log.Information("User {Username} logged in", result.Username);

                this.Hide();
                new Welcome_Form(_api, result.Role, result.Username).ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Login failed");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Login";
            }
        }

        private void chkShowPassword_CheckedChanged( object sender,  EventArgs e)
        {
            txtPassword.PasswordChar =
                chkShowPassword.Checked ? '\0' : '*';
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            var SignUp_form = new SignUp_Form(_api);
            SignUp_form.ShowDialog();
            this.Close();
        }
    }
}
