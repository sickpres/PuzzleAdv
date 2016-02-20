using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PuzzleAdv.Backend.Helpers;

namespace PuzzleAdv.Backend.Helpers
{
    public static class EnumHelper
    {

        public enum PuzzleStatus
        {
            [Display(Name = "In approvazione", ShortName = "fa fa-gavel")]
            ToApprove,

            [Display(Name = "Modifiche necessarie", ShortName = "fa fa-exclamation-triangle")]
            ToReview,

            [Display(Name = "Pronto", ShortName = "fa fa-check-circle-o")]
            ReadyToProduction,

            [Display(Name = "Pubblicato", ShortName = "fa fa-globe")]
            InProduction,

            [Display(Name = "In pausa", ShortName = "fa fa-pause")]
            Paused,

            [Display(Name = "Cancellato", ShortName = "fa fa-trash-o")]
            Deleted
        };

        public enum PrizeStatus
        {
            [Display(Name = "Pronto", ShortName = "fa fa-check-circle-o")]
            ReadyToProduction,

            [Display(Name = "Pubblicato", ShortName = "fa fa-globe")]
            InProduction,

            [Display(Name = "In pausa", ShortName = "fa fa-pause")]
            Paused,

            [Display(Name = "Cancellato", ShortName = "fa fa-trash-o")]
            Deleted
        };


    }


}
