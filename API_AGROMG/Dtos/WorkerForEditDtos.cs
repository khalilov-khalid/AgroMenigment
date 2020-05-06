using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class WorkerForEditDtos
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public int Gender { get; set; }

        [Required]
        public DateTime WorkStartDate { get; set; }

        [Required]
        public int WorkStatus { get; set; }

        [Required]
        public string Fin { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public string SSN { get; set; }

    }
}
