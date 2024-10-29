using PeopleVilleEngine;
using PeopleVilleEngine.Locations;
using PeopleVilleEngine.Villagers.Items;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class BaseVillager
{
    private const int MaxInventorySize = 10; // Maximum antal ting i inventory

    public int Age { get; protected set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Hobby { get; set; }
    public bool IsMale { get; set; }
    public bool IsWhite { get; set; }
    private Village _village;
    public ILocation? Home { get; set; } = null;
    public bool HasHome() => Home != null;
    public string Role { get; set; } = "Arbejdsløs";
    public Wallet PersonalWallet { get; private set; }
    public Pet? Pet { get; set; }
    public List<Item> Inventory { get; private set; } = new List<Item>();
    public Item? Weapon { get; private set; }

    protected BaseVillager(Village village)
    {
        _village = village;
        IsMale = RNG.GetInstance().Next(0, 2) == 0;
        IsWhite = RNG.GetInstance().Next(0, 5) != 0;
        (FirstName, LastName) = village.VillagerNameLibrary.GetRandomNames(IsMale);
        Age = RNG.GetInstance().Next(18, 60);
        if (Age >= 18)
        {
            Role = village.VillagerRoleLibrary.GetRandomRole();
            AssignRandomPet();
            Console.WriteLine($"Tildelt våben til {FirstName} {LastName}"); // Debug output
            ArmedVillager.AssignWeaponToVillager(this);
            Console.WriteLine($"Våben tildelt: {Weapon?.Name ?? "Ingen"}"); // Debug output
        }
        Hobby = village.VillagerHobbyLibrary.GetRandomHobby();
        PersonalWallet = new Wallet("$", 100m);
    }

    private void AssignRandomPet()
    {
        try
        {
            var petType = Pet.AssignRandomPetType();
            var petName = GenerateRandomPetName();
            Pet = new Pet(petName, petType, new Random().Next(1, 11), _village);
        }
        catch (Exception ex) // Error handling
        {
            Console.WriteLine($"Fejl ved tildeling af kæledyr: {ex.Message}");
            var petName = GenerateRandomPetName();
            var fallbackPetType = "Regnorm";

            // Sikre at fallbackPetType er tilføjet til listen over mulige kæledyrstyper
            if (!Pet.PossiblePetTypes.Contains(fallbackPetType))
            {
                Pet.PossiblePetTypes.Add(fallbackPetType);
            }

            Pet = new Pet(petName, fallbackPetType, 1, _village);
        }
    }

    public void CheckPetAge()
    {
        if (Pet != null && Pet.Age >= 10)
        {
            RemovePet();
        }
    }

    public void RemovePet()
    {
        Pet = null;
        Console.WriteLine($"Kæledyret er blevet fjernet fra {FirstName} {LastName}, da det er blevet 10 år.");
    }

    private string GenerateRandomPetName()
    {
        var petNames = new List<string> { "Buddy", "Bella", "Charlie", "Max", "Luna", "Rocky", "Molly", "Daisy", "Bailey", "Coco" };
        var random = new Random();
        return petNames[random.Next(petNames.Count)];
    }

    public void AddItem(Item item)
    {
        if (Inventory.Count >= MaxInventorySize)
        {
            Console.WriteLine($"{FirstName} {LastName} Kan ikke være flere ting.");
        }
        else
        {
            Inventory.Add(item);
            Console.WriteLine($"Added {item.Name} to {FirstName} {LastName}'s inventory."); // Debug output

            if (item.IsWeapon)
            {
                Weapon = item;
                Console.WriteLine($"{item.Name} er et våben og er blevet sat som {FirstName} {LastName}'s våben."); // Debug output
            }
        }
    }

    public override string ToString()
    {
        string weaponInfo = Weapon != null ? $" - bevæbnet med: {Weapon.Name} ({Weapon.Description})" : "";
        string hobbiesInfo = !string.IsNullOrEmpty(Hobby) ? $" - Hobby: {Hobby}" : "";

        return $"{FirstName} {LastName}, Age: {Age}, Rolle: {Role}{weaponInfo}{hobbiesInfo}";
    }
}
