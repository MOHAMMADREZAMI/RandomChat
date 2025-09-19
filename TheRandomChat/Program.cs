using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TheRandomChat.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.WebHost.UseUrls("http://localhost:5041", "http://localhost:5042");

string EnvironmentVariable = Environment.GetEnvironmentVariable("JWT");

if (EnvironmentVariable == null)
{
    Console.WriteLine("Please Create a EnvironmentVariable(JWT)");
    return;
}
    


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Option =>
{
    Option.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "RandomChat",
        ValidAudience = "RandomChat's User",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariable))
    };

    Option.Events = new JwtBearerEvents
    {

        OnMessageReceived = context =>
        {
            var AccessToken = context.Request.Query["access_token"];

            if (context.Request.Path.StartsWithSegments("/ChatHub") && AccessToken.Any())
                context.Token = AccessToken;

            return Task.CompletedTask;
        }
    };



});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/ChatHub");

app.MapControllers();

Console.WriteLine("SignalR Hub: http://localhost:5041/ChatHub");

app.Run();

