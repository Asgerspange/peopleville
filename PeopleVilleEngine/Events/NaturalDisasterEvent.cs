using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Events
{
    public class NaturalDisasterEvent : IEvent
    {
        public string Type { get; set; } = EventType.NaturalDisaster;
        public EventSeverityLevel EventSeverity { get; set; }
        public string Title { get; set; } = "A natural disaster has struck the village!";

        public List<EventDetails> Execute(ref Village village)
        {
            var eventDetails = new List<EventDetails>();

            foreach (var villager in village.Villagers)
            {
                if (new Random().NextDouble() < 0.3)
                {
                    eventDetails.Add(new EventDetails
                    (
                        Title,
                        $"{villager.FirstName + " " + villager.LastName} was affected by the natural disaster.",
                        EventSeverity
                    ));
                }
            }

            return eventDetails;
        }
    }
}
