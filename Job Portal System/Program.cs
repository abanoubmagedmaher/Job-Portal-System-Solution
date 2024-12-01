
using Job_Portal_System.Data;
using Job_Portal_System.Interfaces;
using Job_Portal_System.Models;
using Job_Portal_System.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Job_Portal_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Register DbContext
            builder.Services.AddDbContext<JobPortalContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion
            #region Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                   .AddEntityFrameworkStores<JobPortalContext>()
                   .AddDefaultTokenProviders();
            #endregion

            #region JWT
            // Configure JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });
            #endregion
            #region Register UOW
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            #region Handel CORSE 
            builder.Services.AddCors(optios =>
            {
                optios.AddPolicy("JopPortal", polict =>
                {
                    polict.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                });

            }
            );

            #endregion
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseCors("JopPortal");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
