using SharedLibrary.Enum;

namespace SharedLibrary.RequestModels
{
	public class RegistrationRequest
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
		public Country Country { get; set; } = Country.Armenia;
		public string Email { get; set; }
		public string Password { get; set; }
		public string PassportNumber { get; set; }	
	}
}
