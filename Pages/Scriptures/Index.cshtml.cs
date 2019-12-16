using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScriptureJournal.Models;

namespace ScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        //private readonly ScriptureJournal.Models.ScriptureJournalContext _context;
        private readonly ScriptureJournal.Models.ScriptureJournalContext _context;
        public IndexModel(ScriptureJournal.Models.ScriptureJournalContext context)
        {
            _context = context;
        }

        //public IList<Journal> Book { get;set; }
        //public string SearchString { get; set; }
        //public SelectList Genres { get; set; }
        //public string BookGenre { get; set; }
       
        //public IList<Journal> Journal { get; set; }
        public PaginatedList<Journal> Journal { get; set; }
        public string SearchString { get; set; }
        public string KeySearchString { get; set; }
        public SelectList Book { get; set; }
        public string Books { get; set; }
        public string CurrentSort { get;  set; }

        //Add sorting
        public string BookSort { get; set; }
        public string DateSort { get; set; }
        //public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string Books, string keySearchString, string searchString, string sortOrder, int? pageIndex)


        {
            CurrentSort = sortOrder;
            BookSort = String.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            //Added for pages
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = SearchString;
            }
            // Use LINQ to get list of books.
            //IQueryable<string> bookQuery = from m in _context.Book

            //                                select m.Book;
            IQueryable<Journal> bookQuery = from s in _context.Book
                                            select s;
            //var Scriptures = from m in _context.Book
            //             select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookQuery = bookQuery.Where(s => s.Book.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(keySearchString))
            {
                bookQuery = bookQuery.Where(x => x.Note.Contains(keySearchString));
            }
            switch (sortOrder)
            {
                case "book_desc":
                    bookQuery = bookQuery.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    bookQuery = bookQuery.OrderBy(s => s.DateAdded);
                    break;

                default:
                    bookQuery = bookQuery.OrderBy(s => s.Book);
                    break;
            }

            
            //Journal = await bookQuery.AsNoTracking().ToListAsync();
            
            SearchString = searchString;
            int pageSize = 5;
            Journal = await PaginatedList<Journal>.CreateAsync(bookQuery.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
        //public async Task OnGetAsync(string sortOrder)
        //{
        //    BookSort = String.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
        //    DateSort = sortOrder == "Date" ? "date_desc" : "Date";

        //    IQueryable<Journal> bookOrder = from s in _context.Book
        //                                    select s;

        //    switch (sortOrder)
        //    {
        //        case "book_desc":
        //            bookOrder = bookOrder.OrderByDescending(s => s.Book);
        //            break;
        //        case "Date":
        //            bookOrder = bookOrder.OrderBy(s => s.DateAdded);
        //            break;

        //        default:
        //            bookOrder = bookOrder.OrderBy(s => s.Book);
        //            break;
        //    }

        //    Journal = await bookOrder.AsNoTracking().ToListAsync();
        //}
    }
}
