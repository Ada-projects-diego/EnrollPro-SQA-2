using Microsoft.EntityFrameworkCore;
using StudentEnrolmentNew.src.Data;

namespace StudentEnrolmentWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Obtain the connection string from the configuration
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StudentEnrolmentContext>(opt => opt.UseMySQL(connectionString));
            services.AddScoped<IStudentEnrolmentContext>(x => x.GetService<StudentEnrolmentContext>());

            // Add CORS services
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    builder =>
                    {
                        // Use origins from configuration
                        var allowedOrigins = Configuration.GetSection("CorsPolicy:AllowedOrigins").Get<string[]>();
                        builder.WithOrigins(allowedOrigins)
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StudentEnrolmentContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Use CORS policy
            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            context.Database.EnsureCreated();
        }
    }
}
