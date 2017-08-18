using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeCommunitySite.Models
{
    public class DestinationModel
    {
        public class Attribs
        {
            public string length { get; set; }
        }
        public class ActivityType
        {
            public string created_at { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string updated_at { get; set; }
        }

        public class Activity
        {
            public string name { get; set; }
            public string unique_id { get; set; }
            public int place_id { get; set; }
            public int activity_type_id { get; set; }
            public string activity_type_name { get; set; }
            public string url { get; set; }
            public Attribs attribs { get; set; }
            public string description { get; set; }
            public double length { get; set; }
            public ActivityType activity_type { get; set; }
            public string thumbnail { get; set; }
            public object rank { get; set; }
            public double rating { get; set; }
        }

        public class Place
        {
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string name { get; set; }
            public object parent_id { get; set; }
            public int unique_id { get; set; }
            public string directions { get; set; }
            public double lat { get; set; }
            public double lon { get; set; }
            public object description { get; set; }
            public object date_created { get; set; }
            public List<object> children { get; set; }
            public List<Activity> activities { get; set; }
        }

        public class RootObject
        {
            public List<Place> places { get; set; }
        }
    }
}

