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

        public async Task<Result<CarResponse>> GetCar(int userId, int carId)
        {
            try
            { 
                var (car,carsAuctions) =  await carRepository.GetCarsAuctions(carId);

                if (car is null || carsAuctions == null || !carsAuctions.Any())
                {
                    return Result<CarResponse>.Failure(Message.EmptyCarError);
                }

                var currentUserLastOffer = carsAuctions
                    .Where(car => car.UserId == userId && car.CarId == carId)
					.Select(c => c.Money)
					.DefaultIfEmpty(0) 
					.Max();

				var carMaxPrice = carsAuctions
							   .Where(c => c.CarId == carId)
							   .Select(c => c.Money)
							   .DefaultIfEmpty(0) 
							   .Max();

				var carResponse = new CarResponse()
				{
					Id = car.Id,
					Name = car.Name,
					ImageUrl = car.ImageUrl,
					Description = car.Description,
					Brand = car.Brand,
					Model = car.Model,
					Year = car.Year,
					Mileage = car.Mileage,
					FuelType = car.FuelType,
					Transmission = car.Transmission,
					MaxPrice = carMaxPrice,
					EndDate = car.EndDate,
                    UserLastPriceOffer = currentUserLastOffer
				};

				return Result<CarResponse>.Success(carResponse);    
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
