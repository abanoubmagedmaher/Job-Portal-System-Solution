
namespace Job_Portal_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseCors("JopPortal");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}