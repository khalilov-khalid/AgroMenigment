using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class ParcelForUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ParcelCategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Area { get; set; }

        [Required]
        public int CropId { get; set; }
    }
}
