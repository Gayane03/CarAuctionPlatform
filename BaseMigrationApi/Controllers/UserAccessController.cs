﻿using BaseMigrationApi.Helpers;
using BusinessLayer.Autho;
using BusinessLayer.Helper;
using BusinessLayer.Services.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DbModels.Request;
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

		private readonly int UserId;
        public UserAccessController(
			IJwtTokenHandlerService jwtTokenHandlerService, ICarService carService)
		{
		   this.jwtTokenHandlerService = jwtTokenHandlerService;	
		   this.carService = carService;

            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            UserId = jwtTokenHandlerService.GetUserIdFromToken(token);
        }


		[HttpGet("validateToken")]
		public async Task<IActionResult> ValidateToken()
		{
			return Ok(true);
        }

		//[HttpGet("test")]
		//public bool Test(string token)
		//{
		//	try
		//	{
		//		var a = jwtTokenHandlerService.ValidateJwtToken(token);

		//		return true;
		//	}
		//	catch (Exception)
		//	{

		//		return false;
		//	}

		//}

		[HttpGet("getCars")]
		public async Task<IActionResult> GetCars()
		{
			var result = await carService.GetCars();
			if(!result.IsSuccess)
			{
				return BadRequest(result.Error);
			}

			return Ok(result.Value);
		}

		[HttpGet("getCar/{carId}")]
		public async Task<IActionResult> GetCar(int carId)
		{
            var result = await carService.GetCar(carId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

		[HttpPost("addNewPrice")]
		public async Task<IActionResult> AddNewPriceForCar([FromBody] CarPriceRequest carPriceRequest)
		{
            var result = await carService.AddNewPriceForCar(UserId,carPriceRequest);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
	}
}