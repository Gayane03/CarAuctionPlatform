﻿using CarAuctionPlatformUI;
using CarAuctionPlatformUI.Helper;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedLibrary.RequestModels;
using SharedLibrary.ResponseModels;

namespace CarAuctionPlatformUI.Pages
{
	public partial class RegistrationPage
	{
		[Inject]
		private ApiController? apiController { get; set; }

		[Inject]
		private NavigationManager? navigationManager { get; set; }

		[Inject]
		private ResponseMessageUtile? responseMessageUtile { get; set; }

		[Inject]
		private LocalStorageHelper? localStorageHelper { get; set; }

		private MudForm? formRegistration;
		private MudForm? formLogin;

		private const string RegisterText = "Register";
		private const string LoginText = "Login";

		private CarAuctionPlatformUI.Models.RegistrationModel registrationModel = new();
		private EmailVerificationTokenResponse? emailVerificationTokenResponse;

		private bool isRegistrationProcess = true;

		private LoginRequest loginRequest = new();
		private LoginResponse? loginResponse;


		protected override async Task OnInitializedAsync()
		{

            await base.OnInitializedAsync();

            var accessToken = await localStorageHelper.GetToken(TokenStorageName.UserAccess);

			if(!string.IsNullOrEmpty(accessToken))
			{
				try
				{
                    var response = await apiController.ValidateToken(accessToken);
                    var isValid = await responseMessageUtile.HandleResponse<bool>(response);

                    if (isValid)
                    {
						navigationManager.NavigateTo(Route.Cars);
                    }
                }
				catch (Exception)
				{
					navigationManager.NavigateTo(Route.Registration);
				}				
            }

		
		}

		private async Task OnRegister()
		{
			try
			{
				await formRegistration!.Validate();

				if (formRegistration.IsValid)
				{
					var registrationRequest = new RegistrationRequest()
					{
						FirstName = registrationModel.FirstName,
						LastName = registrationModel.LastName,
						Country = registrationModel.Country,
						Email = registrationModel.Email,
						Password = registrationModel.Password
					};

					var response = await apiController!.RegisterUser(registrationRequest);
					emailVerificationTokenResponse = await responseMessageUtile.HandleResponse<EmailVerificationTokenResponse>(response);

					string? token = emailVerificationTokenResponse?.VerificationToken;
					if (string.IsNullOrEmpty(token))
					{
                        throw new Exception("Token is null.");
                    }

                    await localStorageHelper!.SaveToken(TokenStorageName.EmailVerification, token);

					navigationManager!.NavigateTo(Route.Verification);
				}
			}
            catch (Exception ex)
            {
				navigationManager?.NavigateTo($"/{Route.NotFound}?error={Uri.EscapeDataString(ex.Message)}");
				//navigationManager?.NavigateTo(Route.NotFound);
            }
        }

		private async Task OnLogin()
		{
			try
			{
				await formLogin!.Validate();

				if (formLogin.IsValid)
				{
					var response = await apiController!.LoginUser(loginRequest);
					loginResponse = await responseMessageUtile!.HandleResponse<LoginResponse>(response);

					string? token = loginResponse?.Token;
					if (string.IsNullOrEmpty(token))
					{
						throw new Exception("Token is null.");
					}

					await localStorageHelper!.RemoveToken(TokenStorageName.EmailVerification);
					await localStorageHelper.SaveToken(TokenStorageName.UserAccess, token);

					navigationManager!.NavigateTo(Route.Cars);
				}
			}
            catch (Exception ex)
            {

				navigationManager!.NavigateTo(Route.Cars);
				//navigationManager?.NavigateTo(Route.NotFound);

				//navigationManager?.NavigateTo($"/{Route.NotFound}?error={Uri.EscapeDataString(ex.Message)}");
			}
		}

		private void OpenLoginCard()
		{
			isRegistrationProcess = false;

		}

		private void OpenRegisterCard()
		{
			isRegistrationProcess = true;
		}

	}
}
