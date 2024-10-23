using PeopleVilleEngine.Villagers;
using System;

public static class TradeHandler
{
    // Metode til at handle penge mellem to indbyggere
    public static void TradeMoney(BaseVillager villager1, BaseVillager villager2, decimal amount)
    {
        if (villager1.PersonalWallet.SpendMoney(amount))
        {
            villager2.PersonalWallet.AddMoney(amount);
            Console.WriteLine($"{villager1.FirstName} {villager1.LastName} traded {amount:C} to {villager2.FirstName} {villager2.LastName}.");
        }
        else
        {
            throw new InvalidOperationException($"{villager1.FirstName} {villager1.LastName} har ikke nok penge.");
        }
    }

    // Metode til at handle våben mellem to bevæbnede indbyggere
    public static void TradeWeapon(ArmedVillager villager1, ArmedVillager villager2)
    {
        // Reflection bruges til at tilgå en privat 'setter'
        var weaponProperty = typeof(ArmedVillager).GetProperty("Weapon", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (weaponProperty != null)
        {
            string tempWeapon = villager1.Weapon;
            weaponProperty.SetValue(villager1, villager2.Weapon);
            weaponProperty.SetValue(villager2, tempWeapon);

            Console.WriteLine($"{villager1.FirstName} {villager1.LastName} byttede våben med {villager2.FirstName} {villager2.LastName}. {villager1.FirstName} har nu {villager1.Weapon} og {villager2.FirstName} har nu {villager2.Weapon}.");
        }
        else
        {
            throw new InvalidOperationException("Kunne ikke finde våbenegenskaben på ArmedVillager.");
        }
    }
}
