using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuzzleAdv.Backend.Models;
using PuzzleAdv.Backend.ViewModels.Puzzle;
using System.Security.Claims;

namespace PuzzleAdv.Backend.Interfaces
{
    public interface IPuzzleRepository
    {
        List<PuzzleListViewModel> GetPuzzles(ClaimsPrincipal user);  
        Task AddPuzzleAsync(ClaimsPrincipal user, string puzzleImageId, int distance, int shopId);
    }

}
