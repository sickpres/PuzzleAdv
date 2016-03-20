using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using PuzzleAdv.Backend.Helpers;
using PuzzleAdv.Backend.Interfaces;
using PuzzleAdv.Backend.Models;
using PuzzleAdv.Backend.ViewModels.Shop;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoogleMaps.LocationServices;
using CodeFirstStoreFunctions;


namespace PuzzleAdv.Backend.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopRepository _shopRepository;
        private IHostingEnvironment _environment;

        public ShopController(IShopRepository shopRepository, IHostingEnvironment environment)
        {
            _shopRepository = shopRepository;
            _environment = environment;
        }

        // GET: Puzzle List
        public IActionResult Index()
        {
            var shopVm = _shopRepository.GetShopByUser();
            return View(shopVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ShopViewModel shop)
        {

            _shopRepository.AddShop(shop.Name, 
                                    shop.ShortDesc, 
                                    shop.City,
                                    shop.Address, 
                                    shop.Website);

            return View();
        }
    }
}
