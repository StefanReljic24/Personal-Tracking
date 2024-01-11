using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class DepartmentDAO : EmployeeContext
    {
        public static void AddDepartment(Department department)
        {
            try
            {
                db.Departments.InsertOnSubmit(department);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public static List<Department> GetDepartments()
        {
           return db.Departments.ToList();
        }

        public static void UpdateDepartment(Department department)
        {
            try
            {
                Department dp = db.Departments.First(p => p.ID == department.ID);
                dp.DepartmentName = department.DepartmentName;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void DeleteDepartment(int iD)
        {
            try
            {
                Department department = db.Departments.First(p => p.ID == iD);
                db.Departments.DeleteOnSubmit(department);
                db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
