using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyHomepage.Data;
using MyHomepage.Models;

namespace MyHomepage.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly MyHomepage.Data.MyHomepageContext _context;

        public IndexModel(MyHomepage.Data.MyHomepageContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Movie != null)
            {
                // Use LINQ to get list of genres.
                IQueryable<string> genreQuery = from m in _context.Movie
                                                orderby m.Genre
                                                select m.Genre;

                var movies = from m in _context.Movie
                             select m;

                if (!string.IsNullOrEmpty(SearchString))
                {
                    movies = movies.Where(s => s.Title.Contains(SearchString));
                }

                if (!string.IsNullOrEmpty(MovieGenre))
                {
                    movies = movies.Where(x => x.Genre == MovieGenre);
                }
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
                Movie = await movies.ToListAsync();
            }
        }
    }
}
