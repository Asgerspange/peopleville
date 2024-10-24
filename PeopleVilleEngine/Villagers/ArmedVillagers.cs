using PeopleVilleEngine;
using PeopleVilleEngine.Locations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Definerer interface IArmed
public interface IArmed
{
    string Weapon { get; }
    string WeaponDescription { get; }
    void LoadWeaponsFromJsonFile();
}

public class ArmedVillager : BaseVillager, IArmed
{
    public string Weapon { get; private set; }
    public string WeaponDescription { get; private set; }
    private static List<(string Name, string Description)> Weapons = new List<(string, string)>
    {
        ("Bat", "Et bat af træ"),
        ("Uzi", "Et kompakt maskinegevær"),
        ("Hammer", "Ganske almindelig hammer"),
        ("Kniv", "Skarp og lille"),
        ("Morgenstjerne", "Solid jernkugle med skaft og kæde"),
        ("Knojern", "Lavet af jern"),
        ("Pisk", "Lavet af læder")
    };

    private static List<(string Name, string Description)> _weapons = Weapons;

    public ArmedVillager(Village village) : base(village)
    {
        var weapon = AssignRandomWeapon();
        Weapon = weapon.Name;
        WeaponDescription = weapon.Description;
    }

    // Indlæser Weapondata fra en JSON-fil
    public void LoadWeaponsFromJsonFile()
    {
        string jsonFile = "lib\\weapons.json";
        if (!File.Exists(jsonFile))
            throw new FileNotFoundException(jsonFile);
         
        string jsonData = File.ReadAllText(jsonFile);
        var weaponsData = JsonSerializer.Deserialize<List<(string Name, string Description)>>(jsonData);
        if (weaponsData != null)
        {
            _weapons = weaponsData;
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
