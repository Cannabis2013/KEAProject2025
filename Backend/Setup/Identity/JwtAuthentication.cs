using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ALBackend.Setup.Identity;

public static class JwtAuthentication
{
    public static void SetupJwtAuthentication(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(ConfigureJwtOptions)
            .AddJwtBearer(options => SetupBearerOptions(options,builder));
    }

    private static void ConfigureJwtOptions(AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }

    private static void SetupBearerOptions(JwtBearerOptions o,WebApplicationBuilder builder)
    {
        o.SaveToken = true;
        o.TokenValidationParameters = new()
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    }
}