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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddRecaptcha()
                // Use appsettings.json
                //.AddInMemoryConfigurationStore(Configuration.GetSection("Recaptcha"))
                .AddInMemoryConfigurationStore(new Dictionary<string, RecaptchaConfiguration>
                {
                    ["Sitekey1"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey),
                    ["Sitekey2"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey),
                    ["Sitekey3"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey)
                })
                .AddTokenHeaderProvider()
                .AddConfigurationHeaderProvider()
                .AddRecaptchaHttpClient()
                .UseGoogleUrl();
            // UseGlobalUrl(); // will use recaptcha.net mirror for countries where google.com is blocked.
            // UseCustomUrl("your_url"); // will use custom url for validation.
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
