using FastBusMVC.Models;
using FastBusMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FastBusMVC.Controllers
{
    public class HomeController : Controller
    {
       final_DesignContext db=new final_DesignContext();



        private void FillDropdownLists(ViewModelTrip viewModelTrip)
        {
            viewModelTrip.DeparureCitys = db.Trips.Select(t => t.DeparureCity)
                .Distinct().Select(city => new SelectListItem { Value = city, Text = city }).ToList();

            viewModelTrip.ArrivalCitys = db.Trips.Select(t => t.ArrivalCity)
                .Distinct().Select(city => new SelectListItem { Value = city, Text = city }).ToList();
        }


            [HttpGet]
        public IActionResult Index()
        {


            TempData["Departure"] = "DeparureCitys";
            TempData["Arrival"] = "ArrivalCitys";
            TempData["date"] = DateTime.Now;

            // إنشاء ViewModelTrip
            ViewModelTrip viewModelTrip = new ViewModelTrip();

            // ملء البيانات للقوائم المنسدلة
            FillDropdownLists(viewModelTrip);

            return View(viewModelTrip);

        }

        [HttpPost]
        public IActionResult Index(IFormCollection req, ViewModelTrip viewModelTrip)
        {


            var username = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(username))
            {
               
                FillDropdownLists(viewModelTrip);

             
                TempData["Departure"] = req["DeparureCitys"];
                TempData["Arrival"] = req["ArrivalCitys"];
                TempData["date"] = req["date"];


             
                var departure = TempData["Departure"]?.ToString();
                var destination = TempData["Arrival"]?.ToString();
                var date = TempData["date"]?.ToString();

                if (!string.IsNullOrEmpty(departure) && !string.IsNullOrEmpty(destination))
                {
                    var trip = db.Trips.FirstOrDefault
                   (t => t.DeparureCity == departure && t.ArrivalCity == destination);

                    if (trip != null)
                    {
                        // القيم متطابقة
                        return RedirectToAction("AvailableTrips", "Booking");
                    }
                    else
                    {


                        return RedirectToAction("Unavailable", "Booking");


                    }
                }
                else
                {
                    // عرض رسالة خطأ أو صفحة في حال كانت القيم المطلوبة غير متوفرة
                    return BadRequest("Invalid input parameters.");
                }
            }
            else
                return RedirectToAction("login", "Account");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
