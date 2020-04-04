using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class MedicalStockOperationDto
    {
        public int Id { get; set; }

        [Required]
        public int OperationId { get; set; }

        [Required]
        public int Barcode { get; set; }

        [Required]
        public decimal Count { get; set; }

        [Required]
        public int NameOfDrug { get; set; }

        [Required]
        public int WareHourse { get; set; }

        public DateTime Expirydate { get; set; }
    }
}
