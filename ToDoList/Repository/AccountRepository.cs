using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.DataContext;
using ToDoList.Helpers;

namespace ToDoList.Repository
{
    public class AccountRepository
    {
        private readonly TodoListAppEntities _context;

        public AccountRepository()
        {
            _context = new TodoListAppEntities();
        }

        public bool Register(User user)
        {
            try
            {
                var existing = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existing != null)
                    return false;

                user.PasswordHash = PasswordHasher.HashPassword(user.PasswordHash);

                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Login(string userName, string password)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == userName);

                if (user == null)
                {
                    return false;
                }

                var hashedPassword = PasswordHasher.HashPassword(password);

                if (user.PasswordHash != hashedPassword)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool IsUserNameRegistered(string userName)
        {
            try
            {
                return _context.Users.Any(u => u.UserName == userName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetUserIdByUsername(string userName)
        {
            return _context.Users
                .Where(u => u.UserName == userName)
                .Select(u => u.UserId)
                .FirstOrDefault();
        }

      
    }
}