using PeopleVilleEngine;
using System.Text.Json;

public interface IArmed
{
    void LoadWeaponsFromJsonFile(string jsonFile);
}

public class ArmedVillager : BaseVillager, IArmed
{
    private static List<(string Name, string Description)> _weapons;

    static ArmedVillager()
    {
        var instance = new ArmedVillager(new Village());
        instance.LoadWeaponsFromJsonFile("lib\\weaponDescription.json");
    }

    public ArmedVillager(Village village) : base(village)
    {
        var weapon = AssignRandomWeapon();
        AddItem(weapon); // Tilføjer våbnet til Inventory
    }

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

    private Weapon AssignRandomWeapon()
    {
        try
        {
            var rng = RNG.GetInstance();
            int index = rng.Next(_weapons.Count);
            var weaponData = _weapons[index];
            return new Weapon(weaponData.Name, weaponData.Description, "DefaultType"); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved tildeling af tilfældigt våben: {ex.Message}");
            var defaultWeaponData = _weapons[0];
            return new Weapon(defaultWeaponData.Name, defaultWeaponData.Description, "DefaultType"); // 
        }
    }


    public override string ToString()
    {
        var inventoryItems = string.Join(", ", Inventory.Select(i => i.ToString()));
        return $"{base.ToString()} - Inventar: {inventoryItems}";
    }
}
