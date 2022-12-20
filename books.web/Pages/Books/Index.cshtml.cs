using books.web.dto;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace books.web.Pages.Books
{
    public class IndexModel : PageModel
    {
        public List<Book> listalibros = new List<Book>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlservername;Initial Catalog=databasename;Integrated Security=True;Persist Security Info=False;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sqlsentence = "Select * from libros";

                    using (SqlCommand cmd = new SqlCommand(sqlsentence, connection))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Book book = new Book();

                                book.id = reader.GetInt32(0);
                                book.name = reader.GetString(1);
                                book.autor = reader.GetString(2);
                                book.pages = reader.GetInt16(3);
                                book.genre = reader.GetString(4);
                                book.year = reader.GetString(5);
                                listalibros.Add(book);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("Exception: " + ex.ToString());
            }

        }
    }
}