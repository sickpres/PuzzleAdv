using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleAdv.Backend.ViewModels.Shop
{
    public class ShopViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Ragione sociale")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Breve descrizione")]
        public string ShortDesc { get; set; }

        [Required]
        [Display(Name = "Località")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Indirizzo")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Telefono")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Sito web")]
        public string Website { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
