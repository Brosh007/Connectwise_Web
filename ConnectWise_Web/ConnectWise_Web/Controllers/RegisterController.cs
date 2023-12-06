using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using BCrypt.Net;
using ConnectWise_Web.Models;
using MySql.Data.MySqlClient;

namespace ConnectWise_Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IConfiguration _configuration;

        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MySqlConnection GetMySqlConnection() // Use MySqlConnection
        {
            return new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BusinessOwner()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BusinessOwner([Bind("FirstName, LastName, Email, Password, PhoneNumber, ProfileImage, CompanyName, Industry, Location, Bio")] BusinessOwner businessOwner)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(businessOwner.Password);

                using (MySqlConnection connection = GetMySqlConnection()) // Use MySqlConnection
                {
                    connection.Open();
                    string query = "INSERT INTO BusinessOwners (FirstName, LastName, Email, Password, PhoneNumber, ProfileImage, CompanyName, Industry, Location, Bio) " +
                                   "VALUES (@FirstName, @LastName, @Email, @Password, @PhoneNumber, @ProfileImage, @CompanyName, @Industry, @Location, @Bio)";
                    using (MySqlCommand command = new MySqlCommand(query, connection)) // Use MySqlCommand
                    {
                        command.Parameters.AddWithValue("@FirstName", businessOwner.FirstName);
                        command.Parameters.AddWithValue("@LastName", businessOwner.LastName);
                        command.Parameters.AddWithValue("@Email", businessOwner.Email);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.Parameters.AddWithValue("@PhoneNumber", businessOwner.PhoneNumber);
                        command.Parameters.AddWithValue("@ProfileImage", businessOwner.ProfileImage);
                        command.Parameters.AddWithValue("@CompanyName", businessOwner.CompanyName);
                        command.Parameters.AddWithValue("@Industry", businessOwner.Industry);
                        command.Parameters.AddWithValue("@Location", businessOwner.Location);
                        command.Parameters.AddWithValue("@Bio", businessOwner.Bio);

                        try
                        {
                            command.ExecuteNonQuery();
                            return RedirectToAction("Login", "Login");
                        }
                        catch (Exception ex)
                        {
                            // Log or print the exception message
                            Console.WriteLine(ex.Message);
                            // You can also add an error message to ModelState if needed
                            ModelState.AddModelError("", "An error occurred while registering. Please try again later.");
                        }
                    }
                }
            }

            return View(businessOwner);
        }


        public IActionResult Intern()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Intern([Bind("FirstName, LastName, Email, Password, PhoneNumber, ProfileImage, Skills, Interests, Education, Location, Bio")] Intern intern)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(intern.Password);

                using (MySqlConnection connection = GetMySqlConnection()) // Use MySqlConnection
                {
                    connection.Open();
                    string query = "INSERT INTO Interns (FirstName, LastName, Email, Password, PhoneNumber, ProfileImage, Skills, Interests, Education, Location, Bio) " +
                                   "VALUES (@FirstName, @LastName, @Email, @Password, @PhoneNumber, @ProfileImage, @Skills, @Interests, @Education, @Location, @Bio)";
                    using (MySqlCommand command = new MySqlCommand(query, connection)) // Use MySqlCommand
                    {
                        command.Parameters.AddWithValue("@FirstName", intern.FirstName);
                        command.Parameters.AddWithValue("@LastName", intern.LastName);
                        command.Parameters.AddWithValue("@Email", intern.Email);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.Parameters.AddWithValue("@PhoneNumber", intern.PhoneNumber);
                        command.Parameters.AddWithValue("@ProfileImage", intern.ProfileImage);
                        command.Parameters.AddWithValue("@Skills", intern.Skills);
                        command.Parameters.AddWithValue("@Interests", intern.Interests);
                        command.Parameters.AddWithValue("@Education", intern.Education);
                        command.Parameters.AddWithValue("@Location", intern.Location);
                        command.Parameters.AddWithValue("@Bio", intern.Bio);

                        try
                        {
                            command.ExecuteNonQuery();
                            return RedirectToAction("Login", "Login");
                        }
                        catch (Exception ex)
                        {
                            // Log or print the exception message
                            Console.WriteLine(ex.Message);
                            // You can also add an error message to ModelState if needed
                            ModelState.AddModelError("", "An error occurred while registering. Please try again later.");
                        }
                    }
                }
            }

            return View(intern);
        }




    }
}