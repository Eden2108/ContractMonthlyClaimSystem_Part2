namespace ContractMonthlyClaimSystem.Models
{
    public class User
    {
        public int userID { get; set; }
        public string full_names { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string gender { get; set; }
        public string password { get; set; }
        public DateTime date { get; set; }
    }
}
