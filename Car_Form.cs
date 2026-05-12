using P5_Frontend_Car_App.Models;
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
using Serilog;
using P5_Frontend_Car_App.DTOs;
using P5_Frontend_Car_App.Interfaces;

namespace P5_Frontend_Car_App
{
    public partial class Car_Form : Form
    {
        int selectedCarId = 0;
        int filterManufacturerId = 0;
        int filterEngineId = 0;
        string selectedImagePath = "";

        int _manuId;
        int _engineId;

        private readonly IApiService api;

        public Car_Form(IApiService apiService, int? manuId = null, int? engineId = null)
        {
            InitializeComponent();
            this.AutoScroll = true;
            api = apiService;

            _manuId = manuId ?? 0;
            _engineId = engineId ?? 0;

            if (manuId != null)
                filterManufacturerId = manuId.Value;

            if (engineId != null)
                filterEngineId = engineId.Value;
        }

        private async void Car_form_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            StyleButton(button1);
            ApplyResponsiveLayout();

            dataGridViewCar.AutoGenerateColumns = true;
            dataGridViewCar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadDropdowns();
            LoadYears();

            await LoadManufacturers();
            await LoadEngineCapacity();
            await LoadCars();

            if (_manuId != 0)
            {
                cmbManufacturerId.SelectedValue = _manuId;
                cmbManufacturerId.Enabled = false;
            }

            if (_engineId != 0)
            {
                cmbEngineId.SelectedValue = _engineId;
                cmbEngineId.Enabled = false;
            }

            ClearForm();
        }

        void ApplyResponsiveLayout()
        {
            dataGridViewCar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPrice.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbYear.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            cmbManufacturerId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbEngineId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbTransmission.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbFueltype.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }

        void StyleButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(0, 120, 215);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
        }

        void ApplyTheme()
        {
            this.BackColor = Color.White;
        }

        void LoadDropdowns()
        {
            cmbTransmission.DataSource = Enum.GetValues(typeof(Transmission));
            cmbFueltype.DataSource = Enum.GetValues(typeof(FuelType));
        }
        void LoadYears()
        {
            cmbYear.Items.Clear();

            for (int year = DateTime.Now.Year; year >= 1900; year--)
            {
                cmbYear.Items.Add(year);
            }

            cmbYear.SelectedIndex = 0;
        }

        async Task LoadCars()
        {
            try
            {
                var list = await api.GetAsync<List<CarDto>>("Car");

                if (filterManufacturerId != 0)
                    list = list.Where(c => c.ManufacturerId == filterManufacturerId).ToList();

                if (filterEngineId != 0)
                    list = list.Where(c => c.EngineCapacityId == filterEngineId).ToList();

                dataGridViewCar.DataSource = list.Select(c => new
                {
                    c.Id,
                    c.Name,

                    c.ManufacturerId,
                    c.EngineCapacityId,

                    Manufacturer = c.Manufacturer,
                    Capacity = c.EngineCapacity,

                    c.Transmission,
                    c.FuelType,

                    c.Price,
                    c.Year,
                }).ToList();

                dataGridViewCar.Columns["ManufacturerId"].Visible = false;
                dataGridViewCar.Columns["EngineCapacityId"].Visible = false;

                AddButtonsToGrid();
                StyleGridButtons();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to load cars");
                MessageBox.Show("Failed to load cars");
            }
        }

        async Task LoadManufacturers()
        {
            try
            {
                var list = await api.GetAsync<List<ManufacturerDto>>("Manufacturer");

                cmbManufacturerId.DisplayMember = "Name";
                cmbManufacturerId.ValueMember = "Id";
                cmbManufacturerId.DataSource = list;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to load manufacturers");
                MessageBox.Show("Failed to load manufacturers");
            }
        }

        async Task LoadEngineCapacity()
        {
            try
            {
                var list = await api.GetAsync<List<EngineCapacityDto>>("EngineCapacity");

                cmbEngineId.DisplayMember = "Capacity";
                cmbEngineId.ValueMember = "Id";
                cmbEngineId.DataSource = list;
            }
            catch(Exception ex) 
            {
                Log.Error(ex, "Failed to load engine capacities");
                MessageBox.Show("Failed to load engines");
            }
        }

        void AddButtonsToGrid()
        {
            if (dataGridViewCar.Columns["Edit"] != null)
                return;

            dataGridViewCar.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true
            });

            dataGridViewCar.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true
            });
        }

        private async void dataGridViewCar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dataGridViewCar.Rows[e.RowIndex].Cells["Id"].Value);
            string col = dataGridViewCar.Columns[e.ColumnIndex].Name;

            if (col == "Edit")
            {
                LoadCarToForm(e.RowIndex);
            }
            else if (col == "Delete")
            {
                await DeleteCar(id);
            }
        }

        void LoadCarToForm(int rowIndex)
        {
            var row = dataGridViewCar.Rows[rowIndex];

            selectedCarId = Convert.ToInt32(row.Cells["Id"].Value);

            txtName.Text = row.Cells["Name"].Value.ToString();
            txtPrice.Text = row.Cells["Price"].Value.ToString();
            cmbYear.Text = row.Cells["Year"].Value.ToString();

            cmbManufacturerId.SelectedValue = row.Cells["ManufacturerId"].Value;
            cmbEngineId.SelectedValue = row.Cells["EngineCapacityId"].Value;

            button1.Text = "Update";
        }
        void StyleGridButtons()
        {
            foreach (DataGridViewRow row in dataGridViewCar.Rows)
            {
                // Edit button
                DataGridViewButtonCell editBtn =
                    (DataGridViewButtonCell)row.Cells["Edit"];

                editBtn.Style.BackColor = Color.DodgerBlue;
                editBtn.Style.ForeColor = Color.White;
                editBtn.Style.SelectionBackColor = Color.RoyalBlue;
                editBtn.Style.SelectionForeColor = Color.White;

                // Delete button
                DataGridViewButtonCell deleteBtn =
                    (DataGridViewButtonCell)row.Cells["Delete"];

                deleteBtn.Style.BackColor = Color.Red;
                deleteBtn.Style.ForeColor = Color.White;
                deleteBtn.Style.SelectionBackColor = Color.DarkRed;
                deleteBtn.Style.SelectionForeColor = Color.White;
            }
        }
        async Task DeleteCar(int id)
        {
            try
            {
                if (id == 0)
                {
                    MessageBox.Show("Invalid car selected");
                    return;
                }

                var result = MessageBox.Show(
                    "Are you sure you want to delete this car?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                await api.DeleteAsync($"Car/{id}");

                Log.Warning("Car deleted: {Id}", id);

                MessageBox.Show("Car deleted successfully");

                await LoadCars();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to delete car.");
                MessageBox.Show("Operation failed: " + ex.Message);
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Fill all fields");
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text, out var price) ||
                    !int.TryParse(cmbYear.Text, out var year))
                {
                    MessageBox.Show("Invalid price/year");
                    return;
                }

                if (price <= 0)
                {
                    MessageBox.Show("Price cannot be negative or zero.");
                    return;
                }

                if (year < 1900 || year > DateTime.Now.Year + 1)
                {
                    MessageBox.Show("Invalid year");
                    return;
                }
                if (string.IsNullOrEmpty(selectedImagePath))
                {
                    MessageBox.Show("Please select an image");
                    return;
                }

                var cars = await api.GetAsync<List<CarDto>>("Car");

                bool exists = cars.Any(c =>
                    c.Name.Trim().ToLower() == txtName.Text.Trim().ToLower()
                    && c.ManufacturerId == (int)cmbManufacturerId.SelectedValue
                    && c.Year == year
                    && c.Id != selectedCarId);

                if (exists)
                {
                    MessageBox.Show("Car already exists");
                    return;
                }

                using var client = new HttpClient();
                using var form = new MultipartFormDataContent();

                form.Add(new StringContent(txtName.Text.Trim()), "Name");
                form.Add(new StringContent(cmbManufacturerId.SelectedValue.ToString()), "ManufacturerId");
                form.Add(new StringContent(cmbEngineId.SelectedValue.ToString()), "EngineCapacityId");

                form.Add(new StringContent(cmbTransmission.SelectedItem.ToString()), "Transmission");
                form.Add(new StringContent(cmbFueltype.SelectedItem.ToString()), "FuelType");

                form.Add(new StringContent(price.ToString()), "Price");
                form.Add(new StringContent(year.ToString()), "Year");

                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    var fileStream = File.OpenRead(selectedImagePath);

                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType =
                        new System.Net.Http.Headers.MediaTypeHeaderValue(
                            Path.GetExtension(selectedImagePath).ToLower() == ".png"
                                ? "image/png"
                                : "image/jpeg"
    );

                    form.Add(fileContent, "Image", Path.GetFileName(selectedImagePath));
                }

                HttpResponseMessage response;

                if (selectedCarId == 0)
                {
                    response = await client.PostAsync("http://localhost:5294/api/car", form);
                    Log.Information("User added new car: {CarName}", txtName.Text);
                }
                else
                {
                    response = await client.PutAsync($"http://localhost:5294/api/car/{selectedCarId}", form);
                    Log.Information("User updated car: {CarId}, Name: {CarName}", selectedCarId, txtName.Text);
                }

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed: {error}");
                    return;
                }

                ClearForm();
                selectedImagePath = "";
                await LoadCars();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to add/update cars");
                MessageBox.Show("Failed to add/update cars");
            }
          }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.png;*.jpeg";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = dialog.FileName;

                pictureBoxCar.Image = Image.FromFile(selectedImagePath);
            }
        }

        void ClearForm()
        {
            selectedCarId = 0;
            button1.Text = "Add";

            txtName.Clear();
            txtPrice.Clear();
            cmbYear.SelectedIndex = 0;

            if (cmbManufacturerId.Items.Count > 0)
                cmbManufacturerId.SelectedIndex = 0;

            if (cmbEngineId.Items.Count > 0)
                cmbEngineId.SelectedIndex = 0;
            cmbTransmission.SelectedIndex = 0;
            cmbFueltype.SelectedIndex = 0;
        }

        private async void btnShowAll_Click(object sender, EventArgs e)
        {
            filterManufacturerId = 0;
            filterEngineId = 0;

            await LoadCars();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
