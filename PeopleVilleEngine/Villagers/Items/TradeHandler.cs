using PeopleVilleEngine;
using PeopleVilleEngine.Villagers;
using PeopleVilleEngine.Villagers.Items;

namespace PeopleVilleEngine
{
    public static class TradeHandler
    {
        public static void TradeMoney(BaseVillager villager1, BaseVillager villager2, decimal amount)
        {
            if (villager1.PersonalWallet.SpendMoney(amount))
            {
                villager2.PersonalWallet.AddMoney(amount);
                Console.WriteLine($"{villager1.FirstName} {villager1.LastName} byttede {amount:C} til {villager2.FirstName} {villager2.LastName}.");
            }
            else
            {
                throw new InvalidOperationException($"{villager1.FirstName} {villager1.LastName} Har ikke nok penge.");
            }
        }

        public static void TradeWeapon(ArmedVillager villager1, ArmedVillager villager2)
        {
            if (villager1.Weapon == null || villager2.Weapon == null)
            {
                throw new InvalidOperationException("One of the villagers has no weapon.");
            }

            var tempWeapon = villager1.Weapon;
            var tempWeaponDescription = villager1.WeaponDescription;

            // Use reflection to set private properties
            typeof(ArmedVillager).GetProperty("Weapon")?.SetValue(villager1, villager2.Weapon);
            typeof(ArmedVillager).GetProperty("WeaponDescription")?.SetValue(villager1, villager2.WeaponDescription);

            typeof(ArmedVillager).GetProperty("Weapon")?.SetValue(villager2, tempWeapon);
            typeof(ArmedVillager).GetProperty("WeaponDescription")?.SetValue(villager2, tempWeaponDescription);

            Console.WriteLine($"{villager1.FirstName} {villager1.LastName} byttede våben med {villager2.FirstName} {villager2.LastName}. {villager1.FirstName} har nu {villager1.Weapon.Name} og {villager2.FirstName} har nu {villager2.Weapon.Name}.");
        }

        public static void TradeToy(ChildVillager child1, ChildVillager child2, ChildrenToys toy1, ChildrenToys toy2)
        {
            if (child1.Toys.Contains(toy1) && child2.Toys.Contains(toy2))
            {
                child1.RemoveToy(toy1);
                child2.RemoveToy(toy2);

                child1.AddToy(toy2);
                child2.AddToy(toy1);

                Console.WriteLine($"{child1.FirstName} {child1.LastName} byttede {toy1.Name} med {child2.FirstName} {child2.LastName} for {toy2.Name}.");
            }
            else
            {
                throw new InvalidOperationException("En af børnene har ikke det specificerede legetøj.");
            }
        }

        public static void TradePet(BaseVillager villager1, BaseVillager villager2)
        {
            if (villager1.Pet == null || villager2.Pet == null)
            {
                throw new InvalidOperationException("En af villagers har ikke et kæledyr.");
            }

            Pet tempPet = villager1.Pet;
            villager1.Pet = villager2.Pet;
            villager2.Pet = tempPet;

            Console.WriteLine($"{villager1.FirstName} {villager1.LastName} byttede kæledyr med {villager2.FirstName} {villager2.LastName}. {villager1.FirstName} har nu {villager1.Pet.Name} og {villager2.FirstName} har nu {villager2.Pet.Name}.");
        }

        public static void BuyItem(BaseVillager villager, Item item, decimal price)
        {
            if (villager.PersonalWallet.SpendMoney(price))
            {
                villager.AddItem(item);
                Console.WriteLine($"{villager.FirstName} {villager.LastName} købte {item.Name} for {price:C}.");
            }
            else
            {
                throw new InvalidOperationException($"{villager.FirstName} {villager.LastName} har ikke nok penge til at købe {item.Name}.");
            }
        }
    }
}