using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuzzleAdv.Backend.Models;

namespace PuzzleAdv.Backend.Helpers
{
    public static class StringHelper
    {

        public static string GetStatusInfo(Models.Puzzle puzzle)
        {
            string returnVal = "";

            switch ((EnumHelper.PuzzleStatus)puzzle.Status)
            {
                case EnumHelper.PuzzleStatus.ToApprove:

                    returnVal = "Sarai avvisato via email non appena sarà modificato lo stato";
                    break;

                case EnumHelper.PuzzleStatus.ToReview:

                    returnVal = "Le definizione dell'immagine è inferiore a 96 dpi";
                    break;

                case EnumHelper.PuzzleStatus.ReadyToProduction:

                    returnVal = "<br><br><br>";

                    if (puzzle.StartDate != null)
                    {
                        int days = ((DateTime)puzzle.StartDate - DateTime.Today).Days;

                        returnVal = days == 0 ? "Pubblicazione prevista per oggi <br><br>" :
                                    days == 1 ? "Pubblicazione prevista per domani <br><br>" :
                                                String.Format("In pubblicazione tra {0} giorni <br><br>", days.ToString());
                    }

                    break;

                case EnumHelper.PuzzleStatus.InProduction:

                    returnVal = "Pubblicato da 10 giorni <br><br>";
                    break;

                case EnumHelper.PuzzleStatus.Paused:

                    returnVal = "Fermato da 2 giorni <br><br>";
                    break;

                case EnumHelper.PuzzleStatus.Deleted:

                    returnVal = "Cancellato da 5 giorni <br><br>";
                    break;

                default:

                    returnVal = "";
                    break;

            }

            return returnVal;
        }

        public static string GetStatusInfo(Models.Prize prize)
        {
            string returnVal = "";

            switch ((EnumHelper.PrizeStatus)prize.Status)
            {

                case EnumHelper.PrizeStatus.ReadyToProduction:

                    returnVal = "Il premio sarà pubblicato insieme al primo puzzle";

                    break;

                case EnumHelper.PrizeStatus.InProduction:

                    returnVal = "Pubblicato da 10 giorni <br><br>";
                    break;

                case EnumHelper.PrizeStatus.Paused:

                    returnVal = "Fermato da 2 giorni <br><br>";
                    break;

                case EnumHelper.PrizeStatus.Deleted:

                    returnVal = "Cancellato da 5 giorni <br><br>";
                    break;

                default:

                    returnVal = "";
                    break;

            }

            return returnVal;
        }

    }
}
