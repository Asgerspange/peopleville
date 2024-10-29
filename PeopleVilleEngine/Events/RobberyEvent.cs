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
        public string Title { get; set; } = "Et røveri fandt sted.";

        public List<EventDetails> Execute(Village village)
        {
            var executedEvents = new List<EventDetails>();
            var villagers = village.Villagers.OrderBy(v => Guid.NewGuid()).Take(2).ToList();

            if (villagers.Count == 2)
            {
                var robber = village.Villagers.FirstOrDefault(v => v.Role == "Kriminel");
                var victim = village.Villagers.FirstOrDefault(v => v != robber);
                if (robber == null || victim == null)
                {
                    executedEvents.Add(new EventDetails(Title, $"Der er ingen kriminel.", EventSeverity));
                    return executedEvents;
                }
                decimal stolenAmount = victim.PersonalWallet.Money;

                if (victim.PersonalWallet.Money == 0)
                {
                    executedEvents.Add(new EventDetails(Title, $"{victim.FirstName} {victim.LastName} har ingen penge at stjæle.", EventSeverity));
                    return executedEvents;
                }

                string description = $"{robber.FirstName} {robber.LastName} røvede {victim.FirstName} {victim.LastName} og slap væk med {stolenAmount}";
                robber.PersonalWallet.AddMoney(stolenAmount);
                victim.PersonalWallet.SpendMoney(stolenAmount);
                executedEvents.Add(new EventDetails(Title, description, EventSeverity));
            }
            else
            {
                executedEvents.Add(new EventDetails(Title, "Intet røveri fandt sted.", EventSeverity));
            }

            return executedEvents;
        }
    }
}
