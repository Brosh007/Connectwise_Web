namespace ConnectWise_Web.Models
{
    public class Webinar
    {
        // Parameterless constructor
        public Webinar()
        {
            // You can set default values or initialization logic here
        }
        public Webinar(int webinarID, string title, string description, DateTime dateAndTime, string location, string speakerName, string speakerBio)
        {
            WebinarID = webinarID;
            Title = title;
            Description = description;
            DateAndTime = dateAndTime;
            Location = location;
            SpeakerName = speakerName;
            SpeakerBio = speakerBio;
        }

        public int WebinarID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Location { get; set; }
        public string SpeakerName { get; set; }
        public string SpeakerBio { get; set; }
    }
}
