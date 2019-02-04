using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task OnGet()
        {
            Books = await _db.Books.ToListAsync();
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            var book = await _db.Books.Where(b => b.Id == id).FirstOrDefaultAsync();
            if(book == null)
            {
                return NotFound();
            }
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            Message = "Delete successfully!";
            return RedirectToPage("Index");
        }
    }
}