using System;
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
    }
}