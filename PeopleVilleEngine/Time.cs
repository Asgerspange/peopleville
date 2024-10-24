using System;
using System.Threading;

namespace PeopleVilleEngine
{
    public class Time
    {
        public string Day { get; private set; } = "Day 1";
        private int dayCount = 1;

        public event Action NewDayStarted;

        public Time()
        {
            Thread timeThread = new Thread(CheckForKeyPress);
            timeThread.Start();
        }

        private void CheckForKeyPress()
        {
            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
                {
                    UpdateDay();
                }
            }
        }

        private void UpdateDay()
        {
            dayCount++;
            Day = $"Day {dayCount}";
            OnNewDayStarted();

            Console.SetCursorPosition(0, 5);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 5);
            Console.Write(this.ToString());
        }

        protected virtual void OnNewDayStarted()
        {
            NewDayStarted?.Invoke();
        }

        public override string ToString()
        {
            return Day;
        }
    }
}
