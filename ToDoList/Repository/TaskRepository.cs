using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.DataContext;
using ToDoList.Models;
using ToDoList.Models.ViewModels;

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

        public Task GetTaskById(int taskId)
        {
            return _context.Tasks
                .AsNoTracking()
                .FirstOrDefault(t => t.TaskId == taskId);
        }

        public void UpdateTask(Task task)
        {
            try
            {
                var existingTask = _context.Tasks.FirstOrDefault(t => t.TaskId == task.TaskId);

                if (existingTask == null)
                {
                    throw new Exception("Task not found");
                }

                existingTask.UserId = task.UserId;
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.DueDate = task.DueDate;
                existingTask.Priority = task.Priority;
                existingTask.Status = task.Status;
                existingTask.CategoryId = task.CategoryId;
                existingTask.CreatedAt = task.CreatedAt;
                existingTask.UpdatedAt = task.UpdatedAt;
                existingTask.IsDeleted = task.IsDeleted;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating task", ex);
            }
        }

        public List<TaskViewModel> GetTasks(int userId)
        {
            List<Task> tasks = _context.Tasks.AsNoTracking().Where(x=>x.UserId == userId && x.IsDeleted == false).ToList();

            List<TaskViewModel> viewTask = new List<TaskViewModel>();

            foreach (var task in tasks)
            {
                TaskViewModel taskView = new TaskViewModel
                {
                    TaskId = task.TaskId,
                    Title = task.Title,
                    CategoryId = task.Category.CategoryId,
                    CategoryName = task.Category.Name
                };

                viewTask.Add(taskView);
            }

            return viewTask;
        }
    }
}