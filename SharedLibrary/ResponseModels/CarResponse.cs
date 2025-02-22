namespace SharedLibrary.ResponseModels
{
    public class CarResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;  // Stores path or URL of the image
        public string Description { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal MaxPrice { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; } = string.Empty;  // Petrol, Diesel, Electric, etc.
        public string Transmission { get; set; } = string.Empty;  // Manual, Automatic
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
