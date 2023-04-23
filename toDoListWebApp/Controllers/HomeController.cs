using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Dapper;
using toDoListWebApp.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace toDoListWebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly string connectionString = "Data Source=.;Initial Catalog=ToDoList;Integrated Security=True ; Encrypt=false; TrustServerCertificate=True";

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}


		public IActionResult Index()
		{

			List<Mission> tasksList = new List<Mission>();
			List<Category> categoriesList = new List<Category>();

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				tasksList = connection.Query<Mission>("Select * From Tasks INNER JOIN Categories ON Tasks.CategoryId = Categories.CategoryId ORDER BY TaskStatus, CompletionDate DESC").ToList();
				categoriesList = connection.Query<Category>("Select * From Categories").ToList();

			}
			ViewBag.list = categoriesList;

			return View(tasksList);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		[HttpPost]
		public IActionResult AddTask(string Title, DateTime CompletionDate, int Category)
		{
			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				connection.Query($"INSERT INTO Tasks(Title,CompletionDate,CategoryId) VALUES ('{Title}','{CompletionDate.ToShortDateString()}',{Category})");

			}
			return RedirectToAction("Index");
		}

	}
}