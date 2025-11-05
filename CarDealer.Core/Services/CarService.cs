using Model;
using CarDealer.Data;

namespace CarDealer.Core.Services
{
    public class CarService
    {
        private readonly CarDealerRepository _carRepository;

        public CarService()
        {
            _carRepository = new CarDealerRepository();
        }

        
        public List<car.Car> GetAllCars()
        {
            return _carRepository.GetAllCars();
        }

        
        public car.Car? GetCarById(int carId)
        {
            if (carId <= 0)
                return null;

            return _carRepository.GetCarById(carId);
        }

    
        public int AddCar(car.Car car)
        {
            ValidateCar(car);

            return _carRepository.AddCar(car);
        }

       
        public bool UpdateCar(car.Car car)
        {
            ValidateCar(car);

            return _carRepository.UpdateCar(car);
        }

        
        public bool DeleteCar(int carId)
        {
            if (carId <= 0)
                return false;

            return _carRepository.DeleteCar(carId);
        }

      
        public List<car.Car> SearchCars(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAllCars();

            return _carRepository.SearchCars(searchTerm);
        }

        
        public List<car.Car> GetCarsByYearRange(int minYear, int maxYear)
        {
            return GetAllCars()
                .Where(c => c.Year >= minYear && c.Year <= maxYear)
                .ToList();
        }

        
        public List<car.Car> GetCarsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return GetAllCars()
                .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
                .ToList();
        }

      
        private void ValidateCar(car.Car car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car));

            if (string.IsNullOrWhiteSpace(car.Brand))
                throw new ArgumentException("Brand is required");

            if (string.IsNullOrWhiteSpace(car.Model))
                throw new ArgumentException("Model is required");

            if (car.Price <= 0)
                throw new ArgumentException("Price must be greater than zero");

            if (car.Year < 1900 || car.Year > DateTime.Now.Year + 1)
                throw new ArgumentException($"Year must be between 1900 and {DateTime.Now.Year + 1}");
        }
    }
}