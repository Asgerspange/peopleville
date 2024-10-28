using System;
using System.IO;
using System.Text.Json;

namespace PeopleVilleEngine
{
    public class VillagerHobbies
    {
        private string[] _hobbies = new string[] { };
        RNG _random;
        private static VillagerHobbies? _instance = null;

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
                throw new IndexOutOfRangeException("Hobby data er ikke loaded korrekt.");

            int index = RNG.GetInstance().Next(_hobbies.Length);
            return _hobbies[index];
        }

        public string GetRandomHobbyForVillager() => GetRandomHobby();

        private class HobbiesData
        {
            public string[] Hobbies { get; set; }
        }
    }
}