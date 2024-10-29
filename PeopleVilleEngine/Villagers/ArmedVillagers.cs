using PeopleVilleEngine;
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public interface IArmed
{
    Item Weapon { get; }
    string WeaponDescription { get; }
    void LoadWeaponsFromJsonFile(string jsonFile);
}

public class ArmedVillager : BaseVillager, IArmed
{
    public new Item Weapon { get; private set; }
    public string WeaponDescription { get; private set; }
    private static List<(string Name, string Description)> _weapons;

    static ArmedVillager()
    {
        LoadWeaponsFromJsonFileStatic("lib\\weaponDescription.json");
    }

    public ArmedVillager(Village village) : base(village)
    {
        var weapon = AssignRandomWeapon();
        Weapon = new Item(weapon.Name, weapon.Description);
        WeaponDescription = weapon.Description;
    }

    public void LoadWeaponsFromJsonFile(string jsonFile)
    {
        LoadWeaponsFromJsonFileStatic(jsonFile);
    }

    private static void LoadWeaponsFromJsonFileStatic(string jsonFile)
    {
        try
        {
            var weaponsData = new WeaponData
            {
                WeaponDescription = new List<(string Name, string Description)>
                    {
                        ("Bat", "Et bat af træ"),
                        ("Uzi", "Et kompakt maskinegevær"),
                        ("Hammer", "Ganske almindelig hammer"),
                        ("Kniv", "Skarp og lille"),
                        ("Morgenstjerne", "Solid jernkugle med skaft og kæde"),
                        ("Knojern", "Lavet af jern"),
                        ("Pisk", "Lavet af læder")
                    }
            };


            if (weaponsData != null && weaponsData.WeaponDescription != null)
            {
                _weapons = weaponsData.WeaponDescription;
                Console.WriteLine($"Successfully loaded {_weapons.Count} våben fra {jsonFile}.");
            }
            else
            {
                _weapons = new List<(string Name, string Description)>();
                Console.WriteLine("Ingen våben fundet i JSON fil.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved loading af våben fra JSON file: {ex.Message}");
            _weapons = new List<(string Name, string Description)>();
        }
    }

    private class WeaponData
    {
        public List<(string Name, string Description)> WeaponDescription { get; set; } = new List<(string Name, string Description)>();
    }

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
            Console.WriteLine($"Fejl ved tildeling af våben: {ex.Message}");
            return _weapons[0];
        }
    }

    private void AddWeaponToInventory((string Name, string Description) weapon)
    {
        var weaponItem = new Item(weapon.Name, weapon.Description);
        AddItem(weaponItem);
    }

    public static void AssignWeaponToAdult(BaseVillager villager)
    {
        if (_weapons == null || _weapons.Count == 0)
        {
            throw new InvalidOperationException("Weapons collection is not initialized.");
        }

        var weapon = _weapons[RNG.GetInstance().Next(_weapons.Count)];
        var weaponItem = new Item(weapon.Name, weapon.Description, true);
        villager.AddItem(weaponItem);
        Console.WriteLine($"Weapon {weaponItem.Name} tildelt til {villager.FirstName} {villager.LastName}"); // Debug output
    }

    public static void AssignWeaponToVillager(BaseVillager villager)
    {
        if (_weapons == null || _weapons.Count == 0)
        {
            throw new InvalidOperationException("Weapons collection is not initialized.");
        }

        var weapon = _weapons[RNG.GetInstance().Next(_weapons.Count)];
        var weaponItem = new Item(weapon.Name, weapon.Description, true);
        villager.AddItem(weaponItem);
        Console.WriteLine($"Weapon {weaponItem.Name} assigned to {villager.FirstName} {villager.LastName}"); // Debug output
    }

    public override string ToString()
    {
        return $"{base.ToString()} - Bevæbnet med: {Weapon.Name} ({Weapon.Description})";
    }
}