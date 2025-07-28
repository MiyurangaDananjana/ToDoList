using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class PersonController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPeople()
        {
            var people = new List<Person>
        {
            new Person { Id = 1, FirstName = "Dananjana", LastName = "Miyuranga" },
            new Person { Id = 2, FirstName = "Maduka", LastName = "Sanjeewa" }
        };
            return Json(people, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SavePerson(Person person)
        {
            // Save logic here (e.g., to database)
            return Json(new { success = true, message = "Saved!" });
        }
    }
}