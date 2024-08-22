using Microsoft.AspNetCore.Authorization;

namespace MyWebAPI.Extension
{
    public class AuthRequirement : IAuthorizationRequirement
    {
        public string EmailDomain { get; }

        public AuthRequirement(string email)
        {
            EmailDomain = email;
        }
    }
    public class AuthRequirementHandler : AuthorizationHandler<AuthRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthRequirement requirement)
        {

            var email = context.User.Claims.FirstOrDefault(c => c.Type.ToLower().Contains("email"))?.Value;

            if (email != null && email.Contains(requirement.EmailDomain))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
