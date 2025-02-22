namespace BusinessLayer.Helper
{
	internal static class Message
	{
		public const string SystemError = "There is problem with system. Please try again later.";
		public const string VerificationCodeIncorrected = "Verification code is incorrected.";
		public const string LoginFailError = "There is not user with current login data.";
		public const string EmailActivityError = "There is active user with current email.";

		public const string EmptyCarsListError = "There are not cars in table.";
        public const string EmptyCarError = "There is not car wit current data.";
		public const string PriceAddingError = "There is problem with adding  car new price";
    }
}
