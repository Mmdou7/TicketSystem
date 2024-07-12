using Microsoft.EntityFrameworkCore;
using TicketSystem.BL;
using TicketSystem.DAL;
using TicketSystem.DAL.Repositories.Tickets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var ConnectionString = builder.Configuration.GetConnectionString("TickectSystem");
builder.Services.AddDbContext<SystemContext>(options =>
options.UseSqlServer(ConnectionString));

builder.Services.AddScoped<ITicketManager, TicketManager>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS policy
app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
