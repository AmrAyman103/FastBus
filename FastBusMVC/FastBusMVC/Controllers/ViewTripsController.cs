using FastBusMVC.Models;
using FastBusMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FastBusMVC.Controllers
{
    public class ViewTripsController : Controller
    {
        final_DesignContext db= new final_DesignContext();


        [HttpGet]
        public IActionResult TravelInterfaces()
        {
            var username = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(username))
            {
                //var image=db.Users.FirstOrDefault(u => u.Username == username);

                // string imageUser = image.UImage;
                // ViewBag.Username = username;
                // 
                var trips = db.Trips.ToList();
                var viewModel = new AvailableTripsViewModel
                {
                    Trips = trips
                };

                return View(viewModel);
            }
            else
            { return RedirectToAction("Index","Home"); }
        }
    }
}
