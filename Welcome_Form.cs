using P5_Frontend_Car_App.Interfaces;
using P5_Frontend_Car_App.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P5_Frontend_Car_App
{
    public partial class Welcome_Form : Form
    {
        private readonly IApiService api;
        private Role _role;
        public Welcome_Form(IApiService apiService, Role role)
        {
            InitializeComponent();
            api = apiService;

            StyleButton(btnHomepage);
            StyleButton(btnExploreCars);
            StyleButton(btnExit);

            ApplyTheme();
            ApplyResponsiveLayout();
            _role = role;
        }
        private void ApplyTheme()
        {
            this.BackColor = Color.White;

            lblTitle.ForeColor = Color.Black;
            lblTagline.ForeColor = Color.DimGray;
            lblFooter.ForeColor = Color.Gray;

            lblTitle.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblTagline.Font = new Font("Segoe UI", 12, FontStyle.Italic);
            lblFooter.Font = new Font("Segoe UI", 9);

            pictureBoxBanner.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void StyleButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(0, 120, 215);
            btn.ForeColor = Color.White;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.Width = 180;
            btn.Height = 45;

            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        private void ApplyResponsiveLayout()
        {
            var layout = new TableLayoutPanel();

            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 1;
            layout.RowCount = 7;

            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 15));

            lblTitle.Anchor = AnchorStyles.None;
            lblTagline.Anchor = AnchorStyles.None;

            pictureBoxBanner.Anchor = AnchorStyles.None;

            btnHomepage.Anchor = AnchorStyles.None;
            btnExploreCars.Anchor = AnchorStyles.None;
            btnExit.Anchor = AnchorStyles.None;

            lblFooter.Anchor = AnchorStyles.None;

            btnHomepage.Margin = new Padding(0, 15, 0, 10);
            btnExploreCars.Margin = new Padding(0, 15, 0, 10);
            btnExit.Margin = new Padding(0, 10, 0, 20);

            layout.Controls.Add(lblTitle, 0, 1);
            layout.Controls.Add(lblTagline, 0, 2);
            layout.Controls.Add(pictureBoxBanner, 0, 3);
            layout.Controls.Add(btnHomepage, 0, 4);
            layout.Controls.Add(btnExploreCars, 0, 5);
            layout.Controls.Add(btnExit, 0, 6);
            layout.Controls.Add(lblFooter, 0, 7);

            this.Controls.Add(layout);
        }
        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            if (_role == Role.Customer)
            {
                btnHomepage.Visible = false; // hide button
            }
            else if (_role == Role.Admin)
            {
                btnHomepage.Visible = true; // show button
            }
        }
        private void btnHomepage_Click(object sender, EventArgs e)
        {
            var main_form = new Main_Form(api);
            main_form.ShowDialog();
        }
        private void btnExploreCars_Click(object sender, EventArgs e)
        {
            var explore_form = new Explore_Form(api);
            explore_form.ShowDialog();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            var signUp_form = new SignUp_Form(api);
            signUp_form.ShowDialog();
        }
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            var signIn_form = new SignIn_Form(api);
            signIn_form.ShowDialog();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
