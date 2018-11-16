namespace TransLink
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Newtonsoft.Json;
    using TransLink.Classes;
    
    public class StorageHelper
    {
        static CloudStorageAccount cloudStorageAccount;
        static CloudBlobContainer cloudBlobContainer;
        static string storageConnectionString;
        static string storageContainerName;
        static string storageBlobName;

        /// <Summary>
        /// Initializes the StorageAccount configuration based on keys found in .env
        /// </Summary>
        internal void InitializeStorageAccount()
        {
            // Reading .env files
            storageConnectionString = Utilities.GetEnvironmentVariable("BlobStorageConnectionString");
            storageContainerName = Utilities.GetEnvironmentVariable("BlobStorageContainerName");
            storageBlobName = Utilities.GetEnvironmentVariable("BlobStorageBlobName");
            
            if (CloudStorageAccount.TryParse(storageConnectionString, out cloudStorageAccount))
            {
                var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                cloudBlobContainer = cloudBlobClient.GetContainerReference(storageContainerName);
            }
        }

        /// <Summary>
        /// Gets a tweets list from the tweets.json blob in the container 'Tweets'
        /// </Summary>
        internal async Task<List<Alert>> GetListOfAlertsAsync()
        {
            var latestBlockBlob =  cloudBlobContainer.GetBlockBlobReference(storageBlobName);
            string jsonString = await latestBlockBlob.DownloadTextAsync();
            return JsonConvert.DeserializeObject<List<Alert>>(jsonString);
        }

        /// <Summary>
        /// Uploads an update of the blob tweets.json in the container 'Tweets'
        /// </Summary>
        internal async void UpdateBlobAsync(string jsonFile)
        {
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(storageBlobName);
            await cloudBlockBlob.UploadTextAsync(jsonFile);
        }
    }
}