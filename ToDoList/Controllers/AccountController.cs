using System.Web.Mvc;
using ToDoList.DataContext;
using ToDoList.Models.ViewModels;
using ToDoList.Repository;

namespace ToDoList.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public JsonResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new AccountRepository();
                bool success = repo.Login(model.UserName, model.Password);

                if (success)
                {
                    Session["UserName"] = model.UserName;

                    return Json(new { success = true, message = "Login successful" });
                }
                else
                {
                    return Json(new { success = false, message = "Invalid email or password" });
                }
            }

            return Json(new { success = false, message = "Please fill in all required fields." });
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public JsonResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new AccountRepository();
                var user = new User
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PasswordHash = model.Password
                };

                bool success = repo.Register(user);

                return Json(new { success = success, message = success ? "Registered!" : "Email already used." });
            }

            return Json(new { success = false, message = "Invalid data" });
        }

        [HttpGet]
        public JsonResult IsUserNameRegistered(string userName)
        {
            var repo = new AccountRepository();
            bool isRegistered = repo.IsUserNameRegistered(userName);
            return Json(new { isRegistered }, JsonRequestBehavior.AllowGet);
        }
    }
}