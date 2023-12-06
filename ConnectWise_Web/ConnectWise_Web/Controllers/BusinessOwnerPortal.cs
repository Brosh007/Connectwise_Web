using ConnectWise_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConnectWise_Web.Controllers
{
    public class BusinessOwnerPortal : Controller
    {
        private readonly Bologic _bologic;
         public BusinessOwnerPortal(Bologic bologic)
         {
             _bologic = bologic;
         }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Webinars()
        {
            List<Webinar> webinars = _bologic.GetWebinars();
            return View(webinars);
        }
        public IActionResult CreateWebinars()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateWebinars(Webinar webinar)
        {
            if (ModelState.IsValid)
            {
                _bologic.AddWebinar(webinar);
                return RedirectToAction("Webinars");
            }
            return View(webinar);
        }

        public IActionResult EditWebinars(int id)
        {
            Webinar webinar = _bologic.GetWebinarById(id);
            if (webinar == null)
            {
                return NotFound();
            }
            return View(webinar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditWebinars(int id, Webinar webinar)
        {
            if (ModelState.IsValid)
            {
                _bologic.UpdateWebinar(webinar);
                return RedirectToAction("Webinars");
            }
            return View(webinar);
        }

        public IActionResult Delete(int id)
        {
            Webinar webinar = _bologic.GetWebinarById(id);
            if (webinar == null)
            {
                return NotFound();
            }
            return View(webinar);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _bologic.DeleteWebinar(id);
            return RedirectToAction("Webinars");
        }

        public IActionResult SearchInterns(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return View(new List<Intern>());
            }

            List<Intern> interns = _bologic.GetInternsBySkillOrInterest(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View(interns);
        }

        // Display a list of available interns
        public IActionResult ListInterns()
        {
            var interns = _bologic.ListInterns();
            return View(interns);
        }
        // Display the meeting request form
        public IActionResult RequestMeeting(int internId)
        {
            ViewBag.InternId = internId; // Pass the internId to the view
            return View();
        }

        // Handle the meeting request form submission
        [HttpPost]
        public IActionResult RequestMeeting(int internId, DateTime meetingDateTime, string meetingPurpose)
        {
            try
            {
                var (businessOwnerId, _) = _bologic.GetLoggedInBusinessOwnerInfo(HttpContext);

                _bologic.CreateMeetingRequest(businessOwnerId, internId, meetingDateTime, meetingPurpose);

                return RedirectToAction("ListMeetings");
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it) and return an error view or redirect
                return View("Error", new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        // Display a list of all meetings for the logged-in business owner
        public IActionResult ListMeetings()
        {
            try
            {
                var (businessOwnerId, _) = _bologic.GetLoggedInBusinessOwnerInfo(HttpContext);

                var meetingRequests = _bologic.ListMeetingsForBusinessOwner(businessOwnerId);
                return View(meetingRequests);
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it) and return an error view or redirect
                return View("Error", new ErrorViewModel { ErrorMessage = ex.Message });
            }
        }








    }
}
