using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
using PuzzleAdv.Backend.Helpers;
using PuzzleAdv.Backend.Models;
using PuzzleAdv.Backend.ViewModels.Puzzle;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


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
            var userId = HttpContext.User.GetUserId();

            var puzzles = await _context.Puzzle
                .Where(x => x.InsertUserId == userId && x.Status != (int)EnumHelper.PuzzleStatus.Deleted)
                .OrderByDescending(x => x.InsertDate)
                .ToListAsync();

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
        public IActionResult Create(IFormFile croppedImage1, IFormFile croppedImage2, int distance)
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

                var userId = HttpContext.User.GetUserId();
                var shopId = _context.Shop.Where(x => x.UserId == userId).Select(x => x.ID).FirstOrDefault();

                Puzzle newPuzzle = new Puzzle();
                newPuzzle.PuzzleImage = puzzleImageId;
                newPuzzle.Distance = distance;
                newPuzzle.InsertDate = DateTime.Now;
                newPuzzle.InsertUserId = userId;
                newPuzzle.ShopId = shopId;
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
