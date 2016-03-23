using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuzzleAdv.Backend.Models;
using PuzzleAdv.Backend.ViewModels.Shop;

namespace PuzzleAdv.Backend.Interfaces
{
    public interface IShopRepository
    {
        bool UserHasShop();
        ShopViewModel GetShopByUser();
        void AddShop(ShopViewModel shopViewModel);
    }
}
