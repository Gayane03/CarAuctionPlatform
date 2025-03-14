using AutoMapper;
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
		private readonly IMapper mapper;
		public CarService(ICarRepository carRepository, IMapper mapper)
		{
			this.carRepository = carRepository;
			this.mapper = mapper;
		}

		public async Task<Result<bool>> AddNewPriceForCar(int userId, CarPriceRequest carPriceRequest)
		{
			try
			{
				var carAuctionId = await carRepository.AddNewPriceForCar(userId, carPriceRequest);

				if (carAuctionId == default(int))
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
				var (car, carsAuctions) = await carRepository.GetCarsAuctions(carId);

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
					UserLastPriceOffer = currentUserLastOffer,
					Color = car.Color,
					InteriorColor = car.InteriorColor,
					BodyType = car.BodyType,
					DriveType = car.DriveType,
					EngineSize = car.EngineSize
				};

				return Result<CarResponse>.Success(carResponse);
			}
			catch (Exception ex)
			{
				return Result<CarResponse>.Failure(Message.SystemError);
			}
		}

		public async Task<Result<List<CarViewResponse>>> GetCars(int userId, CarFiltrationRequest? carFiltrationRequest)
		{
			try
			{

				var carDataForFiltering = await carRepository.GetCarsDataForFiltering();

				if (carFiltrationRequest is not null)
				{
					var model = carFiltrationRequest.Model;
					if (!string.IsNullOrEmpty(model) && model != "string")
					{
						carDataForFiltering = carDataForFiltering.Where(car => car.Model.Contains(model)).ToList();
					}

					var brand = carFiltrationRequest.Brand;
					if (!string.IsNullOrEmpty(brand) && brand != "string")
					{
						carDataForFiltering = carDataForFiltering.Where(car => car.Brand.Contains(brand)).ToList();
					}

					var mileage = carFiltrationRequest.Mileage;
					if (mileage.HasValue && mileage.Value  is not default(int))
					{
						carDataForFiltering = carDataForFiltering.Where(car => car.Mileage <= mileage).ToList();
					}

					var fromYear = carFiltrationRequest.FromYear;
					if (fromYear.HasValue && fromYear.Value is not default(int))
					{
						carDataForFiltering = carDataForFiltering.Where(car => car.Year >= fromYear).ToList();
					}

					var toYear = carFiltrationRequest.ToYear;
					if (toYear.HasValue && toYear.Value is not default(int))
					{
						carDataForFiltering = carDataForFiltering.Where(car => car.Year <= toYear).ToList();
					}

				}

				var (_, carsAuctions) = await carRepository.GetCarsAuctions();
				var cars = await carRepository.GetCars();

				var carsViewResponse = new List<CarViewResponse>();

				foreach (var car in cars)
				{
					var currentCar = new CarViewResponse()
					{
						Id = car.Id,
						Name = car.Name,
						Description = car.Description,
						ImageUrl = car.ImageUrl,
						Model = car.Model,
						Brand = car.Brand,
						Year = car.Year,	
					};

					if(carsAuctions.Where(c => c.CarId == car.Id && c.UserId == userId).Any())
					{ currentCar.IsFavorite = carsAuctions.Where(c => c.CarId == car.Id && c.UserId == userId).FirstOrDefault().IsFavorite; }

					if (carDataForFiltering.Where(car => car.Id == currentCar.Id).FirstOrDefault() is not null)
					{
						carsViewResponse.Add(currentCar);
					}		
				}

				

				if (carsViewResponse is null || !carsViewResponse.Any())
				{
					return Result<List<CarViewResponse>>.Failure(Message.EmptyCarsListError);
				}

				return Result<List<CarViewResponse>>.Success(carsViewResponse);
			}
			catch (Exception)
			{
				return Result<List<CarViewResponse>>.Failure(Message.SystemError);
			}
		}

		public async Task<Result<bool>> UpdateCarFavoriteState(int userId, CarFavoriteRequest carFavoriteRequest)
		{
			try
			{
				int isFavoriteBit = 0;

				if (carFavoriteRequest.IsFavorite)
				{
					isFavoriteBit = 1;

				}

				await carRepository.UpdateCarFavoriteState(userId, carFavoriteRequest.CarId, isFavoriteBit);

				return Result<bool>.Success(true);
			}
			catch (Exception)
			{
				return Result<bool>.Failure(Message.SystemError);
			}
		}
	}
}
