namespace SharedLibrary.DbModels.Request
{
    public class CarAuction
    {
        public int UserId { get; set; } 
        public int CarId { get; set; }  
        public decimal Money { get; set; }  
    }
}
