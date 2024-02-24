using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Resume
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required, I'm afraid")]
        public string Name { get; set; } = "Sam Herman";

        // regex från riktiga regex-nördar https://emailregex.com/
        // [RegularExpression(@"^[a-zA-Z\s]*$(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])", ErrorMessage = "Invalid email format, I fear")]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Location { get; set; }
        public string? GitHub { get; set; }
        public string? LinkedIn { get; set; }
        public string? Portfolio { get; set; }


        [Required(ErrorMessage = "Do a lil self summary")]
        public string Profile { get; set; } = "";

        [ValidateComplexType]
        public List<Education>? Educations { get; set; }
        [ValidateComplexType]
        public List<Project>? Projects { get; set; }
        [ValidateComplexType]
        public List<Experience>? Experiences { get; set; }
        [ValidateComplexType]
        public List<SoftSkill>? SoftSkills { get; set; }

        [ValidateComplexType]
        public List<HardSkill>? HardSkills { get; set; }

        public Resume()
        {
            Educations = new List<Education>();
            Projects = new List<Project>();
            Experiences = new List<Experience>();
            SoftSkills = new List<SoftSkill>();
            HardSkills = new List<HardSkill>();
        }

    }
}