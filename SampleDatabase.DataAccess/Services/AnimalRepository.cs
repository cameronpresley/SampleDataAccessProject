using System;
using System.Collections.Generic;
using SampleDataAccess.Models;
using SampleDatabase.DataAccess.Interfaces;

namespace SampleDatabase.DataAccess.Services
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalDataAccess _dataAccess;

        public AnimalRepository(AnimalDataAccess dataAccess)
        {
            if (dataAccess == null) throw new ArgumentNullException(nameof(dataAccess));
            _dataAccess = dataAccess;
        }

        public void Create(Animal animal)
        {
            _dataAccess.Create(animal);
        }

        public Animal Read(int id)
        {
            return _dataAccess.Read(id);
        }

        public List<Animal> ReadAll()
        {
            return _dataAccess.ReadAll();
        }
    }
}
