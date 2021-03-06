﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class Parcel
    {
        [Key]
        public int Id { get; set; }

        public ParcelCategory ParcelCategory { get; set; }

        public string Name { get; set; }

        public decimal Area { get; set; }

        public Crops Crops { get; set; }

        public Company Company { get; set; }

        public bool Status { get; set; }

        public ICollection<MapLayer> ListMaplayers { get; set; }
    }
}
