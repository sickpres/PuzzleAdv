using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Http;

namespace PuzzleAdv.Backend.ViewModels.Puzzle
{
    public class UploadImageViewModel
    {
        [FileExtensions(Extensions = "jpg,jpeg,png")]
        public IFormFile File { get; set; }
    }
}
