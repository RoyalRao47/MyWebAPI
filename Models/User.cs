using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class User
    {
        public int? Id { get; set; }
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
    }

    public class UserModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }

    public class LoginModel
    {
        /// <summary>
        /// User Name for login.
        /// </summary>
        /// <example>Amit</example>
        public required string UserName { get; set; }

        /// <summary>
        /// Password that you saved.
        /// </summary>
        /// <example>dfasdfasdf</example>
        public required string Password { get; set; }
    }
    public class PagingModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }



}
