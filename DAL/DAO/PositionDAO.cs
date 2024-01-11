using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PositionDAO : EmployeeContext
    {
        public static void AddPosition(Position position)
        {
            try
            {
                db.Positions.InsertOnSubmit(position);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<PositionDTO>  GetPositions()
        {
            try
            {
                var list = (from p in db.Positions
                            join d in db.Departments on
                            p.DepartmentID equals d.ID
                            select new
                            {
                                positionID = p.ID,
                                positionName = p.PositionName,
                                departmentID = d.ID,
                                departmentName = d.DepartmentName

                            }).OrderBy(x => x.positionID).ToList();
                List<PositionDTO> positionList = new List<PositionDTO>();
                foreach (var item in list)
                {
                    PositionDTO dto = new PositionDTO();
                    dto.ID = item.positionID;
                    dto.PositionName = item.positionName;
                    dto.DepartmentID = item.departmentID;
                    dto.DepartmentName = item.departmentName;
                    positionList.Add(dto);

                }
                return positionList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void DeletePosition(int iD)
        {
            try
            {
                Position position = db.Positions.First(p => p.ID == iD);
                db.Positions.DeleteOnSubmit(position);
                db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void UpdatePosition(Position position)
        {
            try
            {
                Position ps = db.Positions.First(p => p.ID == position.ID);
                ps.PositionName = position.PositionName;
                ps.DepartmentID = position.DepartmentID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
