using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine
{
    public class BankRobberyEvent : IEvent
    {

        public EventSeverityLevel EventSeverity { get; set; } = EventSeverityLevel.Critical;
        public string Type { get; set; } = EventType.Robbery;
        public string Title { get; set; } = "Et bankrøveri fandt sted.";

        public List<EventDetails> Execute(Village village)
        {
            var executedEvents = new List<EventDetails>();
            var robber = village.Villagers.FirstOrDefault();

            if (robber != null)
            {
                string description = $"{robber.FirstName} {robber.LastName} røvede banken. Og slap væk med 1.000 kr.";
                robber.PersonalWallet.AddMoney(1000m);
                Trace.WriteLine($"{robber.PersonalWallet}");
                executedEvents.Add(new EventDetails(Title, description, EventSeverity));
            }
            else
            {
                executedEvents.Add(new EventDetails(Title, "Intet bankrøveri fandt sted.", EventSeverity));
            }

            return executedEvents;
        }
    }
}
