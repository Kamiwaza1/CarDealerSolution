using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.SqlClient;
using Model;

namespace CarDealer.Data
{
    public class CarDealerRepository : ICarDealerRepository
    {
      private readonly string cs = ConfigurationManager.ConnectionStrings["CarDealerDb"].ConnectionString;

        // Get all cars
    public List<Car.Cars> GetAllCars()
     {
         List<Car.Cars> cars = new List<Car.Cars>();

         using (SqlConnection conn = new SqlConnection(cs))
   {
     string query = @"SELECT CarId, Brand, Model, Price, Currency, Year, Description, 
     Vin, Mileage, Color, FuelType, Transmission, EngineSizeCc, 
      PowerHp, Doors, Seats, FirstRegistration, PurchaseDate 
    FROM Cars";

   SqlCommand cmd = new SqlCommand(query, conn);
        conn.Open();

    using (SqlDataReader reader = cmd.ExecuteReader())
   {
       while (reader.Read())
        {
            cars.Add(MapReaderToCar(reader));
           }
              }
  }

            return cars;
        }

     // Get car by ID
        public Car.Cars? GetCarById(int carId)
  {
            using (SqlConnection conn = new SqlConnection(cs))
   {
         string query = @"SELECT CarId, Brand, Model, Price, Currency, Year, Description, 
       Vin, Mileage, Color, FuelType, Transmission, EngineSizeCc, 
              PowerHp, Doors, Seats, FirstRegistration, PurchaseDate 
    FROM Cars 
   WHERE CarId = @CarId";

       SqlCommand cmd = new SqlCommand(query, conn);
         cmd.Parameters.AddWithValue("@CarId", carId);
     conn.Open();

         using (SqlDataReader reader = cmd.ExecuteReader())
     {
         if (reader.Read())
         {
       return MapReaderToCar(reader);
      }
       }
       }

   return null;
        }

        // Add new car
        public int AddCar(Car.Cars car)
     {
        using (SqlConnection conn = new SqlConnection(cs))
    {
   string query = @"INSERT INTO Cars (Brand, Model, Price, Currency, Year, Description, 
        Vin, Mileage, Color, FuelType, Transmission, EngineSizeCc, 
            PowerHp, Doors, Seats, FirstRegistration, PurchaseDate)
      VALUES (@Brand, @Model, @Price, @Currency, @Year, @Description, 
  @Vin, @Mileage, @Color, @FuelType, @Transmission, @EngineSizeCc, 
     @PowerHp, @Doors, @Seats, @FirstRegistration, @PurchaseDate);
        SELECT CAST(SCOPE_IDENTITY() as int)";

    SqlCommand cmd = new SqlCommand(query, conn);
     AddCarParameters(cmd, car);
     conn.Open();

          int newCarId = (int)cmd.ExecuteScalar();
      return newCarId;
    }
        }

        // Update existing car
 public bool UpdateCar(Car.Cars car)
  {
            using (SqlConnection conn = new SqlConnection(cs))
            {
              string query = @"UPDATE Cars 
       SET Brand = @Brand, Model = @Model, Price = @Price, 
         Currency = @Currency, Year = @Year, Description = @Description, 
             Vin = @Vin, Mileage = @Mileage, Color = @Color, 
FuelType = @FuelType, Transmission = @Transmission, 
              EngineSizeCc = @EngineSizeCc, PowerHp = @PowerHp, 
   Doors = @Doors, Seats = @Seats, 
        FirstRegistration = @FirstRegistration, PurchaseDate = @PurchaseDate
     WHERE CarId = @CarId";

              SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CarId", car.CarId);
       AddCarParameters(cmd, car);
             conn.Open();

    int rowsAffected = cmd.ExecuteNonQuery();
    return rowsAffected > 0;
            }
        }

  // Delete car
      public bool DeleteCar(int carId)
   {
       using (SqlConnection conn = new SqlConnection(cs))
    {
          string query = "DELETE FROM Cars WHERE CarId = @CarId";

         SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@CarId", carId);
      conn.Open();

             int rowsAffected = cmd.ExecuteNonQuery();
        return rowsAffected > 0;
     }
        }

        // Search cars
   public List<Car.Cars> SearchCars(string searchTerm)
  {
         List<Car.Cars> cars = new List<Car.Cars>();

     using (SqlConnection conn = new SqlConnection(cs))
            {
         string query = @"SELECT CarId, Brand, Model, Price, Currency, Year, Description, 
   Vin, Mileage, Color, FuelType, Transmission, EngineSizeCc, 
  PowerHp, Doors, Seats, FirstRegistration, PurchaseDate 
   FROM Cars 
  WHERE Brand LIKE @SearchTerm 
      OR Model LIKE @SearchTerm 
    OR Vin LIKE @SearchTerm
               OR Color LIKE @SearchTerm";

  SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
        conn.Open();

       using (SqlDataReader reader = cmd.ExecuteReader())
      {
           while (reader.Read())
           {
                 cars.Add(MapReaderToCar(reader));
          }
              }
        }

            return cars;
        }

     // Helper method to map SqlDataReader to Car object
        private Car.Cars MapReaderToCar(SqlDataReader reader)
      {
            return new Car.Cars
   {
                CarId = reader.GetInt32(reader.GetOrdinal("CarId")),
       Brand = reader.GetString(reader.GetOrdinal("Brand")),
   Model = reader.GetString(reader.GetOrdinal("Model")),
 Price = reader.GetDecimal(reader.GetOrdinal("Price")),
        Currency = reader.GetString(reader.GetOrdinal("Currency")),
                Year = reader.GetInt32(reader.GetOrdinal("Year")),
           Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
          Vin = reader.IsDBNull(reader.GetOrdinal("Vin")) ? null : reader.GetString(reader.GetOrdinal("Vin")),
      Mileage = reader.IsDBNull(reader.GetOrdinal("Mileage")) ? null : reader.GetInt32(reader.GetOrdinal("Mileage")),
    Color = reader.IsDBNull(reader.GetOrdinal("Color")) ? null : reader.GetString(reader.GetOrdinal("Color")),
      FuelType = reader.IsDBNull(reader.GetOrdinal("FuelType")) ? null : reader.GetString(reader.GetOrdinal("FuelType")),
            Transmission = reader.IsDBNull(reader.GetOrdinal("Transmission")) ? null : reader.GetString(reader.GetOrdinal("Transmission")),
                EngineSizeCc = reader.IsDBNull(reader.GetOrdinal("EngineSizeCc")) ? null : reader.GetInt32(reader.GetOrdinal("EngineSizeCc")),
  PowerHp = reader.IsDBNull(reader.GetOrdinal("PowerHp")) ? null : reader.GetInt32(reader.GetOrdinal("PowerHp")),
                Doors = reader.IsDBNull(reader.GetOrdinal("Doors")) ? null : reader.GetByte(reader.GetOrdinal("Doors")),
      Seats = reader.IsDBNull(reader.GetOrdinal("Seats")) ? null : reader.GetByte(reader.GetOrdinal("Seats")),
 FirstRegistration = reader.IsDBNull(reader.GetOrdinal("FirstRegistration")) ? null : reader.GetDateTime(reader.GetOrdinal("FirstRegistration")),
 PurchaseDate = reader.IsDBNull(reader.GetOrdinal("PurchaseDate")) ? null : reader.GetDateTime(reader.GetOrdinal("PurchaseDate"))
      };
      }

 // Helper method to add car parameters to SqlCommand
        private void AddCarParameters(SqlCommand cmd, Car.Cars car)
        {
        cmd.Parameters.AddWithValue("@Brand", car.Brand);
      cmd.Parameters.AddWithValue("@Model", car.Model);
        cmd.Parameters.AddWithValue("@Price", car.Price);
          cmd.Parameters.AddWithValue("@Currency", car.Currency);
    cmd.Parameters.AddWithValue("@Year", car.Year);
            cmd.Parameters.AddWithValue("@Description", (object?)car.Description ?? DBNull.Value);
          cmd.Parameters.AddWithValue("@Vin", (object?)car.Vin ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Mileage", (object?)car.Mileage ?? DBNull.Value);
  cmd.Parameters.AddWithValue("@Color", (object?)car.Color ?? DBNull.Value);
   cmd.Parameters.AddWithValue("@FuelType", (object?)car.FuelType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Transmission", (object?)car.Transmission ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EngineSizeCc", (object?)car.EngineSizeCc ?? DBNull.Value);
     cmd.Parameters.AddWithValue("@PowerHp", (object?)car.PowerHp ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Doors", (object?)car.Doors ?? DBNull.Value);
  cmd.Parameters.AddWithValue("@Seats", (object?)car.Seats ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FirstRegistration", (object?)car.FirstRegistration ?? DBNull.Value);
    cmd.Parameters.AddWithValue("@PurchaseDate", (object?)car.PurchaseDate ?? DBNull.Value);
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
