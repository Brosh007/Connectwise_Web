using ConnectWise_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConnectWise_Web.Controllers
{
    public class InternPortal : Controller
    {
        private readonly INLogic _inLogic;

        public InternPortal(INLogic inLogic)
        {
            _inLogic = inLogic;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult WebinarList()
        {
            List<Webinar> webinars = _inLogic.GetWebinars();
            return View(webinars);
        }

        public IActionResult SignUpForWebinar(int webinarId)
        {
            int internId = GetCurrentInternId(); //  implement this method
            bool signUpSuccess = _inLogic.SignUpForWebinar(internId, webinarId);

            if (signUpSuccess)
            {
                TempData["Message"] = "Successfully signed up for the webinar.";
            }
            else
            {
                TempData["Message"] = "You are already signed up for this webinar.";
            }

            return RedirectToAction("WebinarList");
        }
        private int GetCurrentInternId()
        {
            int? internId = HttpContext.Session.GetInt32("UserID");
            if (internId.HasValue)
            {
                return internId.Value;
            }
            else
            {
                // Handle the case when the intern ID is not found in the session
                // You might want to redirect the user to log in or handle it according to your application's needs
                throw new Exception("Intern ID not found in session.");
            }
        }

        public IActionResult CompanySearch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // Handle the case where no search term is provided (e.g., display a message or redirect)
                return View(new List<BusinessOwner>()); // Return an empty list to the view
            }

            List<BusinessOwner> searchResults = _inLogic.SearchCompanies(searchTerm);
            return View(searchResults);
        }

        public IActionResult MeetingRequests()
        {
            int internId = GetCurrentInternId(); // Implement this method to get the current intern's ID
            List<MeetingRequest> meetingRequests = _inLogic.GetMeetingRequestsForIntern(internId);

            return View(meetingRequests);
        }

        public IActionResult AcceptMeeting(int meetingRequestId)
        {
            bool accepted = _inLogic.AcceptMeetingRequest(meetingRequestId);

            if (accepted)
            {
                TempData["Message"] = "Meeting request accepted successfully.";
            }
            else
            {
                TempData["Message"] = "Failed to accept the meeting request.";
            }

            return RedirectToAction("MeetingRequests");
        }

        public IActionResult DeclineMeeting(int meetingRequestId)
        {
            bool declined = _inLogic.DeclineMeetingRequest(meetingRequestId);

            if (declined)
            {
                TempData["Message"] = "Meeting request declined successfully.";
            }
            else
            {
                TempData["Message"] = "Failed to decline the meeting request.";
            }

            return RedirectToAction("MeetingRequests");
        }









    }
}
