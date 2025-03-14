using SharedLibrary.DbModels.Response;
using SharedLibrary.RequestModels;
using SharedLibrary.ResponseModels;

namespace RepositoryLayer
{
    public interface ICarRepository
    {
        Task<List<CarViewResponseDB>> GetCars();
		Task<(CarResponseDB, List<CarPriceResponseDB>)> GetCarsAuctions(int? carId =  null);

        Task<List<CarResponseDB>> GetCarsDataForFiltering();
        Task<int> AddNewPriceForCar(int userId, CarPriceRequest carPriceRequest);

        Task UpdateCarFavoriteState(int userId, int carId, int isfavorite);

	}
}
