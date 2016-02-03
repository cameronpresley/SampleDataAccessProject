using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleDataAccess.Models;

namespace SampleDatabase.DataAccess.Interfaces
{
    public interface IAnimalRepository
    {
        void Create(Animal animal);
        Animal Read(int id);
        List<Animal> ReadAll();
    }
}
