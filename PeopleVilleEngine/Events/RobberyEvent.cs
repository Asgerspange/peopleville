using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleVilleEngine.Events
{
    public class RobberyEvent : IEvent
    {
        public string Type { get; set; } = EventType.Robbery;
        public EventSeverityLevel EventSeverity { get; set; }
        public string Title { get; set; } = "A robbery occurred.";

        public List<EventDetails> Execute(ref Village village)
        {
            var executedEvents = new List<EventDetails>();
            var villagers = village.Villagers.OrderBy(v => Guid.NewGuid()).Take(2).ToList();

            if (villagers.Count == 2)
            {
                var robber = village.Villagers.FirstOrDefault(v => v.Role == "Kriminel");
                var victim = village.Villagers.FirstOrDefault(v => v != robber);
                if (robber == null || victim == null)
                {
                    executedEvents.Add(new EventDetails(Title, $"There is no criminal.", EventSeverity));
                    return executedEvents;
                }
                decimal stolenAmount = victim.PersonalWallet.Money;

                if (victim.PersonalWallet.Money == 0)
                {
                    executedEvents.Add(new EventDetails(Title, $"{victim.FirstName} {victim.LastName} has no money to steal.", EventSeverity));
                    return executedEvents;
                }

                string description = $"{robber.FirstName} {robber.LastName} robbed {victim.FirstName} {victim.LastName} and got away with {stolenAmount}";
                robber.PersonalWallet.AddMoney(stolenAmount);
                victim.PersonalWallet.SpendMoney(stolenAmount);
                executedEvents.Add(new EventDetails(Title, description, EventSeverity));
            }
            else
            {
                executedEvents.Add(new EventDetails(Title, "No robbery occurred.", EventSeverity));
            }

            return executedEvents;
        }
    }
}
