using Microsoft.EntityFrameworkCore;
using StudentEnrolmentNew.src.Data;
using Microsoft.OpenApi.Models;
using System.Text.Json;

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

            // Add Swagger services
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EnrolPro API", Version = "v1" });

                // Use your custom swagger.json file
                var filePath = Path.Combine(AppContext.BaseDirectory, "Docs", "swagger.json");
                if (File.Exists(filePath))
                {
                    var jsonString = File.ReadAllText(filePath);
                    var jsonDocument = JsonDocument.Parse(jsonString);
                    var root = jsonDocument.RootElement;

                    if (root.TryGetProperty("info", out var infoElement))
                    {
                        c.SwaggerDoc("v1", infoElement.Deserialize<OpenApiInfo>());
                    }
                }
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

            // Add Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnrolPro API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            context.Database.EnsureCreated();
        }
    }
}