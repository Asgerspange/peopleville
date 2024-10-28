using PeopleVilleEngine;
Console.WriteLine("PeopleVille");

var village = new Village();
var EventManager = new EventManager(ref village);

while (true)
{
    Console.Clear();
    var events = EventManager.ExecuteEvents();
    foreach (var evemt in events) {
        Console.WriteLine($"{evemt.Description}");
    }

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
