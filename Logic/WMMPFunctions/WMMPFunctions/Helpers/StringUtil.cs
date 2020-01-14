using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.IO;
using System.Threading.Tasks;

namespace WMMPFunctions
{
    /// <summary>
    /// updates an account in table storage. Creates it if the account doesn't already exists.
    /// </summary>
    public static class StringUtil
    {
        public static string GetBody(Stream body)
        {
            using (var sr = new StreamReader(body))
            {
                return sr.ReadToEnd();
            }
        }
    }
}