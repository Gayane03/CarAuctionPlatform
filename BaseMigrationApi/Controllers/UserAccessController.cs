﻿using BaseMigrationApi.Helpers;
using BusinessLayer.Autho;
using BusinessLayer.Helper;
using BusinessLayer.Services.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.RequestModels;

namespace BaseMigrationApi.Controllers
{
	
	[ApiController]
	[Route("api/[controller]")] 
	[Authorize(Roles = Role.Admin , AuthenticationSchemes = TokenSchemeType.UserAccess)]
	public class UserAccessController : ControllerBase
	{
		private readonly ICarService carService;
		private readonly IJwtTokenHandlerService jwtTokenHandlerService;

		private int UserId;
        public UserAccessController(
			IJwtTokenHandlerService jwtTokenHandlerService, 
			ICarService carService)
		{
		   this.jwtTokenHandlerService = jwtTokenHandlerService;	
		   this.carService = carService;
        }


		[HttpGet("validateToken")]
		public async Task<IActionResult> ValidateToken()
		{
			return Ok(true);
        }

		[HttpPost("getCars")]
		public async Task<IActionResult> GetCars([FromBody] CarFiltrationRequest? carFiltrationRequest)
		{
			var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
			UserId = jwtTokenHandlerService.GetUserIdFromToken(token);

			var result = await carService.GetCars(UserId, carFiltrationRequest);
			if(!result.IsSuccess)
			{
				return BadRequest(result.Error);
			}

			return Ok(result.Value);
		}

		[HttpGet("getCar/{carId}")]
		public async Task<IActionResult> GetCar(int carId)
		{
			var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
			UserId = jwtTokenHandlerService.GetUserIdFromToken(token);


			var result = await carService.GetCar(UserId, carId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

		[HttpPost("addNewPrice")]
		public async Task<IActionResult> AddNewPriceForCar([FromBody] CarPriceRequest carPriceRequest)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            UserId = jwtTokenHandlerService.GetUserIdFromToken(token);


            var result = await carService.AddNewPriceForCar(UserId,carPriceRequest);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

		[HttpPost("favoritecar")]
		public async Task<IActionResult> ChangeCarFavoriteState([FromBody] CarFavoriteRequest carFavoriteRequest)
		{
			var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
			UserId = jwtTokenHandlerService.GetUserIdFromToken(token);


			var result = await carService.UpdateCarFavoriteState(UserId, carFavoriteRequest);
			if (!result.IsSuccess)
			{
				return BadRequest(result.Error);
			}

			return Ok(result.Value);
		}
	}
}