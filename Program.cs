using Microsoft.EntityFrameworkCore;
using IronDomeV2.Data;
namespace IronDomeV2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add SignalR services
            builder.Services.AddSignalR();

            builder.Services.AddDbContext<IronDomeV2Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("IronDomeV2Context") ?? throw new InvalidOperationException("Connection string 'IronDomeV2Context' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add SignalR services
            builder.Services.AddSignalR(); // <-- Add this line

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

            app.UseAuthorization();

            // Map SignalR hubs.
            app.MapHub<ChatHub>("/chathub");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
