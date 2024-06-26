using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Skill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Resume")]
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public int? Rating { get; set; }
        public int? Experience { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Skill skill)
            {
                return Name == skill.Name;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }
    }

    public class HardSkill : Skill
    {
    }

    public class SoftSkill : Skill
    {
    }
}