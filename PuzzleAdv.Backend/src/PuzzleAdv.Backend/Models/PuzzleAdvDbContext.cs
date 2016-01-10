using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace PuzzleAdv.Backend.Models
{
    public class PuzzleAdvDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Shop> Shop { get; set; }
        public DbSet<Puzzle> Puzzle { get; set; }
        public DbSet<Prize> Prize { get; set; }
    }
}
