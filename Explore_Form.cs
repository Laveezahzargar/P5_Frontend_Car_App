using P5_Frontend_Car_App.DTOs;
using P5_Frontend_Car_App.Interfaces;
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
    public partial class Explore_Form : Form
    {
        private readonly IApiService _api;
        public Explore_Form(IApiService apiService)
        {
            InitializeComponent();

            _api = apiService;

            this.Width = 1550;

            InitLayout();
        }
        private FlowLayoutPanel flow;

        void InitLayout()
        {
            flow = new FlowLayoutPanel();

            flow.Dock = DockStyle.Fill;
            flow.AutoScroll = true;
            flow.WrapContents = true;
            flow.Padding = new Padding(20);

            this.Controls.Add(flow);
        }

        private async void ExploreCars_Load(object sender, EventArgs e)
        {
            await LoadCars();
        }

        async Task LoadCars()
        {
            var car1 = await _api.GetAsync<ApiResponse<List<CarDto>>>("Car");

            var cars = car1.Data;

            flow.Controls.Clear();

            foreach (var car in cars)
            {
                flow.Controls.Add(CreateCarCard(car));
            }
        }
        Panel CreateCarCard(CarDto car)
        {
            Panel card = new Panel();

            card.Width = 260;
            card.Height = 430;

            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(15);

            PictureBox pic = new PictureBox();

            pic.Width = 220;
            pic.Height = 140;
            pic.Top = 10;
            pic.Left = 15;

            pic.SizeMode = PictureBoxSizeMode.Zoom;

            // image loading
            if (!string.IsNullOrWhiteSpace(car.ImageUrl))
            {
                try
                {
                    pic.LoadAsync(car.ImageUrl);
                }
                catch
                {

                }
            }

            int y = 160;

            // Name
            Label lblName = new Label();
            lblName.Text = car.Name;
            lblName.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblName.Top = y;
            lblName.Left = 10;
            lblName.Width = 230;

            y += 35;

            // Manufacturer
            Label lblManufacturer = new Label();
            lblManufacturer.Text = $"Manufacturer: {car.Manufacturer}";
            lblManufacturer.Top = y;
            lblManufacturer.Left = 10;
            lblManufacturer.Width = 230;

            y += 30;

            // Engine
            Label lblEngine = new Label();
            lblEngine.Text = $"Engine: {car.EngineCapacity}";
            lblEngine.Top = y;
            lblEngine.Left = 10;
            lblEngine.Width = 230;

            y += 30;

            // Transmission
            Label lblTransmission = new Label();
            lblTransmission.Text = $"Transmission: {car.Transmission}";
            lblTransmission.Top = y;
            lblTransmission.Left = 10;
            lblTransmission.Width = 230;

            y += 30;

            // Fuel
            Label lblFuel = new Label();
            lblFuel.Text = $"Fuel: {car.FuelType}";
            lblFuel.Top = y;
            lblFuel.Left = 10;
            lblFuel.Width = 230;

            y += 30;

            // Year
            Label lblYear = new Label();
            lblYear.Text = $"Year: {car.Year}";
            lblYear.Top = y;
            lblYear.Left = 10;
            lblYear.Width = 230;

            y += 30;

            // Price
            Label lblPrice = new Label();
            lblPrice.Text = $"Price: ${car.Price:N0}";
            lblPrice.Top = y;
            lblPrice.Left = 10;
            lblPrice.Width = 230;

            y += 45;

            // Button
            Button btn = new Button();

            btn.Text = "View Details";

            btn.Width = 200;
            btn.Height = 35;

            btn.Left = 25;
            btn.Top = y;

            btn.BackColor = Color.FromArgb(0, 120, 215);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;

            btn.Click += (s, e) =>
            {
                CarDetails_Form details =
                    new CarDetails_Form(car,_api);

                details.ShowDialog();
            };

            // Add controls
            card.Controls.Add(pic);

            card.Controls.Add(lblName);
            card.Controls.Add(lblManufacturer);
            card.Controls.Add(lblEngine);
            card.Controls.Add(lblTransmission);
            card.Controls.Add(lblFuel);
            card.Controls.Add(lblYear);
            card.Controls.Add(lblPrice);

            card.Controls.Add(btn);

            return card;
        }
    }
}

