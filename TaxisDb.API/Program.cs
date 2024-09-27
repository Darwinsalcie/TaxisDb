using Microsoft.EntityFrameworkCore;
using TaxisDb.Persistence.Context;
using TaxisDb.Persistence.Interfaces;
using TaxisDb.Persistence.Repositories;
using TaxisDb.Persistence.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Taxisdb>(options =>
                                        options.UseSqlServer(builder.Configuration.GetConnectionString("Taxisdb")));


builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<RoleValidator>();

builder.Services.AddTransient<ITaxiRepository, TaxiRepository>();
builder.Services.AddTransient<TaxiValidator>();

builder.Services.AddTransient<ITripRepository, TripRepository>();
builder.Services.AddTransient<TripValidator>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<UserValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
