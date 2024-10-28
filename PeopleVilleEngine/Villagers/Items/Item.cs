public interface IItem
{
    // Interface metode til at få navn
    string Name { get; set; }

    // Interface metode til at få beskrivelse
    string Description { get; set; }

    //konverterer objektet til en streng
    string ToString();

    //konverterer objekt til streng med weapondescription
    string ToString(bool includeWeaponDescription, string weaponDescription = "");
}

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

// Nedarver fra Item klassen
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

// Nedarver fra Item klassen
public class ChildrenToys : Item
{
    private static List<ChildrenToys> toyList;

    public ChildrenToys(string name, string description)
        : base(name, description)
    {
    }

    public static void LoadToysFromJsonFile()
    {
        // Implement logic to load toys from a JSON file
    }

    public static ChildrenToys GetRandomToy()
    {
        var random = new Random();
        return toyList[random.Next(toyList.Count)];
    }
}
