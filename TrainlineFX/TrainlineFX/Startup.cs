/*
 * Trainline Copyright2021
 * Software Engineer Candidate: Lewis Allan
 * 
 * Description: Startup class with Dependency Resolver
 * 19/03/2021 LA Initial version
 * 
 */

namespace TrainlineFX
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using TrainlineFX.BusinessLogic;
    using TrainlineFX.BusinessLogic.Interfaces;
    using TrainlineFX.Configuration;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<ExchangeRateApiConfig>(this.Configuration.GetSection("ExchangeRatesApi"));
            services.AddTransient<IExchangeRates, ExchangeRates>();
            services.AddTransient<ICurrencyConverterService, CurrencyConverterService>();
            services.AddTransient<ICurrencyConverterService, CurrencyConverterService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
