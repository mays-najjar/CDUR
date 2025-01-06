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
    public class Edit : PageModel
    {
     
      [BindProperty ]
        public int Id { get; set; } 

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

        public String ErrorMessage { get; set; } = "";

        [Obsolete]
        public void OnGet(int id)
        {
            try
            {
                string connectionString = "Server=localhost\\SQLEXPRESS;Database=cmd;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM customers WHERE customer_id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Id = reader.GetInt32(0);
                                Firstname = reader.GetString(1);
                                Lastname = reader.GetString(2);
                                Email = reader.GetString(3);
                                Phone = reader.GetString(4);
                                Address = reader.GetString(5);
                                Company = reader.GetString(6);
                                Notes = reader.GetString(7);
                            }
                            else
                            {
                                Response.Redirect("/Customers/Index");
                            }   
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

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
                    string sql = "UPDATE customers SET first_name = @Firstname, last_name = @Lastname, email = @Email, phone = @Phone, address = @Address, company = @Company, notes = @Notes WHERE customer_id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", Id);
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