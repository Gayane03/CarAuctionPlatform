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

        public async Task<(CarResponseDB,List<CarPriceResponseDB>)> GetCarsAuctions(int carId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add($"carId", carId);
            var whereConditionBody = $" {nameof(CarResponseDB.Id)} = @carId ";

            var car = await Get<Car, CarResponseDB>(ResponseModelGenerator.GenerateCarResponseDB, parameters,whereConditionBody);
            var carsWithPrice = await GetAll<CarAuction, CarPriceResponseDB>(ResponseModelGenerator.GenerateCarPriceResponse);
                 
            return (car, carsWithPrice);
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
