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

        // GET: /<controller>/
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
                           ShopCity = s.City
                       };

            var adminListVm = await list.ToListAsync();

            return View(adminListVm);
        }
    }
}
