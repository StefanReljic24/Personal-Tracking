using BLL;
using DAL;
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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim()=="" || txtPassword.Text.Trim()=="")
            {
                MessageBox.Show("Please fill the userno and password");
            }
            else
            {
                List<Employee> employees = EmployeeBLL.GetEmployees(Convert.ToInt32(txtUserNo.Text),txtPassword.Text);
                if (employees.Count == 0)
                {
                    MessageBox.Show("Please control your information");
                }
                else
                {
                    Employee employee = new Employee();
                    employee = employees.First();
                    UserStatic.EmployeeID = employee.ID;
                    UserStatic.UserNo = employee.UserNo;
                    UserStatic.isAdmin = Convert.ToBoolean(employee.IsAdmin);
                    FrmMain frmMain = new FrmMain();
                    this.Hide();
                    frmMain.ShowDialog();
                }
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
