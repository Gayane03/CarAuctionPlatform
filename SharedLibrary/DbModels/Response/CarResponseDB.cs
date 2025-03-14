﻿namespace SharedLibrary.DbModels.Response
{
    public class CarResponseDB
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public DateTime EndDate { get; set; }   = DateTime.MaxValue;

		public string Color { get; set; }
		public string InteriorColor { get; set; }
		public string BodyType { get; set; }

		public string EngineSize { get; set; }
		public string DriveType { get; set; }
	}
}
