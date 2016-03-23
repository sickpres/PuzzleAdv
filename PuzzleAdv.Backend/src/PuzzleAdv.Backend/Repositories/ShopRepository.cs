using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuzzleAdv.Backend.Interfaces;
using PuzzleAdv.Backend.Models;
using PuzzleAdv.Backend.ViewModels.Shop;
using Microsoft.AspNet.Http;
using System.Security.Claims;
using PuzzleAdv.Backend.Helpers;
using GoogleMaps.LocationServices;

namespace PuzzleAdv.Backend.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly PuzzleAdvDbContext _dbContext;
        private readonly string _loggedUserId;

        public ShopRepository(PuzzleAdvDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _loggedUserId = httpContextAccessor.HttpContext.User.GetUserId();
        }

        public bool UserHasShop()
        {
            var count = _dbContext.Shop.Where(x => x.UserId == _loggedUserId).Count();
            return count > 0;
        }

        public ShopViewModel GetShopByUser()
        {
            return _dbContext.Shop.Where(x => x.UserId == _loggedUserId)
                .Select(x => new ShopViewModel()
                {
                    Address = x.Address,
                    City = x.City,
                    Name = x.Name,
                    ShortDesc = x.ShortDesc,
                    Website = x.Website
                }).SingleOrDefault();
        }

        public void AddShop(ShopViewModel shopViewModel)
        {
            var addressData = new AddressData
            {
                Address = shopViewModel.Address,
                City = shopViewModel.City.ToUpper(),
                Country = "Italy"
            };

            var gls = new GoogleLocationService();
            var latlong = gls.GetLatLongFromAddress(addressData);
            var latitude = latlong.Latitude;
            var longitude = latlong.Longitude;

            Shop newShop = new Shop();

            newShop.Name = shopViewModel.Name;
            newShop.ShortDesc = shopViewModel.ShortDesc;
            newShop.City = shopViewModel.City;
            newShop.Address = shopViewModel.Address;
            newShop.Website = shopViewModel.Website;
            newShop.Latitude = latitude;
            newShop.Longitude = longitude;
            newShop.InsertDate = DateTime.Now;
            newShop.InsertUserId = _loggedUserId;
            newShop.UserId = _loggedUserId;

            _dbContext.Shop.Add(newShop);
            _dbContext.SaveChanges();
        }

    }
}
