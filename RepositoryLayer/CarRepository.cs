using Microsoft.Extensions.Configuration;
using RepositoryLayer.Helper;
using SharedLibrary.DbModels.Request;
using SharedLibrary.DbModels.Response;
using SharedLibrary.RequestModels;
using SharedLibrary.ResponseModels;

namespace RepositoryLayer
{
    public class CarRepository : CoreBaseRepository, ICarRepository
    {
        public CarRepository(IConfiguration configuration) : base(configuration){}

        public async Task<int> AddNewPriceForCar(int userId, CarPriceRequest carPriceRequest)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add($"@{nameof(CarAuction.UserId)}", userId);
            parameters.Add($"@{nameof(CarAuction.CarId)}", carPriceRequest.CarId);
            parameters.Add($"@{nameof(CarAuction.Money)}", carPriceRequest.Price);
           

            var resultId = await Insert<CarAuction>(parameters);
            return resultId;
        }

        public async Task<CarResponse> GetCar(int carId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add($"carId", carId);
            var whereConditionBody = $" {nameof(CarResponseDB.Id)} = @carId ";
            var car = await Get<Car, CarResponseDB>(ResponseModelGenerator.GenerateCarResponseDB, parameters,whereConditionBody);

            var carsWithPrice = await GetAll<CarAuction, CarPriceResponseDB>(ResponseModelGenerator.GenerateCarPriceResponse);
       
             var carMaxPrice = carsWithPrice
                                .Where(c => c.CarId == carId)
                                .Select(c => c.Money)
                                .DefaultIfEmpty(0) // Provide a default value to prevent exception
                                .Max();

            var carResponse =  new CarResponse() {
                Id = car.Id,
                Name = car.Name,
                ImageUrl = car.ImageUrl,
                Description =car.Description,
                Brand = car.Brand,
                Model =car.Model,
                Year = car.Year,
                Mileage = car.Mileage,
                FuelType = car.FuelType,
                Transmission =car.Transmission ,
                MaxPrice = carMaxPrice,
            };

            return carResponse;
        }

        public async Task<List<CarViewResponse>> GetCars()
        {
            var cars = await GetAll<Car, CarViewResponse>(ResponseModelGenerator.GetCarsViews);

            return cars;
        }
    }

    class Car
    { }
}
