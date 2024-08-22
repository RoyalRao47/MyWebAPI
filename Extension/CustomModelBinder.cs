using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyWebAPI.Models;

namespace MyWebAPI.Extension
{
    public class CustomModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var request = bindingContext.HttpContext.Request;
            if (bindingContext.ModelMetadata.ModelType == typeof(User))
            {
                // Custom binding logic (e.g., read from query string or body)
                var id = request.Query["Id"].ToString();
                var name = request.Query["Name"].ToString();
                var email = request.Query["Email"].ToString();
                var password = request.Query["Password"].ToString();
                var phoneNumber = request.Query["PhoneNumber"].ToString();
                var role = request.Query["Role"].ToString();


                var model = new User
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    PhoneNumber = phoneNumber,
                    Role = role
                };
                bindingContext.Result = ModelBindingResult.Success(model);
            }
            if (bindingContext.ModelMetadata.ModelType == typeof(UserModel))
            {
                // Custom binding logic (e.g., read from query string or body)
                var name = request.Query["Name"].ToString();
                var email = request.Query["Email"].ToString();


                var model = new UserModel
                {
                    Name = name,
                    Email = email,
                };
                bindingContext.Result = ModelBindingResult.Success(model);
            }
            return Task.CompletedTask;
        }
    }

    //public class CustomModelBinderProvider : IModelBinderProvider
    //{
    //    public IModelBinder GetBinder(ModelBinderProviderContext context)
    //    {
    //        if (context == null)
    //        {
    //            throw new ArgumentNullException(nameof(context));
    //        }

    //        if (context.Metadata.ModelType == typeof(User))
    //        {
    //            return new CustomModelBinder();
    //        }

    //        return null;
    //    }
    //}
}
