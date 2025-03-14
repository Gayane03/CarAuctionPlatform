using BusinessLayer.Helper.ModelHelper;
using SharedLibrary.RequestModels;
using SharedLibrary.ResponseModels;

namespace BusinessLayer.Services.Car
{
    public interface ICarService
    {
        Task<Result<List<CarViewResponse>>> GetCars(int userId, CarFiltrationRequest? carFiltrationRequest);
        Task<Result<CarResponse>> GetCar(int userId,int carId);
        Task<Result<bool>> AddNewPriceForCar(int userId, CarPriceRequest carPriceRequest);

        Task<Result<bool>> UpdateCarFavoriteState(int userId, CarFavoriteRequest carFavoriteRequest);

	}
}
