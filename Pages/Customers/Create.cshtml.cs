using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CDUR.Pages.Customers
{
    public class Create : PageModel
    {
        public Create()
        {
        }

        [BindProperty, Required(ErrorMessage = "Firstname is required")]
        public string Firstname { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Lastname is required")]
        public string Lastname { get; set; } = "";

        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = "";

        [BindProperty, Phone]
        public string? Phone { get; set; }

        [BindProperty]
        public string? Address { get; set; }

        [BindProperty, Required]
        public string Company { get; set; } = "";

        [BindProperty]
        public string? Notes { get; set; }

        public void OnGet()
        {
        }

        [Obsolete]
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            if (Phone == null) Phone = "";
            if (Address == null) Address = "";
            if (Notes == null) Notes = "";
            try
            {
                string connectionString = "Server=localhost\\SQLEXPRESS;Database=cmd;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO customers (first_name, last_name, email, phone, address, company, notes) VALUES (@Firstname, @Lastname, @Email, @Phone, @Address, @Company, @Notes)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Firstname", Firstname);
                        command.Parameters.AddWithValue("@Lastname", Lastname);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Phone", Phone);
                        command.Parameters.AddWithValue("@Address", Address);
                        command.Parameters.AddWithValue("@Company", Company);
                        command.Parameters.AddWithValue("@Notes", Notes);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        Response.Redirect("/Customers/Index");
        }
    }

}
