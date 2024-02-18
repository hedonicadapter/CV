using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Resume")]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string? Subtitle { get; set; }

        public DateTime[]? Dates { get; set; } = [DateTime.Today, DateTime.Today];
        public string? Location { get; set; }
        public string Summary { get; set; }
        public List<string>? Tags { get; set; }

        public Item()
        {
            Title = "";
            Summary = "";
            Tags = new List<string>();
        }
    }
}