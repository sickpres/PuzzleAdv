using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleAdv.Backend.Models
{
    public class Prize
    {
        public int ID { get; set; }
        public string PrizeImage { get; set; }
        public int Status { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public int NeededPoints { get; set; }

        //Foreign Keys
        public int ShopId { get; set; }

        // Navigation properties
        public Shop Shop { get; set; }
    }
}
