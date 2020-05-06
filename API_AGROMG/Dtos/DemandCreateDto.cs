using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class DemandCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public int ParcelId { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public DateTime RequiredDate { get; set; }
    }
}
