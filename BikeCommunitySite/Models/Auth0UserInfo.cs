using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeCommunitySite.Models
{
    public class Auth0UserInfo
    {
        public bool email_verified { get; set; }
        public string email { get; set; }
        public string clientID { get; set; }
        public string updated_at { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string user_id { get; set; }
        public string nickname { get; set; }
        public string created_at { get; set; }
        public string sub { get; set; }
    }
}
