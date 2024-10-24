using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Buildings.Houses
{
    internal class House
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Owner { get; set; }
        public int NumberOfRooms { get; set; }
        public List<Resident> Residents { get; set; }
    }
}
