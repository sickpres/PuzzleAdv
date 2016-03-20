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

namespace PuzzleAdv.Backend.Repositories
{
    public class PuzzleRepository : IPuzzleRepository
    {
        private readonly PuzzleAdvDbContext _dbContext;
        private readonly string _loggedUserId;

        public PuzzleRepository(PuzzleAdvDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _loggedUserId = httpContextAccessor.HttpContext.User.GetUserId();
        }

        public void AddPuzzle(string puzzleImageId, int distance)
        {
            Puzzle newPuzzle = new Puzzle();
            newPuzzle.PuzzleImage = puzzleImageId;
            newPuzzle.Distance = distance;
            newPuzzle.InsertDate = DateTime.Now;
            newPuzzle.InsertUserId = _loggedUserId;
            newPuzzle.ShopId = GetShopByUser().ID;
            newPuzzle.Status = (int)EnumHelper.PuzzleStatus.ToApprove;

            _dbContext.Puzzle.Add(newPuzzle);
            _dbContext.SaveChanges();
        }

        public Shop GetShopByUser()
        {
            return _dbContext.Shop.Single(x => x.UserId == _loggedUserId);
        }

        public IEnumerable<Puzzle> ListAllPuzzleByUser()
        {
            return _dbContext.Puzzle
                .Where(x => x.InsertUserId == _loggedUserId && x.Status != (int)EnumHelper.PuzzleStatus.Deleted)
                .OrderByDescending(x => x.InsertDate).AsEnumerable();
        }

        
        public IList<ShopDistance> GetPuzzleFromDbFunction()
        {
            string commandText = string.Format("SELECT * FROM [dbo].[GetPuzzle] ({0}, {1})", "45.808060", "9.085176");
            IList<ShopDistance> list = _dbContext.GetPuzzleDbFunction(commandText);
            return list;
        }
        

    }
}
