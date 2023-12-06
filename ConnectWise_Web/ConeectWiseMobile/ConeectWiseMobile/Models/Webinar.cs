using System;
using System.Collections.Generic;
using System.Text;

namespace ConeectWiseMobile.Models
{
    public class Webinar
    {
        public int WebinarID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Location { get; set; }
        public string SpeakerName { get; set; }
        public string SpeakerBio { get; set; }
    }
}
