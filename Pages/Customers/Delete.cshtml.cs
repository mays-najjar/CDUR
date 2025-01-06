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
    public class Delete : PageModel
    {


        public void OnGet()
        {
        }

        [Obsolete]
        public void OnPost(int id)
        {
            deleteCustomer(id);
            Response.Redirect("/Customers/Index");

        }

        [Obsolete]
        private void deleteCustomer(int id)
        {
            try{
                string connectionString = "Server=localhost\\SQLEXPRESS;Database=cmd;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM customers WHERE customer_id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch{

            }

        }
    }
}