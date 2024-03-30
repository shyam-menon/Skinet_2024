using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        /// <summary>
        /// Adds application services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="config">The <see cref="IConfiguration"/> containing the configuration settings.</param>
        /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {           

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            // Add DbContext service for StoreContext
            services.AddDbContext<StoreContext>(options =>
                options.UseSqlite(config.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                // This is the filter that will be used to handle validation errors
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    // This will give us the list of errors that have occurred
                    // in the model state
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            return services;
        }
        
    }
}