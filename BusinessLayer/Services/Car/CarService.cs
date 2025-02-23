using BusinessLayer.Helper;
using BusinessLayer.Helper.ModelHelper;
using RepositoryLayer;
using SharedLibrary.RequestModels;
using SharedLibrary.ResponseModels;

namespace BusinessLayer.Services.Car
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;
        public CarService(ICarRepository carRepository)
        {
             this.carRepository = carRepository;
        }

        public async Task<Result<bool>> AddNewPriceForCar(int userId, CarPriceRequest carPriceRequest)
        {
            try
            {
                var carAuctionId = await carRepository.AddNewPriceForCar(userId, carPriceRequest);

                if(carAuctionId ==  default(int))
                {
                    return Result<bool>.Failure(Message.PriceAddingError);
                }

                return Result<bool>.Success(true);
            }
            catch (Exception)
            {
                return Result<bool>.Failure(Message.SystemError);
            }
        }

        public async Task<Result<CarResponse>> GetCar(int carId)
        {
            try
            { 
                var car =  await carRepository.GetCar(carId);
                if (car == null)
                {
                    return Result<CarResponse>.Failure(Message.EmptyCarError);
                }

                return Result<CarResponse>.Success(car);    
            }
            catch (Exception ex)
            {
                return Result<CarResponse>.Failure(Message.SystemError);
            }
        }

        public async Task<Result<List<CarViewResponse>>> GetCars()
        {
            try
            {
                var cars =  await carRepository.GetCars(); 

                if(cars is null || !cars.Any())
                {
                    return Result<List<CarViewResponse>>.Failure(Message.EmptyCarsListError);
                }
                return Result<List<CarViewResponse>>.Success(cars);
            }
            catch (Exception)
            {
                return Result<List<CarViewResponse>>.Failure(Message.SystemError);
            }
        }
    }
}
