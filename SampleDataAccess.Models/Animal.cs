using System;

namespace SampleDataAccess.Models
{
    public class Animal
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public int NumberOfLegs { get; private set; }

        public Animal(int id, string name, int numberOfLegs)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            Id = id;
            Name = name;
            NumberOfLegs = numberOfLegs;
        }
    }
}
