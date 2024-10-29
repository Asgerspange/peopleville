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
        public string Title { get; set; } = "En naturkatastrofe har ramt landsbyen!";

        public List<EventDetails> Execute(Village village)
        {
            var eventDetails = new List<EventDetails>();

            foreach (var villager in village.Villagers)
            {
                if (new Random().NextDouble() < 0.3)
                {
                    eventDetails.Add(new EventDetails
                    (
                        Title,
                        $"{villager.FirstName + " " + villager.LastName} blev ramt af naturkatastrofen.",
                        EventSeverity
                    ));
                }
            }

            return eventDetails;
        }
    }
}
