using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DapperAtCore.Models;
using Infrastructure.Abstract;
using Infrastructure.Entities;

namespace DapperAtCore.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private IGameRepository _gameRepository;

		public HomeController(ILogger<HomeController> logger, IGameRepository gameRepository)
		{
			_logger = logger;
			_gameRepository = gameRepository;
		}

		[Route("/")]
		[Route("Home/Index")]
		[HttpGet]
		public IActionResult Index()
		{
			return View(_gameRepository.Games);
		}
		[Route("Home/Privacy")]
		[HttpGet]
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		[Route("Home/Error")]
		[HttpGet]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		[Route("Home/Create")]
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[Route("Home/Create")]
		[HttpPost]
		public IActionResult Create(Game game)
		{
			if(ModelState.IsValid)
			{
				_gameRepository.SaveGame(game);
				return RedirectToAction("Index");
			}
			return NotFound();
		}

		[Route("Home/Edit")]
		[HttpGet]
		public IActionResult Edit(int id)
		{
			var game = _gameRepository.GetGame(id);
			if(game != null)
				return View(game);
			return NotFound();
		}
		[Route("Home/Edit")]
		[HttpPost]
		public IActionResult Edit(Game game)
		{
			if (ModelState.IsValid)
			{
				_gameRepository.SaveGame(game);
				return RedirectToAction("Index");
			}
			return NotFound();
		}

		[Route("Home/Details")]
		[HttpGet]
		public IActionResult Details(int id)
		{
			var game = _gameRepository.GetGame(id);
			if (game != null)
				return View(game);
			return NotFound();
		}
		[Route("Home/Delete")]
		[HttpGet]
		public IActionResult Delete(int id)
		{
			_gameRepository.DeleteGame(id);
			return RedirectToAction("Index");
		}
	}
}
