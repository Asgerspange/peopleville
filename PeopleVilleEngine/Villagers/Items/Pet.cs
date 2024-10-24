using PeopleVilleEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
 
// Interface for Pet
public interface IPet
{
    Guid Id { get; }
    string Name { get; set; }
    string Type { get; set; }
    int Age { get; set; }
    void CheckAndRemovePet();
}

// Base class for Animal
public abstract class Animal
{
    public Guid Id { get; protected set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Age { get; set; }

    protected Animal(string name, string type, int age)
    {
        Id = Guid.NewGuid();
        Name = name;
        Type = type;
        Age = age;
    }

    public abstract void CheckAndRemovePet();
}

//Interface der nedarves fra Animal
public class Pet : Animal, IPet
{
    public static readonly List<string> PossiblePetTypes = new List<string> { "Hund", "Kat", "Gris", "Ko", "Hest" };
    private static List<string> _possiblePetTypes = PossiblePetTypes;
    private Village _village;

    public Pet(string name, string type, int age, Village village) : base(name, type, age)
    {
        _village = village;
    }

    // Indlæser kæledyrstyper fra en JSONfil
    public static void LoadPetTypesFromJsonFile()
    {
        string jsonFile = "lib\\petTypes.json";
        if (!File.Exists(jsonFile))
            throw new FileNotFoundException(jsonFile);

        string jsonData = File.ReadAllText(jsonFile);
        var petTypesData = JsonSerializer.Deserialize<List<string>>(jsonData);
        if (petTypesData != null)
        {
            _possiblePetTypes = petTypesData;
        }
    }

    // tildeler en tilfældig kæledyrstype fra listen over mulige typer
    public static string AssignRandomPetType()
    {
        try
        {
            // Bruger RNG
            var rng = RNG.GetInstance();
            int index = rng.Next(_possiblePetTypes.Count);

            return _possiblePetTypes[index];
        }
        catch (Exception ex)
        {
            // Error Handling: Logger undtagelsen og returnerer en standard kæledyrstype
            Console.WriteLine($"Fejl ved tildeling af tilfældigt kæledyr: {ex.Message}");
            return _possiblePetTypes[0]; // Returnerer den første type i listen som standard
        }
    }

    public override void CheckAndRemovePet()
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
            Console.WriteLine($"Kæledyret {Name} (ID: {Id}) er blevet fjernet fra ejeren, da kæledyret er blevet 10 år.");
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
