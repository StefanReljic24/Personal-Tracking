﻿using DAL;
using DAL.DAO;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SalaryBLL
    {
        public static SalaryDTO GetAll()
        {
            SalaryDTO dto = new SalaryDTO();
            dto.Employees = EmployeeDAO.GetEmployees();
            dto.Months = SalaryDAO.GetMonths();
            dto.Departments = DepartmentDAO.GetDepartments();
            dto.Positions = PositionDAO.GetPositions();
            dto.Salaries = SalaryDAO.GetSalaries();
            return dto;
        }

        public static void AddSalary(Salary salary,bool control)
        {
            SalaryDAO.Add(salary);
            if (control)
            {
                EmployeeDAO.UpdateEmployee(salary.EmployeeID, salary.Amount);
            }
        }

        public static void UpdateSalary(Salary salary,bool control)
        {
            SalaryDAO.UpdateSalary(salary);
            if (control)
            {
                EmployeeDAO.UpdateEmployee(salary.EmployeeID, salary.Amount);
            }
        }

        public static void DeleteSalary(int salaryID)
        {
            SalaryDAO.DeleteSalary(salaryID);
        }
    }
}
