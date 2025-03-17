
namespace SharedLibrary.RequestModels
{
	public class CarFiltrationRequest
	{
		public string? Brand { get; set; }
		public string? Model { get; set; }
		public int? FromYear { get; set; } = 2000;
		public int? ToYear { get; set; } = DateTime.Now.Year;	
		public int? Mileage { get; set; }

	}
}
