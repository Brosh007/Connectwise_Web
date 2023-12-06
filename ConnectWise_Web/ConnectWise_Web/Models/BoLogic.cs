using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using ConnectWise_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConnectWise_Web
{
    public class Bologic
    {
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Bologic(string connectionString, IHttpContextAccessor httpContextAccessor)
        {
            _connectionString = connectionString;
            _httpContextAccessor = httpContextAccessor; 
        }

        public List<Webinar> GetWebinars()
        {
            List<Webinar> webinars = new List<Webinar>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM Webinars", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Webinar webinar = new Webinar(
                                (int)reader["WebinarID"],
                                (string)reader["Title"],
                                (string)reader["Description"],
                                (DateTime)reader["DateAndTime"],
                                (string)reader["Location"],
                                (string)reader["SpeakerName"],
                                (string)reader["SpeakerBio"]
                            );
                            webinars.Add(webinar);
                        }
                    }
                }
            }
            return webinars;
        }

        public void AddWebinar(Webinar webinar)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Webinars (Title, Description, DateAndTime, Location, SpeakerName, SpeakerBio) " +
                               "VALUES (@Title, @Description, @DateAndTime, @Location, @SpeakerName, @SpeakerBio)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", webinar.Title);
                    command.Parameters.AddWithValue("@Description", webinar.Description);
                    command.Parameters.AddWithValue("@DateAndTime", webinar.DateAndTime);
                    command.Parameters.AddWithValue("@Location", webinar.Location);
                    command.Parameters.AddWithValue("@SpeakerName", webinar.SpeakerName);
                    command.Parameters.AddWithValue("@SpeakerBio", webinar.SpeakerBio);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Webinar GetWebinarById(int webinarID)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Webinars WHERE WebinarID = @WebinarID";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@WebinarID", webinarID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Webinar(
                                (int)reader["WebinarID"],
                                (string)reader["Title"],
                                (string)reader["Description"],
                                (DateTime)reader["DateAndTime"],
                                (string)reader["Location"],
                                (string)reader["SpeakerName"],
                                (string)reader["SpeakerBio"]
                            );
                        }
                    }
                }
            }
            return null;
        }

        public void UpdateWebinar(Webinar webinar)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Webinars SET Title = @Title, Description = @Description, " +
                               "DateAndTime = @DateAndTime, Location = @Location, " +
                               "SpeakerName = @SpeakerName, SpeakerBio = @SpeakerBio " +
                               "WHERE WebinarID = @WebinarID";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@WebinarID", webinar.WebinarID);
                    command.Parameters.AddWithValue("@Title", webinar.Title);
                    command.Parameters.AddWithValue("@Description", webinar.Description);
                    command.Parameters.AddWithValue("@DateAndTime", webinar.DateAndTime);
                    command.Parameters.AddWithValue("@Location", webinar.Location);
                    command.Parameters.AddWithValue("@SpeakerName", webinar.SpeakerName);
                    command.Parameters.AddWithValue("@SpeakerBio", webinar.SpeakerBio);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteWebinar(int webinarID)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Webinars WHERE WebinarID = @WebinarID";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@WebinarID", webinarID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Intern> GetInternsBySkillOrInterest(string skillOrInterest)
        {
            List<Intern> interns = new List<Intern>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Interns WHERE Skills LIKE @SearchTerm OR Interests LIKE @SearchTerm";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", "%" + skillOrInterest + "%");

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Intern intern = new Intern
                            {
                                InternID = (int)reader["InternID"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                ProfileImage = reader["ProfileImage"].ToString(),
                                Skills = reader["Skills"].ToString(),
                                Interests = reader["Interests"].ToString(),
                                Education = reader["Education"].ToString(),
                                Location = reader["Location"].ToString(),
                                Bio = reader["Bio"].ToString()
                            };
                            interns.Add(intern);
                        }
                    }
                }
            }

            return interns;
        }

        public List<Intern> GetInternsFromDatabase()
        {
            List<Intern> interns = new List<Intern>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT InternID, FirstName, LastName, Email, PhoneNumber, ProfileImage, Skills, Interests, Education, Location, Bio FROM Interns";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Intern intern = new Intern
                            {
                                InternID = (int)reader["InternID"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                ProfileImage = reader["ProfileImage"].ToString(),
                                Skills = reader["Skills"].ToString(),
                                Interests = reader["Interests"].ToString(),
                                Education = reader["Education"].ToString(),
                                Location = reader["Location"].ToString(),
                                Bio = reader["Bio"].ToString()
                            };

                            interns.Add(intern);
                        }
                    }
                }
            }

            return interns;
        }

        public (int BusinessOwnerId, string CompanyName) GetLoggedInBusinessOwnerInfo(HttpContext httpContext)
        {
            int? userId = httpContext.Session.GetInt32("UserID");
            string companyName = httpContext.Session.GetString("CompanyName");

            if (userId.HasValue && !string.IsNullOrEmpty(companyName))
            {
                return (userId.Value, companyName);
            }
            else
            {
                throw new Exception("Business Owner ID or Company Name not found in session.");
            }
        }

        public List<Intern> ListInterns()
        {
            List<Intern> interns = GetInternsFromDatabase();
            return interns;
        }

        public void CreateMeetingRequest(int businessOwnerId, int internId, DateTime meetingDateTime, string meetingPurpose)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO MeetingRequests (BusinessOwnerID, InternID, MeetingDateTime, MeetingPurpose, Status) " +
                               "VALUES (@BusinessOwnerID, @InternID, @MeetingDateTime, @MeetingPurpose, 'Pending')";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BusinessOwnerID", businessOwnerId);
                    command.Parameters.AddWithValue("@InternID", internId);
                    command.Parameters.AddWithValue("@MeetingDateTime", meetingDateTime);
                    command.Parameters.AddWithValue("@MeetingPurpose", meetingPurpose);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<MeetingRequest> ListMeetingsForBusinessOwner(int businessOwnerId)
        {
            List<MeetingRequest> meetingRequests = new List<MeetingRequest>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT MR.MeetingRequestID, MR.MeetingDateTime, MR.MeetingPurpose, MR.Status, I.FirstName AS InternFirstName, I.LastName AS InternLastName " +
                               "FROM MeetingRequests MR " +
                               "INNER JOIN Interns I ON MR.InternID = I.InternID " +
                               "WHERE MR.BusinessOwnerID = @BusinessOwnerID";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BusinessOwnerID", businessOwnerId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MeetingRequest meetingRequest = new MeetingRequest
                            {
                                MeetingRequestID = Convert.ToInt32(reader["MeetingRequestID"]),
                                MeetingDateTime = Convert.ToDateTime(reader["MeetingDateTime"]),
                                MeetingPurpose = reader["MeetingPurpose"].ToString(),
                                Status = reader["Status"].ToString(),
                                // Add intern's name to the MeetingRequest
                                InternFirstName = reader["InternFirstName"].ToString(),
                                InternLastName = reader["InternLastName"].ToString()
                            };
                            meetingRequests.Add(meetingRequest);
                        }
                    }
                }
            }
            return meetingRequests;
        }








    }
}