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
                    ["V3"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey, RecaptchaSecretType.V3),
                    ["V2"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey, RecaptchaSecretType.V2),
                    ["Android"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey, RecaptchaSecretType.V2Android)
                })
                .AddTokenHeaderProvider()
                .AddConfigurationHeaderProvider()
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
