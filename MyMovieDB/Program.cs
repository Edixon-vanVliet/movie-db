using Microsoft.AspNetCore.Mvc;
using MyMovieDB.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDatabase(builder.Configuration)
    .AddRepositories()
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = (context) =>
        {
            return new BadRequestObjectResult(new BadRequest(context));
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsProduction())
{
    builder.WebHost.UseUrls("http://*:" + Environment.GetEnvironmentVariable("PORT"));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
