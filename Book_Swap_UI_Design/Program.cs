using Microsoft.AspNetCore.Authentication.Cookies;

namespace Book_Swap_UI_Design
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
           builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.LoginPath = "/Home/Login";
                    options.AccessDeniedPath = "/Home/Login";
                    options.SlidingExpiration = true;
                });

            builder.Services.AddControllersWithViews();

            //builder.Services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //                      builder =>
            //                      {
            //                          builder.WithOrigins(
            //                              "https://localhost:7022").AllowAnyHeader().AllowAnyMethod();
            //                      });
            //});

            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy(name: MyAllowSpecificOrigins,
            //                      policy =>
            //                      {
            //                          policy.WithOrigins("http://example.com",
            //                                              "http://www.contoso.com");
            //                      });
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}