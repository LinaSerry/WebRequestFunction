# WebRequestFunction
Azure Function triggered every 5 minutes to check against Translink's service disruption API and broadcast alerts using Flow
In order to run locally fill in the following values in the local.settings.json file 
```
{
    "IsEncrypted": false,
    "Values": {
    "AzureWebJobsStorage": "",
    "FUNCTIONS_WORKER_RUNTIME": "",
    "BlobStorageConnectionString": "",
    "BlobStorageAccountName":"",
    "BlobStorageContainerName": "",
    "BlobStorageBlobName": "",
    "StorageAccount":"",
    "BearerToken":"",
    "ApiUrl" : "",
    "Endpoint" :"" 

    }
}
```
