using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PuzzleAdv.Backend.Models;
using PuzzleAdv.Backend.Helpers;

namespace PuzzleAdv.Backend.ViewModels.Prize
{
    public class PrizeListViewModel
    {
        public int ID { get; set; }

        public string IDNumber { get { return ID.ToString("D5"); } }

        public string PrizeImage { get; set; }

        public EnumHelper.PrizeStatus StatusEnum { get; set; }

        public string StatusInfo { get; set; }


        public static implicit operator PrizeListViewModel(Models.Prize prize)
        {
            return new PrizeListViewModel
            {
                ID = prize.ID,
                PrizeImage = prize.PrizeImage,
                StatusEnum = (EnumHelper.PrizeStatus)prize.Status,
                StatusInfo = StringHelper.GetStatusInfo(prize)
            };
        }

        public static implicit operator Models.Prize(PrizeListViewModel vm)
        {
            return new Models.Prize
            {
                ID = vm.ID,
                PrizeImage = vm.PrizeImage
            };
        }
    
    }


}
