using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using ConnectWise_Web.Models;
using System.Data.SqlClient;

namespace ConnectWise_Web.Models
{
    public class INLogic
    {
        private readonly string _connectionString;

        public INLogic(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Webinar> GetWebinars()
        {
            List<Webinar> webinars = new List<Webinar>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Webinars";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var webinar = new Webinar
                            {
                                WebinarID = (int)reader["WebinarID"],
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                DateAndTime = (DateTime)reader["DateAndTime"],
                                Location = reader["Location"].ToString(),
                                SpeakerName = reader["SpeakerName"].ToString(),
                                SpeakerBio = reader["SpeakerBio"].ToString()
                            };

                            webinars.Add(webinar);
                        }
                    }
                }
            }

            return webinars;
        }
        public bool SignUpForWebinar(int internId, int webinarId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM UserWebinars WHERE InternID = @InternID AND WebinarID = @WebinarID";
                using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@InternID", internId);
                    checkCommand.Parameters.AddWithValue("@WebinarID", webinarId);

                    long count = (long)checkCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        // Intern is already signed up
                        return false;
                    }
                }

                string insertQuery = "INSERT INTO UserWebinars (InternID, WebinarID) VALUES (@InternID, @WebinarID)";
                using (var insertCommand = new MySqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@InternID", internId);
                    insertCommand.Parameters.AddWithValue("@WebinarID", webinarId);
                    insertCommand.ExecuteNonQuery();
                }

                return true;
            }
        }

        public List<BusinessOwner> SearchCompanies(string searchTerm)
        {
            List<BusinessOwner> companies = new List<BusinessOwner>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM BusinessOwners WHERE CompanyName LIKE @SearchTerm OR Location LIKE @SearchTerm OR Industry LIKE @SearchTerm OR Bio LIKE @SearchTerm";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var company = new BusinessOwner
                            {
                                CompanyName = reader["CompanyName"].ToString(),
                                Location = reader["Location"].ToString(),
                                Industry = reader["Industry"].ToString(),
                                Bio = reader["Bio"].ToString()
                            };

                            companies.Add(company);
                        }
                    }
                }
            }

            return companies;
        }

        public List<MeetingRequest> GetMeetingRequestsForIntern(int internId)
        {
            List<MeetingRequest> meetingRequests = new List<MeetingRequest>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Define SQL query to select meeting requests for a specific intern
                string query = "SELECT M.*, B.CompanyName AS BusinessOwnerCompanyName " +
                               "FROM meetingrequests M " +
                               "INNER JOIN BusinessOwners B ON M.BusinessOwnerID = B.BusinessOwnerID " +
                               "WHERE M.InternID = @InternId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InternId", internId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var meetingRequest = new MeetingRequest
                            {
                                MeetingRequestID = (int)reader["MeetingRequestID"],
                                BusinessOwnerID = (int)reader["BusinessOwnerID"],
                                InternID = (int)reader["InternID"],
                                MeetingDateTime = (DateTime)reader["MeetingDateTime"],
                                MeetingPurpose = reader["MeetingPurpose"].ToString(),
                                Status = reader["Status"].ToString(),
                                BusinessOwnerCompanyName = reader["BusinessOwnerCompanyName"].ToString()
                            };

                            meetingRequests.Add(meetingRequest);
                        }
                    }
                }
            }

            return meetingRequests;
        }

        public bool AcceptMeetingRequest(int meetingRequestId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Define SQL query to update the status to "Accepted"
                string updateQuery = "UPDATE meetingrequests SET Status = 'Accepted' WHERE MeetingRequestID = @MeetingRequestId";

                using (var command = new MySqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@MeetingRequestId", meetingRequestId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool DeclineMeetingRequest(int meetingRequestId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Define SQL query to update the status to "Declined"
                string updateQuery = "UPDATE meetingrequests SET Status = 'Declined' WHERE MeetingRequestID = @MeetingRequestId";

                using (var command = new MySqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@MeetingRequestId", meetingRequestId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }












    }
}






