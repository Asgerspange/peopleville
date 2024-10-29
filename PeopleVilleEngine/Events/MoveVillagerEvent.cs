using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Events
{
    public class MoveVillagerEvent : IEvent
    {
        public string Type { get; set; } = EventType.MoveVillager;
        public EventSeverityLevel EventSeverity { get; set; } = EventSeverityLevel.Low;
        public string Title { get; set; } = "Move Villager Event";

        public List<EventDetails> Execute(ref Village village)
        {
            var random = new Random();
            var villager = village.Villagers[random.Next(village.Villagers.Count)];
            var fromLocation = village.Locations.FirstOrDefault(loc => loc.Villagers().Contains(villager));
            var toLocation = village.Locations[random.Next(village.Locations.Count)];

            if (villager == null || fromLocation == null || toLocation == null)
            {
                return new List<EventDetails>
                {

                };
            }
            return new List<EventDetails>
                {
                    new EventDetails(
                        Title,
                        description: $"Moved {villager.FirstName} from {fromLocation.Name} to {toLocation.Name}.",
                        EventSeverity
                    )
                };
        }
    }
}
