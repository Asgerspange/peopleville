using PeopleVilleEngine;
using System.Text.Json;

public interface IArmed
{
    string Weapon { get; }
    string WeaponDescription { get; }
    void LoadWeaponsFromJsonFile(string jsonFile);
}

public class ArmedVillager : BaseVillager, IArmed
{
    public string Weapon { get; private set; }
    public string WeaponDescription { get; private set; }
    private static List<(string Name, string Description)> _weapons;

    static ArmedVillager()
    {
        var instance = new ArmedVillager(new Village());
        instance.LoadWeaponsFromJsonFile("lib\\weaponDescription.json");
    }

    public ArmedVillager(Village village) : base(village)
    {
        var weapon = AssignRandomWeapon();
        Weapon = weapon.Name;
        WeaponDescription = weapon.Description;
    }

    // Indlæser Weapondata fra en JSON-fil
    public void LoadWeaponsFromJsonFile(string jsonFile)
    {
        if (!File.Exists(jsonFile))
            throw new FileNotFoundException(jsonFile);

        string jsonData = File.ReadAllText(jsonFile);
        var weaponsData = JsonSerializer.Deserialize<List<(string Name, string Description)>>(jsonData);
        if (weaponsData != null)
        {
            _weapons = weaponsData;
        }
        else
        {
            _weapons = new List<(string Name, string Description)>();
        }
    }

    // Constructor kalder base-klassen "Villager", "Weapon" initialisereres ved at kalde metoden "AssignRandomWeapon"
    private (string Name, string Description) AssignRandomWeapon()
    {
        try
        {
            var rng = RNG.GetInstance();
            int index = rng.Next(_weapons.Count);
            return _weapons[index];
        }
        catch (Exception ex)
        {
            // Error Handling: Logger undtagelsen og returnerer et "standard våben"
            Console.WriteLine($"Fejl ved tildeling af tilfældigt våben: {ex.Message}");
            return _weapons[0]; // Returnerer det første våben i listen som standard
        }
    }

    public override string ToString()
    {
        return $"{base.ToString()} - Bevæbnet med: {Weapon} ({WeaponDescription})";
    }
}
