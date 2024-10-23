using PeopleVilleEngine;
using PeopleVilleEngine.Locations;
using System.Collections.Generic;

public class ArmedVillager : BaseVillager
{
    public string Weapon { get; private set; }
    private static readonly List<string> Weapons = new List<string> { "Uzi", "Bat", "Hammer", "Kniv", "Morgenstjerne", "Knojern", "Pisk" };

    public ArmedVillager(Village village) : base(village)
    {
        Weapon = AssignRandomWeapon();
    }

    //constructor kalder base-klassen "Villager", "Weapon" initialisereres ved at kalde metoden "AssignRandomWeapon"
    private string AssignRandomWeapon()
    {
        var rng = RNG.GetInstance();
        int index = rng.Next(Weapons.Count);
        return Weapons[index];
    }

    public override string ToString()
    {
        return $"{base.ToString()} - Armed with: {Weapon}";
    }
}
