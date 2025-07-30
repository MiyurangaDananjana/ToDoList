using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ToDoList.Models;
using ToDoList.Models.ViewModels;
using ToDoList.Repository;

namespace ToDoList.Controllers
{
    public class TaskController : AuthController
    {
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCategories()
        {
            var repo = new TaskRepository();
            var categories = repo.GetCategories();

            // Map domain model to ViewModel
            var viewModelList = categories.Select(category => new CategoriesViewModel
            {
                Id = category.CategoryId,
                Name = category.Name
            }).ToList();

            return Json(viewModelList, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveTask(TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new TaskRepository();

                var user = new AccountRepository();

                int userId = user.GetUserIdByUsername(Session["Username"].ToString());

                TaskModel task = new TaskModel
                {
                    UserId = userId,
                    Title = model.Title,
                    Description = null,
                    DueDate = DateTime.UtcNow,
                    Priority = 5,
                    Status = "Pending",
                    CategoryId = model.CategoryId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                };

                repo.SaveTask(task);

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Invalid data" });
        }

        [HttpPost]
        public JsonResult DeleteTask(int taskId)
        {
            try
            {
                var repo = new TaskRepository();
                var task = repo.GetTaskById(taskId);
                if (task == null)
                {
                    return Json(new { success = false, message = "Task not found" });
                }

                task.IsDeleted = true;
                repo.UpdateTask(task);

                return Json(new { success = true, taskId = task.TaskId }); // use 'task' instead of 'savedTask'
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetTasks()
        {
            var repo = new TaskRepository();

            int userId = new AccountRepository().GetUserIdByUsername(Session["Username"].ToString());

            List<TaskViewModel> tasks = repo.GetTasks(userId);

            return Json(tasks, JsonRequestBehavior.AllowGet);
        }


    }
}