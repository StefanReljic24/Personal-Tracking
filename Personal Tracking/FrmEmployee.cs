using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using DAL.DAO;
using DAL.DTO;
using System.IO;


namespace Personal_Tracking
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        EmployeeDTO dto = new EmployeeDTO();
        public EmployeeDetailDTO detail = new EmployeeDetailDTO();
        public bool isUpdate = false;
        string imagepath = "";

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.GetAll();
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbPosition.SelectedIndex = -1;
            cmbDepartment.SelectedIndex = -1;
            comboFull = true;
            if (isUpdate)
            {
                txtUserNo.Text = detail.UserNo.ToString();
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtSalary.Text = detail.Salary.ToString();
                txtPassword.Text = detail.Password;
                chAdmin.Checked = Convert.ToBoolean(detail.IsAdmin);
                txtAddress.Text = detail.Address;
                dateTimePicker1.Value = Convert.ToDateTime(detail.BhirtDay);
                cmbDepartment.SelectedValue = detail.DepartmentID;
                cmbPosition.SelectedValue = detail.PositionID;
                imagepath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                txtImagePath.Text = imagepath;
                pictureBox1.ImageLocation = imagepath;
                if (!UserStatic.isAdmin)
                {
                    chAdmin.Enabled = false;
                    txtUserNo.Enabled = false;
                    txtSalary.Enabled = false;
                    cmbDepartment.Enabled = false;
                    cmbPosition.Enabled = false;
                }
            }

        }
        bool comboFull = false;
        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepartmentID == departmentID).ToList();
            }
           
        }
        string fileName = "";
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                txtImagePath.Text = openFileDialog1.FileName;
                string Unqiue = Guid.NewGuid().ToString();
                fileName += Unqiue + openFileDialog1.SafeFileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
            {
                MessageBox.Show("User no is Empity");
            }
            else if (!EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
            {
                MessageBox.Show("Exist user no is used by another employee please change");
            }
            else if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("User no is Password");
            }
            else if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Name no is Empity");
            }
            else if (txtSurname.Text.Trim() == "")
            {
                MessageBox.Show("Surname is Empity");
            }
            else if (txtSalary.Text.Trim() == "")
            {
                MessageBox.Show("Salary no is Empity");
            }
            else if (cmbDepartment.SelectedIndex == -1)
            {
                MessageBox.Show("Department no is Empity");
            }
            else if (cmbPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Position no is Empity");
            }
            else
            {
                if (!isUpdate)
                {
                    if (!EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
                    {
                        MessageBox.Show("Exist user no is used by another employee please change");
                    }
                    else
                    {
                        Employee employee = new Employee();
                        employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                        employee.Password = txtPassword.Text;
                        employee.IsAdmin = chAdmin.Checked;
                        employee.Name = txtName.Text;
                        employee.Surname = txtSurname.Text;
                        employee.Salary = Convert.ToInt32(txtSalary.Text);
                        employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                        employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                        employee.BrithDay = dateTimePicker1.Value;
                        employee.Adress = txtAddress.Text;
                        employee.ImagePath = fileName;
                        EmployeeBLL.AddEmployee(employee);
                        File.Copy(txtImagePath.Text, @"images\\" + fileName);
                        MessageBox.Show("Employee was added");
                        txtUserNo.Clear();
                        txtPassword.Clear();
                        chAdmin.Checked = false;
                        txtName.Clear();
                        txtSurname.Clear();
                        txtSalary.Clear();
                        txtImagePath.Clear();
                        txtAddress.Clear();
                        comboFull = false;
                        pictureBox1.Image = null;
                        cmbDepartment.SelectedIndex = -1;
                        cmbPosition.DataSource = dto.Positions;
                        cmbPosition.SelectedIndex = -1;
                        comboFull = true;
                        dateTimePicker1.Value = DateTime.Today;
                    }
                  
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "Warrning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Employee employee = new Employee();
                        if (txtImagePath.Text != imagepath)
                        {
                            if (File.Exists(@"images\\" + detail.ImagePath))
                            {
                                File.Delete(@"images\\" + detail.ImagePath);
                            }
                            File.Copy(txtImagePath.Text, @"images\\" + fileName);
                            employee.ImagePath = fileName;
                        }
                        else
                        {
                            employee.ImagePath = detail.ImagePath;
                            employee.ID = detail.EmployeeID;
                            employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                            employee.Name = txtName.Text;
                            employee.Surname = txtSurname.Text;
                            employee.IsAdmin = chAdmin.Checked;
                            employee.Password = txtPassword.Text;
                            employee.Adress = txtAddress.Text;
                            employee.BrithDay = dateTimePicker1.Value;
                            employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                            employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                            employee.Salary = Convert.ToInt32(txtSalary.Text);
                            EmployeeBLL.UpdateEmployee(employee);
                            MessageBox.Show("Employee was updated");
                            this.Close();
                        }
                    }
                }


            }
        }
        bool isUnique = false;
        
        private void txtCheck_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
            {
                MessageBox.Show("User no is Empity");
            }
            else
            {
                isUnique = EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text));
                if (!isUnique)
                {
                    MessageBox.Show("This user no is used by another please change ");

                }
                else
                {
                    MessageBox.Show("This user no is usable ");
                }
            }
        }
    }
}
