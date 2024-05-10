namespace Identity
{
    public class Startup
    {
        public IConfiguration? Configuration { get; }
        public Startup(IConfiguration? configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddConfigureHandler(Configuration);
            //services.AddInfrastructure(Configuration);

            //services.AddMongoDb(Configuration);

            //services.AddAutoMapper(typeof(MappingProfile));
            //services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());


            services.AddLogging(configure => configure.AddConsole());

            services.AddEndpointsApiExplorer();
            services.AddSingleton(Configuration);
            services.AddControllers();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "dotnet", Version = "V1" }));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
