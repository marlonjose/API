﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class User
    {

        public int Id { get; set; }
        public string name { get; set; }
        public DateTime lastAcccess { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
