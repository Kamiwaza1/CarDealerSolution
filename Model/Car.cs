namespace Model
{
    public class Car
    {
        public int CarId { get; set; }
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public decimal Price { get; set; }
        public string Currency { get; set; } = "EUR";
        public int Year { get; set; }
        public string? Description { get; set; }
        public string? Vin { get; set; }
        public int? Mileage { get; set; }
        public string? Color { get; set; }
        public string? FuelType { get; set; }
        public string? Transmission { get; set; }
        public int? EngineSizeCc { get; set; }
        public int? PowerHp { get; set; }
        public byte? Doors { get; set; }
        public byte? Seats { get; set; }
        public DateTime? FirstRegistration { get; set; } 
        public DateTime? PurchaseDate { get; set; }

        public class Cars : Car
        {
        }
    }
}
