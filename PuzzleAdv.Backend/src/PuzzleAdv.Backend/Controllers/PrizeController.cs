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
using PuzzleAdv.Backend.ViewModels.Prize;
using Microsoft.Data.Entity;


namespace PuzzleAdv.Backend.Controllers
{
    public class PrizeController : Controller
    {
        private PuzzleAdvDbContext _context;
        private IHostingEnvironment _environment;

        public PrizeController(PuzzleAdvDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Prize List
        public async Task<IActionResult> Index()
        {
            var prizes = await _context.Prize.ToListAsync();

            var prizeListVm = prizes.Select<Prize, PrizeListViewModel>(prize => prize);

            return View(prizeListVm);
        }


        // GET: Prize/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public double GetPoints(double originalPrice, double discountPrice)
        {
            return Math.Round(((0.98 * originalPrice) - discountPrice) * 50, 0);
        }

        // POST: Prize/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormFile croppedImage1, string shortDesc, string longDesc, decimal originalPrice, decimal discountPrice)
        {
            //Save image
            var uploads = Path.Combine(_environment.WebRootPath, "Uploads");
            if (croppedImage1.Length > 0)
            {
                var prizeImageId = Guid.NewGuid().ToString();
                var fileName = string.Format("{0}.jpg", prizeImageId);

                croppedImage1.SaveAs(Path.Combine(uploads, fileName));

                ImageResizer.ImageJob img1 = new ImageResizer.ImageJob(Path.Combine(uploads, fileName), Path.Combine(uploads, "Prizes", "400", fileName),
                                new ImageResizer.Instructions("maxheight=400;&scale=both;format=jpg;mode=max"));

                img1.Build();

                ImageResizer.ImageJob img2 = new ImageResizer.ImageJob(Path.Combine(uploads, fileName), Path.Combine(uploads, "Prizes", "600", fileName), 
                                                new ImageResizer.Instructions("maxheight=600;&scale=both;format=jpg;mode=max"));

                img2.Build();

                Prize newPrize = new Prize();
                newPrize.PrizeImage = prizeImageId;
                newPrize.ShortDesc = shortDesc;
                newPrize.LongDesc = longDesc;
                newPrize.OriginalPrice = originalPrice;
                newPrize.DiscountPrice = discountPrice;

                newPrize.ShopId = 2;
                newPrize.Status = (int)EnumHelper.PrizeStatus.InProduction;

                if (ModelState.IsValid)
                {
                    _context.Prize.Add(newPrize);
                    _context.SaveChanges();
                }
            }

            return View();

        }

    }
}
