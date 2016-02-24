using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuzzleAdv.Backend.Models;

namespace PuzzleAdv.Backend.Interfaces
{
    public interface IPuzzleRepository
    {
        //Puzzle
        IEnumerable<Puzzle> ListAllPuzzleByUser();  
        void AddPuzzle(string puzzleImageId, int distance);


        //Shop
        Shop GetShopByUser();
    }

}
