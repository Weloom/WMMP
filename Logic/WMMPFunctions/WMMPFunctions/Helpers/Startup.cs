using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WMMPFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;
            var hostingEnvironment = services.BuildServiceProvider().GetService<IHostingEnvironment>();
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath).AddEnvironmentVariables();

            if (!hostingEnvironment.IsDevelopment())
            {
                {
                    AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
                    var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

                    configurationBuilder.AddAzureKeyVault($"https://wmmpkeyvault.vault.azure.net/", keyVaultClient, new DefaultKeyVaultSecretManager());
                }
            }

            services.AddSingleton<IConfiguration>(configurationBuilder.Build());
        }
    }
}

