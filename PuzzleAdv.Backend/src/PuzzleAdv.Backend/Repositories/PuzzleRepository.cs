using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuzzleAdv.Backend.Interfaces;
using PuzzleAdv.Backend.Models;
using PuzzleAdv.Backend.ViewModels.Puzzle;
using Microsoft.AspNet.Http;
using System.Security.Claims;
using PuzzleAdv.Backend.Helpers;
using PuzzleAdv.Backend.Repositories;

namespace PuzzleAdv.Backend.Repositories
{
    public class PuzzleRepository : IPuzzleRepository
    {
        private readonly PuzzleAdvDbContext _dbContext;

        public PuzzleRepository(PuzzleAdvDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPuzzleAsync(ClaimsPrincipal user, string puzzleImageId, int distance, int shopId)
        {
            Puzzle newPuzzle = new Puzzle();
            newPuzzle.PuzzleImage = puzzleImageId;
            newPuzzle.Distance = distance;
            newPuzzle.InsertDate = DateTime.Now;
            newPuzzle.InsertUserId = user.GetUserId();
            newPuzzle.ShopId = shopId;
            newPuzzle.Status = (int)EnumHelper.PuzzleStatus.ToApprove;

            _dbContext.Puzzle.Add(newPuzzle);
            await _dbContext.SaveChangesAsync();
        }

        public List<PuzzleListViewModel> GetPuzzles(ClaimsPrincipal user)
        {
            var query = _dbContext.Puzzle
                .Where(x => x.InsertUserId == user.GetUserId() && x.Status != (int)EnumHelper.PuzzleStatus.Deleted)
                .OrderByDescending(x => x.InsertDate)
                .Select(x => new PuzzleListViewModel()
                {
                    ID = x.ID,
                    InsertDate = x.InsertDate,
                    PuzzleImage = x.PuzzleImage,
                    StatusEnum = (EnumHelper.PuzzleStatus)x.Status,
                    StatusInfo = StringHelper.GetStatusInfo(x)
                });

            return query.ToList();

        }

    }
}
