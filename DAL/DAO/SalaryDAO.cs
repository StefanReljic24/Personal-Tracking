using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class SalaryDAO : EmployeeContext
    {

        public static List<Month> GetMonths()
        {
            return db.Months.ToList();
        }

        public static void Add(Salary salary)
        {
            try
            {
                db.Salaries.InsertOnSubmit(salary);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<SalaryDetailDTO> GetSalaries()
        {
            List<SalaryDetailDTO> listSalary = new List<SalaryDetailDTO>();
            var list = (from s in db.Salaries
                        join e in db.Employees on s.EmployeeID equals e.ID
                        join m in db.Months on s.MonthID equals m.ID
                        select new
                        {
                            UserNo = e.UserNo,
                            Name = e.Name,
                            Surname = e.Surname,
                            EmployeeID = s.EmployeeID,
                            Amount = s.Amount,
                            Year = s.Year,
                            MonthName = m.MonthName,
                            MonthID = s.MonthID,
                            SalaryID = s.ID,
                            DepartmentID = e.DepartmentID,
                            PositionID = e.PositionID
                        }).OrderBy(x => x.Year).ToList();
            foreach (var item in list)
            {
                SalaryDetailDTO dto = new SalaryDetailDTO();
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.EmployeeID = item.EmployeeID;
                dto.SalaryAmount = item.Amount;
                dto.SalaryYear = item.Year;
                dto.MonthName = item.MonthName;
                dto.MonthID = item.MonthID;
                dto.SalaryID = item.SalaryID;
                dto.DepartmentID = item.DepartmentID;
                dto.PositionID = item.PositionID;
                dto.OldSalary = item.Amount;
                listSalary.Add(dto);
            }
            return listSalary;
        }

        public static void DeleteSalary(int salaryID)
        {
            try
            {
                Salary sl = db.Salaries.First(p => p.ID == salaryID);
                db.Salaries.DeleteOnSubmit(sl);
                db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void UpdateSalary(Salary salary)
        {
            try
            {
                Salary sl = db.Salaries.First(p => p.ID == salary.ID);
                sl.Amount = salary.Amount;
                sl.Year = salary.Year;
                sl.MonthID = salary.MonthID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
