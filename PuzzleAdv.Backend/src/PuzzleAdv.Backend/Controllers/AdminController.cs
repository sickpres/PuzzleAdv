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
using PuzzleAdv.Backend.ViewModels.Admin;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Authorization;

namespace PuzzleAdv.Backend.Controllers
{
    public class AdminController : Controller
    {
        private PuzzleAdvDbContext _context;
        private IHostingEnvironment _environment;
        private readonly IAuthorizationService _authz;

        public AdminController(PuzzleAdvDbContext context, IHostingEnvironment environment, IAuthorizationService authz)
        {
            _context = context;
            _environment = environment;
            _authz = authz;
        }

        [Authorize("AdminOnly")]
        public async Task<IActionResult> PuzzleList()
        {
            var list = from p in _context.Puzzle
                       join s in _context.Shop on p.ShopId equals s.ID
                       select new AdminListViewModel()
                       {
                           ID = p.ID,
                           InsertDate = p.InsertDate,
                           ShopTitle = s.Name,
                           ShopCity = s.City,
                           StatusEnum = (EnumHelper.PuzzleStatus)p.Status
                       };

            var adminListVm = await list.ToListAsync();

            return View(adminListVm);
        }


        [Authorize("AdminOnly")]
        public async Task<IActionResult> ToApproveList()
        {
            var list = from p in _context.Puzzle
                       join s in _context.Shop on p.ShopId equals s.ID
                       where p.Status == (int)EnumHelper.PuzzleStatus.ToApprove
                       select new AdminListViewModel()
                       {
                           ID = p.ID,
                           InsertDate = p.InsertDate,
                           StartDate = p.StartDate,
                           PuzzleImage = p.PuzzleImage,
                           ShopTitle = s.Name,
                           ShopCity = s.City
                       };

            var adminListVm = await list.ToListAsync();

            return View(adminListVm);
        }


        [Authorize("AdminOnly")]
        public IActionResult ApprovePuzzle(int? id)
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


        [Authorize("AdminOnly")]
        [HttpPost, ActionName("ApprovePuzzle")]
        [ValidateAntiForgeryToken]
        public IActionResult ApprovePuzzle(int id, string approve)
        {

            Puzzle puzzle = _context.Puzzle.Single(m => m.ID == id);

            int status;
            if (approve != null)
            {
                puzzle.StartDate = DateTime.Now;
                status = (int)EnumHelper.PuzzleStatus.InProduction;
            }
            else
            {
                status = (int)EnumHelper.PuzzleStatus.ToReview;
            }

            puzzle.Status = status;
            _context.Update(puzzle);
            _context.SaveChanges();
            return RedirectToAction("ToApproveList");

        }

    }
}
