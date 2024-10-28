namespace PeopleVilleEngine.Villagers.Items
{
    // Weapon-klassen arver fra Item
    public class Weapon : Item
    {
        public string Description { get; }

        public Weapon(string name, string description) : base(name, description)
        {
            Description = description;
        }

        public override string ToString()
        {
            return $"{Name} ({Description})";
        }
    }
}
