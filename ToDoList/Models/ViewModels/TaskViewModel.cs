using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models.ViewModels
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}