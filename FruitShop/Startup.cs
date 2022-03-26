using Domain.Entities;
using Domain.Interfaces;
using FruitShop.V1.Controllers.Articles.Service;
using FruitShop.V1.Controllers.Articles.Service.Interface;
using FruitShop.V1.Controllers.Customers.Service;
using FruitShop.V1.Controllers.Customers.Service.Interface;
using Infraestructure.Models;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace src.FruitShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<FruitShopDbContext>(options => options.UseSqlServer("Data Source=localhost;Initial Catalog=FruitShop;Integrated Security=True"));
            services.AddTransient<IRepository<Article>, ArticleRepository>();
            services.AddTransient<IRepository<Customer>, CustomerRepository>();
            services.AddTransient<IRepository<Purchase>, PurchaseRepository>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
