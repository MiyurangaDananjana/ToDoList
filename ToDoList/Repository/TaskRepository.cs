using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.DataContext;
using ToDoList.Models;

namespace ToDoList.Repository
{
    public class TaskRepository
    {
        private readonly TodoListAppEntities _context;
        public TaskRepository() 
        {
            _context = new TodoListAppEntities();
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.AsNoTracking().ToList();
        }

        public void SaveTask(TaskModel task)
        {
            try
            {
                Task taskDetails = new Task
                {
                    UserId = task.UserId,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    Priority = task.Priority,
                    Status = task.Status,
                    CategoryId = task.CategoryId,
                    CreatedAt = task.CreatedAt,
                    UpdatedAt = task.UpdatedAt,
                    IsDeleted = task.IsDeleted
                };

                _context.Tasks.Add(taskDetails);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving task", ex);
            }
        }
    }
}