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
    public partial class FrmPosition : Form
    {
        public FrmPosition()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public PositionDTO detail = new PositionDTO();
        public bool isUpdate = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPosition.Text.Trim()=="")
            {
                MessageBox.Show("Please fill the feild");
            }
            else if(cmbDepartment.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a department");
            }
            else
            {
                if (!isUpdate)
                {
                    Position position = new Position();
                    position.PositionName = txtPosition.Text;
                    position.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                    BLL.PositionBLL.AddPosition(position);
                    MessageBox.Show("Position was added");
                    txtPosition.Clear();
                    cmbDepartment.SelectedIndex = -1;
                }
                else
                {
                   
                        Position position = new Position();
                        position.ID = detail.ID;
                        position.PositionName = txtPosition.Text;
                        position.DepartmentID= Convert.ToInt32(cmbDepartment.SelectedValue);
                        bool control = false;
                    if (Convert.ToInt32(cmbDepartment.SelectedIndex) != detail.OldDepartmentID)
                    {
                        control = true;
                    }
                    PositionBLL.UpdatePosition(position,control);
                    MessageBox.Show("Position was updated");
                    this.Close();

                }
               
            }
            
        }
        List<Department> departmentlist = new List<Department>();
        private void FrmPosition_Load(object sender, EventArgs e)
        {
           departmentlist = DepartmentBLL.GetDepartments();
           cmbDepartment.DataSource = departmentlist;
           cmbDepartment.DisplayMember = "DepartmentName";
           cmbDepartment.ValueMember = "ID";
           cmbDepartment.SelectedIndex = -1;
            if (isUpdate)
            {
                txtPosition.Text = detail.PositionName;
                cmbDepartment.SelectedValue = detail.DepartmentID;

            }
        }
    }
}
