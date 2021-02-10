using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models.DTO
{
    public class EmployeeCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public int SocialSecurityNumber { get; set; }
        [Required]
        public DateTime DateHired { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

    }
}
