using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using toDoListWebApp.Models;

namespace toDoListWebApp.Controllers
{
	public class CategoryController : Controller
	{
		private readonly string connectionString = "Data Source=.;Initial Catalog=ToDoList;Integrated Security=True ; Encrypt=false; TrustServerCertificate=True";
		public IActionResult Index()
		{
			List<Category> categories = new List<Category>();

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				categories = connection.Query<Category>("Select * From Categories").ToList();

			}
			return View(categories);
		}

		[HttpPost]
		public IActionResult AddCategory(string CategoryName)
		{
			string categoryName = CategoryName;
			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				connection.Query($"INSERT INTO Categories (CategoryName) VALUES('{categoryName}')");

			}

			return RedirectToAction("Index");
		}

		public IActionResult Delete(int id)
		{

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				connection.Query($"DELETE FROM Categories WHERE CategoryId = {id}");
			}

			return RedirectToAction("Index");
		}
	}
}
