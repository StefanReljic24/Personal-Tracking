using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL;

namespace Personal_Tracking
{
    public partial class FrmDepartment : Form
    {
        public FrmDepartment()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

         public Department detail = new Department();
         public bool isUpdate = false;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDepartment.Text.Trim()=="")
            {
                MessageBox.Show("Please fill the name field");
            }
            else
            {
                Department department = new Department();
                if (!isUpdate)
                {
                    department.DepartmentName = txtDepartment.Text;
                    BLL.DepartmentBLL.AddDepartment(department);

                    MessageBox.Show("Department was Added");
                    txtDepartment.Clear();
                }
                else 
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "Waring!!", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        department.ID = detail.ID;
                        department.DepartmentName = txtDepartment.Text;
                        DepartmentBLL.UpdateDepartment(department);
                    }
                    
                }
            }
           
        }

        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                txtDepartment.Text = detail.DepartmentName;
            }
        }
    }
}
