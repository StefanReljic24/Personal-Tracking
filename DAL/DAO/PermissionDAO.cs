using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PermissionDAO : EmployeeContext
    {
       

        public static void AddPermission(Permission permission)
        {
            try
            {
                db.Permissions.InsertOnSubmit(permission);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<PermissionState> GetStates()
        {
            return db.PermissionStates.ToList();
        }

        public static List<PermissionDetailDTO> GetPermissions()
        {
            List<PermissionDetailDTO>permissions = new List<PermissionDetailDTO>();
            var list = (from p in db.Permissions
                        join e in db.Employees on p.EmployeeID equals e.ID
                        join s in db.PermissionStates on p.PermissionState equals s.ID
                        select new
                        {
                            UserNo = e.UserNo,
                            Name = e.Name,
                            Surname = e.Surname,
                            StateName = s.StateName,
                            StateID = p.PermissionState,
                            StartDate = p.PermissionStartDate,
                            EndDate = p.PermissionEndDate,
                            EmployeeID = p.EmployeeID,
                            PermissionID = p.ID,
                            Explanation = p.PermissionExplanation,
                            Dayamount = p.PermissionDay,
                            DepartmentID = e.DepartmentID,
                            PositionID = e.PositionID

                        }).OrderBy(p => p.StartDate).ToList();
            foreach (var item in list)
            {
                PermissionDetailDTO dto = new  PermissionDetailDTO();
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.StartDate = item.StartDate;
                dto.EndDate = item.EndDate;
                dto.EmployeeID = item.EmployeeID;
                dto.PermissionDayAmount = item.Dayamount;
                dto.DepartmentID = item.DepartmentID;
                dto.PositionID = item.PositionID;
                dto.State = item.StateID;
                dto.StateName = item.StateName;
                dto.Explanation = item.Explanation;
                dto.PermissionID = item.PermissionID;
                permissions.Add(dto);




            }
            return permissions;
        }

        public static void Deletepermission(int permissionID)
        {
            try
            {
                Permission pr = db.Permissions.First(p => p.ID == permissionID);
                db.Permissions.DeleteOnSubmit(pr);
                db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void UpdatePermission(int permissionID, int approved)
        {
            try
            {
                Permission pr = db.Permissions.First(x => x.ID == permissionID);
                pr.PermissionState = approved;
                db.SubmitChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdatePermission(Permission permission)
        {
            try
            {
                Permission pr = db.Permissions.First(x => x.ID == permission.ID);
                pr.PermissionStartDate = permission.PermissionStartDate;
                pr.PermissionEndDate = permission.PermissionEndDate;
                pr.PermissionExplanation = permission.PermissionExplanation;
                pr.PermissionDay = permission.PermissionDay;
                db.SubmitChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
