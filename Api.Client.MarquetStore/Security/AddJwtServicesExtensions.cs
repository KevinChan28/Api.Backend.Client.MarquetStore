using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Client.MarquetStore.Security
{
    public static class AddJwtServicesExtensions
    {
        public static void AddJwtServices(this IServiceCollection Services, IConfiguration Configuration)
        {

            // Add settings

            var bindJwtsettings = new JwtSettings();
            Configuration.Bind("JsonWebTokenKeys", bindJwtsettings);

            Services.AddSingleton(bindJwtsettings);

            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = bindJwtsettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtsettings.IssuerSigningKey)),
                    ValidateIssuer = bindJwtsettings.ValidateIssuer,
                    ValidIssuer = bindJwtsettings.ValidIssuer,
                    ValidateAudience = bindJwtsettings.ValidateAudience,
                    ValidAudience = bindJwtsettings.ValidAudience,
                    RequireExpirationTime = bindJwtsettings.RequiredExpirationTime,
                    ValidateLifetime = bindJwtsettings.ValidateLifetime,
                    ClockSkew = TimeSpan.FromDays(1)
                };
            });
        }
    }
}
