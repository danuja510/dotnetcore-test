using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelApp.Models
{
    public class FuelType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double CostPerLiter { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
    }
}
