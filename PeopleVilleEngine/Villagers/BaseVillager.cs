using PeopleVilleEngine;
using PeopleVilleEngine.Locations;
using PeopleVilleEngine.Villagers.Items;
using System;
using System.Collections.Generic;

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

    protected BaseVillager(Village village)
    {
        _village = village;
        IsMale = RNG.GetInstance().Next(0, 2) == 0;
        IsWhite = RNG.GetInstance().Next(0, 5) != 0;
        (FirstName, LastName) = village.VillagerNameLibrary.GetRandomNames(IsMale);
        if (Age >= 18)
        {
            Role = village.VillagerRoleLibrary.GetRandomRole();
            AssignRandomPet();
            ArmedVillager.AssignWeaponToVillager(this); // Assign weapon if over 18
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
            Console.WriteLine($"{FirstName} {LastName} kan ikke bære flere ting.");
        }
        else
        {
            Inventory.Add(item);
        }
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} ({Age} år) - Job: {Role} - Hobby: {Hobby}";
    }
}
