namespace PeopleVilleEngine.Buildings
{
    internal class Houses
    {
        private List<House> houses = new List<House>();

        public void AddHouse(string address, string owner, int numberOfRooms)
        {
            var house = new House
            {
                Id = Guid.NewGuid(),
                Address = address,
                Owner = owner,
                NumberOfRooms = numberOfRooms,
                Residents = new List<Resident>()
            };
            houses.Add(house);
        }

        public void AddResident(Guid houseId, string name, int age)
        {
            var house = houses.Find(h => h.Id == houseId);
            if (house != null)
            {
                house.Residents.Add(new Resident
                {
                    Name = name,
                    Age = age
                });
            }
        }

        public List<House> GetHouses()
        {
            return new List<House>(houses);
        }

        public void RequestMaintenance(Guid houseId, string issue)
        {
            var house = houses.Find(h => h.Id == houseId);
            if (house != null)
            {
                Console.WriteLine($"Maintenance requested for house at {house.Address}: {issue}");
            }
        }
    }

    internal class House
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Owner { get; set; }
        public int NumberOfRooms { get; set; }
        public List<Resident> Residents { get; set; }
    }

    internal class Resident
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

