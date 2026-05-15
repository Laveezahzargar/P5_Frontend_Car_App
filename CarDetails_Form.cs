using P5_Frontend_Car_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P5_Frontend_Car_App.DTOs;

namespace P5_Frontend_Car_App
{
    public partial class CarDetails_Form : Form
    {
        private readonly CarDto _car;
        PictureBox pic;

        Label lblName;
        Label lblManufacturer;
        Label lblEngine;
        Label lblTransmission;
        Label lblFuel;
        Label lblYear;
        Label lblPrice;

        Button btnBuy;
        public CarDetails_Form(CarDto car)
        {
            InitializeComponent();
            _car = car;
            this.Width = 600;
            this.Height = 850;
            this.MinimumSize = new Size(500, 700);
            this.StartPosition =
                FormStartPosition.CenterScreen;

            ApplyResponsiveLayout();

            this.Resize += (s, e) =>
            {
                ApplyResponsiveLayout();
            };

            LoadDetails();
        }
        void LoadDetails()
        {
            this.BackColor = Color.White;

            pic = new PictureBox();

            pic.Width = 500;
            pic.Height = 300;

            pic.Top = 20;
            pic.Left = 30;

            pic.SizeMode = PictureBoxSizeMode.Zoom;

            if (!string.IsNullOrWhiteSpace(_car.ImageUrl))
            {
                pic.LoadAsync(_car.ImageUrl);
            }

            lblName = new Label();
            lblName.AutoSize = false;
            lblName.Width = 520;
            lblName.Height = 50;

            lblName.Text = _car.Name;

            lblName.Font =
                new Font("Segoe UI", 18, FontStyle.Bold);

            lblName.Top = 340;
            lblName.Left = 30;

            lblManufacturer = new Label();

            lblManufacturer.Text =
                $"Manufacturer: {_car.Manufacturer}";

            lblManufacturer.Top = 390;
            lblManufacturer.Left = 30;

            lblManufacturer.Width = 400;

            lblEngine = new Label();

            lblEngine.Text =
                $"Engine: {_car.EngineCapacity}";

            lblEngine.Top = 420;
            lblEngine.Left = 30;

            lblEngine.Width = 400;

            lblTransmission = new Label();

            lblTransmission.Text =
                $"Transmission: {_car.Transmission}";

            lblTransmission.Top = 450;
            lblTransmission.Left = 30;

            lblTransmission.Width = 400;

            lblFuel = new Label();

            lblFuel.Text =
                $"Fuel: {_car.FuelType}";

            lblFuel.Top = 480;
            lblFuel.Left = 30;

            lblFuel.Width = 400;

            lblYear = new Label();

            lblYear.Text =
                $"Year: {_car.Year}";

            lblYear.Top = 510;
            lblYear.Left = 30;

            lblYear.Width = 400;

            lblPrice = new Label();

            lblPrice.Text =
                $"Price: ${_car.Price:N0}";

            lblPrice.Font =
                new Font("Segoe UI", 16, FontStyle.Bold);

            lblPrice.ForeColor = Color.Green;

            lblPrice.Top = 550;
            lblPrice.Left = 30;

            lblPrice.Width = 500;
            lblPrice.Height = 40;

            lblPrice.AutoSize = false;

            btnBuy = new Button();

            btnBuy.Text = "Buy Now";

            btnBuy.Width = 180;
            btnBuy.Height = 45;

            btnBuy.Top = 610;
            btnBuy.Left = 30;

            btnBuy.BackColor =
                Color.FromArgb(0, 120, 215);

            btnBuy.ForeColor = Color.White;

            btnBuy.FlatStyle = FlatStyle.Flat;

            this.Controls.Add(pic);

            this.Controls.Add(lblName);
            this.Controls.Add(lblManufacturer);
            this.Controls.Add(lblEngine);
            this.Controls.Add(lblTransmission);
            this.Controls.Add(lblFuel);
            this.Controls.Add(lblYear);
            this.Controls.Add(lblPrice);

            this.Controls.Add(btnBuy);

            ApplyResponsiveLayout();
        }
        void ApplyResponsiveLayout()
        {
            if (pic == null ||
                lblName == null ||
                lblManufacturer == null ||
                lblEngine == null ||
                lblTransmission == null ||
                lblFuel == null ||
                lblYear == null ||
                lblPrice == null ||
                btnBuy == null)
            {
                return;
            }

            int margin = 30;

            // image responsive
            pic.Width = Math.Min(700,
                this.ClientSize.Width - (margin * 2));

            pic.Height =
                Math.Min(400, this.ClientSize.Height / 2);

            pic.Left =
                (this.ClientSize.Width - pic.Width) / 2;

            pic.Top = margin;

            int contentWidth = 500;

            int centerX =
                (this.ClientSize.Width - contentWidth) / 2;

            // name
            lblName.Width = contentWidth;
            lblName.Left = centerX;
            lblName.Top = pic.Bottom + 25;

            // manufacturer
            lblManufacturer.Width = contentWidth;
            lblManufacturer.Left = centerX;
            lblManufacturer.Top = lblName.Bottom + 15;

            // engine
            lblEngine.Width = contentWidth;
            lblEngine.Left = centerX;
            lblEngine.Top = lblManufacturer.Bottom + 12;

            // transmission
            lblTransmission.Width = contentWidth;
            lblTransmission.Left = centerX;
            lblTransmission.Top = lblEngine.Bottom + 12;

            // fuel
            lblFuel.Width = contentWidth;
            lblFuel.Left = centerX;
            lblFuel.Top = lblTransmission.Bottom + 12;

            // year
            lblYear.Width = contentWidth;
            lblYear.Left = centerX;
            lblYear.Top = lblFuel.Bottom + 12;

            // price
            lblPrice.Width = contentWidth;
            lblPrice.Left = centerX;
            lblPrice.Top = lblYear.Bottom + 20;

            // button
            btnBuy.Width = 240;
            btnBuy.Height = 45;

            btnBuy.Left =
                (this.ClientSize.Width - btnBuy.Width) / 2;

            btnBuy.Top = lblPrice.Bottom + 35;

            this.AutoScroll = true;
        }
    }
}
