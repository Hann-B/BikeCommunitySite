using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BikeCommunitySite.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public enum Role
        {
            Guides,
            Rentals,
            Hosts,
            Clients
        }

        public IList<Role> Roles = new List<Role>();

        public bool IsUserInRole (Role r)
        {
            return this.Roles.Any(a => a == r);
        }
        public ApplicationUser AddUserToRole(Role r)
        {
            this.Roles.Add(r);
            return this;
        }

        public string Name { get; set; }
        public string Nickname { get; set; }

        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }

        public string Photo { get; set; }

        public string Bio { get; set; }
        public string Experience { get; set; }

        [DataType(DataType.Currency)]
        public float? Price { get; set; }
        public string Products { get; set; }
        public string Services { get; set; }
        public string Specialties { get; set; }

        IEnumerable<TripModel> PastTrips = new HashSet<TripModel>();
        IEnumerable<TripModel> PlannedTrips = new HashSet<TripModel>();
        
    }
}
