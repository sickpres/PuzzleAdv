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

        public ShopRepository(PuzzleAdvDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> UserHasShopAsync(ClaimsPrincipal user)
        {
            return await Task.FromResult(_dbContext.Shop.Any(x => x.UserId == user.GetUserId()));
        }

        public ShopViewModel GetUserShop(ClaimsPrincipal user)
        {
            return _dbContext.Shop.Where(x => x.UserId == user.GetUserId())
                .Select(x => new ShopViewModel()
                {
                    ID = x.ID,
                    Address = x.Address,
                    City = x.City,
                    Name = x.Name,
                    ShortDesc = x.ShortDesc,
                    Website = x.Website
                }).SingleOrDefault();
        }

        public void AddShop(ClaimsPrincipal user, ShopViewModel shopViewModel)
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
            newShop.InsertUserId = user.GetUserId();
            newShop.UserId = user.GetUserId();

            _dbContext.Shop.Add(newShop);
            _dbContext.SaveChanges();
        }

        /*
        public IList<ShopDistance> GetPuzzleFromDbFunction()
        {
            string commandText = string.Format("SELECT * FROM [dbo].[GetPuzzle] ({0}, {1})", "45.808060", "9.085176");
            IList<ShopDistance> list = _dbContext.GetPuzzleDbFunction(commandText);
            return list;
        }
        */

    }
}
