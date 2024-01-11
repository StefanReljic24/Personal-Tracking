using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class EmployeeDAO : EmployeeContext
    {
        public static void AddEmployee(Employee employee)
        {
            try
            {
                db.Employees.InsertOnSubmit(employee);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        public static List<EmployeeDetailDTO> GetEmployees()
        {
            List<EmployeeDetailDTO> employeeDetails = new List<EmployeeDetailDTO>();
            var list = (from e in db.Employees
                        join d in db.Departments on e.DepartmentID equals d.ID
                        join p in db.Positions on e.PositionID equals p.ID
                        select new
                        {
                            UserNo = e.UserNo,
                            Password = e.Password,
                            IsAdmin = e.IsAdmin,
                            EmployeeID = e.ID,
                            Name = e.Name,
                            Surname = e.Surname,
                            Salary = e.Salary,
                            DepartmentID = e.DepartmentID,
                            DepartmentName = d.DepartmentName,
                            PositionID = e.PositionID,
                            PositionName = p.PositionName,
                            BhirtDay = e.BrithDay,
                            Address = e.Adress,
                            ImagePath = e.ImagePath,



                        }).OrderBy(p => p.UserNo).ToList();
            foreach (var item in list)
            {
                EmployeeDetailDTO dto = new EmployeeDetailDTO();
                dto.UserNo = item.UserNo;
                dto.Password = item.Password;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.Salary = item.Salary;
                dto.EmployeeID = item.EmployeeID;
                dto.IsAdmin = item.IsAdmin;
                dto.Address = item.Address;
                dto.BhirtDay = item.BhirtDay;
                dto.DepartmentID = item.DepartmentID;
                dto.DepartmentName = item.DepartmentName;
                dto.PositionID = item.PositionID;
                dto.PositionName = item.PositionName;
                dto.ImagePath = item.ImagePath;
                employeeDetails.Add(dto);

            }
            return employeeDetails;
        }

        public static void DeleteEmployee(int employeeID)
        {
            try
            {
                Employee emp = db.Employees.First(p => p.ID == employeeID);
                db.Employees.DeleteOnSubmit(emp);
                db.SubmitChanges();
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void UpdateEmployee(Position position)
        {
            List<Employee> list = db.Employees.Where(p => p.PositionID == position.ID).ToList();
            foreach (var item in list)
            {
                item.DepartmentID = position.DepartmentID;
            }
            db.SubmitChanges();
        }

        public static void UpdateEmployee(Employee employee)
        {
            try
            {
                Employee emp = db.Employees.First(p => p.ID == employee.ID);
                emp.UserNo = employee.UserNo;
                emp.Name = employee.Name;
                emp.Surname = employee.Surname;
                emp.Adress = employee.Adress;
                emp.IsAdmin = employee.IsAdmin;
                emp.BrithDay = employee.BrithDay;
                emp.DepartmentID = employee.DepartmentID;
                emp.PositionID = employee.PositionID;
                emp.Salary = employee.Salary;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateEmployee(int employeeID, int amount)
        {
            try
            {
                Employee employee = db.Employees.First(p => p.ID == employeeID);
                employee.Salary = amount;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Employee> GetEmployees(int v, string text)
        {
            try
            {
                List<Employee> list = db.Employees.Where(x => x.UserNo ==
                v && x.Password == text).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Employee> GetUsers(int v)
        {
           return db.Employees.Where(x => x.UserNo == v).ToList();
        }
    }
}
