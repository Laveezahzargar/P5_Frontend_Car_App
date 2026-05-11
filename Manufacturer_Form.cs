using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using P5_Frontend_Car_App.Models;
using Serilog;
using P5_Frontend_Car_App.DTOs;
using P5_Frontend_Car_App.Services;
using P5_Frontend_Car_App.Interfaces;

namespace P5_Frontend_Car_App
{
    public partial class Manufacturer_Form : Form
    {
        int selectedManufacturerId = 0;
        private readonly IApiService api;

        public Manufacturer_Form(IApiService apiService)
        {
            InitializeComponent();
            this.AutoScroll = true;
            api = apiService;
        }

        private async void Manufacturer_form_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            StyleButton(button1);
            ApplyResponsiveLayout();

            // IMPORTANT: grid setup must be here, not constructor
            dataGridViewManufacturer.AutoGenerateColumns = true;
            dataGridViewManufacturer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            await LoadManufacturers();
        }
        void ApplyResponsiveLayout()
        {
            // Grid fills remaining space
            dataGridViewManufacturer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Textboxes stretch horizontally
            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Button stays top-right
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }
        void StyleGridButtons()
        {
            foreach (DataGridViewRow row in dataGridViewManufacturer.Rows)
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

                // View Cars button
                DataGridViewButtonCell viewBtn =
                    (DataGridViewButtonCell)row.Cells["ViewCars"];

                viewBtn.Style.BackColor = Color.SeaGreen;
                viewBtn.Style.ForeColor = Color.White;
                viewBtn.Style.SelectionBackColor = Color.DarkGreen;
                viewBtn.Style.SelectionForeColor = Color.White;
            }
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

        async Task LoadManufacturers()
        {
            try
            {
                var list = await api.GetAsync<List<ManufacturerDto>>("Manufacturer");

                dataGridViewManufacturer.DataSource = list;

                AddButtons();
                StyleGridButtons();
             
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to load manufacturers.");
                MessageBox.Show("Failed to load manufacturers.");
            }
        }

        void AddButtons()
        {
            // Prevent duplicate buttons
            if (dataGridViewManufacturer.Columns["ViewCars"] != null)
                return;

            dataGridViewManufacturer.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "ViewCars",
                HeaderText = "Cars",
                Text = "View Cars",
                UseColumnTextForButtonValue = true
            });

            dataGridViewManufacturer.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true
            });

            dataGridViewManufacturer.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true
            });
        }

        private async void dataGridViewManufacturer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dataGridViewManufacturer.Rows[e.RowIndex].Cells["Id"].Value);
            string column = dataGridViewManufacturer.Columns[e.ColumnIndex].Name;

            if (column == "ViewCars")
            {
                var form = new Car_Form(api, id, null);
                form.ShowDialog();
            }
            else if (column == "Edit")
            {
                txtName.Text = dataGridViewManufacturer.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                txtDescription.Text = dataGridViewManufacturer.Rows[e.RowIndex].Cells["Description"].Value.ToString();

                selectedManufacturerId = id;
                button1.Text = "Update";
            }
            else if (column == "Delete")
            {
                await DeleteManufacturer(id);
            }
        }
        async Task DeleteManufacturer(int id)
        {
            try
            {
                if (id == 0)
                {
                    MessageBox.Show("Invalid manufacturer selected");
                    return;
                }

                var confirm = MessageBox.Show(
                    "Are you sure you want to delete this manufacturer?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                await api.DeleteAsync($"Manufacturer/{id}");

                await LoadManufacturers();

                Log.Warning("Manufacturer deleted: {Id}", id);

                MessageBox.Show("Manufacturer deleted successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to delete manufacturers.");

                MessageBox.Show(
                    "Cannot delete manufacturer because it is linked to cars.");
            }
        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                   string.IsNullOrWhiteSpace(txtDescription.Text))
                {
                    MessageBox.Show("Fill all fields");
                    return;
                }

                var data = new
                {
                    name = txtName.Text,
                    description = txtDescription.Text
                };

                var manufacturers = await api.GetAsync<List<ManufacturerDto>>("Manufacturer");

                bool exists = manufacturers.Any(m =>
                    m.Name.ToLower() == txtName.Text.Trim().ToLower()
                    && m.Id != selectedManufacturerId);

                if (exists)
                {
                    MessageBox.Show("Manufacturer already exists");
                    return;
                }
                if (selectedManufacturerId == 0)
                {
                    // CREATE
                    await api.PostAsync("Manufacturer", data);
                    Log.Information("User added new manufacturer: {ManufacturerName}", txtName.Text);
                }
                else
                {
                    // UPDATE
                    await api.PutAsync($"Manufacturer/{selectedManufacturerId}", data);
                    Log.Information("User updated manufacturer: {ManufacturerId}, Name: {ManufacturerName}", selectedManufacturerId, txtName.Text);
                }
                await LoadManufacturers();

                ResetForm();

                MessageBox.Show(
                    selectedManufacturerId == 0
                    ? "Manufacturer added successfully"
                    : "Manufacturer updated successfully");

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to add/update manufacturer.");
                MessageBox.Show("Operation failed.");
            }
        }

        void ResetForm()
        {
            selectedManufacturerId = 0;
            button1.Text = "Add";
            txtName.Clear();
            txtDescription.Clear();
        }

    }
}

