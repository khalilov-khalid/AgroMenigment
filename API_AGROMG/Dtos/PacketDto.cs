using API_AGROMG.SimpleforDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class PacketDto
    {
        public int Id { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int HumanContent { get; set; }
        [Required]
        public List<int> ModulId { get; set; }

        public List<LangcontentDto> Content { get; set; }
    }
}
