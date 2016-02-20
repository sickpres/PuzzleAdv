using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using PuzzleAdv.Backend.Models;
using System.Security.Claims;
using PuzzleAdv.Backend.Helpers;
using Microsoft.AspNet.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using System.IO;
using PuzzleAdv.Backend.ViewModels.Puzzle;
using Microsoft.Data.Entity;


namespace PuzzleAdv.Backend.Controllers
{
    public class PuzzleController : Controller
    {
        private PuzzleAdvDbContext _context;
        private IHostingEnvironment _environment;

        public PuzzleController(PuzzleAdvDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Puzzle List
        public async Task<IActionResult> Index()
        {
            var puzzles = await _context.Puzzle.ToListAsync();

            var puzzleListVm = puzzles.Select<Puzzle, PuzzleListViewModel>(puzzle => puzzle);

            return View(puzzleListVm);
        }


        // GET: Campaigns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campaigns/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormFile croppedImage1, IFormFile croppedImage2, DateTime? startDate, int distance)
        {
            //Save image
            var uploads = Path.Combine(_environment.WebRootPath, "Uploads");
            if (croppedImage1.Length > 0 && croppedImage2.Length > 0)
            {
                var puzzleImageId = Guid.NewGuid().ToString();
                var fileName = string.Format("{0}.jpg", puzzleImageId);

                croppedImage1.SaveAs(Path.Combine(uploads, fileName));

                ImageResizer.ImageJob img1 = new ImageResizer.ImageJob(Path.Combine(uploads, fileName), Path.Combine(uploads, "16-9", "400", fileName),
                                new ImageResizer.Instructions("maxheight=400;&scale=both;format=jpg;mode=max"));

                img1.Build();

                ImageResizer.ImageJob img2 = new ImageResizer.ImageJob(Path.Combine(uploads, fileName), Path.Combine(uploads, "16-9", "1065", fileName), 
                                                new ImageResizer.Instructions("maxheight=1065;&scale=both;format=jpg;mode=max"));

                img2.Build();

                Puzzle newPuzzle = new Puzzle();
                newPuzzle.PuzzleImage = puzzleImageId;
                newPuzzle.StartDate = startDate;
                newPuzzle.Distance = distance;
                newPuzzle.InsertDate = DateTime.Now;
                newPuzzle.InsertUserId = HttpContext.User.GetUserId();
                newPuzzle.ShopId = 2;
                newPuzzle.Status = (int)EnumHelper.PuzzleStatus.ToApprove;

                if (ModelState.IsValid)
                {
                    _context.Puzzle.Add(newPuzzle);
                    _context.SaveChanges();
                }
            }

            return View();

        }

        // GET: Campaigns/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Puzzle puzzle = _context.Puzzle.Single(m => m.ID == id);
            if (puzzle == null)
            {
                return HttpNotFound();
            }
            return View(puzzle);
        }

        // POST: Campaigns/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Puzzle puzzle)
        {
            if (ModelState.IsValid)
            {
                _context.Update(puzzle);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(puzzle);
        }

    }
}
