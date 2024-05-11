using Authentication.Application.Extensions;
using Authentication.Infrastructure.Extensions;
using Authentication.Infrastructure.Persistence.Database;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme,
        securityScheme: new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter the Bearer Auth : `Bearer Generated-JWT-Token`",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Type = SecuritySchemeType.Http, // Utilisez SecuritySchemeType.Http pour le schéma Bearer
            Scheme = "Bearer"
        });



    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme // Correction du nom de la classe
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            },
            Array.Empty<string>()
        }
    });
});
/////////////////////////////////////////////////////////////////////////////////////////////////////
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddApplication();
builder.Services.AddInfrastracture(builder.Configuration);
/////////////////////////////////////////////////////////////////////////////////////////////////////

builder.Services.AddSingleton<IMongoDatabase>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<DataBaseSettings>>().Value;
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});


builder.Services.Configure<DataBaseSettings>(builder.Configuration.GetSection("MongoDbConfig"));
/////////////////////////////////////////////////////////////////////////////////////////////////////
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                         .AllowAnyHeader());

});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");
app.MapControllers();
app.Run();