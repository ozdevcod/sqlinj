using books.web.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;

namespace books.web.Pages.Books
{
    public class EditModel : PageModel
    {
        public string errorMessage = string.Empty;
        public string successMessage = string.Empty;

        public string connectionString = "Data Source=.\\sqlservername;Initial Catalog=databasename;Integrated Security=True;Persist Security Info=False;TrustServerCertificate=True";

        public Book book = new Book();

        public void OnGet()
        {
            //string sBookId = Request.QueryString.Value.IndexOf("id").ToString();

            //string sBookId = Request.Query["id"].ToString();
            //int iBookId = Convert.ToUInt16(sBookId);
            //book.id = iBookId;


            getBookInfo(0);
        }

        public void OnPost()
        {

            int iPages;
            Int32.TryParse(Request.Form["pages"].ToString(), out iPages);

            int iId;
            Int32.TryParse(Request.Form["idHidden"].ToString(), out iId);

            book.id = iId;
            book.name = Request.Form["name"];
            book.autor = Request.Form["autor"];
            book.pages = iPages;
            book.genre = Request.Form["genre"];
            book.year = Request.Form["year"];

            //backend validation 
            if (book.name.Length == 0
                || book.autor.Length == 0
                || book.genre.Length == 0
                || book.pages == 0
                || book.year.Length == 0
                )
            {
                errorMessage = "all fields are required";
                return;
            }

            updateBookInfo(book.id);

            successMessage = "book updated correctly";

            //book.name = "";
            //book.autor = "";
            //book.year = "";
            //book.pages = 0;
            //book.genre = "";
            //book.id = 0;
        }

        public void getBookInfo(int id)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //string sqlsentence = string.Format("Select * from libros where id={0}", id);

                    string sqlinj = Request.Query["id"].ToString();
                    var sqlsentence = "select * from libros where id = '" + sqlinj + "'";

                    using (SqlCommand cmd = new SqlCommand(sqlsentence, connection))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                book.id = reader.GetInt32(0);
                                book.name = reader.GetString(1);
                                book.autor = reader.GetString(2);
                                book.pages = reader.GetInt16(3);
                                book.genre = reader.GetString(4);
                                book.year = reader.GetString(5);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Write("Exception: " + ex.ToString());
            }
        }

        public void updateBookInfo(int id)
        {
            book.id = id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlsentence = "update libros set nombre=@name, autor=@autor, paginas=@pages, genero=@genre, year=@year where id=@id;";

                using (SqlCommand command = new SqlCommand(sqlsentence, connection))
                {
                    command.Parameters.AddWithValue("@name", book.name);
                    command.Parameters.AddWithValue("@autor", book.autor);
                    command.Parameters.AddWithValue("@pages", book.pages);
                    command.Parameters.AddWithValue("@genre", book.genre);
                    command.Parameters.AddWithValue("@year", book.year);
                    command.Parameters.AddWithValue("@id", book.id);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}