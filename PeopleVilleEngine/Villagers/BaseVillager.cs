﻿using PeopleVilleEngine;
using PeopleVilleEngine.Locations;

public abstract class BaseVillager
{
    public int Age { get; protected set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Hobby { get; set; }
    public bool IsMale { get; set; }
    private Village _village;
    public ILocation? Home { get; set; } = null;
    public bool HasHome() => Home != null;
    public Wallet PersonalWallet { get; private set; } // Ny egenskab tilføjet til baseclass

    protected BaseVillager(Village village)
    {
        _village = village;
        IsMale = RNG.GetInstance().Next(0, 2) == 0;
        (FirstName, LastName) = village.VillagerNameLibrary.GetRandomNames(IsMale);
        Hobby = village.VillagerHobbyLibrary.GetRandomHobby();
        PersonalWallet = new Wallet("$", 100m); // Initialiserer pungen med $ og 100 dollars som start kapital
    }

    public string PerformHobby()
    {
        return $"{FirstName} {LastName} is {Hobby}.";
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} {Hobby} ({Age} years)";
    }
}
