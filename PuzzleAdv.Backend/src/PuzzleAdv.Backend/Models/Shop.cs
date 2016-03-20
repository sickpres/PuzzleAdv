using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Spatial;

namespace PuzzleAdv.Backend.Models
{
    public class Shop
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        //Technical contacts
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        
        //CRUD info
        public DateTime InsertDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string InsertUserId { get; set; }
        public string DeleteUserId { get; set; }
        public string LastUpdateUserId { get; set; }

        //Geo info
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        //Foreign Keys
        public string UserId { get; set; }

    }
}
