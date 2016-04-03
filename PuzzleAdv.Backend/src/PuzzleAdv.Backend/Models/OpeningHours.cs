using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;


namespace PuzzleAdv.Backend.Models
{
    public class OpeningHours
    {

        public int ID { get; set; }

        public int ShopId { get; set; }

        public int DayOfWeek { get; set; }

        public bool IsClosed { get; set; }

        public TimeSpan? Opening1 { get; set; }
        public TimeSpan? Closing1 { get; set; }

        public TimeSpan? Opening2 { get; set; }
        public TimeSpan? Closing2 { get; set; }

    }
}
