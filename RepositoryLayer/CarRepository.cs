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
			parameters.Add($"@{nameof(CarAuction.IsFavorite)}", 0);


			var resultId = await Insert<CarAuction>(parameters);
            return resultId;
        }

        public async Task<(CarResponseDB,List<CarPriceResponseDB>)> GetCarsAuctions(int? carId = null)
        {
            CarResponseDB car = null;

			if (carId is not null)
			{
				var parameters = new Dictionary<string, object>();
				parameters.Add($"carId", carId);
				var whereConditionBody = $" {nameof(CarResponseDB.Id)} = @carId ";

			    car = await Get<Car, CarResponseDB>(ResponseModelGenerator.GenerateCarResponseDB, parameters, whereConditionBody);
			}

			var carsWithPrice = await GetAll<CarAuction, CarPriceResponseDB>(ResponseModelGenerator.GenerateCarPriceResponse);
                 
            return (car, carsWithPrice);
        }



        public async Task<List<CarViewResponseDB>> GetCars()
        {
            var cars = await GetAll<Car, CarViewResponseDB>(ResponseModelGenerator.GetCarsViews);
            return cars;
        }

		public async Task UpdateCarFavoriteState(int userId, int carId, int isfavorite)
		{
			var conditionParameters = new Dictionary<string, object>();
			conditionParameters.Add($"userId", userId);
			conditionParameters.Add($"carId", carId);

			var whereConditionBody = "UserId = @userId and CarId = @carId";

			var carAuction = await Get<CarAuction, CarPriceResponseDB>(ResponseModelGenerator.GenerateCarForFavorite,conditionParameters,whereConditionBody);

            if(carAuction is default(CarPriceResponseDB))
            {
				var parameters = new Dictionary<string, object>();

				parameters.Add($"@{nameof(CarAuction.UserId)}", userId);
				parameters.Add($"@{nameof(CarAuction.CarId)}", carId);
				parameters.Add($"@{nameof(CarAuction.Money)}", 0);
				parameters.Add($"@{nameof(CarAuction.IsFavorite)}", isfavorite);
				var resultId = await Insert<CarAuction>(parameters);
                return;
			}

			var updatingParameters = new Dictionary<string, object>();
			updatingParameters.Add($"IsFavorite", isfavorite);

		
			await Update<CarAuction>(updatingParameters, conditionParameters, whereConditionBody);
		}

		public async Task<List<CarResponseDB>> GetCarsDataForFiltering()
		{

			List<CarResponseDB> cars = await GetAll<Car,CarResponseDB>(ResponseModelGenerator.GenerateCarResponsesDB);
			return cars;
		}
	}

    class Car
    { }
}
