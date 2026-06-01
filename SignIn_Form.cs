using P5_Frontend_Car_App.DTOs.User;
using P5_Frontend_Car_App.Interfaces;
using Serilog;
using System.Drawing.Drawing2D;


namespace P5_Frontend_Car_App
{
    public partial class SignIn_Form : Form
    {
        private readonly IApiService _api;
        private bool _otpMode = false;
        private string _pendingEmail = string.Empty;
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
            StyleTextBox(txtOtp);

            txtOtp.Visible = false;
            lblOtp.Visible = false;

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

        //private async void btnLogin_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var username = txtUsername.Text.Trim();
        //        var password = txtPassword.Text;

        //        if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
        //            string.IsNullOrWhiteSpace(txtPassword.Text))
        //        {
        //            MessageBox.Show("Please fill all fields");
        //            return;
        //        }

        //        btnLogin.Enabled = false;
        //        btnLogin.Text = "Please wait...";

        //        var result = await _api.PostAsync<LoginResponseDto>(
        //            "User/login",
        //            new LoginRequestDto
        //            {
        //                Username = username,
        //                Password = password
        //            });

        //        if (result == null)
        //        {
        //            MessageBox.Show("Invalid login response");
        //            return;
        //        }

        //        MessageBox.Show("Login successful!");

        //        Log.Information("User {Username} logged in", result.Username);

        //        this.Hide();
        //        new Welcome_Form(_api, result.Role, result.Username).ShowDialog();
        //        this.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Login failed");

        //        txtPassword.Clear();
        //        txtPassword.Focus();

        //        // ✅ EMAIL NOT VERIFIED FLOW
        //        if (ex.Message.Contains("EMAIL_NOT_VERIFIED"))
        //        {
        //            var resend = MessageBox.Show(
        //                "Your email is not verified. Resend verification code?",
        //                "Verification Required",
        //                MessageBoxButtons.YesNo
        //            );

        //            if (resend == DialogResult.Yes)
        //            {
        //                try
        //                {
        //                    await _api.PostAsync<bool>(
        //                        "User/SendVerificationCode",
        //                        new { Email = txtUsername.Text.Trim() } // ⚠️ see note below
        //                    );

        //                    MessageBox.Show("Verification code sent.");

        //                    // OPTIONAL: show OTP UI (like signup)
        //                    txtOtp.Visible = true;
        //                    lblOtp.Visible = true;
        //                }
        //                catch
        //                {
        //                    MessageBox.Show("Failed to resend verification code.");
        //                }
        //            }

        //            return;
        //        }

        //        // ✅ ACCOUNT LOCKED
        //        if (ex.Message.Contains("locked"))
        //        {
        //            MessageBox.Show("Account locked. Try again later.");
        //            return;
        //        }

        //        // ✅ DEFAULT
        //        MessageBox.Show("Invalid username or password");
        //    }
        //    finally
        //    {
        //        btnLogin.Enabled = true;
        //        btnLogin.Text = "Login";
        //    }
        //}
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var username = txtUsername.Text.Trim();
                var password = txtPassword.Text;

                if (!_otpMode)
                {
                    // 🔹 NORMAL LOGIN
                    if (string.IsNullOrWhiteSpace(username) ||
                        string.IsNullOrWhiteSpace(password))
                    {
                        MessageBox.Show("Please fill all fields");
                        return;
                    }

                    btnLogin.Enabled = false;
                    btnLogin.Text = "Please wait...";

                    var result = await _api.PostAsync<LoginResponseDto>(
                        "User/login",
                        new LoginRequestDto
                        {
                            Username = username,
                            Password = password
                        });

                    if (result == null)
                    {
                        MessageBox.Show("Invalid login response");
                        return;
                    }

                    MessageBox.Show("Login successful!");

                    this.Hide();
                    new Welcome_Form(_api, result.Role, result.Username).ShowDialog();
                    this.Close();
                }
                else
                {
                    // 🔹 OTP VERIFY FLOW
                    if (string.IsNullOrWhiteSpace(txtOtp.Text))
                    {
                        MessageBox.Show("Enter verification code");
                        return;
                    }

                    btnLogin.Enabled = false;
                    btnLogin.Text = "Verifying...";

                    var result = await _api.PostAsync<LoginResponseDto>(
                        "User/VerifyLoginOtp",   // ✅ Correct endpoint
                        new
                        {
                            Email = _pendingEmail,   // ✅ Correct email source
                            Code = txtOtp.Text.Trim()
                        });

                    if (result == null)
                    {
                        MessageBox.Show("Invalid or expired code");
                        return;
                    }

                    MessageBox.Show("Verification successful!");

                    this.Hide();
                    new Welcome_Form(_api, result.Role, result.Username).ShowDialog();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Login failed");

                txtPassword.Clear();
                txtPassword.Focus();

                // 🔹 EMAIL NOT VERIFIED FLOW
                if (ex.Message.Contains("EMAIL_NOT_VERIFIED"))
                {
                    var resend = MessageBox.Show(
                        "Your email is not verified. Resend verification code?",
                        "Verification Required",
                        MessageBoxButtons.YesNo
                    );

                    if (resend == DialogResult.Yes)
                    {
                        try
                        {
                            // ⚠️ Ideally extract email from backend response
                            // TEMP fallback (if username == email)
                       //****     _pendingEmail = ;

                            await _api.PostAsync<bool>(
                                "User/SendVerificationCode",
                                new { Email = _pendingEmail }
                            );

                            MessageBox.Show("Verification code sent.");

                            // 🔥 SWITCH TO OTP MODE
                            txtOtp.Visible = true;
                            lblOtp.Visible = true;

                            txtUsername.Enabled = false;
                            txtPassword.Enabled = false;

                            _otpMode = true;

                            btnLogin.Text = "Verify Code";

                            txtOtp.Focus();
                        }
                        catch
                        {
                            MessageBox.Show("Failed to resend verification code.");
                        }
                    }

                    return;
                }

                // 🔹 ACCOUNT LOCKED
                if (ex.Message.Contains("locked"))
                {
                    MessageBox.Show("Account locked. Try again later.");
                    return;
                }

                // 🔹 DEFAULT ERROR
                MessageBox.Show("Invalid username or password");
            }
            finally
            {
                btnLogin.Enabled = true;

                if (!_otpMode)
                    btnLogin.Text = "Login";
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
