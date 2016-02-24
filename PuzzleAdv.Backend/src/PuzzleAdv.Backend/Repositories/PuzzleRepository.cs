using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuzzleAdv.Backend.Interfaces;
using PuzzleAdv.Backend.Models;
using Microsoft.AspNet.Http;
using System.Security.Claims;
using PuzzleAdv.Backend.Helpers;

namespace PuzzleAdv.Backend.Repositories
{
    public class PuzzleRepository : IPuzzleRepository
    {
        private readonly PuzzleAdvDbContext _dbContext;
        private readonly string _loggedUser;

        public PuzzleRepository(PuzzleAdvDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _loggedUser = httpContextAccessor.HttpContext.User.GetUserId();
        }

        public void AddPuzzle(string puzzleImageId, int distance)
        {
            Puzzle newPuzzle = new Puzzle();
            newPuzzle.PuzzleImage = puzzleImageId;
            newPuzzle.Distance = distance;
            newPuzzle.InsertDate = DateTime.Now;
            newPuzzle.InsertUserId = _loggedUser;
            newPuzzle.ShopId = GetShopByUser().ID;
            newPuzzle.Status = (int)EnumHelper.PuzzleStatus.ToApprove;

            _dbContext.Puzzle.Add(newPuzzle);
            _dbContext.SaveChanges();
        }

        public Shop GetShopByUser()
        {
            return _dbContext.Shop.Single(x => x.UserId == _loggedUser);
        }

        public IEnumerable<Puzzle> ListAllPuzzleByUser()
        {
            return _dbContext.Puzzle
                .Where(x => x.InsertUserId == _loggedUser && x.Status != (int)EnumHelper.PuzzleStatus.Deleted)
                .OrderByDescending(x => x.InsertDate).AsEnumerable();
        }
    }
}
