namespace PeopleVilleEngine.Locations.Buildings.Prison
{
    public class Inmate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Crime { get; set; }
        public int CellNumber { get; set; }
    }
}
