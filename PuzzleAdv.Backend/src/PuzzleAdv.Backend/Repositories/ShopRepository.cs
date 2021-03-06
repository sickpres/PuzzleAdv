﻿using System;
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
using System.Data.Entity;

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
            return _dbContext.Shop
                .Where(x => x.UserId == user.GetUserId())
                .Select(x => new ShopViewModel()
                {
                    ID = x.ID,
                    Address = x.Address,
                    City = x.City,
                    Name = x.Name,
                    ShortDesc = x.ShortDesc,
                    Website = x.Website,
                    Phone = x.Phone,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    OpeningHours = x.OpeningHours.Where(y => y.ShopId == x.ID)
                                    .Select(z => new OpeningHoursViewModel()
                                    {
                                        ID = z.ID,
                                        ShopId = z.ShopId,
                                        DayOfWeek = z.DayOfWeek,
                                        DayOfWeekName = StringHelper.GetLocalizedDayOfWeekName(z.DayOfWeek),
                                        IsClosed = z.IsClosed,
                                        Opening1 = z.Opening1,
                                        Opening2 = z.Opening2,
                                        Closing1 = z.Closing1,
                                        Closing2 = z.Closing2
                                    }).ToList()
                }).SingleOrDefault();
        }

        public async Task AddShopAsync(ClaimsPrincipal user, ShopViewModel shopViewModel)
        {
            Shop shop = new Shop();

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

            shop.Name = shopViewModel.Name;
            shop.ShortDesc = shopViewModel.ShortDesc;
            shop.City = shopViewModel.City;
            shop.Address = shopViewModel.Address;
            shop.Website = shopViewModel.Website;
            shop.Phone = shopViewModel.Phone;
            shop.Latitude = latitude;
            shop.Longitude = longitude;
            shop.InsertDate = DateTime.Now;
            shop.InsertUserId = user.GetUserId();
            shop.UserId = user.GetUserId();

            _dbContext.Shop.Add(shop);

            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateShopAsync(ClaimsPrincipal user, ShopViewModel shopViewModel)
        {
            Shop shop = _dbContext.Shop.FirstOrDefault(x => x.UserId == user.GetUserId());

            shop.Name = shopViewModel.Name;
            shop.ShortDesc = shopViewModel.ShortDesc;
            shop.City = shopViewModel.City;
            shop.Address = shopViewModel.Address;
            shop.Website = shopViewModel.Website;
            shop.Phone = shopViewModel.Phone;
            shop.Latitude = shopViewModel.Latitude;
            shop.Longitude = shopViewModel.Longitude;
            shop.InsertDate = DateTime.Now;
            shop.InsertUserId = user.GetUserId();
            shop.UserId = user.GetUserId();

            _dbContext.Shop.Update(shop);

            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateLatLongAsync(double latitude, double longitude, int shopId)
        {
            Shop shop = _dbContext.Shop.FirstOrDefault(x => x.ID == shopId);

            shop.Latitude = latitude;
            shop.Longitude = longitude;

            _dbContext.Shop.Update(shop);

            await _dbContext.SaveChangesAsync();

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
