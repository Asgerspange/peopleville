namespace PeopleVilleEngine.Buildings.Houses
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
}

