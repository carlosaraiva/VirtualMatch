using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMatch.Entities.DTO
{
    public class LoginPostRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Pass { get; set; }
    }
}
