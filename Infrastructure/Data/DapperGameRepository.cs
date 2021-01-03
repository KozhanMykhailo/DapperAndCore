using Infrastructure.Abstract;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
//using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Data
{
	public class DapperGameRepository : IGameRepository
	{
		/// <summary>
		/// строка подключения задается в Startup --> ConfigureServices  с помощью autofac
		/// </summary>
		private string connectionString;
		public DapperGameRepository(string conn)
		{
			connectionString = conn;
		}
		public IEnumerable<Game> Games
		{
			get
			{
				List<Game> games = new List<Game>();
				using (IDbConnection db = new SqlConnection(connectionString))
				{
					games = db.Query<Game>("SELECT * FROM Games").ToList();
				}
				return games;
			}
		}

		public void DeleteGame(int gameId)
		{
			using (IDbConnection db = new SqlConnection(connectionString))
			{
				var querySql = "DELETE FROM Games WHERE GameId = @id";
				db.Execute(querySql, new { id = gameId });
			}
		}

		public Game GetGame(int id)
		{
			Game game;
			using (IDbConnection db = new SqlConnection(connectionString))
			{
				game = db.Query<Game>("SELECT * FROM Games WHERE GameId =@Id", new { id }).FirstOrDefault();
			}
			if (game == null)
				return new Game();
			return game;
		}

		public void SaveGame(Game game)
		{
			string querySql;
			var dbGame = GetGame(game.GameId);
			using (IDbConnection db = new SqlConnection(connectionString))
			{
				if (dbGame.GameId == 0)
				{
					dbGame.Name = game.Name;
					dbGame.Price = game.Price;
					dbGame.Description = game.Description;
					dbGame.Category = game.Category;
					querySql = @"INSERT INTO Games (Name,Description,Category,Price)
										VALUES(@Name,@Description,@Category,@Price)";
					db.Execute(querySql, dbGame);
				}
				else
				{
					querySql = @"UPDATE Games SET Name = @Name,Description = @Description,Category =@Category,Price = @Price WHERE GameID = @GameID";
					db.Execute(querySql, game);
				}
			}
		}
	}
}
