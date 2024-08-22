using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Extension;
using MyWebAPI.Models;

namespace MyWebAPI.Service
{
    public interface IUserService :  IDisposable
    {
        List<User> GetUserList();
        User GetUser(string Name, string Password);

        void SaveUser(User user);
        Task<PagedList<User>> GetPagedItems(PagingModel model);
    }
}
