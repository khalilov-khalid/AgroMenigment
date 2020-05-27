using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Model
{
    public class MapLayer
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Coordinates { get; set; }
        
        [ForeignKey("Parcel")]
        public int? ParcelId { get; set; }

        public Parcel Parcel { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
