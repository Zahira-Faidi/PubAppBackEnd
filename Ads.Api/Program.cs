using Ads.Application.Extensions;
using Ads.Infrastructure.Extensions;
using Ads.Infrastructure.Persistence.DataBase;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IMongoDatabase>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<DataBaseSettings>>().Value;
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.Configure<DataBaseSettings>(builder.Configuration.GetSection("MyDb"));
builder.Services.AddInfrastractureConfiguration();
builder.Services.AddApplicationConfiguration();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3030")
                          .AllowAnyMethod()
                         .AllowAnyHeader());

});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
