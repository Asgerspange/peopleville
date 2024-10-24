using System;
using System.Collections.Generic;
using PeopleVilleEngine.Villagers.Items;

namespace PeopleVilleEngine.Villagers
{
    public class ChildVillager : BaseVillager
    {
        public List<ChildrenToys> Toys { get; private set; }

        public ChildVillager(Village village) : base(village)
        {
            Age = RNG.GetInstance().Next(0, 18);
            Toys = new List<ChildrenToys>();
        }
         
        public ChildVillager(Village village, int age) : this(village)
        {
            Age = age;
        }

        public ChildVillager(Village village, int age, bool isMale) : this(village, age)
        {
            IsMale = isMale;
        }

        public void AddToy(ChildrenToys toy)
        {
            Toys.Add(toy);
        }

        public void RemoveToy(ChildrenToys toy)
        {
            Toys.Remove(toy);
        }
    }
}