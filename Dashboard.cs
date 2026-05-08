using P5_Frontend_Car_App.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace P5_Frontend_Car_App
{
    public partial class Dashboard : Form
    {
        int maxCardWidth = 260;
        int maxCardHeight = 140;
        ApiService api = new ApiService();

        Chart chartCars = new Chart();
        public Dashboard()
        {
            InitializeComponent();

            this.AutoScroll = false;
            this.DoubleBuffered = true;

            ApplyTheme();
            StyleCards();

            InitChart();

            panelCars.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            panelManufacturers.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            panelEngines.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            panelAverage.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            panelFuel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            panelYear.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        }
        private void InitChart()
        {
            chartCars.Parent = this;
            chartCars.SetBounds(50, 400, 800, 300);

            chartCars.ChartAreas.Clear();
            chartCars.Series.Clear();

            ChartArea area = new ChartArea("MainArea");
            chartCars.ChartAreas.Add(area);

            chartCars.BackColor = Color.White;
            chartCars.BorderlineColor = Color.LightGray;
            chartCars.BorderlineWidth = 1;

            this.Controls.Add(chartCars);
        }
        void LoadBarChart(List<Car> cars)
        {
            chartCars.Series.Clear();

            Series series = new Series("Cars")
            {
                ChartType = SeriesChartType.Column
            };

            var data = cars
                .GroupBy(c => c.Manufacturer.Name)
                .Select(g => new
                {
                    Name = g.Key,
                    Count = g.Count()
                });

            foreach (var item in data)
            {
                series.Points.AddXY(item.Name, item.Count);
            }

            chartCars.Series.Add(series);
        }
        private void Dashboard_Resize(object sender, EventArgs e)
        {
            ArrangeDashboard();
        }
        private void ArrangeDashboard()
        {
            int margin = 20;

            int cols = 3;
            int rows = 2;

            int availableWidth = this.ClientSize.Width - (margin * (cols + 1));
            int availableHeight = this.ClientSize.Height - (margin * (rows + 1));

            int cardWidth = Math.Min(maxCardWidth, availableWidth / cols);
            int cardHeight = Math.Min(maxCardHeight, availableHeight / rows);

            // center horizontally
            int totalGridWidth = (cardWidth * cols) + (margin * (cols - 1));
            int startX = (this.ClientSize.Width - totalGridWidth) / 2;

            int y1 = 80; // top padding (important so it doesn't stick to top)

            // Row 1
            panelCars.SetBounds(startX, y1, cardWidth, cardHeight);
            panelManufacturers.SetBounds(startX + (cardWidth + margin), y1, cardWidth, cardHeight);
            panelEngines.SetBounds(startX + (cardWidth + margin) * 2, y1, cardWidth, cardHeight);

            // Row 2
            int y2 = y1 + cardHeight + margin;

            panelAverage.SetBounds(startX, y2, cardWidth, cardHeight);
            panelFuel.SetBounds(startX + (cardWidth + margin), y2, cardWidth, cardHeight);
            panelYear.SetBounds(startX + (cardWidth + margin) * 2, y2, cardWidth, cardHeight);

            int chartY = y2 + cardHeight + 40;

            int chartWidth = Math.Max(300, this.ClientSize.Width - (startX * 2));
            chartCars.SetBounds(startX, chartY, chartWidth, 300);
        }
        private async void Dashboard_Form_Load(object sender, EventArgs e)
        {
            await LoadStatistics();
            ArrangeDashboard();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ArrangeDashboard();
        }
        void ApplyTheme()
        {
            this.BackColor = Color.FromArgb(245, 247, 250);
        }
        void StyleCards()
        {
            panelCars.Controls.Add(lblTotalCars);
            panelManufacturers.Controls.Add(lblTotalManufacturers);
            panelEngines.Controls.Add(lblTotalEngines);
            panelAverage.Controls.Add(lblAveragePrice);
            panelFuel.Controls.Add(lblTopFuel);
            panelYear.Controls.Add(lblNewestYear);

            StylePanel(panelCars);
            StylePanel(panelManufacturers);
            StylePanel(panelEngines);
            StylePanel(panelAverage);
            StylePanel(panelFuel);
            StylePanel(panelYear);

            StyleValueLabel(lblTotalCars);
            StyleValueLabel(lblTotalManufacturers);
            StyleValueLabel(lblTotalEngines);
            StyleValueLabel(lblAveragePrice);
            StyleValueLabel(lblTopFuel);
            StyleValueLabel(lblNewestYear);
        }
        void StylePanel(Panel panel)
        {
            panel.BackColor = Color.FromArgb(0, 120, 215);
            panel.BorderStyle = BorderStyle.None;
            panel.ForeColor = Color.White;
            panel.AutoSize = false;
            panel.Dock = DockStyle.None;
            panel.Size = new Size(200, 120); // fallback default
        }
        void StyleValueLabel(Label lbl)
        {
            lbl.Font = new Font("Segoe UI", 12);
            lbl.ForeColor = Color.White;
            lbl.BackColor = Color.Transparent;
            lbl.AutoSize = false;
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
        }
        async Task LoadStatistics()
        {
            try
            {
                var cars = await api.GetAsync<List<Car>>("Car");
                var manufacturers = await api.GetAsync<List<Manufacturer>>("Manufacturer");
                var engines = await api.GetAsync<List<EngineCapacity>>("EngineCapacity");

                lblTotalCars.Text = cars.Count.ToString();

                lblTotalManufacturers.Text = manufacturers.Count.ToString();

                lblTotalEngines.Text = engines.Count.ToString();
                lblAveragePrice.Text =
                    cars.Any()
                    ? cars.Average(c => c.Price).ToString("N2")
                    : "0";

                lblNewestYear.Text =
                    cars.Any()
                    ? cars.Max(c => c.Year).ToString()
                    : "-";

                lblTopFuel.Text =
                    cars.GroupBy(c => c.FuelType)
                        .OrderByDescending(g => g.Count())
                        .Select(g => g.Key.ToString())
                        .FirstOrDefault() ?? "-";

                LoadBarChart(cars);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to load dashboard statistics.");
                MessageBox.Show("Failed to load dashboard");
            }
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
