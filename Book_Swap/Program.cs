using Book_Swap_DL;
using Book_Swap_Service.Interface;
using Book_Swap_Service.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("MasterDatabase");
builder.Services.AddDbContext<BookSwapContext>(option =>
option.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IBookInterface, BookService>();
builder.Services.AddScoped<IEncrypt,EncriptService>();
builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<IWishListService, WishListService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      builder =>
                      {
                          builder.WithOrigins(
                              "https://localhost:7022").AllowAnyHeader().AllowAnyMethod();
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

app.UseCors(builder =>
{
    builder.WithOrigins(
        "https://localhost:7022")
  .AllowAnyMethod()
  .AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
