using Microsoft.EntityFrameworkCore;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepositery;
using Bulky.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BulkyWeb
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<ApplicationDBContext>(options=>options
			.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection")));
			
			builder.Services.AddIdentity<IdentityUser,IdentityRole>(/*options => options.SignIn.RequireConfirmedAccount = true*/).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
			//configure after identity to work
			builder.Services.ConfigureApplicationCookie(options => 
			{
				options.LoginPath = $"/Identity/Account/Login";
				options.LogoutPath = $"/Identity/Account/Logout";
				options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
			});
			builder.Services.AddRazorPages();
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
			builder.Services.AddScoped<IEmailSender,EmailSender>();
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
			app.MapRazorPages();
			app.MapControllerRoute(
				name: "default",
				pattern: "{area=customer}/{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
