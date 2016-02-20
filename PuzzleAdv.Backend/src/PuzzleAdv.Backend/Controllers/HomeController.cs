using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;

namespace PuzzleAdv.Backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authz;

        public HomeController(IAuthorizationService authz)
        {
            _authz = authz;
        }

        public async Task<IActionResult> Index()
        {

            bool isAdmin = await _authz.AuthorizeAsync(User, "AdminOnly");

            if (isAdmin)
            {
                return RedirectToAction(nameof(AdminController.PuzzleList), "Admin");
            }
            else
            {
                return RedirectToAction(nameof(PuzzleController.Index), "Puzzle");
            }

        }

    }
}
