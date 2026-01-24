//using Framework.Common;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using System.Threading.Tasks;

//namespace SGen.Framework.Web.Authentication
//{
//    public static class JwtAuthentication
//    {
//        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
//        {
//            // Bind strongly typed settings
//            var jwtSettings = new JwtSettings();
//            configuration.GetSection("Jwt").Bind(jwtSettings);
//            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

//            var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

//            services.AddAuthentication(options =>
//            {
//                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//            .AddJwtBearer(options =>
//            {
//                options.RequireHttpsMetadata = false;
//                options.SaveToken = true;
//                options.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuer = true,
//                    ValidIssuer = jwtSettings.Issuer,
//                    ValidateAudience = true,
//                    ValidAudience = jwtSettings.Audience,
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ValidateLifetime = true,
//                    ClockSkew = TimeSpan.FromSeconds(30)
//                };
//            });

//            return services;
//        }
//    }
//}
