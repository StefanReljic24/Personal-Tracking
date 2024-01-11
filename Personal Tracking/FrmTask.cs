
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
using DAL.DTO;
using DAL.DAO;
using DAL;
using Task = DAL.Task;

namespace Personal_Tracking
{
    public partial class FrmTask : Form
    {
        public FrmTask()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool comboFull = false;
        TaskDTO dto = new TaskDTO(); 
        private void FrmTask_Load(object sender, EventArgs e)
        {
           
            dto = TaskBLL.GetAll();
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
            comboFull = true;
            cmbTaskState.DataSource = dto.TaskStates;
            cmbTaskState.DisplayMember = "TaskName";
            cmbTaskState.ValueMember = "ID";
            cmbTaskState.SelectedIndex = -1;
            if (isUpdate)
            {
                label10.Visible = true;
                cmbTaskState.Visible = true;
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtTitle.Text = detail.Title;
                txtUserNo.Text = detail.UserNo.ToString();
                txtContent.Text = detail.Content;
                cmbTaskState.DataSource = dto.TaskStates;
                cmbTaskState.DisplayMember = "StateName";
                cmbTaskState.ValueMember = "ID";
                cmbTaskState.SelectedValue = detail.TaskStateID;

            }
        }

        internal TaskDetailDTO detail;

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepartmentID ==
                Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
                List<EmployeeDetailDTO> list = dto.Employees;
                dataGridView1.DataSource = list.Where(x => x.DepartmentID ==
                Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
          txtUserNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
          txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
          txtSurname.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
          task.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

          txtTitle.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
          txtContent.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<EmployeeDetailDTO> list = dto.Employees;
                dataGridView1.DataSource = list.Where(x => x.PositionID ==
                Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            }
           
        }

        Task task = new Task();
        private bool isUpdate;

        private async void btnSave_Click(object sender, EventArgs e)
        {
            
            if (task.EmployeeID == 0)
            {
                MessageBox.Show("Please select an employee on table");
            }
            else if (txtTitle.Text.Trim()=="")
            {
                MessageBox.Show("Task title is empty");
            }
            else if (txtContent.Text.Trim()=="")
            {
                MessageBox.Show("Content title is empty");
            }
            else
            {
                if (!isUpdate)
                {
                    task.TaskTitle = txtTitle.Text;
                    task.TaskContent = txtContent.Text;
                    task.TaskStartDate = DateTime.Today;
                    task.TaskState = 1;
                    TaskBLL.AddTask(task);
                    MessageBox.Show("Task was added");
                    txtTitle.Clear();
                    txtContent.Clear();
                    task = new Task();
                }
                else if (!isUpdate)
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "Warring", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Task update = new Task();
                        update.ID = detail.TaskID;
                        if (Convert.ToInt32(txtUserNo.Text) != detail.UserNo)
                        {
                            update.EmployeeID = task.EmployeeID;
                        }
                        else
                        {
                            update.EmployeeID = detail.EmployeeID;
                            update.TaskTitle = txtTitle.Text;
                            update.TaskContent = txtContent.Text;
                            update.TaskState = Convert.ToInt32(cmbTaskState.SelectedValue);
                            TaskBLL.UpdateTask(update);
                            MessageBox.Show("Task was updated");
                            this.Close();
                        }
                    }
                }
            }
        }
    }
}
