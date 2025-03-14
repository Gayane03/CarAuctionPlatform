namespace SharedLibrary.DbModels.Response
{
	public class CarViewResponseDB
	{

		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;  // Stores path or URL of the image
		public string Description { get; set; } = string.Empty;
		public string Brand { get; set; } = string.Empty;
		public string Model { get; set; } = string.Empty;
		public int Year { get; set; }
	}
}
