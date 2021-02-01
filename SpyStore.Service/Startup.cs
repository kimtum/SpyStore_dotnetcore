using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using SpyStore.Dal.EfStructures;
using SpyStore.Dal.Initialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SpyStore.Dal.Repos;
using SpyStore.Dal.Repos.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using SpyStore.Service.Filters;



namespace SpyStore.Service
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;


        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //revert to Pascal casing
            services.AddMvcCore(config =>
                    config.Filters.Add(new SpyStoreExceptionFilter(_env)))
                .AddJsonFormatters(j =>
            {
                j.ContractResolver = new DefaultContractResolver();
                j.Formatting = Formatting.Indented;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials();
                });
            });

            //NOTE: Did not disable mixed mode running here
            services.AddDbContextPool<StoreContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("SpyStore")));

            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IShoppingCartRepo, ShoppingCartRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IOrderDetailRepo, OrderDetailRepo>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "SpyStore Service",
                        Version = "v1",
                        Description = "Service to support the SpyStore sample eCommercesite",
                        //TermsOfService = "None",
                        License = new OpenApiLicense
                        {
                            Name = "Freeware",
                            Url = new Uri("http://localhost:60371/LICENSE.txt")
                        }
                    });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                using(var serviceScope = app.ApplicationServices
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<StoreContext>();
                    SampleDataInitializer.InitializeData(context);
                }
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpyStore Service v1");
            });
            app.UseStaticFiles();

            //needed by JavaScript frameworks
            app.UseCors("AllowAll");

            app.UseMvc();
        }
    }
}
