using CarAPI.Entities;
using CarAPI.Repositories_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryAPI.Tests.Stups
{
    internal class CarRepoStup : ICarsRepository
    {
        public bool AddCar(Car car)
        {
            throw new NotImplementedException();
        }

        public bool AssignToOwner(int carId, int OwnerId)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAllCars()
        {
            throw new NotImplementedException();
        }

        public Car GetCarById(int id)
        {
            return new Car()
            {
                Id = 5,
                Price = 1000,
                Velocity = 50,
                Type = CarType.Audi,
                VIN = "6732742734",
                OwnerId = 2,
                Owner = new Owner() { Name = "Ali", Id = 2 }
            };
        }

        public bool Remove(int carId)
        {
            throw new NotImplementedException();
        }
    }
}
