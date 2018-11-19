# WebRequestFunction

This is code for an [Azure Function](https://azure.microsoft.com/en-us/services/functions/) that is triggered every 5 minutes to do a GET from an API, check for duplicated records in [Azure blob storage](https://docs.microsoft.com/en-us/azure/storage/) it has already seen and POST any new alerts to another API.

This code was written specifically to poll the [Translink API](https://developer.translink.ca/) for Alerts and broadcast them to a [Microsoft Flow](https://flow.microsoft.com/en-us/) endpoint which in turn sends notifications via [Teams](https://products.office.com/en-us/microsoft-teams/group-chat-software) and SMS, but could be adapted to poll many APIs and broadcast updates. 

To make this work for any other API change the Classes.cs file to match the response from the API you would like to poll. 

* Note: If you don't need to check for duplicates consider using a [Logic App](https://azure.microsoft.com/en-us/services/logic-apps/) or [Microsoft Flow](https://flow.microsoft.com/en-us/) instead

## Quick Deploy to Azure

[![Deploy to Azure](http://azuredeploy.net/deploybutton.svg)](https://azuredeploy.net/)

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
