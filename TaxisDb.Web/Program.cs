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

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
