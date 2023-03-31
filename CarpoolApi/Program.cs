using CarpoolApi.Interfaces;
using CarpoolApi.Models;
using CarpoolApi.ServiceHelpers;
using CarpoolApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var policyName = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName, builder =>
    {
        builder.AllowAnyOrigin().WithMethods("GET","POST","PUT").AllowAnyHeader();
    });   
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(opt =>
                                opt.UseSqlServer("Server=LAPTOP-V94GBDP5\\SQLEXPRESS;" +
                                "Database = Carpool_DB;" +
                                "Trusted_Connection=True;" +
                                "Integrated Security=True;" +
                                "Trust Server Certificate=true"

                                ));
builder.Services.AddScoped<UpdateActiveOffers>();
builder.Services.AddSingleton<OfferMatchesHolder>();
builder.Services.AddScoped<BookingHelper>();
builder.Services.AddScoped<ILoginService,LoginService>();
builder.Services.AddScoped<IOfferService,OfferService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProfileService,ProfileService>();
builder.Services.AddScoped<ISignupService,SignupService>();
builder.Services.AddCors();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Jwt:ValidIssuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Jwt:ValidAudience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:Key")))
        };
    }
   );

var app = builder.Build();

var scope = app.Services.CreateScope();
scope.ServiceProvider.GetService<UpdateActiveOffers>();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(policyName);

app.Run();

