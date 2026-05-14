
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using P5_Frontend_Car_App.Interfaces;
using P5_Frontend_Car_App.Services;



namespace P5_Frontend_Car_App
{
    public partial class Main_Form : Form
    {
        private readonly IApiService api;
        public Main_Form(IApiService apiService)
        {
            InitializeComponent();

            api = apiService;

            Color bg = Color.White;
            Color panel = Color.FromArgb(245, 245, 245);
            Color accent = Color.FromArgb(0, 120, 215);
            Color text = Color.Black;

            StyleButton(button1);
            StyleButton(button2);
            StyleButton(button3);
            StyleButton(button5);
            StyleButton(button4);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            ApplyResponsiveLayout();
        }
        void ApplyResponsiveLayout()
        {
            var layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 1;
            layout.RowCount = 7;

            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20)); // top space
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 80)); // bottom space

            layout.Controls.Add(button1, 0, 1);
            layout.Controls.Add(button2, 0, 2);
            layout.Controls.Add(button3, 0, 3);
            layout.Controls.Add(button5, 0, 5);
            layout.Controls.Add(button4, 0, 4);

            // center buttons
            button1.Anchor = AnchorStyles.None;
            button2.Anchor = AnchorStyles.None;
            button3.Anchor = AnchorStyles.None;
            button5.Anchor = AnchorStyles.None;
            button4.Anchor = AnchorStyles.None;

            // consistent spacing
            button1.Margin = new Padding(0, 10, 0, 10);
            button2.Margin = new Padding(0, 10, 0, 10);
            button3.Margin = new Padding(0, 10, 0, 10);
            button5.Margin = new Padding(0, 10, 0, 10);
            button4.Margin = new Padding(0, 10, 0, 10);

            this.Controls.Add(layout);
        }
        private void StyleButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(0, 120, 215);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
        }
        private void ApplyTheme()
        {
            this.BackColor = Color.White;
        }
        private void btnCars_Click(object sender, EventArgs e)
        {
            Car_Form f = new Car_Form(api);
            f.ShowDialog();
        }
        private void btnManf_Click(object sender, EventArgs e)
        {
            Manufacturer_Form f = new Manufacturer_Form(api);
            f.ShowDialog();
        }
        private void btnEngine_Click(object sender, EventArgs e)
        {
            EngineCapacity_Form f = new EngineCapacity_Form(api);
            f.ShowDialog();
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard f = new Dashboard(api);
            f.ShowDialog();
        }
        private void btnUser_Click(object sender, EventArgs e)
        {
            User_Form f = new User_Form(api);
            f.ShowDialog();
        }
    }
    
}
