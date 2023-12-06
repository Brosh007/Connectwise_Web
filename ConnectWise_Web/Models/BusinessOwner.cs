namespace ConnectWise_Web.Models
{
    public class BusinessOwner
    {
        public BusinessOwner()
        {
            // Add any initialization logic if needed
        }
        public BusinessOwner(int businessOwnerID, string firstName, string lastName, string email, string password, string phoneNumber, string profileImage, string companyName, string industry, string location, string bio)
        {
            BusinessOwnerID = businessOwnerID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            ProfileImage = profileImage;
            CompanyName = companyName;
            Industry = industry;
            Location = location;
            Bio = bio;
        }

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
