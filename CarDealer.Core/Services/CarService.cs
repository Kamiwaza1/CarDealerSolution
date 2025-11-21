using Model;
using CarDealer.Data;

namespace CarDealer.Core.Services
{
    public class CarService : ICarService
    {
        private readonly CarDealerRepository _carRepository;

        public CarService()
        {
            _carRepository = new CarDealerRepository();
        }

        
        public List<Car.Cars> GetAllCars()
        {
            return _carRepository.GetAllCars();
        }

        
        public Car.Cars? GetCarById(int carId)
        {
            if (carId <= 0)
                return null;

            return _carRepository.GetCarById(carId);
        }

    
        public int AddCar(Car.Cars car)
        {
            ValidateCar(car);

            return _carRepository.AddCar(car);
        }

       
        public bool UpdateCar(Car.Cars car)
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

      
        public List<Car.Cars> SearchCars(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAllCars();

            return _carRepository.SearchCars(searchTerm);
        }
        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _carRepository.GetAllCarsAsync();
        }

        public List<Car.Cars> GetCarsByYearRange(int minYear, int maxYear)
        {
            return GetAllCars()
                .Where(c => c.Year >= minYear && c.Year <= maxYear)
                .ToList();
        }

        
        public List<Car.Cars> GetCarsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return GetAllCars()
                .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
                .ToList();
        }

      
        private void ValidateCar(Car.Cars car)
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

        public Task<List<Car>> GetAllCar()
        {
            throw new NotImplementedException();
        }

        List<Car> ICarService.GetAllCars()
        {
            throw new NotImplementedException();
        }
    }
}