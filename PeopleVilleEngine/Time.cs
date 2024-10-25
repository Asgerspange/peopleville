using System;
using System.Threading;

namespace PeopleVilleEngine
{
    public class Time
    {
        public string Day { get; set; } = "Day 1";
        public int dayCount = 1;
        private readonly Village _village;

        public event Action NewDayStarted;



        public Time(Village village)
        {
            _village = village;
        }

        public string UpdateDay()
        {
            dayCount++;
            Day = $"Day {dayCount}";
            OnNewDayStarted();
            TriggerDailyEvents();

            return this.ToString();
        }
        private void TriggerDailyEvents()
        {
            
        }

        public virtual void OnNewDayStarted()
        {
            NewDayStarted?.Invoke();
        }

        public override string ToString()
        {
            return Day;
        }
    }
}