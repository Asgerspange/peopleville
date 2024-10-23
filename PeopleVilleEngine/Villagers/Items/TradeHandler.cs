using PeopleVilleEngine.Villagers;
using PeopleVilleEngine.Villagers.Items;
using System;

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
            throw new InvalidOperationException($"{villager1.FirstName} {villager1.LastName} Har ikke nok penge .");
        }
    }

    //skal initialiserers gennem konsol/gui / event
    public static void TradeWeapon(ArmedVillager villager1, ArmedVillager villager2)
    {
        // "Reflection gør det muligt at tilgå en privat 'setter'"
        var weaponProperty = typeof(ArmedVillager).GetProperty("Våben", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        string tempWeapon = villager1.Weapon;
        weaponProperty.SetValue(villager1, villager2.Weapon);
        weaponProperty.SetValue(villager2, tempWeapon);

        Console.WriteLine($"{villager1.FirstName} {villager1.LastName} Byttede våben med {villager2.FirstName} {villager2.LastName}. {villager1.FirstName} har nu {villager1.Weapon} og {villager2.FirstName} har nu {villager2.Weapon}.");
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
}
