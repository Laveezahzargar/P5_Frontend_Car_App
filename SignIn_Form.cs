using P5_Frontend_Car_App.DTOs;
using P5_Frontend_Car_App.Interfaces;
using P5_Frontend_Car_App.Types;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;


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
                // validation
                if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show(
                        "Please fill all fields",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                btnLogin.Enabled = false;
                btnLogin.Text = "Please wait...";

                {
                    var form = new MultipartFormDataContent
                    {
                        {
                            new StringContent(txtUsername.Text.Trim()),
                            "Username"
                        },

                        {
                            new StringContent(txtPassword.Text),
                            "Password"
                        }
                    };
                    var cookies = new CookieContainer();

                    var handler = new HttpClientHandler
                    {
                        CookieContainer = cookies,
                        UseCookies = true
                    };

                    using var client = new HttpClient(handler);
                    var response = await client.PostAsync(
                        "http://localhost:5294/api/user/login",
                        form);

                    if (!response.IsSuccessStatusCode)
                    {
                        var error =
                            await response.Content.ReadAsStringAsync();

                        MessageBox.Show(
                            $"Login failed: {error}",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        txtPassword.Clear();

                        return;
                    }
                    // VIEW COOKIES
                    var uri =
                        new Uri("http://localhost:5294");

                    var cookieCollection =
                        cookies.GetCookies(uri);

                    foreach (Cookie cookie in cookieCollection)
                    {
                        Log.Information(
                            "Cookie -> {Name} = {Value}",
                            cookie.Name,
                            cookie.Value);
                    }
                    MessageBox.Show(
                        "Login successful!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Log.Information(
                        "User {Username} logged in successfully",
                        txtUsername.Text.Trim());

                    var json = await response.Content.ReadAsStringAsync();

                    var options = new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    MessageBox.Show(json);
                    var responseObj = JsonSerializer.Deserialize<ApiResponse<LoginResponseDto>>(json, options);
                    var loginData = responseObj?.Data;

                    Role roleEnum = loginData?.Role ?? Role.Customer;

                    Welcome_Form welcome = new Welcome_Form(_api,roleEnum);
                    welcome.ShowDialog();

                    txtUsername.Clear();
                    txtPassword.Clear();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Login failed");

                MessageBox.Show(
                    "Something went wrong",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
            var SignUp_form = new SignUp_Form(_api);
            SignUp_form.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
