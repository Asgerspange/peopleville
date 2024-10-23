using System;
using System.Collections.Generic;

namespace PeopleVilleEngine.Villagers.Items
{
    public class ChildrenToys
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Constructor tager parametre name/description og initialiserer objektet.
        public ChildrenToys(string name, string description = "Legetøj")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Legetøjets navn kan ikke være null eller tomt.", nameof(name));
            }

            Name = name;
            Description = description;
        }

        // Statisk liste af legetøj
        private static List<ChildrenToys> toyList = new List<ChildrenToys>
        {
            new ChildrenToys("Legetøjsbil"),
            new ChildrenToys("ActionFigur"),
            new ChildrenToys("Fodbold"),
            new ChildrenToys("Dukke"),
            new ChildrenToys("Rubiksterning")
        };

        // Metode til at få tilfældigt legetøj
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
