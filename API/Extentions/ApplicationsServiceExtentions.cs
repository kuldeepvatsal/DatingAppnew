using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Extentions
{
    public static class ApplicationsServiceExtentions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,IConfiguration config)
        {
            services.AddScoped<ITokenService,TokenService>();
           services.AddDbContext<DataContext>(Options=>
            {
                Options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
          
             return services;
        }
         
    }
}