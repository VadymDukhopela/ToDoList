using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using toDoListWebApp.Models;
using Dapper;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Routing;

namespace toDoListWebApp.Controllers
{
	public class TaskController : Controller
	{

		private readonly string connectionString = "Data Source=.;Initial Catalog=ToDoList;Integrated Security=True ; Encrypt=false; TrustServerCertificate=True";

		public IActionResult Detail(int id)
		{
			List<Mission> task = new List<Mission>();
			List<Category> categoriesList = new List<Category>();
			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				task = connection.Query<Mission>($"Select * From Tasks INNER JOIN Categories ON Tasks.CategoryId = Categories.CategoryId WHERE TaskId = {id}").ToList();
				categoriesList = connection.Query<Category>("Select * From Categories").ToList();
			}
			ViewBag.list = categoriesList;

			return View( task);
		}

		public IActionResult Delete(int id)
		{

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				connection.Query($"DELETE FROM Tasks WHERE TaskId = {id}");
			}

			return RedirectToAction("Index", "Home");
		}

		public IActionResult ChangeStatus(int id)
		{
			List<Mission> task = new List<Mission>();
			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				task = connection.Query<Mission>($"Select * From Tasks WHERE TaskId = {id}").ToList();

				if (task[0].TaskStatus == 0)
				{
					connection.Query<Mission>($"UPDATE Tasks SET TaskStatus = 1 WHERE TaskId = {id}");
				}
				else if(task[0].TaskStatus == 1)
				{
					connection.Query<Mission>($"UPDATE Tasks SET TaskStatus = 0 WHERE TaskId = {id}");
				}
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public IActionResult Edit(string Title, DateTime CompletionDate, int Category, int id)
		{
			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				connection.Query($"UPDATE Tasks SET Title = '{Title}', CompletionDate = '{CompletionDate.ToShortDateString()}', CategoryId = '{Category}' WHERE TaskId = {id}");

			}
			return RedirectToAction("Index", "Home");
		}
	}
}
