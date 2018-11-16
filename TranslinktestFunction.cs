using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TransLink.Classes;
using System.Net.Http.Headers;
using System.Net.Http;

namespace TransLink
{
    public static class TranslinktestFunction
    {
        private static StorageHelper storageHelper= new StorageHelper();
        [StorageAccount("BlobStorageConnectionString")]
        [FunctionName("translinktestFunction")]
       public static async Task Run([TimerTrigger("0 */2 * * * *")]TimerInfo myTimer, ILogger log)
        {
            if(myTimer.IsPastDue)
            {
                log.LogInformation("Timer is running late!");
            }
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            storageHelper.InitializeStorageAccount();

            var apimUrl = Utilities.GetEnvironmentVariable("ApiUrl");

             HttpClient Client = new HttpClient();

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Utilities.GetEnvironmentVariable("TranslinkToken"));

            var result = await Client.GetAsync(apimUrl);

            //var requestBody = new StreamReader(result.).ReadToEnd();
            var requestBody = await result.Content.ReadAsStringAsync();
            var alerts =  JsonConvert.DeserializeObject<List<Alert>>(requestBody);
            
            try
            {

                log.LogInformation($"Updating and looking for duplicated Tweets list in Storage");
                alerts = CheckDuplicatesInStorage(alerts);
                log.LogInformation($"Update completed");

                if(alerts.Count>0)
                {
                    var endpoint = Utilities.GetEnvironmentVariable("Endpoint");
                   HttpClient c = new HttpClient();
                   var myContent = JsonConvert.SerializeObject(alerts);
                   var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                   var byteContent = new ByteArrayContent(buffer);
                   byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                   var trigger = await c.PostAsync(endpoint, byteContent); 
                   Console.WriteLine(JsonConvert.SerializeObject(alerts));
                } 
                //else return null;
            }
            catch(Exception e)
            {
                log.LogInformation(e.ToString());
                //return null;
            }
        }

        static List<Alert> CheckDuplicatesInStorage(List<Alert> alerts)
        {
            List<Alert> filteredAlerts = new List<Alert>();
            var alertsFromStorage = storageHelper.GetListOfAlertsAsync().Result;
            if (alertsFromStorage != null){
                foreach(var alert in alerts)
            {
                int index = alertsFromStorage.FindIndex(f => string.Equals(f.alert_id, alert.alert_id));
                if (index < 0) 
                {
                    alertsFromStorage.Add(alert);
                    filteredAlerts.Add(alert);
                }
            }
            storageHelper.UpdateBlobAsync(JsonConvert.SerializeObject(alertsFromStorage));
                
            }
            
            return filteredAlerts;
        }
    }
}