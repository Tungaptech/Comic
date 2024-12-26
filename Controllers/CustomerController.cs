using Comic.Data;
using Comic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Comic.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customer/Index - Hiển thị danh sách khách hàng
        public IActionResult Index()
        {
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        // GET: Customer/Register - Form đăng ký
        public IActionResult Register()
        {
            return View();
        }

        // POST: Customer/Register - Xử lý đăng ký
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.RegisterDate = DateTime.Now; // Lấy ngày hiện tại
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
    }
}