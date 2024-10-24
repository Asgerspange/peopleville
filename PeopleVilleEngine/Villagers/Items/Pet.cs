using PeopleVilleEngine;
using System;
using System.Collections.Generic;

public class Pet
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Age { get; set; }
    public static readonly List<string> PossiblePetTypes = new List<string> { "Hund", "Kat", "Gris", "Ko", "Hest" };
    private Village _village;

    public Pet(string name, string type, int age, Village village)
    {
        Id = Guid.NewGuid();
        Name = name;
        Type = type;
        Age = age;
        _village = village;
    }

    // Metode til at tildele en tilfældig kæledyrstype fra listen over mulige typer
    public static string AssignRandomPetType()
    {
        try
        {
            // Henter en instans af RNG
            var rng = RNG.GetInstance();
            // Genererer et tilfældigt indeks ud fra mulige typer kæledyr
            int index = rng.Next(PossiblePetTypes.Count);
            
            return PossiblePetTypes[index];
        }
        catch (Exception ex)
        {
            // Error Handling: Logger undtagelsen og returnerer en "Regnorm" som standard
            Console.WriteLine($"Fejl ved tildeling af tilfældigt kæledyr: {ex.Message}");
            return "Regnorm";
        }
    }

    public void CheckAndRemovePet()
    {
        if (Age >= 10)
        {
            RemovePetFromOwner();
        }
    }

    private void RemovePetFromOwner()
    {
        var owner = FindOwner();
        if (owner != null)
        {
            owner.RemovePet();
            Console.WriteLine($"Kæledyret {Name} (ID: {Id}) er blevet fjernet fra ejeren, da det er blevet 10 år.");
        }
    }

    private BaseVillager? FindOwner()
    {
        foreach (var villager in _village.Villagers)
        {
            if (villager.Pet?.Id == this.Id)
            {
                return villager;
            }
        }
        return null;
    }

    public override string ToString()
    {
        return $"{Name} ({Type}, {Age} år)";
    }
}
