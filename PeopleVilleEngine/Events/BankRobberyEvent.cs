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
        public string Title { get; set; } = "A bank robbery occurred.";

        public List<EventDetails> Execute(Village village)
        {
            var executedEvents = new List<EventDetails>();
            var robber = village.Villagers.FirstOrDefault();

            if (robber != null)
            {
                string description = $"{robber.FirstName} {robber.LastName} robbed the bank. And got away with $1.000";
                robber.PersonalWallet.AddMoney(1000m);
                Trace.WriteLine($"{robber.PersonalWallet}");
                executedEvents.Add(new EventDetails(Title, description, EventSeverity));
            }
            else
            {
                executedEvents.Add(new EventDetails(Title, "No bank robbery occurred.", EventSeverity));
            }

            return executedEvents;
        }
    }
}
