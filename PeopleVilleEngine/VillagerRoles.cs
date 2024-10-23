using System.Text.Json;

namespace PeopleVilleEngine;
public class VillagerRoles
{
    private (string Role, int Weight)[] _rolesWithWeights;
    RNG _random;
    private static VillagerRoles? _instance = null;

    private VillagerRoles()
    {
        _random = RNG.GetInstance();
        LoadNamesFromJsonFile();
    }

    public static VillagerRoles GetInstance()
    {
        if (_instance == null)
            _instance = new VillagerRoles();
        return _instance;
    }

    private void LoadNamesFromJsonFile()
    {
        string jsonFile = "lib\\roles.json";
        if (!File.Exists(jsonFile))
            throw new FileNotFoundException(jsonFile);

        string jsonData = File.ReadAllText(jsonFile);
        var roleData = JsonSerializer.Deserialize<RolesData>(jsonData);

        _rolesWithWeights = new (string, int)[]
        {
            ("Kriminel", 5),  // 5% chance
            ("Politi", 10),   // 10% chance
            ("Civil", 85)     // 85% chance
        };
    }

    private string GetWeightedRandomRole((string Role, int Weight)[] rolesWithWeights)
    {
        var validRoles = rolesWithWeights.Where(r => r.Weight > 0).ToArray();
        if (validRoles.Length == 0)
            throw new InvalidOperationException("No valid roles with non-zero weight.");

        int totalWeight = validRoles.Sum(r => r.Weight);
        int randomValue = _random.Next(totalWeight);
        int cumulativeWeight = 0;

        foreach (var (role, weight) in validRoles)
        {
            cumulativeWeight += weight;

            if (randomValue < cumulativeWeight)
            {
                return role;
            }
        }

        throw new InvalidOperationException("Failed to select a role.");
    }

    public string GetRandomRole() => GetWeightedRandomRole(_rolesWithWeights);

    private class RolesData
    {
        public string[] Roles { get; set; }
    }
}
