﻿using PeopleVilleEngine;
using PeopleVilleEngine.Locations;

public abstract class BaseVillager
{
    public int Age { get; protected set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsMale { get; set; }
    private Village _village;
    public ILocation? Home { get; set; } = null;
    public bool HasHome() => Home != null;
    public string Role {  get; set; }

    protected BaseVillager(Village village)
    {
        _village = village;
        IsMale = RNG.GetInstance().Next(0, 2) == 0;
        (FirstName, LastName) = village.VillagerNameLibrary.GetRandomNames(IsMale);
        Role = village.VillagerRoleLibrary.GetRandomRole();
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} ({Age} years) - Job: {Role}";
    }
}