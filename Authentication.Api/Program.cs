using Authentication.Application.Extensions;
using Authentication.Infrastructure.Extensions;
using Authentication.Infrastructure.Persistence.Database;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MediatR;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/////////////////////////////////////////////////////////////////////////////////////////////////////
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
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
/////////////////////////////////////////////////////////////////////////////////////////////////////


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