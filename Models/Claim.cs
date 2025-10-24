using System;

namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {
        public int claimID { get; set; }
        public int number_of_sessions { get; set; }
        public int number_of_hours { get; set; }
        public int amount_of_rate { get; set; }
        public string module_name { get; set; }
        public string faculty_name { get; set; }
        public string supporting_documents { get; set; }
        public string claim_status { get; set; }
        public DateTime creating_date { get; set; }
        public int lecturerID { get; set; }
    }
}
