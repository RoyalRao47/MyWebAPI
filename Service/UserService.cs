using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.DataBase;
using MyWebAPI.Extension;
using MyWebAPI.Models;

namespace MyWebAPI.Service
{
    public class UserService : IUserService
    {
        private bool disposedValue = false;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
		

		public UserService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            
        }
        public List<User> GetUserList()
        {
            var userList = _context.Users.ToList();
            return userList;
        }
        public User GetUser(string Name, string Password)
        {
            var userList = GetUserList();
            var user = userList.FirstOrDefault(x => x.Name.ToLower() == Name.ToLower() && x.Password.ToLower() == Password.ToLower());
            return user;
        }
        public void SaveUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task<PagedList<User>> GetPagedItems(PagingModel model)
        {
            var users = _context.Users.AsQueryable();
            return await PagedList<User>.ToPagedList(users, model.PageNumber, model.PageSize);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
