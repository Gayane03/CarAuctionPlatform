using Microsoft.Data.SqlClient;
using SharedLibrary.DbModels.Response;

namespace RepositoryLayer.Helper
{
    internal static class ResponseModelGenerator
    {
        public static UserActivityDataResponse GetUserActivityData(SqlDataReader reader)
        {
            return new UserActivityDataResponse()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }
        public static LoginResponseDB GenerateLoginResponse(SqlDataReader reader)
        {
			return new()
			{
				Id = reader.GetInt32(reader.GetOrdinal("Id")),
				RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
				PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
				Email = reader.GetString(reader.GetOrdinal("Email"))
			};
		}
        public static RoleResponse GenerateRoleResponse(SqlDataReader reader)
        {
            return new() { RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")) };
        }
        public static CarResponseDB GenerateCarResponseDB(SqlDataReader reader)
        {
            return new CarResponseDB
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Brand = reader.GetString(reader.GetOrdinal("Brand")),
                Model = reader.GetString(reader.GetOrdinal("Model")),
                Year = reader.GetInt32(reader.GetOrdinal("Year")),
                Mileage = reader.GetInt32(reader.GetOrdinal("Mileage")),
                FuelType = reader.GetString(reader.GetOrdinal("FuelType")),
                Transmission = reader.GetString(reader.GetOrdinal("Transmission")),
                EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                Color = reader.GetString(reader.GetOrdinal("Color")),
                InteriorColor = reader.GetString(reader.GetOrdinal("InteriorColor")),
				BodyType = reader.GetString(reader.GetOrdinal("BodyType")),
				EngineSize = reader.GetString(reader.GetOrdinal("EngineSize")),
				DriveType = reader.GetString(reader.GetOrdinal("DriveType")),
			};
        }

		public static List<CarPriceResponseDB> GenerateCarPriceResponse(SqlDataReader reader)
		{
			var carsWithMoney = new List<CarPriceResponseDB>();

			do
			{
				var car = new CarPriceResponseDB
				{
					CarId = reader.GetInt32(reader.GetOrdinal("CarId")),
					UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
					Money = reader.GetDecimal(reader.GetOrdinal("Money")),
					IsFavorite = reader.GetBoolean(reader.GetOrdinal("IsFavorite"))
				};

				carsWithMoney.Add(car);
			} while (reader.Read());

			return carsWithMoney;
		}



		public static List<CarResponseDB> GenerateCarResponsesDB(SqlDataReader reader)
        {
            var carsWithMoney = new List<CarResponseDB>();

            do
            {
                var car = GenerateCarResponseDB(reader);
				
				carsWithMoney.Add(car);

            } while (reader.Read());

            return carsWithMoney;
        }

		public static CarPriceResponseDB GenerateCarForFavorite(SqlDataReader reader)
		{
			
				var car = new CarPriceResponseDB
				{
					CarId = reader.GetInt32(reader.GetOrdinal("CarId")),
					UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
					Money = reader.GetDecimal(reader.GetOrdinal("Money")),
					IsFavorite = reader.GetBoolean(reader.GetOrdinal("IsFavorite"))
				};

		

			return car;
		}
		public static List<CarViewResponseDB> GetCarsViews(SqlDataReader reader)
        {
            var cars = new List<CarViewResponseDB>();

            do
            {
                var car = new CarViewResponseDB
				{
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    Brand = reader.GetString(reader.GetOrdinal("Brand")),
                    Model = reader.GetString(reader.GetOrdinal("Model")),
                    Year = reader.GetInt32(reader.GetOrdinal("Year")),
                };

                cars.Add(car);
            } while (reader.Read());

            return cars;
        }
    }
}
