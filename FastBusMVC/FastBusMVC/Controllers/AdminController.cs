using FastBusMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FastBusMVC.Controllers
{
    public class AdminController : Controller
    {
        final_DesignContext db=new final_DesignContext();
        public IActionResult Administration()
        {

            return View();
        }

        [HttpGet]
        public IActionResult AddBus()
        {


            return View();
        }

        [HttpPost]
        public IActionResult AddBus(IFormCollection req)
        {
            TripBu b = new TripBu();

            b.TripId = Convert.ToInt32(req["tripID"]);
            b.SeatNumber = Convert.ToInt32(req["seatNumber"]);
            b.Code = Convert.ToInt32(req["code"]);
            b.BusId = Convert.ToInt32(req["busID"]);
            if(b!=null)
            {
                db.TripBus.Add(b);
                db.SaveChanges();
                return RedirectToAction("Administration", "Admin");
            }
            return View();
        }


    }
}
