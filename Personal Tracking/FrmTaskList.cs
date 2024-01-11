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
    public partial class FrmTaskList : Form
    {
        public FrmTaskList()
        {
            InitializeComponent();
        }

        private void txtUserNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        TaskDTO dto = new TaskDTO();
        private bool comboFull;
        TaskDetailDTO detail = new TaskDetailDTO();
        public bool isUpdate = false;

        void FillAllDate()
        {
            dto = TaskBLL.GetAll();
            if (!UserStatic.isAdmin)
            {
                dto.Tasks = dto.Tasks.Where(p => p.EmployeeID == UserStatic.EmployeeID).ToList();

            }
            dataGridView1.DataSource = dto.Tasks;
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
            cmbStateTask.DataSource = dto.TaskStates;
            cmbStateTask.DisplayMember = "TaskName";
            cmbStateTask.ValueMember = "ID";
            cmbStateTask.SelectedIndex = -1;
        }
        private void FrmTaskList_Load(object sender, EventArgs e)
        {
            FillAllDate();
            dataGridView1.Columns[0].HeaderText = "Task Title";
            dataGridView1.Columns[1].HeaderText = "UserNo";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].HeaderText = "Start Date";
            dataGridView1.Columns[5].HeaderText = "Delivery Date";
            dataGridView1.Columns[6].HeaderText = "Task State";
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            if (!UserStatic.isAdmin)
            {
                btnNew.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                btnClose.Location = new Point(161, 22);
                pnlForAdmin.Hide();
                btnApprove.Text = "Delivery";

            }
          
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmTask frmTask = new FrmTask();
            this.Hide();
            frmTask.ShowDialog();
            this.Visible = true;
            FillAllDate();
            CleanFilters();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if (detail.TaskID == 0)
            {
                MessageBox.Show("Please select a task on table");
            }
            else
            {
                FrmTask frm = new FrmTask();
                isUpdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillAllDate();
                CleanFilters();
            }
            FrmTask frmTask = new FrmTask();
            this.Hide();
            frmTask.ShowDialog();
            this.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                cmbPosition.DataSource = dto.Positions.Where(p => p.DepartmentID ==
                Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<TaskDetailDTO> list = dto.Tasks;
            if (txtUserNo.Text.Trim() != "")
            {
                list = list.Where(x => x.UserNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            }
            if (txtName.Text.Trim() != "")
            {
                list = list.Where(x => x.Name.Contains(txtName.Text)).ToList();
            }
            if (txtSurname.Text.Trim() != "")
            {
                list = list.Where(x => x.Surname.Contains(txtSurname.Text)).ToList();
            }
            if (cmbDepartment.SelectedIndex != -1)
            {
                list = list.Where(p => p.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
                    
            }
            if (cmbPosition.SelectedIndex != -1)
            {
                list = list.Where(p => p.PositionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            }
            if (rbStartDate.Checked)
            {
                list = list.Where(p => p.TaskStartDate > Convert.ToDateTime(dpStart.Value) &&
                p.TaskStartDate < Convert.ToDateTime(dpFinish.Value)).ToList(); 
                
            }
            if (rbDeliveryDate.Checked)
            {
                list = list.Where(p => p.TaskDeliveryDate > Convert.ToDateTime(dpStart.Value) &&
                p.TaskDeliveryDate < Convert.ToDateTime(dpFinish.Value)).ToList();
            }
            if (cmbStateTask.SelectedIndex != -1)
            {
                list = list.Where(p => p.TaskStateID == Convert.ToInt32(cmbStateTask.SelectedValue)).ToList();
            }
            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanFilters();
        }
        
        private void CleanFilters()
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            comboFull = false;
            cmbPosition.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Positions;
            cmbDepartment.SelectedIndex = -1;
            cmbDepartment.DataSource = dto.Departments;
            comboFull = true;
            rbStartDate.Checked = false;
            rbDeliveryDate.Checked = false;
            cmbStateTask.SelectedIndex = -1;
            dataGridView1.DataSource = dto.Tasks;
        }
        
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.Name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            detail.Surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.Title = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            detail.Content = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            detail.UserNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.TaskStateID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[14].Value);
            detail.TaskID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
            detail.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
            detail.TaskStartDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            detail.TaskDeliveryDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure delete task", "Waring", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                TaskBLL.DeleteTask(detail.TaskID);
                MessageBox.Show("Task was deleted");
                FillAllDate();
                CleanFilters();

            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (UserStatic.isAdmin && detail.TaskStateID == TaskStates.OnEmployee && detail.EmployeeID != UserStatic.EmployeeID)
            {
                MessageBox.Show("Before approve a task employee have to delivery task");
            }
            else if (UserStatic.isAdmin && detail.TaskStateID == TaskStates.Approved)
            {
                MessageBox.Show("This task is already approved");
            }
            else if (UserStatic.isAdmin && detail.TaskStateID == TaskStates.Delivered)
            {
                MessageBox.Show("This task is already delivered");
            }
            else 
            {
                TaskBLL.ApproveTask(detail.TaskID, UserStatic.isAdmin);
                MessageBox.Show("That was updated");
                FillAllDate();
                CleanFilters();
            }
        }

        private void txtExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExcelExport(dataGridView1);
        }
    }
}
