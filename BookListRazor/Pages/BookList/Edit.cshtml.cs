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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task OnGet(int id)
        {
            Book = await _db.Books.Where(b=>b.Id==id).FirstOrDefaultAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Books.Update(Book);
            await _db.SaveChangesAsync();
            Message = "Edit successfully!";
            return RedirectToPage("Index");
        }
    }
}