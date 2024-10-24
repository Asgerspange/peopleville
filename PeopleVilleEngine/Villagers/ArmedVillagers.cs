using PeopleVilleEngine;
using PeopleVilleEngine.Locations;
using System;
using System.Collections.Generic;

public class ArmedVillager : BaseVillager
{
    public string Weapon { get; private set; }
    public string WeaponDescription { get; private set; }
    private static readonly List<(string Name, string Description)> Weapons = new List<(string, string)>
    {
        ("Uzi", "Et kompakt maskinegevær"),
        ("Bat", "Et bat af træ"),
        ("Hammer", "Ganske almindelig hammer"),
        ("Kniv", "Skarp og lille"),
        ("Morgenstjerne", "Solid jernkugle med skaft og kæde"),
        ("Knojern", "Lavet af jern"),
        ("Pisk", "Lavet af læder")
    };

    public ArmedVillager(Village village) : base(village)
    {
        var weapon = AssignRandomWeapon();
        Weapon = weapon.Name;
        WeaponDescription = weapon.Description;
    }

    //constructor kalder base-klassen "Villager", "Weapon" initialisereres ved at kalde metoden "AssignRandomWeapon"
    private (string Name, string Description) AssignRandomWeapon()
    {
        try
        {
            var rng = RNG.GetInstance();
            int index = rng.Next(Weapons.Count);
            return Weapons[index];
        }
        catch (Exception ex)
        {
            // Error Handling: Logger undtagelsen og returnerer et "standard våben"
            Console.WriteLine($"Fejl ved tildeling af tilfældigt våben: {ex.Message}");
            return ("Suppe-Ske", "God til madlavning, dårlig til overlevelse");
        }
    }

    public override string ToString()
    {
        return $"{base.ToString()} - Bevæbnet med: {Weapon} ({WeaponDescription})";
    }
}
