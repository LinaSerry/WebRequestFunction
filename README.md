# WebRequestFunction

Azure Function triggered every 5 minutes to check against an API, check for duplicated and POST any new alerts to another API.
This code was written specifically to poll the Translink API for Alerts and broadcast them to Microsoft Flow.

To make this work for any other API change the Classes.cs file to match the response from the API you would like to poll. 

To run in Azure
1) Create a storage account, container and add an empty json file
2) Create a function app and use the deployment center to connect to your forked Github repo
3) Fill in the app settings with the following values:
- BlobStorageConnectionString : From the blob storage account you created in step 1
- BlobStorageAccountName : From the blob storage account you created in step 1
- BlobStorageContainerName : From the blob storage account you created in step 1
- BlobStorageBlobName : From the blob storage account you created in step 1 (example alerts.json)
- BearerToken : the Auth token for the API you want to GET from
- ApiUrl : The URL of the API you want to get from
- Endpoint : The URL of the API you want to POST to

To run locally fill in the following values in the local.settings.json file 
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
    "BearerToken":"",
    "ApiUrl" : "",
    "Endpoint" :"" 

    }
}
```
