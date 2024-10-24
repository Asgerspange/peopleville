public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Item(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public override string ToString()
    {
        return $"{Name}: {Description}";
    }

    public string ToString(bool includeWeaponDescription, string weaponDescription = "")
    {
        if (includeWeaponDescription && !string.IsNullOrEmpty(weaponDescription))
        {
            return $"{Name}: {Description} - Våben Beskrivelse: {weaponDescription}";
        }
        return ToString();
    }
}
