using books.web.dto;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;

namespace books.web.Pages.Books
{
    public class CreateModel : PageModel
    {
        public Book book = new Book();
        public string errorMessage = string.Empty;
        public string successMessage = string.Empty;

        public void OnGet()
        {
        } 
        public void OnPost()
        {

            try
            {
                book.name = Request.Form["name"];
                book.autor = Request.Form["autor"];

                int pages;

                Int32.TryParse(Request.Form["pages"].ToString(), out pages);
                
                book.pages = pages;

                book.genre = Request.Form["genre"];
                book.year = Request.Form["year"];

                //backend validation 
                if ( book.name.Length==0
                    || book.autor.Length == 0
                    || book.genre.Length == 0
                    || book.pages == 0
                    || book.year.Length == 0
                    )
                {
                    errorMessage = "all fields are required";
                    return;
                }
                //insertSqlCommand(book);

                insertSqlCommandParameters(book);

                successMessage = "book created correctly";

                book.name = "";
                book.autor = "";
                book.year = "";
                book.pages = 0;
                book.genre = "";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

        }

        public void insertSqlCommand(Book book) {
            string connectionString = "Data Source=.\\sqlservername;Initial Catalog=databasename;Integrated Security=True;Persist Security Info=False;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlsentence = string.Format("insert into libros (nombre,autor,paginas,genero,year) values ('{0}','{1}',{2},'{3}','{4}')"
                    ,book.name, book.autor, book.pages, book.genre, book.year);

                using (SqlCommand command = new SqlCommand(sqlsentence, connection))
                {
                    command.ExecuteScalar();
                }
                connection.Close();
            }
        }

        public void insertSqlCommandParameters(Book book)
        {
            string connectionString = "Data Source=.\\sqlservername;Initial Catalog=databasename;Integrated Security=True;Persist Security Info=False;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlsentence = string.Format("insert into libros (nombre,autor,paginas,genero,year) VALUES (@name, @autor, @pages, @genre, @year);"
                    , book.name, book.autor, book.pages, book.genre, book.year);

                using (SqlCommand command = new SqlCommand(sqlsentence, connection))
                {
                    command.Parameters.AddWithValue("@name", book.name);
                    command.Parameters.AddWithValue("@autor", book.autor);
                    command.Parameters.AddWithValue("@pages", book.pages);
                    command.Parameters.AddWithValue("@genre", book.genre);
                    command.Parameters.AddWithValue("@year", book.year);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}