using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class DemandDetailDto
    {
        public int Id { get; set; }

        public ProductReadDto ProductReadDto { get; set; }

        public decimal Quantity { get; set; }

        public string Parsel { get; set; }

        public string Country { get; set; }

        public string Workers { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime RequiredDate { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
