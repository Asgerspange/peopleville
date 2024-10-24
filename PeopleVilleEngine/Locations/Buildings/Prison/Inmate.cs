using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Locations.Buildings.Prison
{
    internal class Inmate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Crime { get; set; }
        public int CellNumber { get; set; }
    }
}
