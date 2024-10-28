/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PeopleVilleEngine.Locations.Buildings.Prison;

namespace PeopleVilleEngine.Events
{
    public class Retssystem
    {
        private RNG _random;
        private const int ChanceForFreedom = 30;
        private const int ChanceForJail = 70;
        private List<Forbrydelse> _forbrydelser;
        private Prison _prison;

        public Retssystem()
        {
            _random = RNG.GetInstance();
            _prison = new Prison();
            LoadForbrydelserFromJsonFile();
        }

        private void LoadForbrydelserFromJsonFile()
        {
            string json = File.ReadAllText("forbrydelsesTyper.json");
            var forbrydelseData = JsonSerializer.Deserialize<ForbrydelseData>(json);
            _forbrydelser = forbrydelseData.ForbrydelsesTyper;
        }

        public Event GenerateTrialEvent(string villagerName, int villagerAge)
        {
            if (villagerAge < 18)
                throw new ArgumentException("Kun personer over 18 år kan blive dømt i retssystemet.");

            string rolle = "Kriminel";
            if (rolle != "Kriminel")
                throw new ArgumentException("Kun kriminielle kan blive dømt i retssystemet.");
            int udfald = _random.Next(100); // 0-99

            if (udfald < ChanceForFreedom)
            {
                return new Event
                {
                    Title = $"{villagerName} er blevet frikendt",
                    Description = $"Kriminelle {villagerName} er blevet frikendt.",
                    Severity = "1"
                };
            }
            else
            {
                var forbrydelse = GetRandomForbrydelse();
                TransferToPrison(villagerName, villagerAge, forbrydelse.Navn, forbrydelse.StrafAar);
                return new Event
                {
                    Title = $"{villagerName} {forbrydelse.StrafAar} års fængsel",
                    Description = $"{forbrydelse.Navn} mod {villagerName}",
                    Severity = "2"
                };
            }
        }

        private Forbrydelse GetRandomForbrydelse()
        {
            int index = _random.Next(_forbrydelser.Count);
            return _forbrydelser[index];
        }

        private void TransferToPrison(string villagerName, int villagerAge, string crime, int years)
        {
            _prison.RegisterInmate(villagerName, villagerAge, crime);
            var inmate = _prison.GetInmates().Last(); 
            _prison.AssignCell(inmate.Id, _random.Next(1, 100)); 
            Console.WriteLine($"{villagerName} er forflyttet til fængsel for {years} år.");

        }
    }

    public class Forbrydelse
    {
        public string Navn { get; set; }
        public int StrafAar { get; set; }
    }

    public class ForbrydelseData
    {
        public List<Forbrydelse> ForbrydelsesTyper { get; set; }
    }

    public class Event
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
    }
}
/*var retssystem = new Retssystem();
var event = retssystem.GenerateTrialEvent("Lars Jensen", 25); //Kriminelle skal namedroppes
*/