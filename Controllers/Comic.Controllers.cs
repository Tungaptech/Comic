using Comic.Data;
using Comic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Comic.Controllers
{
    public class ComicBookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComicBookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComicBook - Hiển thị danh sách
        public IActionResult Index()
        {
            var comicBooks = _context.ComicBooks.ToList();
            return View(comicBooks);
        }

        // GET: ComicBook/Create - Form thêm mới sách
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComicBook/Create - Xử lý thêm mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                _context.ComicBooks.Add(comicBook);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // GET: ComicBook/Edit/5 - Form chỉnh sửa
        public IActionResult Edit(int id)
        {
            var comicBook = _context.ComicBooks.Find(id);
            if (comicBook == null)
            {
                return NotFound();
            }
            return View(comicBook);
        }

        // POST: ComicBook/Edit/5 - Xử lý chỉnh sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ComicBook comicBook)
        {
            if (id != comicBook.ComicBookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.ComicBooks.Update(comicBook);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // GET: ComicBook/Delete/5 - Xác nhận xóa
        public IActionResult Delete(int id)
        {
            var comicBook = _context.ComicBooks.Find(id);
            if (comicBook == null)
            {
                return NotFound();
            }
            return View(comicBook);
        }

        // POST: ComicBook/Delete/5 - Xử lý xóa
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var comicBook = _context.ComicBooks.Find(id);
            if (comicBook != null)
            {
                _context.ComicBooks.Remove(comicBook);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
