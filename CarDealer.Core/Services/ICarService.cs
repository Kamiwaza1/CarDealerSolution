using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace CarDealer.Core.Services
{
    public interface ICarService
    {
      
    
     Task<List<Model.Car>> GetAllCar();
        List<Car> GetAllCars();
    } 
}
