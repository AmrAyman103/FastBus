using Microsoft.AspNetCore.Mvc;
using FastBusMVC.ViewModel;
using FastBusMVC.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.Intrinsics.X86;
namespace FastBusMVC.Controllers
{
    public class BookingController : Controller
    {



        final_DesignContext db = new final_DesignContext();
        [HttpGet]
        public IActionResult PrivateTrip()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PrivateTrip(IFormCollection req)
        {

            SpBooking spBooking = new SpBooking();

            var username = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(username))
            {
                var idU = db.Users.Where(u => u.Username == username).FirstOrDefault();
                //int idu =Convert.ToInt32(idU.UId);

                spBooking.UId = idU.UId;
                spBooking.RequiredSeats = req["RequiredSeates"];
                spBooking.BookingDateTime = DateTime.Parse(req["DateTime"]);
                spBooking.Perpose = req["Prepose"];
                if (spBooking != null)
                {
                    db.SpBookings.Add(spBooking);
                    db.SaveChanges();


                    return View();
                }
                return View();

            }
            else { return RedirectToAction("login","Account"); }
            
        
        }

        private void FillDropdownLists(ViewModelTrip viewModelTrip)
        {
          
               viewModelTrip.DeparureCitys = db.Trips.Select(t => t.DeparureCity)
                .Distinct()  .Select(city => new SelectListItem { Value = city, Text = city }).ToList();

            viewModelTrip.ArrivalCitys = db.Trips.Select(t => t.ArrivalCity)
                .Distinct().Select(city => new SelectListItem { Value = city, Text = city }).ToList();

        }

        [HttpGet]
        public IActionResult BookingTrip(int userId)
        {
            var image = HttpContext.Session.GetString("image");
            var username = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(username))
            {

                TempData["Departure"] = "DeparureCitys";
                TempData["Arrival"] = "ArrivalCitys";
                TempData["date"] = DateTime.Now;

              
                ViewModelTrip viewModelTrip = new ViewModelTrip();

              
                FillDropdownLists(viewModelTrip);

                ViewData["image"] = image;
                ViewBag.username = username;
                return View(viewModelTrip);

            }
            else
                return RedirectToAction("login", "Account");
        }

        [HttpPost]
        public IActionResult BookingTrip(IFormCollection req, ViewModelTrip viewModelTrip)
        {

            var username = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(username))
            {

                
                FillDropdownLists(viewModelTrip);

               
                TempData["Departure"] = req["DeparureCitys"];
                TempData["Arrival"] = req["ArrivalCitys"];
                TempData["date"] = req["departureDateTime"];


                var departure = TempData["Departure"]?.ToString();
                var destination = TempData["Arrival"]?.ToString();
                var departureTime = TempData["date"]?.ToString();

                if (!string.IsNullOrEmpty(departure) && !string.IsNullOrEmpty(destination) && !string.IsNullOrEmpty(departureTime))
                {
                    if (DateTime.TryParse(departureTime, out DateTime DTime))
                    {

                        var tripIds = db.Trips
                          .Where(t => t.DeparureCity == departure && t.ArrivalCity == destination && t.DeparureDataTime.Date == DTime.Date)
                           .Select(t => t.TripId) // استخراج معرفات الرحلات
                                      .FirstOrDefault();




                        if (tripIds != null)
                        {
                            TempData["id"] = tripIds;
                            
                            return RedirectToAction("AvailableTrips", "Booking");
                        }
                        else
                        {


                            return RedirectToAction("Unavailable", "Booking");


                        }
                    }
                    else { return BadRequest("Invalid input parameters."); }

                }
                else
                {
                   
                    return BadRequest("Invalid input parameters.");
                }
            


            }
            else
                return RedirectToAction("login", "Account");
        }


        public IActionResult Unavailable()
        {

            return View();

        }






        //AvailableTrips

        [HttpGet]
        public IActionResult AvailableTrips()
        {

            var username = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(username))
            {

                ViewModelTrip trip1 = new ViewModelTrip();

                var departure = TempData.Peek("Departure")?.ToString();
                var destination = TempData.Peek("Arrival")?.ToString();
                var departureTime = TempData.Peek("date")?.ToString();
                int? id = TempData["id"] as int?;



                if (id.HasValue)
                {
                    int tripId = id.Value;
                    ViewData["tripid"] = tripId;
                    // استرجاع الرحلة من قاعدة البيانات باستخدام المعرف
                    var trip = db.Trips.FirstOrDefault(t => t.TripId == tripId);

                    if (trip != null)
                    {
                        // وضع الرحلة في ViewData لتمريرها إلى العرض
                        ViewData["departure"] = trip.DeparureCity;
                        ViewData["destination"] = trip.ArrivalCity;
                        ViewData["DepartureDate"] = trip.DeparureDataTime;
                        ViewData["ArrivalDate"] = trip.ArrivalDateTime;
                        ViewData["price"] = trip.Price;

                        return View();
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Trip not found.";
                    }


                }
                return BadRequest("Invalid input parameters.");
            }
            else
            { return RedirectToAction("login", "Home"); }
        }

        [HttpPost]
        public IActionResult AvailableTrips(IFormCollection req, AvailableTripsViewModel avTripVM)
        {

            //TempData["Cdeparure"] = req["DeparureCity"];
            //TempData["Cdestination"] = req["Destination"];
            //string departureDate = req["DepartureDate"];
            //string arrivalDate = req["ArrivalDate"];
            //TempData["CticketCount"] = (req["ticketCount"]);
            //TempData["CtotalPrice"] = (req["totalPrice"]);

            int tripId = avTripVM.TripIdVM;
            int ticketCount = avTripVM.ticketCountMV;
            int totalPrice = avTripVM.totalPriceMV;
            TempData["TotalPrice"] = req["price"];
            //int p = int.Parse(req["price"]);

            return RedirectToAction("BookingConfirmation", "Booking", new { CId = tripId, ticketCount = ticketCount, totalPrice = totalPrice });
        }




        [HttpGet]
        public IActionResult BookingConfirmation(int CId, int ticketCount, int totalPrice)
        {

            var username = HttpContext.Session.GetString("username");
            if (username != null)
            {

                AvailableTripsViewModel VM = new AvailableTripsViewModel();


                var trip = db.Trips.FirstOrDefault(t => t.TripId == CId);

                if (trip != null)

                {
                    // وضع الرحلة في ViewData لتمريرها إلى العرض
                    ViewData["departure"] =  trip.DeparureCity;
                    ViewData["destination"] = trip.ArrivalCity;
                    ViewData["DepartureDate"] = trip.DeparureDataTime;
                    ViewData["ArrivalDate"] = trip.ArrivalDateTime;
                    ViewData["ticketCount"] = ticketCount;
                   
                   TempData["IDTRIP"]=trip.TripId;
                   totalPrice = trip.Price * ticketCount;
                    ViewData["totalPrice"] = totalPrice;

                    return View();
                }

                return View(trip);


            }
            else { return RedirectToAction("Index", "Home"); }

        }

        [HttpPost]
        public IActionResult BookingConfirmation(AvailableTripsViewModel BookingVM,IFormCollection req)
        {
            var username = HttpContext.Session.GetString("username");
            if (username != null)
            {
                var userId = db.Users
                        .Where(u => u.Username == username)
                         .Select(u => u.UId) // استخراج معرف المستخدم
                           .FirstOrDefault();

               
               int id = Convert.ToInt32(TempData["IDTRIP"]);
              

                   Booking booking = new Booking();

               
                booking.UId = userId;
                booking.TripId = id;
               booking.Ticket = BookingVM.ticketCountMV.ToString();
               booking.BookingDateTime = DateTime.Now;
                if (booking != null)
                {
                   
                  
                        // Add the booking to the database and save changes
                        db.Bookings.Add(booking);
                        db.SaveChanges();

                        // Return the booking confirmation view
                        return View("Ticket", "Booking");
                   
                   
                }
                else
                {
                    // If the booking object is null, return a default view
                    return View();
                }
            }
            else { return RedirectToAction("Index", "Home"); }
        }
    }
}

        
