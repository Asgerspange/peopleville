﻿using System.Text.Json;

namespace PeopleVilleEngine
{
    public class VillagerRoles
    {
        private (string Role, int Weight)[] _rolesWithWeights;
        RNG _random;
        private static VillagerRoles? _instance = null;

        public VillagerRoles()
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
            {
                throw new FileNotFoundException($"JSON file not found: {jsonFile}");
            }

            var json = File.ReadAllText(jsonFile);
            var rolesWrapper = JsonSerializer.Deserialize<RoleData[]>(json);
            _rolesWithWeights = rolesWrapper
                    .Select(r => (r.Role, r.Weight))
                    .ToArray();     
        }

        private string GetWeightedRandomRole((string Role, int Weight)[] rolesWithWeights)
        {
            if (rolesWithWeights.Length == 0)
                throw new InvalidOperationException("No valid roles with non-zero weight.");

            int totalWeight = rolesWithWeights.Sum(r => r.Weight);
            int randomValue = _random.Next(totalWeight);
            int cumulativeWeight = 0;

            foreach (var (role, weight) in rolesWithWeights)
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

        private class RoleData
        {
            public string Role { get; set; }
            public int Weight { get; set; }
        }
    }
}
