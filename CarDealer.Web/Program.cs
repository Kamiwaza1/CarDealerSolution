using CarDealer.Core.Services;
using CarDealer.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();



//DAL
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICarDealerRepository, CarDealerRepository>();
//BLL
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IUserService, UserService>();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
