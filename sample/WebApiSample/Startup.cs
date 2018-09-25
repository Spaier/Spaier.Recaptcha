using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
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
                    ["Sitekey1"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey, RecaptchaSecretType.V2),
                    ["Sitekey2"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey, RecaptchaSecretType.V2Android),
                    ["Sitekey3"] = new RecaptchaConfiguration(RecaptchaDefaults.TestSecretKey, RecaptchaSecretType.V3),
                })
                .AddTokenHeaderProvider()
                .AddConfigurationHeaderProvider()
                .AddRecaptchaHttpClient(configureHttpBuilder: httpBuilder =>
                {
                    // You can setup Polly here
                    httpBuilder.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10)
                    }));
                })
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
