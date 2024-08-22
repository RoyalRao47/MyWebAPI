using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWebAPI.DataBase;
using MyWebAPI.Extension;
using MyWebAPI.Models;
using MyWebAPI.Service;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly ICacheService _cacheService;

        public UsersController(DataContext context, IConfiguration configuration, IUserService userService, ICacheService cacheService)
        {
            _context = context;
            _configuration = configuration;
            _userService = userService;
            _cacheService = cacheService;
        }


        [HttpGet("{Name:alpha},{Password}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<User> GetUser(string Name, string Password)
        {
            var user = _userService.GetUser(Name, Password);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }


        [HttpGet("GetAllUsers")]
        [Authorize(Policy = "AuthRequirement")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            //return _userService.GetUserList();
            return _cacheService.GetMData("UserList");
        }

		// GET: api/Users/5
		/// <summary>
		/// Retrieves a specific item by ID.
		/// </summary>
		/// <param name="id">The ID of the item to retrieve.</param>
		/// <returns>The item with the specified ID.</returns>
		[HttpGet("GetUserByID")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserByID(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            try
            {
                _userService.SaveUser(user);
                return user;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return NotFound();
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser([ModelBinder(BinderType = typeof(CustomModelBinder))] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            _userService.SaveUser(user);
            // Process the order
            return Ok(user);
        }

        [HttpGet("FilterUser")]
        public ActionResult<IEnumerable<User>> FilterUser([ModelBinder(BinderType = typeof(CustomModelBinder))] UserModel model)
        {
            var userList = _userService.GetUserList();

            if (!string.IsNullOrEmpty(model.Name) && model.Name != "string")
            {
                userList = userList.Where(p => p.Name.ToLower().Contains(model.Name.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(model.Email) && model.Email != "string")
            {
                userList = userList.Where(p => p.Email.ToLower().Contains(model.Email.ToLower())).ToList();
            }
            return userList;
        }

        [HttpGet("FilterUserNew")]
        public ActionResult<IEnumerable<User>> FilterUserNew(string? Name, string? Email)
        {
            var userList = _userService.GetUserList();

            if (!string.IsNullOrEmpty(Name) && Name != "string")
            {
                userList = userList.Where(p => p.Name.ToLower().Contains(Name.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(Email) && Email != "string")
            {
                userList = userList.Where(p => p.Email.ToLower().Contains(Email.ToLower())).ToList();
            }
            return userList;
        }

        [HttpGet("SortUser")]
        public ActionResult<IEnumerable<User>> SortUser(string? sortBy, string sortOrder)
        {
            var userList = _userService.GetUserList();

            if (!string.IsNullOrEmpty(sortBy))
            {
                bool descending = !string.IsNullOrEmpty(sortOrder) && sortOrder.ToLower() == "desc";

                switch (sortBy.ToLower())
                {
                    case "name":
                        userList = descending ? userList.OrderByDescending(i => i.Name).ToList() : userList.OrderBy(i => i.Name).ToList();
                        break;
                    case "value":
                        userList = descending ? userList.OrderByDescending(i => i.Email).ToList() : userList.OrderBy(i => i.Email).ToList();
                        break;
                    case "id":
                        userList = descending ? userList.OrderByDescending(i => i.Id).ToList() : userList.OrderBy(i => i.Id).ToList();
                        break;
                    case "email":
                        userList = descending ? userList.OrderByDescending(i => i.Email).ToList() : userList.OrderBy(i => i.Email).ToList();
                        break;
                    default:
                        return BadRequest("Invalid sort parameter.");
                }
            }
            return userList;
        }

        [HttpGet("GetPagingUser")]
        public async Task<IActionResult> GetPagingUser([FromQuery] PagingModel model)
        {
            var pagedData = await _userService.GetPagedItems(model);
            return Ok(pagedData);
        }

    }
}
