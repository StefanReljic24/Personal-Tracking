using BLL;
using DAL;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Tracking
{
    public partial class FrmSalary : Form
    {
        public FrmSalary()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SalaryDTO dto = new SalaryDTO();
        private bool comboFull;
        public SalaryDetailDTO detail = new SalaryDetailDTO();
        public bool isUpdate = false;

        private void FrmSalary_Load(object sender, EventArgs e)
        {
            dto = SalaryBLL.GetAll();
            if (!isUpdate)
            {
                dataGridView1.DataSource = dto.Employees;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "UserNo";
                dataGridView1.Columns[2].HeaderText = "Name";
                dataGridView1.Columns[3].HeaderText = "Surname";
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].Visible = false;
                comboFull = false;
                cmbDepartment.DataSource = dto.Departments;
                cmbDepartment.DisplayMember = "DepartmentName";
                cmbDepartment.ValueMember = "ID";
                cmbPosition.DataSource = dto.Positions;
                cmbPosition.DisplayMember = "PositionName";
                cmbPosition.ValueMember = "ID";
                cmbPosition.SelectedIndex = -1;
                cmbDepartment.SelectedIndex = -1;
                if (dto.Departments.Count > 0)
                    comboFull = true;

            }
            cmbMonth.DataSource = dto.Months;
            cmbMonth.DisplayMember = "MonthName";
            cmbMonth.ValueMember = "ID";
            cmbMonth.SelectedIndex = -1;
            if(isUpdate)
            {
                panel1.Hide();
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtSalary.Text = detail.SalaryAmount.ToString();
                txtYear.Text = detail.SalaryYear.ToString();
                cmbMonth.SelectedValue = detail.MonthID;

            }
        }
        Salary salary = new Salary();
        public  int OldSalary = 0;
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtUserNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSurname.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtYear.Text = DateTime.Today.Year.ToString();
            txtSalary.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            salary.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            OldSalary = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtYear.Text.Trim()=="")
            {
                MessageBox.Show("Please fill the year");
            }
            if (cmbMonth.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a month");
            }
            if (txtSalary.Text.Trim()=="")
            {
                MessageBox.Show("Please select salary");
            }
            else
            {
                bool control = false;
                if (!isUpdate)
                {
                    if (salary.EmployeeID == 0)
                    {
                        MessageBox.Show("Please select an employee from table");
                    }
                    else
                    {
                        
                        salary.Year = Convert.ToInt32(txtYear.Text);
                        salary.MonthID = Convert.ToInt32(cmbMonth.SelectedValue);
                        salary.Amount = Convert.ToInt32(txtSalary.Text);
                        if (salary.Amount > OldSalary)
                        {
                            control = true;
                        }
                        SalaryBLL.AddSalary(salary,control);
                        MessageBox.Show("Salary was Added");
                        cmbMonth.SelectedIndex = -1;
                        salary = new Salary();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure", "title", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Salary salary = new Salary();
                        salary.ID = detail.SalaryID;
                        salary.EmployeeID = detail.EmployeeID;
                        salary.Year = Convert.ToInt32(txtYear.Text);
                        salary.MonthID = Convert.ToInt32(cmbMonth.SelectedValue);
                        salary.Amount = Convert.ToInt32(txtSalary.Text);
                        if (salary.Amount > detail.OldSalary)
                        {
                            control = true;
                        }
                        SalaryBLL.UpdateSalary(salary,control);

                    }
                }
              
             
                
            }
        }
    }
}
