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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
           
                FrmEmployeeList frm = new FrmEmployeeList();
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
            
           
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            FrmTaskList frmTaskList = new FrmTaskList();
            this.Hide();
            frmTaskList.ShowDialog();
            this.Visible = true;
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            FrmSalaryList frmSalaryList = new FrmSalaryList();
            this.Hide();
            frmSalaryList.ShowDialog();
            this.Visible = true;
        }

        private void btnPermission_Click(object sender, EventArgs e)
        {
            FrmPermissionList frmPermissionList = new FrmPermissionList();
            this.Hide();
            frmPermissionList.ShowDialog();
            this.Visible = true;
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            FrmDepartmentList frmDepartmentList = new FrmDepartmentList();
            this.Hide();
            frmDepartmentList.ShowDialog();
            this.Visible = true;
        }

        private void btnPosition_Click(object sender, EventArgs e)
        {
            FrmPositionList frmPositionList = new FrmPositionList();
            this.Hide();
            frmPositionList.ShowDialog();
            this.Visible = true;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            this.Hide();
            frmLogin.ShowDialog();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (!UserStatic.isAdmin)
            {
                btnDepartment.Visible = false;
                btnPosition.Visible = false;
                btnLogOut.Location = new Point(12, 177);
                btnExit.Location = new Point(200, 177);

            }
        }
    }
}
