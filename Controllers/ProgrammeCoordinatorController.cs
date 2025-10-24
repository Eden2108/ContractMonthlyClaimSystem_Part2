using System.Data.SqlClient;
using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ProgrammeCoordinatorController : Controller
    {
        private readonly string connectionString = @"Server=(localdb)\claim_system;Database=claims_database;Integrated Security=true;";

        // Show all pending claims
        public IActionResult Index()
        {
            List<Claim> pendingClaims = new List<Claim>();
            string query = "SELECT * FROM Claims WHERE claim_status = 'Pending'";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pendingClaims.Add(new Claim
                        {
                            claimID = (int)reader["claimID"],
                            module_name = reader["module_name"].ToString(),
                            faculty_name = reader["faculty_name"].ToString(),
                            number_of_sessions = (int)reader["number_of_sessions"],
                            number_of_hours = (int)reader["number_of_hours"],
                            amount_of_rate = (int)reader["amount_of_rate"],
                            claim_status = reader["claim_status"].ToString(),
                            creating_date = (DateTime)reader["creating_date"]
                        });
                    }
                }
            }

            return View(pendingClaims);
        }

        // Approve a claim
        [HttpPost]
        public IActionResult Approve(int id)
        {
            UpdateClaimStatus(id, "Verified by Coordinator");
            return RedirectToAction("Index");
        }

        // Reject a claim
        [HttpPost]
        public IActionResult Reject(int id)
        {
            UpdateClaimStatus(id, "Rejected by Coordinator");
            return RedirectToAction("Index");
        }

        // Method to update claim status
        private void UpdateClaimStatus(int claimID, string newStatus)
        {
            string query = "UPDATE Claims SET claim_status = @status WHERE claimID = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@id", claimID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
