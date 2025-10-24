using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly string connectionString = @"Server=(localdb)\claim_system;Database=claims_database;Integrated Security=true;";

        // ----------------------------
        // Show Registration Form
        // ----------------------------
        public IActionResult Register()
        {
            return View();
        }

        // ----------------------------
        // Handle Registration
        // ----------------------------
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (user == null)
            {
                ViewBag.Message = " Invalid data. Please fill all fields.";
                return View();
            }

            string query = @"INSERT INTO Users (full_names, surname, email, role, gender, password, date)
                             VALUES (@full_names, @surname, @email, @role, @gender, @password, GETDATE())";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@full_names", user.full_names);
                    cmd.Parameters.AddWithValue("@surname", user.surname);
                    cmd.Parameters.AddWithValue("@email", user.email);
                    cmd.Parameters.AddWithValue("@role", user.role);
                    cmd.Parameters.AddWithValue("@gender", user.gender);
                    cmd.Parameters.AddWithValue("@password", user.password);

                    cmd.ExecuteNonQuery();
                }
            }

            // Redirect based on role
            TempData["UserRole"] = user.role;

            return RedirectToAction("RegistrationSuccess");
        }

        // ----------------------------
        // Registration Success Page
        // ----------------------------
        public IActionResult RegistrationSuccess()
        {
            string role = TempData["UserRole"]?.ToString() ?? "";
            ViewBag.Message = $" Registration successful! Welcome {role}!";

            // Redirect to correct dashboard
            switch (role.ToLower())
            {
                case "lecturer":
                    return RedirectToAction("Index", "Lecturer");
                case "coordinator":
                case "programme coordinator":
                    return RedirectToAction("Index", "Coordinator");
                case "manager":
                case "academic manager":
                    return RedirectToAction("Index", "Manager");
                default:
                    return RedirectToAction("Index", "Home");
            }
        }

        // ----------------------------
        // Show Login Page
        // ----------------------------
        public IActionResult Login()
        {
            return View();
        }

        // ----------------------------
        // Handle Login
        // ----------------------------
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Message = "⚠️ Please enter both email and password.";
                return View();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE email=@Email AND password=@Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string role = reader["role"].ToString();

                            // Redirect based on role
                            switch (role.ToLower())
                            {
                                case "lecturer":
                                    return RedirectToAction("Index", "Lecturer");
                                case "coordinator":
                                case "programme coordinator":
                                    return RedirectToAction("Index", "Coordinator");
                                case "manager":
                                case "academic manager":
                                    return RedirectToAction("Index", "Manager");
                                default:
                                    return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            ViewBag.Message = " Invalid email or password. Please try again.";
                            return View();
                        }
                    }
                }
            }
        }
    }
    }