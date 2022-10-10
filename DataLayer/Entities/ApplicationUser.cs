﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class ApplicationUser: IdentityUser
    {
        //Навигационное свойства
        public List<Ticket> Tickets { get; set; }
    }
}
