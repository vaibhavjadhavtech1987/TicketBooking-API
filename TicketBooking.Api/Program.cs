using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TicketBooking.Api.DataContext;
using TicketBooking.Api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TicketBookingDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//Aded by vaibhav
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICoachRepository, CoachRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ITokenhandler, Tokenhandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


//Aded by vaibhav JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Aded by vaibhav CROS
app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
app.UseHttpsRedirection();

//Aded by vaibhav JWT
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
