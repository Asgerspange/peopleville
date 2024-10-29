using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Events
{
    public class BuyStuffEvent : IEvent
    {
        public string Type { get; set; } = EventType.Buy;
        public EventSeverityLevel EventSeverity { get; set; } = EventSeverityLevel.Low;
        public string Title { get; set; } = "Went to SuperMarket";

        public List<EventDetails> Execute(ref Village village)
        {
            var random = new Random();
            var villager = village.Villagers[random.Next(village.Villagers.Count)];
            var itemsForSale = new List<(string Name, decimal Price)>
                {
                    ("Banan", 2m),
                    ("Æble", 1m),
                    ("Appelsin", 3m),
                };

            foreach (var item in itemsForSale)
            {
                var price = item.Price;
                var name = item.Name;

                if (villager.PersonalWallet.Money - price <= 0)
                {
                    return new List<EventDetails>
                    {
                        new EventDetails(
                            title: "Bought Food",
                            description: "failed",
                            severity: EventSeverityLevel.Low
                        )
                    };
                }

                villager.PersonalWallet.SpendMoney(price);
                Item foodItem = new Item(name, name);

                villager.Inventory.Add(foodItem);
            }

            return new List<EventDetails>
            {
                new EventDetails(
                    title: "Bought Food",
                    description: $"{villager.FirstName} bought some food.",
                    severity: EventSeverityLevel.Low
                )
            };
        }
    }
}
