using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class TaskDAO : EmployeeContext
    {
        public static List<TaskState> GettaskStates()
        {
            return db.TaskStates.ToList();
        }

        public static List<TaskDetailDTO> GetTasks()
        {
            List<TaskDetailDTO> taskList = new List<TaskDetailDTO>();
            var list = (from t in db.Tasks
                        join ts in db.TaskStates on t.TaskState equals ts.ID
                        join e in db.Employees on t.EmployeeID equals e.ID
                        join d in db.Departments on e.DepartmentID equals d.ID
                        join p in db.Positions on e.PositionID equals p.ID
                        select new
                        {
                            EmployeeID = t.EmployeeID,
                            UserNo = e.UserNo,
                            Name = e.Name,
                            Surname = e.Surname,
                            DepatmentName = d.DepartmentName,
                            DepartmentID = e.DepartmentID,
                            PositionName = p.PositionName,
                            PositionID = e.PositionID,
                            TaskID = t.ID,
                            Title = t.TaskTitle,
                            Content = t.TaskContent,
                            TaskStateID = t.TaskState,
                            TaskStateName = ts.StateName,
                            TaskStartDate = t.TaskStartDate,
                            TaskDeliveryDate = t.TaskDeliveryDate,

                        }).OrderBy(p => p.TaskStartDate).ToList();
            foreach (var item in list)
            {
                TaskDetailDTO dto = new TaskDetailDTO();
                dto.EmployeeID = item.EmployeeID;
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.DepartmentName = item.DepatmentName;
                dto.DepartmentID = item.DepartmentID;
                dto.PositionName = item.PositionName;
                dto.PositionID = item.PositionID;
                dto.TaskID = item.TaskID;
                dto.Title = item.Title;
                dto.Content = item.Content;
                dto.TaskStateID = item.TaskStateID;
                dto.TaskStateName = item.TaskStateName;
                dto.TaskStartDate = item.TaskStartDate;
                dto.TaskDeliveryDate = item.TaskDeliveryDate;
                taskList.Add(dto);
            }
            return taskList;
        }

        public static void ApproveTask(int taskID, bool isAdmin)
        {
            try
            {
                Task tsk = db.Tasks.First(p => p.ID == taskID);

                if (isAdmin)
                {
                    tsk.TaskState = TaskStates.Approved;
                }
                else
                {
                    tsk.TaskState = TaskStates.Delivered;
                }
                tsk.TaskDeliveryDate = DateTime.Today;
                db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void DeleteTask(int taskID)
        {
            try
            {
                Task ts = db.Tasks.First(p => p.ID == taskID);
                db.Tasks.DeleteOnSubmit(ts);
                db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void UpdateTask(Task task)
        {
            try
            {
                Task ts = db.Tasks.First(p => p.ID == task.ID);
                ts.TaskTitle = task.TaskTitle;
                ts.TaskContent = task.TaskContent;
                ts.TaskState = task.TaskState;
                ts.EmployeeID = task.EmployeeID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void AddTask(Task task)
        {
            try
            {
                db.Tasks.InsertOnSubmit(task);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
