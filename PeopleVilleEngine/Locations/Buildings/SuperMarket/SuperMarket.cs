namespace PeopleVilleEngine.Locations.Buildings.SuperMarket
{
    public class SuperMarketLocation : ILocation
    {
        public string Name { get; set; } 

        public List<BaseVillager> Villagers() 
        {
            return new List<BaseVillager>();
        }
    }

    public class SuperMarket : ILocation
    {
        private Dictionary<string, Food> inventory = new Dictionary<string, Food>();
        private List<SuperMarketLocation> locations = new List<SuperMarketLocation>();

        public string Name { get; set; }

        public SuperMarket()
        {
            AddFoodItem(new Apples());
            AddFoodItem(new Oranges());
            AddFoodItem(new Bananas());
        }

        public List<BaseVillager> Villagers() 
        {
            return new List<BaseVillager>();
        }

        public void AddFoodItem(Food food)
        {
            if (inventory.ContainsKey(food.Name))
            {
                inventory[food.Name].Quantity += food.Quantity;
            }
            else
            {
                inventory[food.Name] = food;
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

        public void AddLocation(string name)
        {
            locations.Add(new SuperMarketLocation { Name = name });
        }
    }
}
