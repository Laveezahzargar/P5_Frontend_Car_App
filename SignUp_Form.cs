using P5_Frontend_Car_App.Interfaces;
using Serilog;
using P5_Frontend_Car_App.DTOs.User;

namespace P5_Frontend_Car_App
{
    public partial class SignUp_Form : Form
    {
        private readonly IApiService _api;
        public SignUp_Form(IApiService apiService)
        {
            InitializeComponent();

            _api=apiService;
        }
        private void SignUp_Form_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            ApplyResponsiveLayout();

            StyleButton(btnSignUp);

            StyleTextBox(txtFullName);
            StyleTextBox(txtEmail);
            StyleTextBox(txtUsername);
            StyleTextBox(txtPassword);
            StyleTextBox(txtConfirmPassword);

            txtPassword.PasswordChar = '*';
            txtConfirmPassword.PasswordChar = '*';
        }
        void ApplyTheme()
        {
            this.BackColor = Color.White;

            this.Font = new Font("Segoe UI", 10);
        }

        void ApplyResponsiveLayout()
        {
            txtFullName.Anchor =
                AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            txtEmail.Anchor =
                AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            txtUsername.Anchor =
                AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            txtPassword.Anchor =
                AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            txtConfirmPassword.Anchor =
                AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            btnSignUp.Anchor =
                AnchorStyles.Top | AnchorStyles.Right;
        }

        void StyleButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(0, 120, 215);
            btn.ForeColor = Color.White;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.Height = 40;

            btn.Cursor = Cursors.Hand;

            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        void StyleTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.FixedSingle;

            txt.Font = new Font("Segoe UI", 10);

            txt.BackColor = Color.White;
            txt.ForeColor = Color.Black;

            txt.Height = 30;
        }
        private async void btnSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs()) return;

                btnSignUp.Enabled = false;
                btnSignUp.Text = "Please wait...";

                var result = await _api.PostAsync<UserDto>("user",
                    new RegisterRequestDto
                {
                    FullName = txtFullName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text
                });

                if (result == null)
                {
                    MessageBox.Show("Unexpected server response");
                    return;
                }

                MessageBox.Show("Account created successfully");

                Log.Information("User {Username} registered", result.Username);

                ClearForm();

                this.Hide();
                new Welcome_Form(_api, result.Role, result.Username).ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Signup failed");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnSignUp.Enabled = true;
                btnSignUp.Text = "Sign Up";
            }
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Please fill all fields");
                return false;
            }

            // Email validation (better than Contains("@"))
            try
            {
                var addr = new System.Net.Mail.MailAddress(txtEmail.Text.Trim());
            }
            catch
            {
                MessageBox.Show("Invalid email format");
                return false;
            }

            // Username rules
            if (txtUsername.Text.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters");
                return false;
            }

            // Password rules
            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters");
                return false;
            }

            // Optional stronger password check
            if (!txtPassword.Text.Any(char.IsUpper) ||
                !txtPassword.Text.Any(char.IsLower) ||
                !txtPassword.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Password must contain uppercase, lowercase, and a number");
                return false;
            }
            // Confirm password
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match");
                return false;
            }

            return true;
        }
        void ClearForm()
        {
            txtFullName.Clear();
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            var signIn_form = new SignIn_Form(_api);
            signIn_form.ShowDialog();
            this.Close();
        }
    }
}
