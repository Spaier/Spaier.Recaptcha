using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spaier.Recaptcha;

namespace WebApiSample
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Option 1
            //services.AddRecaptcha()
            //    .AddTokenHeaderProvider()
            //    .AddConfigurationHeaderProvider(options =>
            //    {
            //        options.Configurations = new Dictionary<string, RecaptchaConfiguration>
            //        {
            //            [""] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey, RecaptchaSecretType.V2),
            //            ["Android"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey, RecaptchaSecretType.V2Android),
            //            ["V3"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey, RecaptchaSecretType.V3)
            //        };
            //    })
            //    .AddRecaptchaHttpClient()
            //    .UseGoogleUrl();

            // Option 2
            services.AddRecaptcha()
                .AddTokenHeaderProvider()
                .AddConfigurationHeaderProvider(Configuration.GetSection("Recaptcha"))
                .AddRecaptchaHttpClient()
                .UseGoogleUrl();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
