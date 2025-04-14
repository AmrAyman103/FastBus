using FastBusMVC.Models;
using Microsoft.AspNetCore.Mvc;

using FastBusMVC.ViewModel;
using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;


namespace FastBusMVC.Controllers.Account
{
    public class Account : Controller
    {
        final_DesignContext db=new final_DesignContext();
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Account(IWebHostEnvironment webHostEnvironment)
        { 
            _webHostEnvironment=webHostEnvironment;

        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.JsonResult CheckPhoneUnique(string PhoneNumber)
        {
            var CheckPhoneUnique = db.Users.FirstOrDefault(u => u.Phone == PhoneNumber);
            if (CheckPhoneUnique == null)
                return Json(true);
            return Json(false);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.JsonResult CheckUserNameUnique(string UserName)
        {
            var isUserNameUnique = db.Users.FirstOrDefault(u=>u.Username == UserName);
            if (isUserNameUnique == null)
                return Json(true);
            return Json(false);
        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.JsonResult CheckEmailUnique(string Email)
        {
            var isEmailUnique = db.Users.FirstOrDefault(u => u.Email == Email);
            if (isEmailUnique == null)
                return Json(true);
            return Json(false);
        }


        //login
        [HttpGet]
        public IActionResult login()
        { 
            
            return View(new LoginUserViewModel()); 
        
        
        }

        [HttpPost]
        public IActionResult Login(LoginUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                // Hash the input password to compare with stored hash
               /* var hashedPassword = HashPassword(user.Password);*/ // Implement this function
                Admin admin= new Admin();   

                
                var user1 = db.Users
                    .FirstOrDefault(u => u.Username == user.UserName && u.PassWord==user.Password );

                var adm = db.Admins
                    .FirstOrDefault(a=> a.Username == user.UserName && a.PassWord == user.Password);


                if (user1 != null)
                {
                    user1.Username = user.UserName;
                    user1.PassWord = user.Password;
                    HttpContext.Session.SetString("username", user1.Username);
                    HttpContext.Session.SetString("image", user1.UImage.ToString());

                    if (user.RememberMe)
                    {
                        // Create a new cookie
                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(7),
                            HttpOnly = true
                        };

                        Response.Cookies.Append("UserCookie", user1.UId.ToString(), cookieOptions);
                    }

                    // Redirect to the desired page after successful login
                    return RedirectToAction("BookingTrip", "Booking", new { userId = user1.UId });
                }
                else if(adm!=null)
                {
                    return RedirectToAction("Administration", "Admin");

                }
                
                else
                {
                    ModelState.AddModelError("", "The UserName or Password are wrong.");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(user);
            }
        }


        [HttpGet]
        public IActionResult register() 
        
        {
           return View(new RegisterUserViewModel());
        }

        [HttpPost]
        
        public async Task<IActionResult> register(RegisterUserViewModel newUser,IFormFile image)
        {
            
            if (ModelState.IsValid )
            {
                // Generate a unique filename based on the person's ID
                string fileName = newUser.FirstName.ToString() + Path.GetExtension(newUser.image.FileName);

                // Set the image path as a combination of a directory and the filename
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images/EndUser", fileName);

                // Save the image to the specified path
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await newUser.image.CopyToAsync(stream);
                }

                //Mapping from ViewModel to Model
                User user = new User();

                user.FirstName = newUser.FirstName;
                user.LastName = newUser.LastName;
                user.Email = newUser.Email;
                user.Username = newUser.UserName;
                user.Gendr = newUser.Gender.ToString();
                user.PassWord = newUser.Password;
                user.BathDate = newUser.DateOfBirth;
                user.UImage = fileName;
                user.Phone = newUser.PhoneNumber;

                    db.Users.Add(user);
                    db.SaveChanges();
                    return View("login");
            }
            return View("register",newUser);
        }


        [HttpGet]
        public IActionResult Profile()
        {

            var username = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(username))
            {

                var user =db.Users.Where(t=>t.Username == username).FirstOrDefault();
                
                ViewData["username"] = username;
                ViewData["firstname"] = user.FirstName;
                ViewData["lastname"]=user.LastName;
                ViewData["userid"] = user.UId;
                ViewData["email"]=user.Email;
                int uid = user.UId;
                var booking= db.Bookings.Where(t => t.UId == uid).FirstOrDefault();
                if (booking != null)
                {
                    ViewData["Bid"] = booking.BookId;
                    ViewData["NumberTickets"] = booking.Ticket;

                    var trip = (from b in db.Bookings
                                 join t in db.Trips on b.TripId equals t.TripId
                                 select t).Distinct().ToList();
                    

                    var viewModel = new AvailableTripsViewModel
                    {
                        Trips = trip
                    };
                    return View(viewModel);
                }
                else
                { return View(); }
                
                
                
                


            }


             return View(); 



        }


        [HttpPost]
        public IActionResult Profile(IFormCollection req)
        {



            return View();


        }


    }
}

