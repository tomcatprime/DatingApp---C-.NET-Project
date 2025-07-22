using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//register Dbcontext as a service, opt => means lambda expression to configure the DbContext
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    // UseSqlite is used to configure the context to use SQLite database with the connection string
});
builder.Services.AddCors(); // Add CORS policy to allow requests from the client application, then after builder.Builder we can configure it.



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200")); // Allow requests from the specified origins

app.MapControllers();

app.Run();
