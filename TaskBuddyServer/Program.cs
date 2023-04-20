using Microsoft.EntityFrameworkCore;
using System.Net;
using TaskBuddyServer.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TaskBuddyDbContext>(options =>
    options.UseSqlite("Data Source=TaskBuddy.db"));

var app = builder.Build();


// Ensure database is created.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<TaskBuddyDbContext>();
    db.Database.EnsureCreated();
}

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