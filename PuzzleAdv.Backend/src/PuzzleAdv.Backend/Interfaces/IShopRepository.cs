﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuzzleAdv.Backend.Models;
using PuzzleAdv.Backend.ViewModels.Shop;
using System.Security.Claims;

namespace PuzzleAdv.Backend.Interfaces
{
    public interface IShopRepository
    {
        Task<bool> UserHasShopAsync(ClaimsPrincipal user);

        ShopViewModel GetUserShop(ClaimsPrincipal user);

        Task AddShopAsync(ClaimsPrincipal user, ShopViewModel shopViewModel);

        Task UpdateShopAsync(ClaimsPrincipal user, ShopViewModel shopViewModel);

        Task UpdateLatLongAsync(double latitude, double longitude, int shopId);
    }
}
