using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using PuzzleAdv.Backend.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PuzzleAdv.Backend.API
{
    [Route("api/puzzle")]
    public class PuzzleController : Controller
    {
        private PuzzleAdvDbContext _context;

        public PuzzleController(PuzzleAdvDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        /*
        [HttpGet]
        public Puzzle GetLastInserted()
        {
            var puzzle = _context.Puzzle.OrderByDescending(x => x.InsertDate).FirstOrDefault();
            return puzzle;
        }
        */

        
        [HttpGet]
        public Puzzle GetRandom()
        {
            Random rand = new Random();

            var puzzle = _context.Puzzle.Skip(rand.Next(0, _context.Puzzle.Count())).Take(1).SingleOrDefault();
                
            return puzzle;
        }
        
    }
}
