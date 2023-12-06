using System.ComponentModel.DataAnnotations;

namespace ConnectWise_Web.Models
{
    

    public class Intern
    {
        public Intern() 
        { 
        
        }
        public Intern(int internID, string firstName, string lastName, string email, string password, string phoneNumber, string profileImage, string skills, string interests, string education, string location, string bio)
        {
            InternID = internID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            ProfileImage = profileImage;
            Skills = skills;
            Interests = interests;
            Education = education;
            Location = location;
            Bio = bio;
        }

        public int InternID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Profile Image URL is required")]
        [Display(Name = "Profile Image URL")]
        public string ProfileImage { get; set; }

        [Required(ErrorMessage = "Skills are required")]
        public string Skills { get; set; }

        [Required(ErrorMessage = "Interests are required")]
        public string Interests { get; set; }

        [Required(ErrorMessage = "Education is required")]
        public string Education { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Bio is required")]
        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }

 
    }
}