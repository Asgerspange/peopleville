using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PeopleVilleEngine.Villagers.Items
{
    // Interface for ChildrenToys
    public interface IChildrenToys
    {
        string Name { get; set; }
        string Description { get; set; }
    }

    // Base class for Toy
    public abstract class Toy
    {
        public string Name { get; set; }
        public string Description { get; set; }

        protected Toy(string name, string description = "Legetøj")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Legetøjets navn kan ikke være null eller tomt.", nameof(name));
            }
             
            Name = name;
            Description = description;
        }
    }

    public class ChildrenToys : Toy, IChildrenToys
    {
        // Statisk liste af legetøj
        private static List<ChildrenToys> toyList = new List<ChildrenToys>();

        public ChildrenToys(string name, string description = "Legetøj") : base(name, description)
        {
        }

        // Indlæser legetøjsdata fra en JSON fil
        public static void LoadToysFromJsonFile()
        {
            string jsonFile = "lib\\toys.json";
            if (!File.Exists(jsonFile))
                throw new FileNotFoundException(jsonFile);

            string jsonData = File.ReadAllText(jsonFile);
            var toysData = JsonSerializer.Deserialize<List<ChildrenToys>>(jsonData);
            if (toysData != null)
            {
                toyList = toysData;
            }
        }

        // Tilfældig legetøj
        public static ChildrenToys GetRandomToy()
        {
            try
            {
                if (toyList == null || toyList.Count == 0)
                {
                    throw new InvalidOperationException("Legetøjslisten er tom eller ikke initialiseret.");
                }

                Random random = new Random();
                int index = random.Next(toyList.Count);
                return toyList[index];
            }
            catch (Exception ex)
            {
                // Error Handling: Logger undtagelsen og returnerer et "standard legetøj"
                Console.WriteLine($"Fejl ved tildeling af tilfældigt legetøj: {ex.Message}");
                return new ChildrenToys("Sej pind");
            }
        }
    }
}
