namespace PeopleVilleEngine.Buildings
{
    internal class SuperMarket
    {
        private Dictionary<string, Food> inventory = new Dictionary<string, Food>();
        private List<SuperMarketLocation> locations = new List<SuperMarketLocation>();

        public void AddFoodItem(string name, decimal price, int quantity)
        {
            if (inventory.ContainsKey(name))
            {
                inventory[name].Quantity += quantity;
            }
            else
            {
                inventory[name] = new Food
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity
                };
            }
        }

        public bool SellFoodItem(string name, int quantity)
        {
            if (inventory.ContainsKey(name) && inventory[name].Quantity >= quantity)
            {
                inventory[name].Quantity -= quantity;
                return true;
            }
            return false;
        }

        public void RestockFoodItem(string name, int quantity)
        {
            if (inventory.ContainsKey(name))
            {
                inventory[name].Quantity += quantity;
            }
        }

        public List<Food> GetInventory()
        {
            return new List<Food>(inventory.Values);
        }

        public void AddLocation(string address)
        {
            locations.Add(new SuperMarketLocation { Address = address });
        }

        public class SuperMarketLocation
        {
            public string Address { get; set; }
        }

        public class Food
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
    }
}

