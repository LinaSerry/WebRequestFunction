# WebRequestFunction

This is code for an Azure Function that is triggered every 5 minutes to get from an API, check for duplicated records it has already seen and POST any new alerts to another API.

This code was written specifically to poll the Translink API for Alerts and broadcast them to Microsoft Flow, but could be adapted to poll many APIs and broadcast updates. 

To make this work for any other API change the Classes.cs file to match the response from the API you would like to poll. 

### To run in Azure, run the ARM Template found in azuredeploy.json and then fill in the app settings with the following values:
- BearerToken : the Auth token for the API you want to GET from
- PollUrl : The URL of the API you want to get from
- PostUrl : The URL of the API you want to POST to

### To run locally open in VSCode and fill in the following values in the local.settings.json file 
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
    "PollUrl" : "",
    "PostUrl" :"" 

    }
}
```
