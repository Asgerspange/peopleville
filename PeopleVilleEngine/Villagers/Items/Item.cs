// IItem Interface
public interface IItem
{
    string Name { get; set; }
    string Description { get; set; }

    // Metoder til at konvertere objekt til string
    string ToString();
    string ToString(bool includeWeaponDescription, string weaponDescription = "");
}

// Item-klassen implementerer IItem
public class Item : IItem
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Item(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public override string ToString()
    {
        return $"{Name}: {Description}";
    }

    public string ToString(bool includeWeaponDescription, string weaponDescription = "")
    {
        if (includeWeaponDescription && !string.IsNullOrEmpty(weaponDescription))
        {
            return $"{Name}: {Description} - Våben Beskrivelse: {weaponDescription}";
        }
        return ToString();
    }
}

// Weapon-klassen arver fra Item
public class Weapon : Item
{
    public string WeaponType { get; set; }

    public Weapon(string name, string description, string weaponType)
        : base(name, description)
    {
        WeaponType = weaponType;
    }

    public override string ToString()
    {
        return $"{Name}: {Description} - Type: {WeaponType}";
    }
}
