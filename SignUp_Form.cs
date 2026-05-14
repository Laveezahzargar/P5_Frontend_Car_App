using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;
using System.Net.Http;

namespace P5_Frontend_Car_App
{
    public partial class SignUp_Form : Form
    {
        public SignUp_Form()
        {
            InitializeComponent();
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
                // validation
                if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                {
                    MessageBox.Show("Fill all fields");
                    return;
                }

                // email validation
                if (!txtEmail.Text.Contains("@"))
                {
                    MessageBox.Show("Invalid email");
                    return;
                }

                // password length
                if (txtPassword.Text.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters");
                    return;
                }

                // password match
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match");
                    return;
                }

                using var client = new HttpClient();

                var form = new MultipartFormDataContent
        {
            { new StringContent(txtFullName.Text.Trim()), "FullName" },
            { new StringContent(txtEmail.Text.Trim()), "Email" },
            { new StringContent(txtUsername.Text.Trim()), "Username" },
            { new StringContent(txtPassword.Text), "Password" }
        };

                var response = await client.PostAsync(
                    "http://localhost:5294/api/auth/register",
                    form);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();

                    MessageBox.Show($"Registration failed: {error}");

                    ClearForm();

                    return;
                }

                MessageBox.Show("Account created successfully");
                Log.Information("User {Username} registered successfully", txtUsername.Text.Trim());

                ClearForm();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Signup failed");
                MessageBox.Show("Signup failed");
            }
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
            var signIn_form = new SignIn_Form();
            signIn_form.ShowDialog();
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
