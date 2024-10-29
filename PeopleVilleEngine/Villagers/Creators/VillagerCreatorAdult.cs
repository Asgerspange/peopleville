using PeopleVilleEngine.Locations;
using PeopleVilleEngine.Villagers;
using System.Diagnostics;

namespace PeopleVilleEngine.Villagers.Creators
{
    public class VillagerCreatorAdult : IVillagerCreator
    {
        public bool CreateVillager(Village village)
        {
            var random = RNG.GetInstance();
            var adult = new AdultVillager(village, random.Next(18, 40));
            var home = FindHome(village);
            var assignRandomWeapon = new ArmedVillager(village);

            if (home.Villagers().Count(v => v.GetType() == typeof(AdultVillager)) >= 1)
            {
                var first = home.Villagers().First(v => v.GetType() == typeof(AdultVillager));
                adult.LastName = first.LastName;
                adult.IsMale = !first.IsMale;
                adult.FirstName = village.VillagerNameLibrary.GetRandomFirstName(adult.IsMale);
                adult.Role = village.VillagerRoleLibrary.GetRandomRole();
            }

            home.Villagers().Add(adult);
            adult.Home = home;

            AssignWeaponToAdult(adult);

            village.Villagers.Add(adult);
            return true;
        }

        private IHouse FindHome(Village village)
        {
            var random = RNG.GetInstance();

            var potentialHomes = village.Locations.Where(p => p.GetType().IsAssignableTo(typeof(IHouse)))
                .Where(p => p.Villagers().Count(v => v.GetType() == typeof(AdultVillager)) < 2)
                .Where(p => ((IHouse)p).Population < ((IHouse)p).MaxPopulation).ToList();

            if (potentialHomes.Count > 0 && random.Next(1, 5) != 1)
                return (IHouse)potentialHomes[random.Next(0, potentialHomes.Count)];

            IHouse house = new SimpleHouse();
            village.Locations.Add(house);
            return house;
        }

        private void AssignWeaponToAdult(BaseVillager adult)
        {
            if (adult.Age >= 18)
            {
                ArmedVillager.AssignWeaponToAdult(adult);
            }
        }
    }
}
