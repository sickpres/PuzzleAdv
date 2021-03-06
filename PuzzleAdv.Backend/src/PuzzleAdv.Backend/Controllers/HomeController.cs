﻿using System;
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

            bool userIsAdmin = await _authz.AuthorizeAsync(User, "AdminOnly");
            bool userHasShop = await _shopRepository.UserHasShopAsync(User);


            if (userIsAdmin)
            {
                return RedirectToAction(nameof(AdminController.ToApproveList), "Admin");
            }
            else
            {
                if (userHasShop)
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
