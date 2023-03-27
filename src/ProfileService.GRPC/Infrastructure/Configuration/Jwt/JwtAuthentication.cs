using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ProfileService.GRPC.Infrastructure.Configuration.Jwt
{
    public static class JwtAuthentication
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtConfig jwtConfig)
        {
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = jwtConfig.Issuer,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = false,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        //требуется ли наличие времени существования, default = true
                        RequireExpirationTime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });
            return services;
        }
    }
}