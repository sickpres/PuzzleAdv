using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PuzzleAdv.Backend.Models;
using PuzzleAdv.Backend.Helpers;

namespace PuzzleAdv.Backend.ViewModels.Puzzle
{
    public class PuzzleListViewModel
    {
        public int ID { get; set; }

        public string IDNumber { get { return ID.ToString("D5"); } }

        public string PuzzleImage { get; set; }

        [Display(Name = "Data di inserimento")]
        [DataType(DataType.Date)]
        public DateTime InsertDate { get; set; }

        public EnumHelper.PuzzleStatus StatusEnum { get; set; }

        public string StatusInfo { get; set; }



        public static implicit operator PuzzleListViewModel(Models.Puzzle puzzle)
        {
            return new PuzzleListViewModel
            {
                ID = puzzle.ID,
                InsertDate = puzzle.InsertDate,
                PuzzleImage = puzzle.PuzzleImage,
                StatusEnum = (EnumHelper.PuzzleStatus)puzzle.Status,
                StatusInfo = StringHelper.GetStatusInfo(puzzle)
            };
        }

        public static implicit operator Models.Puzzle(PuzzleListViewModel vm)
        {
            return new Models.Puzzle
            {
                ID = vm.ID,
                InsertDate = vm.InsertDate,
                PuzzleImage = vm.PuzzleImage
            };
        }
    
    }


}
