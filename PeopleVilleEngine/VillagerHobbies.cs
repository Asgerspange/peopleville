using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PeopleVilleEngine
{
    public class VillagerHobbies
    {
        private string[] _hobbies = new string[] { };
        RNG _random;
        private static VillagerHobbies? _instance = null;
        private List<BaseVillager> _villagers = new List<BaseVillager>(); // Holds the villagers

        private VillagerHobbies()
        {
            _random = RNG.GetInstance();
            LoadHobbiesFromJsonFile();
        }

        public static VillagerHobbies GetInstance()
        {
            if (_instance == null)
                _instance = new VillagerHobbies();
            return _instance;
        }

        private void LoadHobbiesFromJsonFile()
        {
            string jsonFile = "lib\\hobbies.json";
            if (!File.Exists(jsonFile))
                throw new FileNotFoundException(jsonFile);

            string jsonData = File.ReadAllText(jsonFile);
            var hobbiesData = JsonSerializer.Deserialize<HobbiesData>(jsonData);
            _hobbies = hobbiesData.Hobbies;
        }

        public string GetRandomHobby()
        {
            if (_hobbies.Length == 0)
                throw new IndexOutOfRangeException("Hobbies data not properly loaded.");

            int index = RNG.GetInstance().Next(_hobbies.Length);
            return _hobbies[index];
        }
        public void PerformVillagerHobbies(IEnumerable<BaseVillager> villagers)
        {
            foreach (var villager in villagers)
            {
                Console.WriteLine(villager.PerformHobby());
            }
        }

        private class HobbiesData
        {
            public string[] Hobbies { get; set; }
        }
    }
}
