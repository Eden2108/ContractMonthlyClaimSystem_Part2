using System.Data.SqlClient;
using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class LecturerController : Controller
    {
        private readonly string connectionString = @"Server=(localdb)\claim_system;Database=claims_database;Integrated Security=true;";

        // -------------------------------
        // Show Submit Claim Form
        // -------------------------------
        public IActionResult Index()
        {
            return View();
        }

        // -------------------------------
        // Handle Claim Submission
        // -------------------------------
        [HttpPost]
        public IActionResult SubmitClaim(Claim claim)
        {
            string query = @"INSERT INTO Claims (number_of_sessions, number_of_hours, amount_of_rate, 
                             module_name, faculty_name, supporting_documents, claim_status, creating_date, lecturerID)
                             VALUES (@sessions, @hours, @rate, @module, @faculty, @docs, 'Pending', GETDATE(), 1)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@sessions", claim.number_of_sessions);
                    cmd.Parameters.AddWithValue("@hours", claim.number_of_hours);
                    cmd.Parameters.AddWithValue("@rate", claim.amount_of_rate);
                    cmd.Parameters.AddWithValue("@module", claim.module_name);
                    cmd.Parameters.AddWithValue("@faculty", claim.faculty_name);
                    cmd.Parameters.AddWithValue("@docs", claim.supporting_documents ?? "");
                    cmd.ExecuteNonQuery();
                }
            }

            ViewBag.Message = "Claim submitted successfully!";
            return View("Index");
        }

        // -------------------------------
        // Track Claim Page
        // -------------------------------
        public IActionResult TrackClaim()
        {
            List<Claim> claims = new List<Claim>();
            string query = "SELECT * FROM Claims";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        claims.Add(new Claim
                        {
                            claimID = (int)reader["claimID"],
                            number_of_sessions = (int)reader["number_of_sessions"],
                            number_of_hours = (int)reader["number_of_hours"],
                            amount_of_rate = (int)reader["amount_of_rate"],
                            module_name = reader["module_name"].ToString(),
                            faculty_name = reader["faculty_name"].ToString(),
                            claim_status = reader["claim_status"].ToString(),
                            creating_date = (DateTime)reader["creating_date"]
                        });
                    }
                }
            }

            return View(claims);
        }
    }
}
