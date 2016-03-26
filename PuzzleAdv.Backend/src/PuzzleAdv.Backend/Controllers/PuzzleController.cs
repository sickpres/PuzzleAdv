using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using PuzzleAdv.Backend.Helpers;
using PuzzleAdv.Backend.Interfaces;
using PuzzleAdv.Backend.Models;
using PuzzleAdv.Backend.ViewModels.Puzzle;
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
    [RequireHttps]
    public class PuzzleController : Controller
    {
        private readonly IPuzzleRepository _puzzleRepository;
        private readonly IShopRepository _shopRepository;
        private IHostingEnvironment _environment;

        public PuzzleController(IPuzzleRepository puzzleRepository, IShopRepository shopRepository, IHostingEnvironment environment)
        {
            _puzzleRepository = puzzleRepository;
            _shopRepository = shopRepository;
            _environment = environment;
        }

        // GET: Puzzle List
        public IActionResult Index()
        {
            var puzzleListVm = _puzzleRepository.GetPuzzles(User);
            return View(puzzleListVm);
        }

        // GET: Puzzle/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Puzzle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile croppedImage1, IFormFile croppedImage2, int distance)
        {

            var uploads = Path.Combine(_environment.WebRootPath, "Uploads");

            if (croppedImage1.Length > 0 && croppedImage2.Length > 0)
            {
                var puzzleImageId = Guid.NewGuid().ToString();
                var fileName = string.Format("{0}.jpg", puzzleImageId);

                await croppedImage1.SaveAsAsync(Path.Combine(uploads, fileName));

                ImageResizer.ImageJob img1 = new ImageResizer.ImageJob(Path.Combine(uploads, fileName), Path.Combine(uploads, "16-9", "400", fileName),
                                new ImageResizer.Instructions("maxheight=400;&scale=both;format=jpg;mode=max"));

                img1.Build();

                ImageResizer.ImageJob img2 = new ImageResizer.ImageJob(Path.Combine(uploads, fileName), Path.Combine(uploads, "16-9", "1065", fileName),
                                                new ImageResizer.Instructions("maxheight=1065;&scale=both;format=jpg;mode=max"));

                img2.Build();


                if (ModelState.IsValid)
                {
                    await _puzzleRepository.AddPuzzleAsync(User, puzzleImageId, distance, _shopRepository.GetUserShop(User).ID);
                }
            }

            return View();

        }


    }
}
