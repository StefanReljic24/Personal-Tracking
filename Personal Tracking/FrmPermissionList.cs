using BLL;
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
    public partial class FrmPermissionList : Form
    {
        public FrmPermissionList()
        {
            InitializeComponent();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void cmbDayAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDayAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmPermission frmPermission = new FrmPermission();
            this.Hide();
            frmPermission.ShowDialog();
            this.Visible = true;
            FillAllDate();
            CleanFilter();

        }
        void FillAllDate()
        {
            dto = PermissionBLL.GetAll();
            dataGridView1.DataSource = dto.Permissions;
            comboFull = false;
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbPosition.SelectedIndex = -1;
            cmbDepartment.SelectedIndex = -1;
            comboFull = true;
            cmbState.DataSource = dto.States;
            cmbState.DisplayMember = "StateName";
            cmbState.ValueMember = "ID";
            cmbState.SelectedIndex = -1;

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.PermissionID == 0)
            {
                MessageBox.Show("please select permission from table");
            }
            else if (detail.State == PermissionStates.Approved || detail.State == PermissionStates.Disapproved)
            {
                MessageBox.Show("You can not update any approved or disapproved permission");
            }
            else
            {
                FrmPermission frm = new FrmPermission();
                frm.isUpdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillAllDate();
                CleanFilter();
            }
            
        }
        PermissionDTO dto = new PermissionDTO();
        private bool comboFull;

        private void FrmPermissionList_Load(object sender, EventArgs e)
        {
            FillAllDate();
            if (!UserStatic.isAdmin)
            {
                dto.Permissions = dto.Permissions.Where(p => p.EmployeeID == UserStatic.EmployeeID).ToList();
            }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "UserNo";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].HeaderText = "Start Date";
            dataGridView1.Columns[9].HeaderText = "End Date";
            dataGridView1.Columns[10].HeaderText = "Day Amount";
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[11].HeaderText = "State";
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            if (!UserStatic.isAdmin)
            {
                pnlForAdmin.Visible = false;
                btnApprove.Hide();
                btnDelete.Hide();
                btnDisapprove.Hide();
                btnClose.Location = new Point(462, 37);
            }

        }
      
        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<PermissionDetailDTO> list = dto.Permissions;
            if (txtUserNo.Text.Trim() != "")
            {
                list = list.Where(p => p.UserNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            }
            if (txtName.Text.Trim() != "")
            {
                list = list.Where(p => p.Name.Contains(txtName.Text)).ToList();
            }
            if (txtSurname.Text.Trim() != "")
            {
                list = list.Where(p => p.Surname.Contains(txtSurname.Text)).ToList();
            }
            if (cmbDepartment.SelectedIndex != -1)
            {
                list = list.Where(p => p.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
            if (cmbPosition.SelectedIndex != -1)
            {
                list = list.Where(p => p.PositionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            }
            if (cmbState.SelectedIndex != -1)
            {
                list = list.Where(p => p.State == Convert.ToInt32(cmbState.SelectedValue)).ToList();
            }
            if (txtDayAmount.Text.Trim() != "")
            {
                list = list.Where(p => p.PermissionDayAmount == Convert.ToInt32(txtDayAmount.Text)).ToList();
            }
            if (rbStartDate.Checked)
            {
                list = list.Where(p => p.StartDate > Convert.ToDateTime(dpStart.Value)
                && p.EndDate < Convert.ToDateTime(dpFinish.Value)).ToList();
            }
            if (rbEndDate.Checked)
            {
                list = list.Where(p => p.EndDate > Convert.ToDateTime(dpStart.Value)
                && p.EndDate < Convert.ToDateTime(dpFinish.Value)).ToList();
            }
            dataGridView1.DataSource = list;
        }
        private void CleanFilter()
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            comboFull = false;
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
            rbStartDate.Checked = false;
            rbEndDate.Checked = false;
            cmbState.SelectedIndex = -1;
            txtDayAmount.Clear();
            dataGridView1.DataSource = dto.Permissions;

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanFilter();
        }
        PermissionDetailDTO detail = new PermissionDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.PermissionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[14].Value);
            detail.StartDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
            detail.EndDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.Explanation = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            detail.UserNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.State = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
            detail.PermissionDayAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermission(detail.PermissionID, PermissionStates.Approved);
            MessageBox.Show("Approved");
            FillAllDate();
            CleanFilter();
        }

        private void btnDisapprove_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermission(detail.PermissionID, PermissionStates.Disapproved);
            MessageBox.Show("Disapproved");
            FillAllDate();
            CleanFilter();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this permission?", "Waring!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (detail.State == PermissionStates.Approved || detail.State == PermissionStates.Disapproved)
                {
                    MessageBox.Show("You cannot delete approved or disapproved permission");
                }
                else
                {
                    PermissionBLL.Deletepermission(detail.PermissionID);
                    MessageBox.Show("PErmission was deleted");
                    FillAllDate();
                    CleanFilter();

                }
            }
        }

        private void txtExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExcelExport(dataGridView1);
        }
    }
}
