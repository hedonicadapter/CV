using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Education : Item
    {
        public Education() : base()
        {
            Degree = "";
        }

        public string Degree { get; set; }
    }
}