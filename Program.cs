using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
using MyWebAPI.DataBase;
using MyWebAPI.Models;
using MyWebAPI.Service;
using MyWebAPI.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Stripe;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped<IGoogleSearchService, GoogleSearchService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IRestCountriesService, RestCountriesService>();
//builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(x =>
{
    x.IdleTimeout = TimeSpan.FromMinutes(15);
});

//builder.Services.AddControllers(options =>
//{
//    options.ModelBinderProviders.Insert(0, new CustomModelBinderProvider());
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    //opt.OrderActionsBy((apiDesc) => $"{swaggerControllerOrder.SortKey(apiDesc.ActionDescriptor.RouteValues["controller"])}");

    opt.EnableAnnotations();
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Rao's Custom API"
	});

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Authorization",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<JWTConfiguration>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});




builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthRequirement", policy =>
        policy.Requirements.Add(new AuthRequirement("dotsquares.com")));
});
builder.Services.AddSingleton<IAuthorizationHandler, AuthRequirementHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://example.com") // The URL of the client that is allowed to access the resource.
                   .AllowAnyHeader()
                   .WithMethods("GET", "POST")
                   .AllowCredentials();
        });
});
builder.Services.AddLogging(builder => builder.AddConsole());

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    //options.ApiVersionReader = new UrlSegmentApiVersionReader();
    //options.ApiVersionReader = new QueryStringApiVersionReader("v");
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
});
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<DataContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DBConnection"),
		sqlOptions => sqlOptions.EnableRetryOnFailure(
			maxRetryCount: 5,                // Number of retry attempts
			maxRetryDelay: TimeSpan.FromSeconds(10), // Maximum delay between retries
			errorNumbersToAdd: null          // Optionally, specify SQL error numbers to retry on
		)
	));
builder.Services.AddResponseCaching();

var app = builder.Build();

app.Use(async (context, next) =>
{
    //if (!context.Request.Path.StartsWithSegments("/swagger") &&
    //    !context.Request.Cookies.ContainsKey("AuthToken") &&
    //    !context.Request.Path.StartsWithSegments("/Login"))
    //{
    //    context.Response.Redirect("/Login");
    //    return;
    //}
    ////if (context.Request.Path.StartsWithSegments("/Login"))
    ////{
    ////    context.Response.Redirect("/Login");
    ////    return;
    ////}
    await next.Invoke();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

   
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseSwaggerAuthorized();

app.MapControllers();
app.MapRazorPages();
app.UseSession();

app.UseSwagger();
app.UseSwaggerUI(c =>
{

    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Amit Kumar Yadav");
    c.RoutePrefix = "swagger";
	c.DocumentTitle = "My Custom API Documentation"; // Custom title for the Swagger UI
	c.DefaultModelsExpandDepth(-1); // Hides the models section
	c.DisplayRequestDuration(); // Displays the duration of requests
	c.EnableValidator(); // Enable request validation (optional)
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");
app.UseResponseCaching();
app.UseMiddleware<RateLimitingMiddleware>();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
