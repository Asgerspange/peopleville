using PeopleVilleEngine;
Console.WriteLine("PeopleVille");

//Create village
var village = new Village();
var time = new Time();
Console.WriteLine(village.ToString());

foreach (var location in village.Locations)
{
    var locationStatus = location.Name;
    foreach(var villager in location.Villagers().OrderByDescending(v => v.Age))
    {
        string Status = "Barn";
        if (villager.Age > 18) {
            Status = "Voksen";
        }
        locationStatus += $"\n{Status}: {villager}";
    }
    Console.WriteLine(locationStatus + "\n");
}
