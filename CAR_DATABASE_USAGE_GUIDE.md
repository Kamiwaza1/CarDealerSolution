# Car Database Integration - Usage Guide

## What Was Set Up

Your desktop application is now connected to your MS SQL Cars table with full CRUD (Create, Read, Update, Delete) functionality.

### Project Structure

1. **Model** (Model\Car.cs)
   - Contains the `car.Car` class that matches your database table structure

2. **CarDealer.Data** (CarDealer.Data\CarDealerRepository.cs)
   - Repository class that handles all database operations
   - Uses ADO.NET with `Microsoft.Data.SqlClient`
   - Connection string from `App.config`

3. **CarDealer.Desktop** (CarDealer.Desktop\CarsForm.cs)
   - Windows Forms UI for managing cars
   - Access via "Manage Cars Database" button on MenuForm

## How to Use

### 1. Open the Cars Management Form
- After logging in, click the **"Manage Cars Database"** button at the top-right of the MenuForm
- This opens the Cars management window

### 2. Available Operations

#### View All Cars
- The DataGridView displays all cars from the database automatically
- Click **"Refresh"** to reload the data

#### Search Cars
- Enter a search term in the search box (Brand, Model, VIN, or Color)
- Click **"Search"** to filter results
- Click **"Refresh"** to show all cars again

#### Add a Car (Example)
- Click **"Add Car"** button
- Currently adds a sample Toyota Camry (modify the code to add your own form)

#### Edit a Car
- Select a car from the grid
- Click **"Edit Car"**
- Currently increases price by 1000 (modify to add your own edit form)

#### Delete a Car
- Select a car from the grid
- Click **"Delete Car"**
- Confirm the deletion

## Using the Repository in Your Code

### Example: Get All Cars
```csharp
var repository = new CarDealerRepository();
List<car.Car> allCars = repository.GetAllCars();
```

### Example: Get Car by ID
```csharp
var repository = new CarDealerRepository();
car.Car? myCar = repository.GetCarById(123);
if (myCar != null)
{
    MessageBox.Show($"{myCar.Brand} {myCar.Model}");
}
```

### Example: Add a New Car
```csharp
var repository = new CarDealerRepository();
car.Car newCar = new car.Car
{
    Brand = "BMW",
    Model = "X5",
    Year = 2024,
    Price = 75000,
    Currency = "EUR",
    Color = "Black",
    Mileage = 0,
    FuelType = "Diesel",
    Transmission = "Automatic"
};

int newCarId = repository.AddCar(newCar);
MessageBox.Show($"Car added with ID: {newCarId}");
```

### Example: Update a Car
```csharp
var repository = new CarDealerRepository();
car.Car? carToUpdate = repository.GetCarById(123);
if (carToUpdate != null)
{
    carToUpdate.Price = 45000;
    carToUpdate.Mileage = 50000;
    bool success = repository.UpdateCar(carToUpdate);
}
```

### Example: Delete a Car
```csharp
var repository = new CarDealerRepository();
bool success = repository.DeleteCar(123);
```

### Example: Search Cars
```csharp
var repository = new CarDealerRepository();
List<car.Car> results = repository.SearchCars("BMW");
```

## Database Table Expected Structure

Make sure your SQL Server table has these columns:
```sql
CREATE TABLE Cars (
    CarId INT PRIMARY KEY IDENTITY(1,1),
    Brand NVARCHAR(100) NOT NULL,
    Model NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Currency NVARCHAR(10) NOT NULL,
 Year INT NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Vin NVARCHAR(17) NULL,
    Mileage INT NULL,
    Color NVARCHAR(50) NULL,
    FuelType NVARCHAR(50) NULL,
    Transmission NVARCHAR(50) NULL,
    EngineSizeCc INT NULL,
    PowerHp INT NULL,
    Doors TINYINT NULL,
    Seats TINYINT NULL,
    FirstRegistration DATETIME NULL,
    PurchaseDate DATETIME NULL
)
```

## Next Steps

1. **Customize the Add/Edit functionality**: Create proper forms with input fields instead of using hardcoded values
2. **Add validation**: Validate user inputs before saving to database
3. **Add error handling**: Wrap database calls in try-catch blocks for better error messages
4. **Add filtering**: Add more search filters (by year, price range, etc.)
5. **Add sorting**: Allow users to sort by different columns
6. **Add pagination**: If you have many cars, implement paging

## Connection String Location

The connection string is stored in:
- `CarDealer.Desktop\App.config`

Current connection:
```
Data Source=mssqlstud.fhict.local;
Initial Catalog=dbi568895_zlok1966;
User ID=dbi568895_zlok1966;
Password=zlok.1966;
Trust Server Certificate=True
```
