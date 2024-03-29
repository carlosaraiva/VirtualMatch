﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMatch.Entities.DTO
{
    public class LoginPostResponse
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string Token { get; set; }

        public string PhotoUrl { get; set; }
        public string KnownAs { get; set; }

        public string Gender { get; set; }
    }
}
