using System;
using System.Linq;
using SampleDataAccess.Models;
using SampleDatabase.DataAccess;
using SampleDatabase.DataAccess.Interfaces;
using SampleDatabase.DataAccess.Services;

namespace SampleDataAccessProject
{
    class Program
    {
        private static IAnimalRepository _repository = new AnimalRepository(new AnimalDataAccess());
        static void Main(string[] args)
        {
            string choice;

            do
            {
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1). Add an animal.");
                Console.WriteLine("2). Read an animal.");
                Console.WriteLine("3). Read all animals");
                Console.WriteLine("4). Exit");
                choice = Console.ReadLine();
                if (choice == "1")
                    AddAnAnimal();
                else if (choice == "2")
                    ReadAnAnimal();
                else if (choice == "3")
                    ReadAllAnimals();
                else if (choice != "4")
                    PrintInValidMessage();
            } while (choice != "4");
        }

        private static void ReadAllAnimals()
        {
            var animals = _repository.ReadAll();
            if (!animals.Any())
            {
                Console.WriteLine("No animals in the database.");
                return;
            }
            foreach (var animal in animals)
            {
                WriteAnimal(animal);
            }
        }

        private static void PrintInValidMessage()
        {
            Console.WriteLine("Please choose an option from 1, 2, 3, or 4.");
        }

        private static void ReadAnAnimal()
        {
            Console.WriteLine("Enter an animal id.");
            var id = Int32.Parse(Console.ReadLine());

            var animal = _repository.Read(id);
            if (animal == null)
                Console.WriteLine("Couldn't find animal with Id of " + id);
            else
            {
                WriteAnimal(animal);
            }
        }

        private static void WriteAnimal(Animal animal)
        {
            Console.WriteLine($"Animal {animal.Id} is a {animal.Name} with {animal.NumberOfLegs} legs.");
        }

        private static void AddAnAnimal()
        {
            Console.WriteLine("What's the animal called?");
            var name = Console.ReadLine();
            Console.WriteLine("How many legs does it have?");
            var numberOfLegs = Int32.Parse(Console.ReadLine());

            var animal = new Animal(1, name, numberOfLegs);
            _repository.Create(animal);
        }
    }
}
