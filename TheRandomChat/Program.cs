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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT")))
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

app.UseStaticFiles();

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

app.MapFallbackToFile("index.html");

app.Run();
