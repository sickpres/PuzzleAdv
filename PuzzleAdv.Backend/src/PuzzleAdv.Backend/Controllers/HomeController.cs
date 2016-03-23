using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using PuzzleAdv.Backend.Interfaces;

namespace PuzzleAdv.Backend.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authz;
        private readonly IShopRepository _shopRepository;

        public HomeController(IAuthorizationService authz, IShopRepository shopRepository)
        {
            _authz = authz;
            _shopRepository = shopRepository;
        }

        public async Task<IActionResult> Index()
        {

            bool isAdmin = await _authz.AuthorizeAsync(User, "AdminOnly");
            bool hasShop = _shopRepository.UserHasShop();


            if (isAdmin)
            {
                return RedirectToAction(nameof(AdminController.ToApproveList), "Admin");
            }
            else
            {
                if (hasShop)
                {
                    return RedirectToAction(nameof(PuzzleController.Index), "Puzzle");
                }
                else
                {
                    return RedirectToAction(nameof(ShopController.Index), "Shop");
                }
            }

        }

    }
}
