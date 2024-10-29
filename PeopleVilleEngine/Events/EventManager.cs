using System;
using System.Collections.Generic;
using System.Linq;

namespace PeopleVilleEngine
{
    public class EventManager
    {
        private Village _village;

        public EventManager(ref Village village)
        {
            _village = village;
        }

        public List<EventDetails> ExecuteEvents()
        {
            var allExecutedEvents = new List<EventDetails>();
            var events = new List<IEvent>();

            LoadEvents(AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()), events);
            var random = new Random();
            int numberOfEventsToExecute = random.Next(1, events.Count);
            var selectedEvents = events.OrderBy(x => random.Next()).Take(numberOfEventsToExecute);

            foreach (var gameEvent in selectedEvents)
            {
                var executedEvents = gameEvent.Execute(ref _village);
                allExecutedEvents.AddRange(executedEvents);
            }

            return allExecutedEvents;
        }

        private void LoadEvents(IEnumerable<Type> types, List<IEvent> events)
        {
            var eventInterface = typeof(IEvent);
            var eventTypes = types.Where(p => eventInterface.IsAssignableFrom(p) && !p.IsInterface).ToList();

            foreach (var type in eventTypes)
            {
                events.Add((IEvent)Activator.CreateInstance(type));
            }
        }
    }
}
