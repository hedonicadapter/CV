using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Project : Item
    {
        public Project() : base()
        {
            Bulletpoints = new List<string>();
        }
        public List<string>? Bulletpoints { get; set; }
        public string? Link { get; set; }
    }
}