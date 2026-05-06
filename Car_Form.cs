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

namespace P5_Frontend_Car_App
{
    public partial class Car_Form : Form
    {
        int selectedCarId = 0;
        int filterManufacturerId = 0;
        int filterEngineId = 0;

        int _manuId;
        int _engineId;

        ApiService api = new ApiService();

        public Car_Form(int? manuId = null, int? engineId = null)
        {
            InitializeComponent();
            this.AutoScroll = true;

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
            txtYear.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

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

        async Task LoadCars()
        {
            try
            {
                var list = await api.GetAsync<List<Car>>("Car");

                if (filterManufacturerId != 0)
                    list = list.Where(c => c.ManufacturerId == filterManufacturerId).ToList();

                if (filterEngineId != 0)
                    list = list.Where(c => c.EngineCapacityId == filterEngineId).ToList();

                dataGridViewCar.DataSource = list.Select(c => new
                {
                    c.Id,
                    c.Name,
                    Manufacturer = c.Manufacturer?.Name,
                    Capacity = c.EngineCapacity?.Capacity,
                    c.Transmission,
                    c.FuelType,
                    c.Price,
                    c.Year,
                    c.ManufacturerId,
                    c.EngineCapacityId
                }).ToList();

                AddButtonsToGrid();
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
                var list = await api.GetAsync<Manufacturer>("Manufacturer");

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
                var list = await api.GetAsync<EngineCapacity>("EngineCapacity");

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
                await api.DeleteAsync($"Car/{id}");
                await LoadCars();
            }
        }

        void LoadCarToForm(int rowIndex)
        {
            var row = dataGridViewCar.Rows[rowIndex];

            selectedCarId = Convert.ToInt32(row.Cells["Id"].Value);

            txtName.Text = row.Cells["Name"].Value.ToString();
            txtPrice.Text = row.Cells["Price"].Value.ToString();
            txtYear.Text = row.Cells["Year"].Value.ToString();

            cmbManufacturerId.SelectedValue = row.Cells["ManufacturerId"].Value;
            cmbEngineId.SelectedValue = row.Cells["EngineCapacityId"].Value;

            button1.Text = "Update";
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
                    !int.TryParse(txtYear.Text, out var year))
                {
                    MessageBox.Show("Invalid price/year");
                    return;
                }

                var car = new Car
                {
                    Id = selectedCarId,
                    Name = txtName.Text,
                    ManufacturerId = (int)cmbManufacturerId.SelectedValue,
                    EngineCapacityId = (int)cmbEngineId.SelectedValue,
                    Transmission = (Transmission)cmbTransmission.SelectedItem,
                    FuelType = (FuelType)cmbFueltype.SelectedItem,
                    Price = price,
                    Year = year
                };

                if (selectedCarId == 0)
                {
                    await api.PostAsync("Car", car);
                    Log.Information("User added new car: {CarName}", txtName.Text);
                }
                else
                {
                    await api.PutAsync($"Car/{selectedCarId}", car);
                    Log.Information("User updated car: {CarId}, Name: {CarName}", selectedCarId, txtName.Text);
                }
                   

                ClearForm();
                await LoadCars();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to add/update cars");
                MessageBox.Show("Failed to add/update cars");
            }
        }

        void ClearForm()
        {
            selectedCarId = 0;
            button1.Text = "Add";

            txtName.Clear();
            txtPrice.Clear();
            txtYear.Clear();

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
