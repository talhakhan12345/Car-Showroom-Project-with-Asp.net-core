using Car_web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Car_web.Controllers
{
    public class WebsiteController : Controller
    {
        private readonly dbcontext _mycon;
        private readonly IWebHostEnvironment _env;

        public WebsiteController(dbcontext mycon, IWebHostEnvironment env)
        {
            _mycon = mycon;
            _env = env;
        }

        public IActionResult Index()
        {
            var data = _mycon.tbl_addcar.ToList();
            return View(data);
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitRequest(TestDriveRequest thor, [FromServices] EmailService emailService)
        {
            if (ModelState.IsValid)
            {
                _mycon.tbl_testdrive.Add(thor);
                _mycon.SaveChanges();

                // 📧 Email
                string subject = "New Test Drive Request";
                string body = $@"
            <h2>Test Drive Request</h2>
            <p><strong>Full Name:</strong> {thor.FullName}</p>
            <p><strong>Email:</strong> {thor.Email}</p>
            <p><strong>Phone:</strong> {thor.PhoneNumber}</p>
            <p><strong>Preferred Date:</strong> {thor.PreferredDate}</p>
            <p><strong>Preferred Time:</strong> {thor.PreferredTime}</p>
            <p><strong>Car:</strong> {thor.CarName}</p>
            <p><strong>Stock #:</strong> {thor.StockNumber}</p>
        ";

                emailService.SendEmail(subject, body);

                TempData["TestDriveMessage"] = "Your test drive request has been submitted successfully!";
            }
            else
            {
                TempData["TestDriveMessage"] = "Something went wrong. Please try again.";
            }

            return RedirectToAction("cardetail", new { id = thor.StockNumber });
        }



        public IActionResult Pricing(string search, string year, string make, string model, string price)
        {
            var cars = _mycon.tbl_addcar.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                cars = cars.Where(c =>
                    (c.Brand != null && c.Brand.Contains(search)) ||
                    (c.Model != null && c.Model.Contains(search)) ||
                    (c.FuelType != null && c.FuelType.Contains(search)));
            }

            if (!string.IsNullOrWhiteSpace(year) && int.TryParse(year, out int y))
            {
                cars = cars.Where(c => c.Year == y);
            }

            if (!string.IsNullOrWhiteSpace(make))
            {
                cars = cars.Where(c => c.Brand == make);
            }

            if (!string.IsNullOrWhiteSpace(model))
            {
                cars = cars.Where(c => c.Model == model);
            }

            if (!string.IsNullOrWhiteSpace(price))
            {
                var parts = price.Split('-');
                if (parts.Length == 2 &&
                    decimal.TryParse(parts[0], out decimal min) &&
                    decimal.TryParse(parts[1], out decimal max))
                {
                    cars = cars.Where(c => c.Price >= min && c.Price <= max);
                }
                else if (price.Contains("+") && decimal.TryParse(price.Replace("+", ""), out decimal minOnly))
                {
                    cars = cars.Where(c => c.Price >= minOnly);
                }
            }

            return View(cars.ToList());
        }

        public IActionResult Cars(int? minYear, int? maxYear,
        string make, string model,
        string fuelType, string transmission,
        int? minPrice, int? maxPrice,
         int? minMileage, int? maxMileage)
        {
            var cars = _mycon.tbl_addcar.AsQueryable();

            // ✅ Validation Messages
            if (minYear.HasValue && maxYear.HasValue && minYear > maxYear)
                ViewBag.YearError = "Min Year should be less than or equal to Max Year.";

            if (minPrice.HasValue && maxPrice.HasValue && minPrice > maxPrice)
                ViewBag.PriceError = "Min Price should be less than or equal to Max Price.";

            if (minMileage.HasValue && maxMileage.HasValue && minMileage > maxMileage)
                ViewBag.MileageError = "Min Mileage should be less than or equal to Max Mileage.";

            // ✅ Filtering Logic
            if (minYear.HasValue)
                cars = cars.Where(c => c.Year >= minYear);

            if (maxYear.HasValue)
                cars = cars.Where(c => c.Year <= maxYear);

            if (!string.IsNullOrEmpty(make))
                cars = cars.Where(c => c.Brand != null && c.Brand == make);

            if (!string.IsNullOrEmpty(model))
                cars = cars.Where(c => c.Model != null && c.Model == model);

            if (!string.IsNullOrEmpty(fuelType))
                cars = cars.Where(c => c.FuelType != null && c.FuelType == fuelType);

            if (!string.IsNullOrEmpty(transmission))
                cars = cars.Where(c => c.Transmission != null && c.Transmission == transmission);

            if (minPrice.HasValue)
                cars = cars.Where(c => c.Price >= minPrice);

            if (maxPrice.HasValue)
                cars = cars.Where(c => c.Price <= maxPrice);

            if (minMileage.HasValue)
                cars = cars.Where(c => c.Mileage >= minMileage);

            if (maxMileage.HasValue)
                cars = cars.Where(c => c.Mileage <= maxMileage);

            // ✅ Final Car List
            var carList = cars.ToList();

            // ✅ Result Count Message
            ViewBag.ResultCount = carList.Count;

            // ✅ Populate Dropdowns
            ViewBag.Makes = _mycon.tbl_addcar
                .Where(c => c.Brand != null)
                .Select(c => c.Brand)
                .Distinct()
                .OrderBy(b => b)
                .ToList();

            ViewBag.Models = _mycon.tbl_addcar
                .Where(c => c.Model != null)
                .Select(c => c.Model)
                .Distinct()
                .OrderBy(m => m)
                .ToList();

            return View(carList);
        }

        public IActionResult cardetail(string id)
        {
            var tt = _mycon.tbl_addcar.FirstOrDefault(c => c.Stock_id == id);

            if (tt == null)
            {
                // 404 error ya kisi aur page pe redirect kar do
                return NotFound(); // ya RedirectToAction("Index")
            }

            return View(tt);
        }

        [HttpGet]
        public IActionResult Finance(string stockId, string brand, string model, int? year, decimal? price, string vin)
        {
            var cars = _mycon.tbl_addcar.ToList();
            ViewBag.Cars = cars;

            if (!string.IsNullOrEmpty(stockId) && !string.IsNullOrEmpty(brand))
            {
                var selectedCar = $"{year} {brand} {model} - VIN: {vin} - STOCK ID: {stockId} - ${price}";
                ViewBag.SelectedCar = selectedCar;
            }

            return View();
        }


        [HttpPost]
        public IActionResult finance(CreditApplication credit, [FromServices] EmailService emailService)
        {
            // Save to database
            credit.CreatedAt = DateTime.Now;
            _mycon.tbl_finance.Add(credit);
            _mycon.SaveChanges();

            // 📧 Build email content
            string subject = "New Finance Application Submission";
            string message = $@"
        <h2>Finance Application</h2>
        <p><strong>Name:</strong> {credit.FirstName} {credit.LastName}</p>
        <p><strong>Email:</strong> {credit.Email}</p>
        <p><strong>Phone:</strong> {credit.Phone}</p>
        <p><strong>Application Type:</strong> {credit.ApplicationType}</p>
        <p><strong>Vehicle:</strong> {credit.Year} {credit.Make} {credit.Model} (Stock: {credit.StockNumber})</p>
        <p><strong>Down Payment:</strong> ${credit.DownPayment}</p>
        <p><strong>Monthly Payment:</strong> ${credit.MonthlyPayment}</p>
        <p><strong>Amount to Finance:</strong> ${credit.AmountToFinance}</p>
        <p><strong>Trade-In:</strong> {(credit.HasTrade ? "Yes" : "No")}</p>
        <p><strong>Submitted At:</strong> {credit.CreatedAt?.ToString("f")}</p>
    ";

            // Send email
            emailService.SendEmail(subject, message);

            // Show confirmation
            TempData["Message"] = "Application submitted successfully!";
            ViewBag.Cars = _mycon.tbl_addcar.ToList();

            return View();
        }


        [HttpGet]
        public IActionResult Contact()
        {
            // Form ke liye model
            var contactModel = new contact_us();

            // Featured Cars data
            var featuredCars = _mycon.tbl_addcar.OrderByDescending(x => x.CarId).Take(6).ToList();

            // Send featured cars to view via ViewBag
            ViewBag.FeaturedCars = featuredCars;

            // Send contact model to view
            return View(contactModel);
        }

        [HttpPost]
        public IActionResult Contact(IFormCollection form, [FromServices] EmailService emailService)
        {
            var contact = new contact_us
            {
                Name = form["Name"],
                Email = form["Email"],
                Phone = form["Phone"],
                Message = form["Message"],
                PreferredReplyMethods = string.Join(", ", form["PreferredReplyMethods"]),
                SubmittedAt = DateTime.Now
            };

            _mycon.tbl_contact.Add(contact);
            _mycon.SaveChanges();

            // 📧 Email send logic
            string subject = "New Contact Us Submission";
            string message = $@"
    <h2>Contact Us Request</h2>
    <p><strong>Name:</strong> {contact.Name}</p>
    <p><strong>Email:</strong> {contact.Email}</p>
    <p><strong>Phone:</strong> {contact.Phone}</p>
    <p><strong>Preferred Contact:</strong> {contact.PreferredReplyMethods}</p>
    <p><strong>Message:</strong> {contact.Message}</p>
    ";

            emailService.SendEmail(subject, message);

            TempData["ContactMessage"] = "Thank you! Your message has been received.";
            return RedirectToAction("Contact");
        }

        public IActionResult Inventory(string search, string year, string make, string model, string price)
        {
            return RedirectToAction("Pricing", new { search, year, make, model, price });
        }

        public IActionResult ExportCsv()
        {
            var cars = _mycon.tbl_addcar.ToList();

            var sb = new StringBuilder();
            sb.AppendLine("CarId,Stock_id,Trim,Engine,Exterior_Color,Interior_Color,Seats,Brand,Model,Year,Price,Mileage,FuelType,Transmission,Description,Images");

            string siteUrl = "https://foxvalleyluxury.com/uploads/";

            foreach (var car in cars)
            {
                var imageList = new List<string>();

                if (!string.IsNullOrEmpty(car.ImagePath))
                {
                    var fileNames = car.ImagePath.Split(',');
                    foreach (var name in fileNames)
                    {
                        imageList.Add(siteUrl + name);
                    }
                }

                string imageString = string.Join(" | ", imageList);

                sb.AppendLine($"{car.CarId},{car.Stock_id},{car.Trim},{car.Engine},{car.Exteriro_Color},{car.Interior_Color},{car.Seats},{car.Brand},{car.Model},{car.Year},{car.Price},{car.Mileage},{car.FuelType},{car.Transmission},\"{car.Description}\",\"{imageString}\"");
            }

            byte[] buffer = Encoding.UTF8.GetBytes(sb.ToString());

            return File(buffer, "text/csv", "inventory_feed.csv");
        }

        [HttpPost]
        public IActionResult request(RequestMoreInfo Req, [FromServices] EmailService emailService)
        {
            // Bind checkboxes manually
            Req.PreferCall = Request.Form["PreferCall"] == "true";
            Req.PreferSMS = Request.Form["PreferSMS"] == "true";
            Req.PreferEmail = Request.Form["PreferEmail"] == "true";

            Req.SubmittedAt = DateTime.Now;

            _mycon.tbl_moreinfo.Add(Req);
            _mycon.SaveChanges();

            // Email subject and message
            string subject = "New Request More Info Submission";
            string message = $@"
        <h2>Request More Info</h2>
        <p><strong>Name:</strong> {Req.FirstName} {Req.LastName}</p>
        <p><strong>Email:</strong> {Req.Email}</p>
        <p><strong>Phone:</strong> {Req.Phone}</p>
        <p><strong>Preferred Contact:</strong>
            {(Req.PreferCall ? "Call " : "")}
            {(Req.PreferSMS ? "SMS " : "")}
            {(Req.PreferEmail ? "Email" : "")}
        </p>
        <p><strong>Message:</strong> {Req.Message}</p>
    ";

            // Synchronous email send
            emailService.SendEmail(subject, message);

            TempData["Message"] = "Your request has been submitted successfully.";
            return RedirectToAction("Finance", "Website");
        }



        [HttpPost]
        public IActionResult SubmitTradeIn(Request tdr, [FromServices] EmailService emailService)
        {
            try
            {
                // Checkbox manual binding
                tdr.ContactCall = Request.Form["ContactCall"] == "true";
                tdr.ContactSMS = Request.Form["ContactSMS"] == "true";
                tdr.ContactEmail = Request.Form["ContactEmail"] == "true";
                tdr.Consent = Request.Form["Consent"] == "true";

                // Save to DB
                _mycon.tbl_req.Add(tdr);
                _mycon.SaveChanges();

                // Email content
                string subject = "New Trade-In Request";
                string body = $@"
            <h2>Trade-In Request</h2>
            <p><strong>Vehicle Info:</strong> {tdr.VehicleInfo}</p>
            <p><strong>Mileage:</strong> {tdr.Mileage}</p>
            <p><strong>Name:</strong> {tdr.FirstName} {tdr.LastName}</p>
            <p><strong>Email:</strong> {tdr.Email}</p>
            <p><strong>Phone:</strong> {tdr.Phone}</p>
            <p><strong>Zip Code:</strong> {tdr.ZipCode}</p>
            <p><strong>Preferred Contact:</strong>
                {(tdr.ContactCall ? "Call " : "")}
                {(tdr.ContactSMS ? "SMS " : "")}
                {(tdr.ContactEmail ? "Email" : "")}
            </p>
            <p><strong>Consent Given:</strong> {(tdr.Consent ? "Yes" : "No")}</p>
        ";

                // Send Email
                emailService.SendEmail(subject, body);

                TempData["TradeInMessage"] = "Your request has been submitted!";
                return RedirectToAction("Index", "Website");
            }
            catch (Exception ex)
            {
                return Content($"❌ ERROR: {ex.Message}<br><br><pre>{ex.StackTrace}</pre>", "text/html");
            }
        }




    }
}
