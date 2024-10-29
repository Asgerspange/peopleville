using System.Diagnostics;
using System.Text.Json;

public interface IItem
{
    string Name { get; set; }
    string Description { get; set; }

    // Metoder til at konvertere objekt til string
    string ToString();
    string ToString(bool includeWeaponDescription, string weaponDescription = "");
}

public class Item : IItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual bool IsWeapon { get; set; } // Ensure this property is set correctly

    public Item(string name, string description, bool isWeapon = false)
    {
        Name = name;
        Description = description;
        IsWeapon = isWeapon;
    }

    public override string ToString()
    {
        return Name;
    }

    public string ToString(bool includeWeaponDescription, string weaponDescription = "")
    {
        throw new NotImplementedException();
    }
}

public class Weapon : Item
{
    public string WeaponType { get; set; }

    public override bool IsWeapon => true;

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
    private static List<ChildrenToys> toyList = new List<ChildrenToys>();

    public ChildrenToys(string name, string description)
        : base(name, description)
    {
    }

    public static void LoadToysFromJsonFile()
    {
        string jsonFile = "lib\\toys.json";
        if (!File.Exists(jsonFile))
        {
            Trace.WriteLine($"File not found: {jsonFile}");
            throw new FileNotFoundException(jsonFile);
        }

        string jsonData = File.ReadAllText(jsonFile);
        try
        {
            var jsonObject = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, string>>>>(jsonData);
            if (jsonObject != null && jsonObject.ContainsKey("Legetøj"))
            {
                toyList = jsonObject["Legetøj"].Select(toy => new ChildrenToys(toy["name"], toy["description"])).ToList();
                Trace.WriteLine($"Loaded {toyList.Count} toys.");
            }
            else
            {
                Trace.WriteLine("Failed to deserialize toys data.");
            }
        }
        catch (JsonException ex)
        {
            Trace.WriteLine($"JSON Deserialization error: {ex.Message}");
        }
    }

    public static int GetToyListCount()
    {
        return toyList.Count;
    }

    public static ChildrenToys GetRandomToy()
    {
        if (toyList == null || toyList.Count == 0)
        {
            throw new InvalidOperationException("toyList is not initialized or empty.");
        }

        var random = new Random();
        return toyList[random.Next(toyList.Count)];
    }
}
