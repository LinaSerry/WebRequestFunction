using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs;

// Use a json to C# generator to easily generate the required classes : https://jsonutils.com/
namespace PollingWebRequest.Classes
{
    public class Effect
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Cause
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Header
    {
        public string language { get; set; }
        public string text { get; set; }
    }

    public class Description
    {
        public string language { get; set; }
        public string text { get; set; }
    }

    public class ServiceEffect
    {
        public string language { get; set; }
        public string text { get; set; }
    }

    public class ShortHeader
    {
        public string language { get; set; }
        public string text { get; set; }
    }

    public class ActivePeriod
    {
        public DateTime start { get; set; }
        public string end { get; set; }
    }

    public class Trip
    {
        public string route_id { get; set; }
        public string trip_id { get; set; }
    }

    public class InformedEntity
    {
        public string agency_id { get; set; }
        public int route_type { get; set; }
        public string route_id { get; set; }
        public string route_short_name { get; set; }
        public string route_long_name { get; set; }
        public string stop_id { get; set; }
        public object direction_id { get; set; }
        public Trip trip { get; set; }
        public List<string> activities { get; set; }
    }

    public class Alert
    {
        public Cause cause { get; set; }
        public string guid { get; set; }

        public string alert_id { get; set; }
        public string severity { get; set; }

        public List<InformedEntity> informed_entity { get; set; }

    }

}
