using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using ConnectWise_Web.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Humanizer;
using System.Data.Common;

namespace ConnectWise_Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MySqlConnection GetSqlConnection()
        {
            return new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (MySqlConnection connection = GetSqlConnection())
                {
                    await connection.OpenAsync();

                    // Check if the provided credentials match a Business Owner
                    string businessOwnerQuery = "SELECT BusinessOwnerID, Password, FirstName, CompanyName, PhoneNumber, Email FROM BusinessOwners WHERE Email = @Email";
                    using (MySqlCommand businessOwnerCommand = new MySqlCommand(businessOwnerQuery, connection))
                    {
                        businessOwnerCommand.Parameters.AddWithValue("@Email", model.Email);
                        using (DbDataReader businessOwnerReader = await businessOwnerCommand.ExecuteReaderAsync())
                        {
                            if (businessOwnerReader.Read() && BCrypt.Net.BCrypt.Verify(model.Password, businessOwnerReader["Password"].ToString()))
                            {
                                // Set session variables for a logged-in Business Owner
                                SetSessionVariables(
                                    Convert.ToInt32(businessOwnerReader["BusinessOwnerID"]),
                                    "BusinessOwner",
                                    businessOwnerReader["CompanyName"].ToString() // Set the CompanyName
                                );

                                return RedirectToAction("Index", "BusinessOwnerPortal");
                            }
                        }
                    }

                    // Check if the provided credentials match an Intern
                    string internQuery = "SELECT InternID, FirstName, LastName, Password FROM Interns WHERE Email = @Email";
                    using (MySqlCommand internCommand = new MySqlCommand(internQuery, connection))
                    {
                        internCommand.Parameters.AddWithValue("@Email", model.Email);
                        using (DbDataReader internReader = await internCommand.ExecuteReaderAsync())
                        {
                            if (internReader.Read() && BCrypt.Net.BCrypt.Verify(model.Password, internReader["Password"].ToString()))
                            {
                                // Set session variables for a logged-in Intern
                                SetSessionVariables(
                                    Convert.ToInt32(internReader["InternID"]),
                                    "Intern",
                                    string.Empty // No CompanyName for Intern, set it as needed
                                );

                                return RedirectToAction("Index", "InternPortal");
                            }
                        }
                    }

                    // If no valid user is found, show an error message
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            // If ModelState is not valid or no valid user was found, return to the login view
            return View(model);
        }

        // Updated helper method to set session variables
        private void SetSessionVariables(int userId, string userType, string companyName)
        {
            HttpContext.Session.SetInt32("UserID", userId);
            HttpContext.Session.SetString("UserType", userType);
            HttpContext.Session.SetString("CompanyName", companyName); // Set CompanyName
        }
    }
}