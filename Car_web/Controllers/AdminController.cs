using Car_web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Car_web.Controllers
{
    public class AdminController : Controller
    {
        private readonly dbcontext _mycon;
        private readonly IWebHostEnvironment _env;

        public AdminController(dbcontext mycon, IWebHostEnvironment env)
        {
            _mycon = mycon;
            _env = env;
        }

        public IActionResult Index()
        {
            var adminSession = HttpContext.Session.GetString("adminSession");

            if (adminSession != null)
            {
                // Total counts
                var totalCars = _mycon.tbl_addcar.Count();
                var totalInquiries = _mycon.tbl_contact.Count();
                var totalFinance = _mycon.tbl_finance.Count();

                // Recent Messages from contact_us table (latest 5)
                var recentMessages = _mycon.tbl_contact
                    .OrderByDescending(x => x.SubmittedAt)
                    .Take(5)
                    .ToList();

                // Send to view
                ViewBag.TotalCars = totalCars;
                ViewBag.TotalInquiries = totalInquiries;
                ViewBag.TotalFinance = totalFinance;
                ViewBag.RecentMessages = recentMessages;

                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }


        public IActionResult Managecars()
        {
            var adminsession = HttpContext.Session.GetString("adminSession");

            if (adminsession != null)
            {
                var cars = _mycon.tbl_addcar.ToList();
                return View(cars);
            }
            else
            {
                return RedirectToAction("login");
            }
           
        }

        public IActionResult Edit(int id)
        {
            var adminsession = HttpContext.Session.GetString("adminSession");
            if (adminsession != null)
            {
                var car = _mycon.tbl_addcar.Find(id);
                if (car == null) return NotFound();
                return View(car);
            }
            else
            {
                return RedirectToAction("login");
            }
          
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Car car, List<IFormFile> newImages)
        {
            var existingCar = _mycon.tbl_addcar.Find(car.CarId);
            if (existingCar == null)
                return NotFound();

            // Update basic fields
            existingCar.Brand = car.Brand;
            existingCar.Model = car.Model;
            existingCar.Year = car.Year;
            existingCar.Price = car.Price;
            existingCar.Mileage = car.Mileage;
            existingCar.FuelType = car.FuelType;
            existingCar.Transmission = car.Transmission;
            existingCar.Seats = car.Seats;
            existingCar.Description = car.Description;

            // Handle image updates
            if (newImages != null && newImages.Count > 0)
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                List<string> imagePaths = new List<string>();

                foreach (var file in newImages)
                {
                    if (file.Length > 0)
                    {
                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(uploadsDir, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        imagePaths.Add(uniqueFileName);
                    }
                }

                // Overwrite existing images with new ones
                existingCar.ImagePath = string.Join(",", imagePaths);
            }

            _mycon.tbl_addcar.Update(existingCar);
            await _mycon.SaveChangesAsync();

            return RedirectToAction("ManageCars");
        }


        public IActionResult Delete(int id)
        {
            var car = _mycon.tbl_addcar.Find(id);
            if (car == null) return NotFound();

            _mycon.tbl_addcar.Remove(car);
            _mycon.SaveChanges();

            return RedirectToAction("ManageCars");
        }




        public IActionResult addcars()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addcars(Car card, List<IFormFile> ImagePath)
        {
            if (ImagePath != null && ImagePath.Count > 0)
            {
                List<string> fileNames = new List<string>();

                foreach (var file in ImagePath)
                {
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fs);
                    }

                    fileNames.Add(uniqueFileName); // Add to list
                }

                // Join all file names as a single string (comma-separated or whatever format you prefer)
                card.ImagePath = string.Join(",", fileNames);
            }

            _mycon.tbl_addcar.Add(card);
            _mycon.SaveChanges();
            return RedirectToAction("Managecars");
        }



        public IActionResult inquries()
        {
            var adminSession = HttpContext.Session.GetString("adminSession");
            if (adminSession != null)
            {
                var data = _mycon.tbl_contact.OrderByDescending(x => x.SubmittedAt).ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("login");
            }
          
        }

        public IActionResult ToggleRead(int id)
        {
            var inquiry = _mycon.tbl_contact.Find(id);
            if (inquiry != null)
            {
                inquiry.IsRead = !inquiry.IsRead;
                _mycon.SaveChanges();
            }
            return RedirectToAction("inquries");
        }

        // ✅ Delete
        public IActionResult DeleteInquiry(int id)
        {
            var inquiry = _mycon.tbl_contact.Find(id);
            if (inquiry != null)
            {
                _mycon.tbl_contact.Remove(inquiry);
                _mycon.SaveChanges();
            }
            return RedirectToAction("Inquiries");
        }


        public IActionResult finance()
        {
            var adminsession = HttpContext.Session.GetString("adminSession");
            if (adminsession != null)
            {
                var data = _mycon.tbl_finance.ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("login");
            }
          
        }

        [HttpPost]
        public IActionResult DeleteFinance(int id)
        {
            // 1. Database context
            var app = _mycon.tbl_finance.FirstOrDefault(x => x.Id == id);

            // 2. Null check
            if (app == null)
            {
                TempData["Error"] = "Application not found.";
                return RedirectToAction("finance"); // ya jis view par redirect karna hai
            }

            // 3. Remove and save
            _mycon.tbl_finance.Remove(app);
            _mycon.SaveChanges();

            // 4. Success message (optional)
            TempData["Success"] = "Application deleted successfully.";

            return RedirectToAction("finance"); // list view page
        }


        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string pwd)
        {
            // Check if email is empty
            if (string.IsNullOrWhiteSpace(email))
            {
                ViewBag.message = "Email is required.";
                return View();
            }

            // Check valid email format
            if (!new EmailAddressAttribute().IsValid(email))
            {
                ViewBag.message = "Invalid email format. Please enter a valid email.";
                return View();
            }

            // Check if it's specifically gmail
            if (!email.ToLower().EndsWith("@gmail.com"))
            {
                ViewBag.message = "Only Gmail addresses are allowed.";
                return View();
            }

            // Check credentials
            var row = _mycon.tbl_admin.FirstOrDefault(a => a.Admin_email == email);

            if (row != null && row.Admin_pwd == pwd)
            {
                HttpContext.Session.SetString("adminSession", row.Admin_id.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Incorrect email or password.";
                return View();
            }
        }

        // GET
        public IActionResult UpdateAdmin()
        {
            var adminsession = HttpContext.Session.GetString("adminSession");
            if (adminsession != null)
            {
                var adminid = HttpContext.Session.GetString("adminSession");
                var id = _mycon.tbl_admin.Where(a => a.Admin_id == int.Parse(adminid)).ToList();
                return View(id);
            }
            else
            {
                return RedirectToAction("login");
            }
              
        }

        [HttpPost]

        public IActionResult UpdateAdmin(admin ad)
        {

            _mycon.tbl_admin.Update(ad);
            _mycon.SaveChanges();

            TempData["msg"] = "Profile updated successfully!";

            return RedirectToAction("UpdateAdmin", new { id = ad.Admin_id });
        }

        public IActionResult logout()
        {
            HttpContext.Session.Remove("adminSession");
            return RedirectToAction("login");
        }
        public IActionResult MoreInfo()
        {
            var requests = _mycon.tbl_moreinfo.OrderByDescending(r => r.SubmittedAt).ToList();
            return View(requests);
        }
        [HttpPost]
        public IActionResult DeleteRequest(int id)
        {
            var request = _mycon.tbl_moreinfo.FirstOrDefault(r => r.Id == id);
            if (request != null)
            {
                _mycon.tbl_moreinfo.Remove(request);
                _mycon.SaveChanges();
            }
            return RedirectToAction("MoreInfo");
        }

        public IActionResult TradeInList()
        {
            var allRequests = _mycon.tbl_req.ToList(); 
            return View(allRequests);
        }

        [HttpPost]
        public IActionResult DeleteTradeIn(int id)
        {
            var req = _mycon.tbl_req.Find(id);
            if (req != null)
            {
                _mycon.tbl_req.Remove(req);
                _mycon.SaveChanges();
                TempData["Success"] = "Request deleted successfully.";
            }
            return RedirectToAction("TradeInList");
        }



    }
}
