using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using PuzzleAdv.Backend.Models;

namespace PuzzleAdv.Backend.Controllers
{
    public class DemoController : Controller
    {
        private PuzzleAdvDbContext _context;

        public DemoController(PuzzleAdvDbContext context)
        {
            _context = context;    
        }

        // GET: Demo
        public IActionResult Index()
        {
            var puzzleAdvDbContext = _context.Puzzle.Include(p => p.Shop);
            return View(puzzleAdvDbContext.ToList());
        }

        // GET: Demo/Details/5
        public IActionResult Details(int? id)
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

        // GET: Demo/Create
        public IActionResult Create()
        {
            ViewData["ShopId"] = new SelectList(_context.Shop, "ID", "Shop");
            return View();
        }

        // POST: Demo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Puzzle puzzle)
        {
            if (ModelState.IsValid)
            {
                _context.Puzzle.Add(puzzle);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ShopId"] = new SelectList(_context.Shop, "ID", "Shop", puzzle.ShopId);
            return View(puzzle);
        }

        // GET: Demo/Edit/5
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
            ViewData["ShopId"] = new SelectList(_context.Shop, "ID", "Shop", puzzle.ShopId);
            return View(puzzle);
        }

        // POST: Demo/Edit/5
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
            ViewData["ShopId"] = new SelectList(_context.Shop, "ID", "Shop", puzzle.ShopId);
            return View(puzzle);
        }

        // GET: Demo/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
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

        // POST: Demo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string button)
        {
            Puzzle puzzle = _context.Puzzle.Single(m => m.ID == id);
            _context.Puzzle.Remove(puzzle);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
