using System;
using System.Threading;

namespace PeopleVilleEngine
{
    public class Time
    {
        public string Day { get; set; } = "Day 1";
        public int dayCount = 1;

        public event Action NewDayStarted;


        public string UpdateDay()
        {
            dayCount++;
            Day = $"Day {dayCount}";
            OnNewDayStarted();

            return this.ToString();
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