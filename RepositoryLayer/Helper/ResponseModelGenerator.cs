using Microsoft.Data.SqlClient;
using SharedLibrary.DbModels.Response;
using SharedLibrary.ResponseModels;

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
                    Money = reader.GetDecimal(reader.GetOrdinal("Money"))
                };

                carsWithMoney.Add(car);
            } while (reader.Read());

            return carsWithMoney;
        }
        public static List<CarViewResponse> GetCarsViews(SqlDataReader reader)
        {
            var cars = new List<CarViewResponse>();

            do
            {
                var car = new CarViewResponse
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
