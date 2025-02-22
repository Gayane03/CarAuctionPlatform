
using SharedLibrary.RequestModels;
using SharedLibrary.ResponseModels;

namespace RepositoryLayer
{
    public interface ICarRepository
    {
        Task<List<CarViewResponse>> GetCars();
        Task<CarResponse> GetCar(int carId);
        Task<int> AddNewPriceForCar(int userId, CarPriceRequest carPriceRequest);
    }
}
