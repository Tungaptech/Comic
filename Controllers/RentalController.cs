using Comic.Data;
using Comic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Thêm để hỗ trợ Include

namespace Comic.Controllers
{
    public class RentalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rental/Create - Hiển thị form thuê sách
        public IActionResult Create()
        {
            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.ComicBooks = _context.ComicBooks.ToList();
            return View();
        }

        // POST: Rental/Create - Xử lý thuê sách
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rental rental, List<RentalDetail> rentalDetails)
        {
            if (ModelState.IsValid)
            {
                rental.RentalDate = DateTime.Now;
                rental.Status = "Đang thuê";

                _context.Rentals.Add(rental);
                _context.SaveChanges();

                foreach (var detail in rentalDetails)
                {
                    detail.RentalId = rental.RentalId;
                    _context.RentalDetails.Add(detail);
                }
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.ComicBooks = _context.ComicBooks.ToList();
            return View(rental);
        }

        // GET: Rental/Index - Hiển thị danh sách phiếu thuê
        public IActionResult Index()
        {
            var rentals = _context.Rentals.ToList();
            return View(rentals);
        }

        // GET: Rental/Report - Báo cáo danh sách thuê sách
        public IActionResult Report(DateTime? startDate, DateTime? endDate)
        {
            // Nếu không chọn ngày, mặc định lấy toàn bộ dữ liệu
            startDate ??= DateTime.MinValue;
            endDate ??= DateTime.MaxValue;

            var reportData = from rental in _context.Rentals
                join rentalDetail in _context.RentalDetails on rental.RentalId equals rentalDetail.RentalId
                join comicBook in _context.ComicBooks on rentalDetail.ComicBookId equals comicBook.ComicBookId
                join customer in _context.Customers on rental.CustomerId equals customer.CustomerId
                where rental.RentalDate >= startDate && rental.RentalDate <= endDate
                select new
                {
                    BookName = comicBook.Name,
                    RentalDate = rental.RentalDate,
                    ReturnDate = rental.ReturnDate,
                    CustomerName = customer.Fullname,
                    Quantity = rentalDetail.Quantity
                };

            ViewBag.StartDate = startDate.Value.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate.Value.ToString("yyyy-MM-dd");

            return View(reportData.ToList());
        }
    }
}
