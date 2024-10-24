using PeopleVilleEngine;
Console.WriteLine("PeopleVille");

//Create village
var village = new Village();
while (true)
{
    Console.Clear();
    Console.WriteLine(village.ToString());
    Console.WriteLine(village.Time);
    foreach (var location in village.Locations)
    {
        var locationStatus = location.Name;
        foreach (var villager in location.Villagers().OrderByDescending(v => v.Age))
        {
            string Status = "Barn";
            if (villager.Age > 18)
            {
                Status = "Voksen";
            }
            locationStatus += $"\n{Status}: {villager}";
        }
        Console.WriteLine(locationStatus + "\n");
    }

    if (Console.ReadKey(true).Key == ConsoleKey.Q)
    {
        village.Time.UpdateDay();
    }
}
