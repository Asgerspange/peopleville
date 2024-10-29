using System.Collections.Generic;
using System.Linq;
using PeopleVilleEngine;

namespace PeopleVilleEngine
{
    public class VillagerConflictEvent : IEvent
    {
        public EventSeverityLevel EventSeverity { get; set; } = EventSeverityLevel.Critical;
        public string Type { get; set; } = EventType.Murder;
        public string Title { get; set; } = "A villager conflict led to a fatality.";

        public List<EventDetails> Execute(ref Village village)
        {
            var executedEvents = new List<EventDetails>();
            var attacker = village.Villagers.FirstOrDefault(v => v.Role == "Kriminel");
            var victim = village.Villagers.FirstOrDefault(v => v != attacker);

            if (victim != null && attacker != null)
            {
                village.Villagers.Remove(victim);

                for (int i = village.Locations.Count - 1; i >= 0; i--)
                {
                    var location = village.Locations[i];

                    if (location.Villagers().Contains(victim))
                    {
                        location.Villagers().Remove(victim);
                    }

                    if (location.Villagers().Count == 0)
                    {
                        village.Locations.RemoveAt(i);
                    }
                }

                string description = $"{attacker.FirstName} {attacker.LastName} killed {victim.FirstName} {victim.LastName} in a conflict.";
                executedEvents.Add(new EventDetails(Title, description, EventSeverity));
            }
            else
            {
                executedEvents.Add(new EventDetails(Title, "failed", EventSeverity));
            }

            return executedEvents;
        }
    }
}
