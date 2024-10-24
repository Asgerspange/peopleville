namespace PeopleVilleEngine.Locations.Buildings.TownHall
{
    internal class TownHall
    {
        private List<Citizen> citizens = new List<Citizen>();

        public void RegisterCitizen(string name, int age)
        {
            var citizen = new Citizen
            {
                Id = Guid.NewGuid(),
                Name = name,
                Age = age
            };
            citizens.Add(citizen);
        }

        public List<Citizen> GetCitizens()
        {
            return new List<Citizen>(citizens);
        }

        public void HoldMeeting(string topic)
        {
            Console.WriteLine($"Town meeting on '{topic}' is being held.");
        }

        public void IssuePermit(string permitType, string citizenName)
        {
            Console.WriteLine($"Permit '{permitType}' issued to {citizenName}.");
        }
    }
}
