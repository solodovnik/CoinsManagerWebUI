﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CoinsManagerWebUI.Models
{
    public partial class Continent
    {
        public Continent()
        {
            Countries = new HashSet<Country>();
        }

        public int Id { get; set; }
        public string Continent1 { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
