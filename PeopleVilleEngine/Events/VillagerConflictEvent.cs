using System.Collections.Generic;
using System.Linq;
using PeopleVilleEngine;

namespace PeopleVilleEngine
{
    public class VillagerConflictEvent : IEvent
    {
        public EventSeverityLevel EventSeverity { get; set; } = EventSeverityLevel.Critical;
        public string Type { get; set; } = EventType.Murder;
        public string Title { get; set; } = "En konflikt mellem landsbyboere førte til en dødsfald.";

        public List<EventDetails> Execute(Village village)
        {
            var executedEvents = new List<EventDetails>();
            var victim = village.Villagers.FirstOrDefault(v => v.IsWhite);
            var attacker = village.Villagers.FirstOrDefault(v => !v.IsWhite && v != victim);

            if (victim != null && attacker != null)
            {
                village.Villagers.Remove(victim);
                string description = $"{attacker.FirstName} {attacker.LastName} dræbte {victim.FirstName} {victim.LastName} i en konflikt.";
                executedEvents.Add(new EventDetails(Title, description, EventSeverity));
            }
            else
            {
                executedEvents.Add(new EventDetails(Title, "Ingen konflikt opstod.", EventSeverity));
            }

            return executedEvents;
        }
    }
}
