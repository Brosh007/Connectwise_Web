namespace ConnectWise_Web.Models
{
    public class MeetingRequest
    {
        public int MeetingRequestID { get; set; }
        public int BusinessOwnerID { get; set; }
        public int InternID { get; set; }
        public DateTime MeetingDateTime { get; set; }
        public string MeetingPurpose { get; set; }
        public string Status { get; set; }
        public string InternFirstName { get; set; } // Add these properties
        public string InternLastName { get; set; }
        public string BusinessOwnerCompanyName { get; set; }

    }
}
