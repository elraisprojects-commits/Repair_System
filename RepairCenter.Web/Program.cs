
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepairCenter.data.Contexts;
using RepairCenter.data.Entities;
using RepairCenter.data.Seed;
using RepairCenter.Services.AdminReview;
using RepairCenter.Services.AdminReview.RepairCenter.Services.AdminReviews;
using RepairCenter.Services.Auth;
using RepairCenter.Services.DeliverRequest;
using RepairCenter.Services.EmployeeBonuses;
using RepairCenter.Services.EmployeeReports;
using RepairCenter.Services.Employees;
using RepairCenter.Services.Inspection;
using RepairCenter.Services.Interfaces;
using RepairCenter.Services.Inventory;
using RepairCenter.Services.Invoices;
using RepairCenter.Services.Notification;
using RepairCenter.Services.Reports;
using RepairCenter.Services.Reports.RepairCenter.Services.Dashboard;
using RepairCenter.Services.RequestNotes;
using RepairCenter.Services.RequestReports;
using RepairCenter.Services.Requests;
using RepairCenter.Web.Hubs;
using RepairCenter.Web.SignalR;
using System.Text;

namespace RepairCenter.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSignalR();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",

                    Name = "Authorization",

                    In = ParameterLocation.Header,

                    Type = SecuritySchemeType.Http,

                    Scheme = "bearer",

                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
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
                Array.Empty<string>()
            }
                    });
            });

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<IInspectionService,InspectionService>();
            builder.Services.AddScoped<IAdminReviewService, AdminReviewService>();
            builder.Services.AddScoped<IRequestNoteService, RequestNoteService>();
            builder.Services.AddScoped<IDeliveryService, DeliveryService>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddScoped<IInventoryService, InventoryService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IReportService, ReportService > ();
            builder.Services.AddScoped<IEmployeeBonusService, EmployeeBonusService>();
            builder.Services.AddScoped<IEmployeeReportService, EmployeeReportService>();
            builder.Services.AddScoped<IRequestReportService, RequestReportService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



           

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            builder.Services.AddScoped<
                INotificationSender,
                NotificationSender>();
            builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                    options.TokenValidationParameters = new TokenValidationParameters
               {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,

                       ValidIssuer = builder.Configuration["Jwt:Issuer"],
                       ValidAudience = builder.Configuration["Jwt:Audience"],

                              IssuerSigningKey = new SymmetricSecurityKey(
                              Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                     };
               });


            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var context =
                    scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var userManager =
                    scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var roleManager =
                    scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await AppDbContextSeed.SeedRolesAndAdminAsync(
                    context,
                    userManager,
                    roleManager);
            }


           
                app.UseSwagger();
                app.UseSwaggerUI();
            

            app.UseHttpsRedirection();
          

            app.UseCors("AllowFrontend");


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapHub<NotificationHub>("/notificationHub");


          
            app.Run();
        }
    }
}
