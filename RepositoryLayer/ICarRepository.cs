using SharedLibrary.DbModels.Response;
using SharedLibrary.RequestModels;
using SharedLibrary.ResponseModels;

namespace RepositoryLayer
{
    public interface ICarRepository
    {
        Task<List<CarViewResponse>> GetCars();
		Task<(CarResponseDB, List<CarPriceResponseDB>)> GetCarsAuctions(int carId);
        Task<int> AddNewPriceForCar(int userId, CarPriceRequest carPriceRequest);
    }
}
