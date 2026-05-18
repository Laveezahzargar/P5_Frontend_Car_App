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
using Serilog;
using P5_Frontend_Car_App.DTOs;

namespace P5_Frontend_Car_App
{
    public partial class User_Form : Form
    {
        int selectedUserId = 0;

        private readonly IApiService api;

        public User_Form(IApiService apiService)
        {
            InitializeComponent();

            this.AutoScroll = true;

            api = apiService;
        }

        private async void User_Form_Load(
            object sender,
            EventArgs e)
        {
            ApplyTheme();

            StyleButton(btnAdd);

            ApplyResponsiveLayout();

            txtPassword.UseSystemPasswordChar = true;

            dataGridViewUser.AutoGenerateColumns = true;

            dataGridViewUser.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            await LoadUsers();

            ClearForm();
        }

        void ApplyTheme()
        {
            this.BackColor = Color.White;
        }

        void StyleButton(Button btn)
        {
            btn.BackColor =
                Color.FromArgb(0, 120, 215);

            btn.ForeColor = Color.White;

            btn.FlatStyle = FlatStyle.Flat;

            btn.FlatAppearance.BorderSize = 0;
        }

        void ApplyResponsiveLayout()
        {
            dataGridViewUser.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            txtFullName.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            txtEmail.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            txtUsername.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            txtPassword.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            btnAdd.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;
        }

        async Task LoadUsers()
        {
            try
            {
                var users =
                    await api.GetAsync<List<UserDto>>(
                        "User");

                dataGridViewUser.DataSource =
                    users.Select(x => new
                    {
                        x.Id,
                        x.FullName,
                        x.Email,
                        x.Username,
                        x.Role,
                        x.CreatedAt
                    }).ToList();

                AddButtonsToGrid();

                StyleGridButtons();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to load users");

                MessageBox.Show(
                    "Failed to load users");
            }
        }

        void AddButtonsToGrid()
        {
            if (dataGridViewUser.Columns["Edit"] != null)
                return;

            dataGridViewUser.Columns.Add(
                new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true
                });

            dataGridViewUser.Columns.Add(
                new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true
                });
        }

        void StyleGridButtons()
        {
            foreach (DataGridViewRow row
                in dataGridViewUser.Rows)
            {
                DataGridViewButtonCell editBtn =
                    (DataGridViewButtonCell)
                    row.Cells["Edit"];

                editBtn.Style.BackColor =
                    Color.DodgerBlue;

                editBtn.Style.ForeColor =
                    Color.White;

                DataGridViewButtonCell deleteBtn =
                    (DataGridViewButtonCell)
                    row.Cells["Delete"];

                deleteBtn.Style.BackColor =
                    Color.Red;

                deleteBtn.Style.ForeColor =
                    Color.White;
            }
        }

        private async void btnAdd_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (
                    string.IsNullOrWhiteSpace(
                        txtFullName.Text) ||

                    string.IsNullOrWhiteSpace(
                        txtEmail.Text) ||

                    string.IsNullOrWhiteSpace(
                        txtUsername.Text) )
                {
                    MessageBox.Show(
                        "Fill all fields");

                    return;
                }
                if (selectedUserId == 0 &&
                 string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password is required");
                    return;
                }

                var form =
                    new MultipartFormDataContent
                    {
                        {
                            new StringContent(
                                txtFullName.Text.Trim()),
                            "FullName"
                        },

                        {
                            new StringContent(
                                txtEmail.Text.Trim()),
                            "Email"
                        },

                        {
                            new StringContent(
                                txtUsername.Text.Trim()),
                            "Username"
                        },

                        { new StringContent("customer"), "Role" }
                    };

                using var client = new HttpClient();

                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    form.Add(new StringContent(txtPassword.Text), "Password");
                }

                HttpResponseMessage response;

                if (selectedUserId == 0)
                {
                    response =
                        await client.PostAsync(
                            "http://localhost:5294/api/user",
                            form);

                    Log.Information(
                        "User added: {Username}",
                        txtUsername.Text);
                }
                else
                {
                    response =
                        await client.PutAsync(
                            $"http://localhost:5294/api/user/{selectedUserId}",
                            form);

                    Log.Information(
                        "User updated: {Id}",
                        selectedUserId);
                }

                if (!response.IsSuccessStatusCode)
                {
                    var error =
                        await response.Content
                        .ReadAsStringAsync();

                    MessageBox.Show(
                        $"Failed: {error}");

                    return;
                }

                MessageBox.Show(
                    "Operation successful");

                ClearForm();

                await LoadUsers();
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Failed to save user");

                MessageBox.Show(
                    "Operation failed");
            }
        }

        private async void dataGridViewUser_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int id =
                Convert.ToInt32(
                    dataGridViewUser
                    .Rows[e.RowIndex]
                    .Cells["Id"].Value);

            string col =
                dataGridViewUser
                .Columns[e.ColumnIndex]
                .Name;

            if (col == "Edit")
            {
                LoadUserToForm(e.RowIndex);
            }
            else if (col == "Delete")
            {
                await DeleteUser(id);
            }
        }

        void LoadUserToForm(int rowIndex)
        {
            var row =
                dataGridViewUser.Rows[rowIndex];

            selectedUserId =
                Convert.ToInt32(
                    row.Cells["Id"].Value);

            txtFullName.Text =
                row.Cells["FullName"]
                .Value.ToString();

            txtEmail.Text =
                row.Cells["Email"]
                .Value.ToString();

            txtUsername.Text =
                row.Cells["Username"]
                .Value.ToString();

            btnAdd.Text = "Update";
        }

        async Task DeleteUser(int id)
        {
            try
            {
                var result =
                    MessageBox.Show(
                        "Delete this user?",
                        "Confirm",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;

                await api.DeleteAsync(
                    $"User/{id}");

                Log.Warning(
                    "User deleted: {Id}",
                    id);

                MessageBox.Show(
                    "User deleted");

                await LoadUsers();
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Delete failed");

                MessageBox.Show(
                    "Delete failed");
            }
        }

        void ClearForm()
        {
            selectedUserId = 0;

            btnAdd.Text = "Add";

            txtFullName.Clear();

            txtEmail.Clear();

            txtUsername.Clear();

            txtPassword.Clear();
        }
    }
}
    

