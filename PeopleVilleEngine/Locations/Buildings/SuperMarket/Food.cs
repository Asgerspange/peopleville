using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Locations.Buildings.SuperMarket
{
    public class Food
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
    // create a few products, apples, oranges, and bananas
    public class Apples : Food
    {
        public Apples()
        {
            Name = "Apples";
            Price = 1.00m;
            Quantity = 100;
        }
    }
    public class Oranges : Food
    {
        public Oranges()
        {
            Name = "Oranges";
            Price = 1.50m;
            Quantity = 100;
        }
    }
    public class Bananas : Food
    {
        public Bananas()
        {
            Name = "Bananas";
            Price = 0.50m;
            Quantity = 100;
        }
    }
}
