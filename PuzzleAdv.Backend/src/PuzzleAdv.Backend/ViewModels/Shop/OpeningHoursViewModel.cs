using PuzzleAdv.Backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PuzzleAdv.Backend.ViewModels.Shop
{
    public class OpeningHoursViewModel
    {
        public int ID { get; set; }

        public int ShopId { get; set; }

        public int DayOfWeek { get; set; }

        public String DayOfWeekName { get; set; }

        public bool IsClosed { get; set; }

        public TimeSpan? Opening1 { get; set; }
        public TimeSpan? Closing1 { get; set; }

        public TimeSpan? Opening2 { get; set; }
        public TimeSpan? Closing2 { get; set; }

    }
}
