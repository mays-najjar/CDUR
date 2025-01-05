using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CDUR.Pages.Customers
{
    public class Index : PageModel
    {
        public List<Customer> CustomersList {get; set;} = [];

        [Obsolete]
        public void OnGet()
        {
            try
            {
                string connectionString = "Server=localhost\\SQLEXPRESS;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM customers";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customer customer = new Customer();
                                customer.Id = reader.GetInt32(0);
                                customer.Firstname = reader.GetString(1);
                                customer.Lastname = reader.GetString(2);
                                customer.Email = reader.GetString(3);
                                customer.Phone = reader.GetString(4);
                                customer.Address = reader.GetString(5);
                                customer.Company = reader.GetString(6);
                                customer.Notes = reader.GetString(7);
                                customer.CreatedAt = reader.GetDateTime(8).ToString("MM/dd/yyyy");
                                CustomersList.Add(customer);
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
    }

    public class Customer
    {
        public  int Id { get; set; }
        public  string Firstname { get; set; } = "";
        public  string Email { get; set; } = "";
        public  string Lastname { get; set; } = "";
        public  string Phone { get; set; } = "";
        public  string Address { get; set; } = "";
        public  string Company { get; set; } = "";
        public  string Notes { get; set; } = "";

        public  string CreatedAt { get; set; } = "";


    }
}