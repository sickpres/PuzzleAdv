using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PuzzleAdv.Backend.Models;
using PuzzleAdv.Backend.Helpers;

namespace PuzzleAdv.Backend.ViewModels.Admin
{
    public class AdminListViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Codice #")]
        public string IDNumber { get { return ID.ToString("D5"); } }

        public string PuzzleImage { get; set; }

        [Display(Name = "Data di inserimento")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "Data di inizio")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        public EnumHelper.PuzzleStatus StatusEnum { get; set; }

        [Display(Name = "Shop")]
        public string ShopTitle { get; set; }

        [Display(Name = "Città")]
        public string ShopCity { get; set; }

    }


}
