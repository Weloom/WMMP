using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System.IO;
using System.Threading.Tasks;

namespace WMMPFunctions
{
    public static class StorageAccess
    {
        public static async Task<CloudTable> GetTable(string tableName, ILogger log)
        {
            CloudStorageAccount cloudStorageAccount = await StorageAccess.GetStorageAccount(log);
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference(tableName);
        }

        public static async Task<CloudBlobContainer> GetBlobContainer(string containerName, ILogger log)
        {
            CloudStorageAccount cloudStorageAccount = await StorageAccess.GetStorageAccount(log);
            log.LogInformation($"BLOB step 1");
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            log.LogInformation($"BLOB step 2");
            var container = blobClient.GetContainerReference(containerName);
            log.LogInformation($"BLOB step 3 containername: " + containerName);
            await container.CreateIfNotExistsAsync();
            log.LogInformation($"BLOB step 4");
            return container;
        }

        private static async Task<CloudStorageAccount> GetStorageAccount(ILogger log)
        {
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            log.LogInformation($"token: {azureServiceTokenProvider.ToString()}");

            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            log.LogInformation($"key vault accessed:  {keyVaultClient.ToString()}");

            var secret = await keyVaultClient.GetSecretAsync(@"https://wmmpkeyvault.vault.azure.net", "connectionstring-datastore");
            log.LogInformation($"Secret: {secret.Value}");

            return CloudStorageAccount.Parse(secret.Value);
        }
    }
}