using System;
using System.Collections.Generic;
using System.Text;

namespace ConeectWiseMobile.Models
{
    public class BusinessOwner
    {
        public int BusinessOwnerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImage { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
    }
}
