using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuzzleAdv.Backend.Models;
using PuzzleAdv.Backend.ViewModels.Shop;

namespace PuzzleAdv.Backend.Interfaces
{
    public interface IPuzzleRepository
    {
        IEnumerable<Puzzle> ListAllPuzzleByUser();  
        void AddPuzzle(string puzzleImageId, int distance);

        Shop GetShopByUser();
        IList<ShopDistance> GetPuzzleFromDbFunction();
    }

}
