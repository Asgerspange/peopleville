﻿namespace PeopleVilleEngine.Buildings
{
    internal class Prison
    {
        private List<Inmate> inmates = new List<Inmate>();
        private Dictionary<int, List<Inmate>> cells = new Dictionary<int, List<Inmate>>();

        public void RegisterInmate(string name, int age, string crime)
        {
            var inmate = new Inmate
            {
                Id = Guid.NewGuid(),
                Name = name,
                Age = age,
                Crime = crime,
                CellNumber = -1 // Unassigned
            };
            inmates.Add(inmate);
        }

        public void AssignCell(Guid inmateId, int cellNumber)
        {
            var inmate = inmates.Find(i => i.Id == inmateId);
            if (inmate != null)
            {
                if (!cells.ContainsKey(cellNumber))
                {
                    cells[cellNumber] = new List<Inmate>();
                }
                inmate.CellNumber = cellNumber;
                cells[cellNumber].Add(inmate);
            }
        }

        public List<Inmate> GetInmates()
        {
            return new List<Inmate>(inmates);
        }

        public void RecordIncident(string description)
        {
            Console.WriteLine($"Security incident recorded: {description}");
        }

        public void ScheduleVisit(Guid inmateId, string visitorName, DateTime visitDate)
        {
            var inmate = inmates.Find(i => i.Id == inmateId);
            if (inmate != null)
            {
                Console.WriteLine($"Visit scheduled for {visitorName} to see {inmate.Name} on {visitDate}");
            }
        }
    }

    internal class Inmate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Crime { get; set; }
        public int CellNumber { get; set; }
    }
}
