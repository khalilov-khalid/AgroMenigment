using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class ProductForFeltilizerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int FertilizerKindId { get; set; }

        public int MainIngredientId { get; set; }
    }
}
