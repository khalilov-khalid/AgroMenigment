using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class DemandReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DemandNumber { get; set; }

        public DateTime CreateDate { get; set; }

        public int CheckStatus { get; set; }

        public List<DemandProductReadDto> DemandProducts { get; set; }
    }

    public class DemandProductReadDto
    {
        public ProductReadDto Product { get; set; }

        public decimal Quantity { get; set; }

        public string Parcel { get; set; }

        public string Country { get; set; }

        public string RequestingWorker { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime RequiredDate { get; set; }
    }
}
