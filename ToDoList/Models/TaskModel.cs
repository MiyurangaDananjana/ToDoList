using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class TaskModel
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime? DueDate { get; set; }
        public int? Priority { get; set; }

        public string Status { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}