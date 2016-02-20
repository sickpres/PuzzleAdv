using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;


namespace PuzzleAdv.Backend.Models
{
    public class Puzzle
    {
        public int ID { get; set; }
        public string PuzzleImage { get; set; }
        public int Status { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Distance { get; set; }

        public DateTime InsertDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string InsertUserId { get; set; }
        public string DeleteUserId { get; set; }
        public string LastUpdateUserId { get; set; }

        //Foreign Keys
        public int ShopId { get; set; }

        // Navigation properties
        public Shop Shop { get; set; }

    }
}
