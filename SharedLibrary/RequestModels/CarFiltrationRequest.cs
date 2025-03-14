
namespace SharedLibrary.RequestModels
{
	public class CarFiltrationRequest
	{
		public string? Brand { get; set; }
		public string? Model { get; set; }
		public int? FromYear { get; set; }
		public int? ToYear { get; set; }
		public int? Mileage { get; set; }

	}
}
