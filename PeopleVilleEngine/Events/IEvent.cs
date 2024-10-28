using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace PeopleVilleEngine
{
    public enum EventSeverityLevel
    {
        Low = 1,
        Moderate = 2,
        High = 3,
        Critical = 4
    }

    public static class EventType
    {
        public const string
            NaturalDisaster = "Natur Katastrofe",
            Murder = "Mord",
            BankRobbery = "Bankrøveri",
            Robbery = "Robbery";
    }
    public class EventDetails
    {
        public EventDetails( string title, string description, EventSeverityLevel severity)
        {
            Title = title;
            Description = description;
            Severity = severity;
        }
        public string Title;
        public string Description;
        public EventSeverityLevel Severity;
    }
    public interface IEvent
    {
        string Type { get; set; }
        EventSeverityLevel EventSeverity { get; set; }
        string Title { get; set; }
        List<EventDetails> Execute(ref Village village);
    }
}
